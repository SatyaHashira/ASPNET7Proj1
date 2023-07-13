using ASPNET7Proj1.Data;
using ASPNET7Proj1.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            Models.Domain.Tags tag = new Models.Domain.Tags()
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();
            

            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<Models.Domain.Tags> tags =await bloggieDbContext.Tags.ToListAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            //1 Method
            var exTag = bloggieDbContext.Tags.Find(Id);
            //2 Method
            var tag =await bloggieDbContext.Tags.FirstOrDefaultAsync(s=>s.Id== Id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest()
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Models.Domain.Tags
            {
                Id= editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var ExistingTag = await bloggieDbContext.Tags.FirstOrDefaultAsync(s => s.Id == tag.Id);

            if(ExistingTag != null)
            {
                ExistingTag.Name = tag.Name;
                ExistingTag.DisplayName = tag.DisplayName;

                //Save
                await bloggieDbContext.SaveChangesAsync();

                //show success Notification
                return RedirectToAction("Edit", new {id=editTagRequest.Id});
            }
            //show error Notification
            return RedirectToAction("Edit", new {id=editTagRequest.Id});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var tag = bloggieDbContext.Tags.Find(editTagRequest.Id);
            if(tag != null)
            {
                bloggieDbContext.Tags.Remove(tag);
                await bloggieDbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }

            //show a Error Notification
            return RedirectToAction("Edit",new {id=editTagRequest.Id });
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
