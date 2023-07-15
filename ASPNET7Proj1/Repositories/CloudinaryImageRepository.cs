using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace ASPNET7Proj1.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration configuration;
        private readonly Account account;
        public CloudinaryImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]
                );
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var Client = new Cloudinary(account);

            var UploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };
            var UploadResult = await Client.UploadAsync(UploadParams);

            if(UploadResult != null && UploadResult.StatusCode == System.Net.HttpStatusCode.OK) 
            {
                return UploadResult.SecureUri.ToString();
            }
            return null;

        }
    }
}
