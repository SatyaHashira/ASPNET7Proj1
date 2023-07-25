namespace ASPNET7Proj1.Models.ViewModels
{
    public class BlogPostDetailsViewModel
    {
        public Guid Id { get; set; }
        public string? Heading { get; set; }
        public string? PageTitle { get; set; }
        public string? Content { get; set; }
        public string? ShortDescription { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public string? UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? Author { get; set; }
        public bool visible { get; set; }
        public ICollection<Domain.Tags>? tags { get; set; }
        public int totalLikes { get; set; }
        public bool Liked { get; set; }
    }
}
