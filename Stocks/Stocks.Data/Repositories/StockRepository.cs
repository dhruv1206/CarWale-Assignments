using Internal;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using MySql.Data.MySqlClient;
using Stocks.Application.Common;
using Stocks.Application.Dtos;
using Stocks.Application.Entities;
using Stocks.Application.Interfaces.IRepositories;
using Stocks.Application.Extensions;

namespace Stocks.Data.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly IConfiguration _config;
        private readonly string? _connectionString;

        public StockRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Stock>> GetAll()
        {
            using (var _connection = new MySqlConnection(_connectionString))
            {
                return await _connection.QueryAsync<Stock>("SELECT * FROM stocks;");
            }
        }

        public async Task<IEnumerable<Stock>> GetAll(List<IndividualFilter> filters)
        {
            using (var _connection = new MySqlConnection(_connectionString))
            {
                var whereClause = BuildWhereClause(filters, out var parameters);
                var sql = $"SELECT * FROM stocks {whereClause};";

                return await _connection.QueryAsync<Stock>(sql, parameters);
            }
        }

        public async Task<Stock?> GetById(int id)
        {
            using (var _connection = new MySqlConnection(_connectionString))
            {
                return await _connection.QueryFirstOrDefaultAsync<Stock>("SELECT * FROM stocks WHERE Id = @Id;", new { Id = id });
            }
        }

        public async Task<Stock> DeleteById(int id)
        {
            using (var _connection = new MySqlConnection(_connectionString))
            {
                var sql = @"
                    SELECT * FROM stocks WHERE Id = @Id;
                    DELETE FROM stocks WHERE Id = @Id;
                ";
                using var multi = await _connection.QueryMultipleAsync(sql, new { Id = id });
                var deletedStock = await multi.ReadSingleAsync<Stock>();
                return deletedStock;
            }
        }

        public async Task<bool> Exists(int id)
        {
            using (var _connection = new MySqlConnection(_connectionString))
            {
                var count = await _connection.QuerySingleAsync<int>("SELECT COUNT(*) FROM stocks WHERE Id = @Id;", new { Id = id });
                return count > 0;
            }
        }

        public async Task<PaginatedDataDto<Stock>> GetPaginatedData(int pageNumber, int pageSize)
        {
            return await GetPaginatedData(pageNumber, pageSize, new List<IndividualFilter>(), "Id", "ASC");
        }

        public async Task<PaginatedDataDto<Stock>> GetPaginatedData(int pageNumber, int pageSize, List<IndividualFilter> filters)
        {
            return await GetPaginatedData(pageNumber, pageSize, filters, "Id", "ASC");
        }

        public async Task<PaginatedDataDto<Stock>> GetPaginatedData(int pageNumber, int pageSize, List<IndividualFilter> filters, string sortBy, string sortOrder)
        {
            using (var _connection = new MySqlConnection(_connectionString))
            {
                var whereClause = BuildWhereClause(filters, out var parameters);
                int skip = (pageNumber - 1) * pageSize;
                parameters.AddDynamicParams(new { skip, pageSize });

                sortOrder = sortOrder?.ToUpper() == "DESC" ? "DESC" : "ASC";
                sortBy = ValidateSortBy(sortBy);

                var orderByClause = $"ORDER BY {sortBy} {sortOrder}";

                var sql = $@"
                SELECT * FROM stocks
                {whereClause}
                {orderByClause}
                OFFSET @skip ROWS FETCH NEXT @pageSize ROWS ONLY;
                SELECT COUNT(*) FROM stocks {whereClause};
            ";

                using var multi = await _connection.QueryMultipleAsync(sql, parameters);
                var items = (await multi.ReadAsync<Stock>()).ToList();
                var totalCount = await multi.ReadSingleAsync<int>();

                return new PaginatedDataDto<Stock>(items, totalCount);
            }
        }

        public async Task<Stock> Create(Stock stock)
        {
            Console.WriteLine(stock.ToString());
            using (var _connection = new MySqlConnection(_connectionString))
            {
                var sql = @"
                    INSERT INTO stocks (Make, Model, Year, Price, Kms, FuelType)
                    VALUES (@Make, @Model, @Year, @Price, @Kms, @FuelType);
                    SELECT * FROM stocks WHERE Id = LAST_INSERT_ID();
                ";
                var parameters = new DynamicParameters(stock);
                parameters.Add("FuelType", stock.FuelType.ToString());
                using var multi = await _connection.QueryMultipleAsync(sql, parameters);
                var createdStock = await multi.ReadSingleAsync<Stock>();
                return createdStock;
            }
        }

        public async Task<Stock> Update(Stock stock)
        {
            using (var _connection = new MySqlConnection(_connectionString))
            {
                var sql = @"
                    UPDATE stocks
                    SET Make = @Make, Model = @Model, Year = @Year, Price = @Price, Kms = @Kms, FuelType = @FuelType
                    WHERE Id = @Id;
                    SELECT * FROM stocks WHERE Id = @Id;
                ";
                var parameters = new DynamicParameters(stock);
                parameters.Add("FuelType", stock.FuelType.ToString());
                using var multi = await _connection.QueryMultipleAsync(sql, parameters);
                var updatedStock = await multi.ReadSingleAsync<Stock>();
                return updatedStock;
            }
        }

        private string BuildWhereClause(List<IndividualFilter> filters, out DynamicParameters parameters)
        {
            parameters = new DynamicParameters();
            if (filters == null || !filters.Any())
                return string.Empty;

            var allowedFields = new HashSet<string>(new[] { "Make", "Model", "Year", "Price", "Kms", "FuelType" }, StringComparer.OrdinalIgnoreCase);
            var allowedOperators = new HashSet<string>(new[] { "=", "!=", ">", "<", ">=", "<=", "In" });

            var conditions = new List<string>();
            for (int i = 0; i < filters.Count; i++)
            {
                var filter = filters[i];
                var comparisonString = filter.Comparison.GetDisplayName();

                if (!allowedFields.Contains(filter.PropertyName))
                    throw new ArgumentException($"Invalid field name: {filter.PropertyName}");
                if (!allowedOperators.Contains(comparisonString))
                    throw new ArgumentException($"Invalid operator: {comparisonString}");

                string paramName = $"@p{i}";

                if (filter.Comparison == Comparison.In && filter.Value is List<FuelType> valueList)
                {
                    var valueStrings = valueList.Select(value =>
                        value is Enum e ? e.ToString() : value.ToString()
                    ).ToList();

                    conditions.Add($"{filter.PropertyName} IN @p{i}");
                    parameters.Add(paramName, valueStrings);
                }
                else
                {
                    conditions.Add($"{filter.PropertyName} {comparisonString} {paramName}");
                    parameters.Add(paramName, filter.Value);
                }

            }

            return $"WHERE {string.Join(" AND ", conditions)}";
        }

        private string ValidateSortBy(string sortBy)
        {
            var validColumns = new HashSet<string>(new[] { "Id", "Make", "Model", "Year", "Price", "Kms", "FuelType" }, StringComparer.OrdinalIgnoreCase);
            return validColumns.Contains(sortBy) ? sortBy : "Id";
        }
    }
}