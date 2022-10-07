using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.ApplicationUserEntities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BuyHouse.BLL.Services
{
    public class PhotosService : IPhotosService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly string advertPhotosPath = "/Files/";
        private readonly string userAvatars = "/Files/Avatars/";
        public PhotosService(IHostingEnvironment hostingEnviroment, ApplicationDbContext context)
        {
            _hostingEnvironment = hostingEnviroment;
            _context = context;
        }

        /// <summary>
        /// Add photos to DTO
        /// </summary>
        /// <param name="uploads"></param>
        /// <param name="currentUserId"></param>
        /// <returns>DTO with photos</returns>
        public async Task<IEnumerable<RealtyPhoto>> AddPhotoToAdvertAsync(IFormFileCollection uploads, string currentUserId)
        {
            List<RealtyPhoto> realtyPhotos = new List<RealtyPhoto>();
            foreach (var uploadedFile in uploads)
            {
                string path = advertPhotosPath + currentUserId + uploadedFile.FileName;
                using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                RealtyPhoto file = new RealtyPhoto { Name = uploadedFile.FileName, Path = path };

                realtyPhotos.Add(file);
            }
            return realtyPhotos;
        }

        /// <summary>
        /// Update user avatar
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <param name="currentUser"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public async Task UpdateUserAvatarPhotoAsync(IFormFile uploadedFile, UserAvatar currentUsersAvatar, string currentUserId)
        {
            if (uploadedFile != null && currentUsersAvatar != null)
            {
                string path = userAvatars + currentUserId + uploadedFile.FileName;

                using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                UserAvatar file = new UserAvatar { Name = currentUserId, Path = path, ApplicationUserId = currentUserId };

                _context.UserAvatars.Remove(currentUsersAvatar);
                _context.UserAvatars.Add(file);
                await _context.SaveChangesAsync();
            }
        }
    }
}
