using ASPNET7Proj1.Models.ViewModels;
using ASPNET7Proj1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET7Proj1.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        public BlogsController(IBlogPostRepository blogPostRepository,IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
            var blogPostDetailsViewModel = new BlogPostDetailsViewModel();

            if (blogPost != null)
            {
                var TotalLikes = await blogPostLikeRepository.GetTotalLikes(blogPost.Id);

                blogPostDetailsViewModel = new BlogPostDetailsViewModel
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    ShortDescription = blogPost.ShortDescription,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    PublishedDate = blogPost.PublishedDate,
                    Author = blogPost.Author,
                    visible = blogPost.visible,
                    totalLikes = TotalLikes,
                    tags = blogPost.tags,

                };
               
            }

            return View(blogPostDetailsViewModel);
        }
    }
}
