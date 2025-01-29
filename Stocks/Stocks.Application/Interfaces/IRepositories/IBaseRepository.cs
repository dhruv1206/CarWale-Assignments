using Stocks.Application.Common;
using Stocks.Application.Dtos;

namespace Stocks.Application.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(List<IndividualFilter> filters);
        Task<T?> GetById(int id);
        Task<T> DeleteById(int id);

        Task<bool> Exists(int id);

        Task<PaginatedDataDto<T>> GetPaginatedData(int pageNumber, int pageSize);
        Task<PaginatedDataDto<T>> GetPaginatedData(int pageNumber, int pageSize, List<IndividualFilter> filters);
        Task<PaginatedDataDto<T>> GetPaginatedData(int pageNumber, int pageSize, List<IndividualFilter> filters, string sortBy, string sortOrder);
    }
}