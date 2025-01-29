namespace Stocks.Application.Dtos
{
    public class PaginatedDataDto<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Data { get; set; }

        public PaginatedDataDto(IEnumerable<T> data, int totalCount)
        {
            Data = data;
            TotalCount = totalCount;
        }
    }
}