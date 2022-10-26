using AutoMapper;
using BuyHouse.BLL.Services.Providers.DateTimeProvider;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Entities.HelperEnum;
using BuyHouse.WEB.Models.AdvertModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuyHouse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class RoomAdvertController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;

        public RoomAdvertController(ApplicationDbContext context, IMapper mapper, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// Get room advert by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Room advert</returns>
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetRoomAdvertById(int Id)
        {
            try
            {
                var result = await _context.RoomAdverts.FindAsync(Id);
                if (result == null)
                    return NotFound($"Room advert with Id = {Id} not found");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting data");
            }
        }

        /// <summary>
        /// Create room advert
        /// </summary>
        /// <param name="roomAdvert"></param>
        /// <param name="currentUserId"></param>
        /// <returns>created room advert</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRoomAdvert(RoomAdvertModel roomAdvert, string? currentUserId)
        {
            try
            {
                if (string.IsNullOrEmpty(currentUserId))
                    return NotFound("Not found current user");

                if (roomAdvert != null)
                {
                    RoomAdvert advert = new();
                    advert = _mapper.Map<RoomAdvertModel, RoomAdvert>(roomAdvert);

                    advert.UserID = currentUserId;
                    advert.CreationDate = _dateTimeProvider.Now();

                    if (advert.TypePrice == TypeOfPrice.TotalPrice)
                        advert.PricePerSquareMeter = (ulong)(advert.TotalPrice / advert.TotalArea);
                    else
                    {
                        advert.PricePerSquareMeter = advert.TotalPrice;
                        advert.TotalPrice = (ulong)(advert.TotalPrice * advert.TotalArea);
                    }

                    advert.Description = advert.Description.Replace("\n", "<br/>");

                    await _context.RoomAdverts.AddAsync(advert);
                    await _context.SaveChangesAsync();
                    return Ok(advert);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data");
            }
        }

        // <summary>
        /// Update flat advert
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="flatAdvert"></param>
        /// <returns>updated flat advert</returns>
        [HttpPut("{Id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateRoomAdvert(int Id, RoomAdvertModel roomAdvert, string currentUserId)
        {
            try
            {
                if (Id != roomAdvert.Id)
                    return BadRequest("Room advert ID is mismatch");

                if (string.IsNullOrEmpty(currentUserId))
                    return NotFound("Not found current user");

                RoomAdvert roomAdvert_ = new();
                roomAdvert_ = _mapper.Map<RoomAdvertModel, RoomAdvert>(roomAdvert);

                var roomAdvertToUpdate = await _context.RoomAdverts.FindAsync(Id);

                if (roomAdvertToUpdate == null)
                    return NotFound($"Room advert with Id = {Id} not found");

                if (roomAdvertToUpdate.UserID != currentUserId)
                    return BadRequest("This advert isn't belong current user");

                roomAdvertToUpdate.MainInfo.Region = roomAdvert_.MainInfo.Region;
                roomAdvertToUpdate.MainInfo.City = roomAdvert_.MainInfo.City;
                roomAdvertToUpdate.MainInfo.Street = roomAdvert_.MainInfo.Street;
                roomAdvertToUpdate.MainInfo.HouseNumber = roomAdvert_.MainInfo.HouseNumber;
                roomAdvertToUpdate.MainInfo.FlatNumber = roomAdvert_.MainInfo.FlatNumber;
                roomAdvertToUpdate.MainInfo.RegistrationDate = roomAdvert_.MainInfo.RegistrationDate;

                foreach (var photo in roomAdvert_.Photos)
                    roomAdvertToUpdate.Photos.Add(photo);

                roomAdvertToUpdate.Description = roomAdvert_.Description.Replace("\n", "<br/>");
                roomAdvertToUpdate.Type = roomAdvert_.Type;
                roomAdvertToUpdate.TotalArea = roomAdvert_.TotalArea;
                roomAdvertToUpdate.LivingArea = roomAdvert_.LivingArea;
                roomAdvertToUpdate.Floor = roomAdvert_.Floor;
                roomAdvertToUpdate.TotalCountFloors = roomAdvert_.TotalCountFloors;
                roomAdvertToUpdate.Heating = roomAdvert_.Heating;
                roomAdvertToUpdate.RegistrationNumber = roomAdvert_.RegistrationNumber;

                if (roomAdvert_.TypePrice == TypeOfPrice.TotalPrice)
                    roomAdvertToUpdate.PricePerSquareMeter = (ulong)(roomAdvert_.TotalPrice / roomAdvert_.TotalArea);
                else
                {
                    roomAdvertToUpdate.PricePerSquareMeter = roomAdvert_.TotalPrice;
                    roomAdvertToUpdate.TotalPrice = (ulong)(roomAdvert_.TotalPrice * roomAdvert_.TotalArea);
                }

                roomAdvertToUpdate.Currency = roomAdvert_.Currency;
                roomAdvertToUpdate.TypePrice = roomAdvert_.TypePrice;

                _context.RoomAdverts.Update(roomAdvertToUpdate);
                await _context.SaveChangesAsync();
                return Ok(roomAdvertToUpdate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        /// <summary>
        /// Delete room advert from db
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>deleted room advert</returns>
        [HttpDelete("{Id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteRoomAdvert(int Id, string currentUserId)
        {
            try
            {
                if (string.IsNullOrEmpty(currentUserId))
                    return NotFound("Not found current user");

                var roomAdvertToDelete = await _context.RoomAdverts.FindAsync(Id);

                if (roomAdvertToDelete.UserID != currentUserId)
                    return BadRequest("This advert isn't belong current user");

                _context.RoomAdverts.Remove(roomAdvertToDelete);
                await _context.SaveChangesAsync();

                return Ok(roomAdvertToDelete);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
    }
}
