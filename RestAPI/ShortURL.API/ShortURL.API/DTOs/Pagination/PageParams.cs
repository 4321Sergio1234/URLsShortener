namespace ShortURL.API.DTOs.Pagination
{
    public class PageParams
    {
        public required int PageNumber { get; set; }
        public required int PageSize { get; set; }
    }
}
