﻿using ASPNET7Proj1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET7Proj1.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        public BlogsController(IBlogPostRepository blogPostRepository)
        {
                this.blogPostRepository = blogPostRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
            return View(blogPost);
        }
    }
}
