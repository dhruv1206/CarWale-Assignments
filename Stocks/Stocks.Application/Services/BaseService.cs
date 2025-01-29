using Stocks.Application.Dtos;
using Stocks.Application.Common;
using Stocks.Application.Interfaces.IServices;
using Stocks.Application.Interfaces.IMapper;
using Stocks.Application.Interfaces.IRepositories;


namespace Stocks.Application.Services
{
    public class BaseService<T, TDto, FDto, F> : IBaseService<TDto, FDto>
        where T : class
        where TDto : class
        where F : class
        where FDto : class
    {
        private readonly IBaseMapper<T, TDto> _mapper;
        private readonly IBaseMapper<F, List<IndividualFilter>> _filterMapper;
        private readonly IBaseMapper<FDto, F> _filterDtoToFilterMapper;
        private readonly IBaseRepository<T> _repository;

        public BaseService(
            IBaseMapper<T, TDto> viewModelMapper,
            IBaseMapper<FDto, F> filterDtoToFilterMapper,
            IBaseMapper<F, List<IndividualFilter>> filterMapper,
            IBaseRepository<T> repository)
        {
            _mapper = viewModelMapper;
            _filterMapper = filterMapper;
            _filterDtoToFilterMapper = filterDtoToFilterMapper;
            _repository = repository;
        }

        public virtual async Task<IEnumerable<TDto>> GetAll()
        {
            return _mapper.MapList(await _repository.GetAll());
        }

        public virtual async Task<IEnumerable<TDto>> GetAll(FDto filters)
        {
            return _mapper.MapList(await _repository.GetAll(_filterMapper.MapModel(_filterDtoToFilterMapper.MapModel(filters))));
        }

        public virtual async Task<TDto?> GetById(int id)
        {
            return _mapper.MapModel(await _repository.GetById(id));
        }

        public virtual async Task<TDto> DeleteById(int id)
        {
            return _mapper.MapModel(await _repository.DeleteById(id));
        }

        public virtual async Task<bool> Exists(int id)
        {
            return await _repository.Exists(id);
        }

        public virtual async Task<PaginatedDataDto<TDto>> GetPaginatedData(int pageNumber, int pageSize)
        {
            var paginatedData = await _repository.GetPaginatedData(pageNumber, pageSize);
            var mappedData = _mapper.MapList(paginatedData.Data);
            var paginatedDataDto = new PaginatedDataDto<TDto>(mappedData.ToList(), paginatedData.TotalCount);
            return paginatedDataDto;
        }

        public virtual async Task<PaginatedDataDto<TDto>> GetPaginatedData(int pageNumber, int pageSize, FDto filters)
        {
            var paginatedData = await _repository.GetPaginatedData(pageNumber, pageSize, _filterMapper.MapModel(_filterDtoToFilterMapper.MapModel(filters)));
            var mappedData = _mapper.MapList(paginatedData.Data);
            var paginatedDataDto = new PaginatedDataDto<TDto>(mappedData.ToList(), paginatedData.TotalCount);
            return paginatedDataDto;
        }

        public virtual async Task<PaginatedDataDto<TDto>> GetPaginatedData(int pageNumber, int pageSize, FDto filters, string sortBy, string sortOrder)
        {
            var paginatedData = await _repository.GetPaginatedData(pageNumber, pageSize, _filterMapper.MapModel(_filterDtoToFilterMapper.MapModel(filters)), sortBy, sortOrder);
            var mappedData = _mapper.MapList(paginatedData.Data);
            var paginatedDataDto = new PaginatedDataDto<TDto>(mappedData.ToList(), paginatedData.TotalCount);
            return paginatedDataDto;
        }
    }
}