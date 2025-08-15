using System.Collections.Generic;

namespace MyApi.Models.DTOs
{
    public class PaginatedResponse<T>
    {
        public required IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
