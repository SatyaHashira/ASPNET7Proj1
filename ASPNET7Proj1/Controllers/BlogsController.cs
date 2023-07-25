using ASPNET7Proj1.Models.ViewModels;
using ASPNET7Proj1.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET7Proj1.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        public BlogsController(IBlogPostRepository blogPostRepository,IBlogPostLikeRepository blogPostLikeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;
            var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
            var blogPostDetailsViewModel = new BlogPostDetailsViewModel();

            if (blogPost != null)
            {
                var TotalLikes = await blogPostLikeRepository.GetTotalLikes(blogPost.Id);

                if (signInManager.IsSignedIn(User))
                {
                    //Get likes for this blog for this User
                    var LikesForBlog =await  blogPostLikeRepository.GetLikesForBlog(blogPost.Id);
                    var userId = userManager.GetUserId(User); 
                    if(userId != null)
                    {
                        var LikeFromUser = LikesForBlog.FirstOrDefault(s=>s.UserId == Guid.Parse(userId));
                        liked = LikeFromUser != null;

                    }
                }

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
                    Liked = liked
                };
               
            }

            return View(blogPostDetailsViewModel);
        }
    }
}
