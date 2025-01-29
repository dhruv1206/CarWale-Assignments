using System.Threading.Tasks;
using Stocks.Application.Common;
using Stocks.Application.Dtos;
namespace Stocks.Application.Interfaces.IServices
{
    public interface IBaseService<TDto, FDto>
        where TDto : class
        where FDto : class
    {
        Task<IEnumerable<TDto>> GetAll();
        Task<IEnumerable<TDto>> GetAll(FDto filters);

        Task<TDto?> GetById(int id);

        Task<TDto> DeleteById(int id);

        Task<bool> Exists(int id);

        Task<PaginatedDataDto<TDto>> GetPaginatedData(int pageNumber, int pageSize);
        Task<PaginatedDataDto<TDto>> GetPaginatedData(int pageNumber, int pageSize, FDto filters);
        Task<PaginatedDataDto<TDto>> GetPaginatedData(int pageNumber, int pageSize, FDto filters, string sortBy, string sortOrder);
    }
}
