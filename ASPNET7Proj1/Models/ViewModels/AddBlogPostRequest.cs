using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPNET7Proj1.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        public string? Heading { get; set; }
        public string? PageTitle { get; set; }
        public string? Content { get; set; }
        public string? ShortDescription { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public string? UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? Author { get; set; }
        public bool visible { get; set; }

        //Display Tags
        public IEnumerable<SelectListItem> Tags { get; set; }
        //Collect Tags
        //public string SelectedTag { get; set; }  @this is for dropdown
        public string[] SelectedTag { get; set; } = Array.Empty<string>(); //this is for selcting multiple items from
                                                                            //tags Db in form as Array @recommended to call
                                                                            //it as a listbox
    }
}
