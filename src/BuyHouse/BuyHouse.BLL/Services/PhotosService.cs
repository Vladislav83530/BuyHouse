using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Entities.ApplicationUserEntities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ICollection<RealtyPhoto>> AddPhotoToAdvertAsync(IFormFileCollection uploads, string currentUserId)
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

        /// <summary>
        /// Delete photo from advert
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="advertId"></param>
        /// <param name="photoId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<FlatAdvert> DeletePhotoFromFlatAdvertAsync(string currentUserId,int advertId, int photoId)
        {
            if (currentUserId == null)
                throw new Exception("Not found user");

            var flatAdvert= await _context.FlatAdverts.Where(x=>x.Id== advertId).FirstAsync();

            if (flatAdvert.UserID != currentUserId)
                throw new Exception("Not found users advert");

            foreach(var photo in flatAdvert.Photos)
            {
                if (photo.Id == photoId)
                {
                    flatAdvert.Photos.Remove(photo);
                        await _context.SaveChangesAsync();
                }
            }
            return flatAdvert;
        }

        /// <summary>
        /// Delete photo from advert
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="advertId"></param>
        /// <param name="photoId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<HouseAdvert> DeletePhotoFromHouseAdvertAsync(string currentUserId, int advertId, int photoId)
        {
            if (currentUserId == null)
                throw new Exception("Not found user");

            var houseAdvert = await _context.HouseAdverts.Where(x => x.Id == advertId).FirstAsync();

            if (houseAdvert.UserID != currentUserId)
                throw new Exception("Not found users advert");

            foreach (var photo in houseAdvert.Photos)
            {
                if (photo.Id == photoId)
                {
                    houseAdvert.Photos.Remove(photo);
                    await _context.SaveChangesAsync();
                }
            }
            return houseAdvert;
        }
    }
}
