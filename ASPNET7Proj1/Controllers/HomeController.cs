﻿using ASPNET7Proj1.Models;
using ASPNET7Proj1.Models.ViewModels;
using ASPNET7Proj1.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPNET7Proj1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ITagRepository tagRepository;

        public HomeController(ILogger<HomeController> logger,IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            this.blogPostRepository = blogPostRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();
            var tags = await tagRepository.GetAllAsync();

            var model = new HomeViewModel
            {
                blogPosts = blogPosts,
                tags = tags
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}