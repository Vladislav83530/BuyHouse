using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services.Abstract;
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
        public async Task<List<RealtyPhotoDTO>> AddPhoto(IFormFileCollection uploads, string currentUserId)
        {
            List<RealtyPhotoDTO> realtyPhotoDTOs = new List<RealtyPhotoDTO>();
            foreach (var uploadedFile in uploads)
            {
                string path = "/Files/" + currentUserId + uploadedFile.FileName;
                using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                RealtyPhotoDTO file = new RealtyPhotoDTO { Name = uploadedFile.FileName, Path = path };

                realtyPhotoDTOs.Add(file);
            }
            return realtyPhotoDTOs;
        }
    }
}
