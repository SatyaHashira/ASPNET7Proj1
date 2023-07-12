using ASPNET7Proj1.Data;
using ASPNET7Proj1.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET7Proj1.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;
        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            Models.Domain.Tags tag = new Models.Domain.Tags()
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            bloggieDbContext.Tags.Add(tag);
            bloggieDbContext.SaveChanges();
            

            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult List()
        {
            List<Models.Domain.Tags> tags = bloggieDbContext.Tags.ToList();

            return View(tags);
        }




        //Refer this method as example for reading the value from form
        //[HttpPost]
        //[ActionName("Add")]
        //public IActionResult Submit(AddTagRequest addTagRequest)
        //{
        //    Not the recemmended way
        //    {
        //        var name = Request.Form["name"];
        //        var displayName = Request.Form["displayName"];
        //    }

        //    var name = addTagRequest.Name;
        //    var displayName = addTagRequest.DisplayName;

        //    return View("Add");
        //}
    }
}
