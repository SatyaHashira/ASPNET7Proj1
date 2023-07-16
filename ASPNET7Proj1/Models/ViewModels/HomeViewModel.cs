using ASPNET7Proj1.Models.Domain;

namespace ASPNET7Proj1.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> blogPosts { get; set; }

        public IEnumerable<Tags> tags { get; set; }
    }
}
