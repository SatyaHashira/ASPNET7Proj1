using ASPNET7Proj1.Models.Domain;
using ASPNET7Proj1.Models.ViewModels;
using ASPNET7Proj1.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ASPNET7Proj1.Controllers
{
    public class AdminBlogController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogController(ITagRepository tagRepository,IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = 
                tags.Select(s => new SelectListItem { Text= s.Name,Value=s.Id.ToString()})
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest) 
        {
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                visible = addBlogPostRequest.visible
            };
            var selectedTags = new List<Tags>();
            foreach(var selectedTag in addBlogPostRequest.SelectedTag)
            {
                Guid selectedTagAsGuid = Guid.Parse(selectedTag);
                var existingTag = await tagRepository.GetAsync(selectedTagAsGuid);
                if(existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            blogPost.tags = selectedTags;

            await blogPostRepository.AddAsync(blogPost);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();
            return View(blogPosts);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            //Retrieve the result from repository
            var blogPost = await blogPostRepository.GetAsync(Id);

            var tagDomainModel = await tagRepository.GetAllAsync();

            //Map Domain model into the view model
            if(blogPost != null) 
            {
                var model = new EditBlogPostRequest
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
                    Tags = tagDomainModel.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }),
                    SelectedTag = blogPost.tags.Select(s=>s.Id.ToString()).ToArray()
                };
                return View(model);
            }

            //pass the data to view
            return View(null);
        }

    }
}
