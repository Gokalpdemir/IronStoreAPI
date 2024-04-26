namespace ETıcaretAPI.Application.Features.ProductImageFiles.Queries.GetById
{
    public class GetByIdProductImagesFileResponse
    {
        public string Path { get; set; }
        public Guid Id { get; set; }
        public string  FileName { get; set; }
        public bool ShowCase { get; set; }
    }
}
