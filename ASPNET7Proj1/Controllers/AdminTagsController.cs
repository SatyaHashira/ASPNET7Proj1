using ASPNET7Proj1.Data;
using ASPNET7Proj1.Models.ViewModels;
using ASPNET7Proj1.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNET7Proj1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;
        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
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
             
            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            //1 Method
            //var exTag = bloggieDbContext.Tags.Find(Id);
            //2 Method

            var tag = await tagRepository.GetAsync(Id);

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
            var UpdatedTag = await tagRepository.UpdateAsync(tag);

            if(UpdatedTag != null)
            {
                 //Show success Notification
            }
            else
            {
                //show error Notification
            }
            return RedirectToAction("Edit", new {id=editTagRequest.Id});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
             var DeletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);
            if (DeletedTag != null)
            {
                //show success notification
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
