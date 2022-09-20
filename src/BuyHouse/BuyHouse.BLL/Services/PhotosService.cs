using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BuyHouse.BLL.Services
{
    public class PhotosService : IPhotosService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public PhotosService(IHostingEnvironment hostingEnviroment)
        {
            _hostingEnvironment = hostingEnviroment;
        }

        /// <summary>
        /// Add photos to DTO
        /// </summary>
        /// <param name="uploads"></param>
        /// <param name="currentUserId"></param>
        /// <returns>DTO with photos</returns>
        public async Task<IEnumerable<RealtyPhoto>> AddPhotoToAdvert(IFormFileCollection uploads, string currentUserId)
        {
            List<RealtyPhoto> realtyPhotos = new List<RealtyPhoto>();
            foreach (var uploadedFile in uploads)
            {
                string path = "/Files/" + currentUserId + uploadedFile.FileName;
                using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                RealtyPhoto file = new RealtyPhoto { Name = uploadedFile.FileName, Path = path };

                realtyPhotos.Add(file);
            }
            return realtyPhotos;
        }
    }
}
