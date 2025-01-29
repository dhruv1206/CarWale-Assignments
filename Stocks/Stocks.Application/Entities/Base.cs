using System.ComponentModel.DataAnnotations;

namespace Stocks.Application.Entities
{
    public class Base<T>
    {
        [Key]
        public T? Id { get; set; }
    }
}
