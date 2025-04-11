namespace ShortURL.API.DTOs.Pagination
{
    public class PageResults<TModel> where TModel : class
    {
        public required int PageTotal { get; set; }
        public required int PageNumber { get; set; }
        public required int PageSize { get; set; }

        public IList<TModel>? PageResult { get; set; }
    }
}
