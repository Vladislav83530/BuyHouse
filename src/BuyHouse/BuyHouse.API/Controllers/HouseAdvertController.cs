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
    public class HouseAdvertController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;

        public HouseAdvertController(ApplicationDbContext context, IMapper mapper, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
        }


        /// <summary>
        /// Get house advert by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>House advert</returns>
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetHouseAdvertById(int Id)
        {
            try
            {
                var result = await _context.HouseAdverts.FindAsync(Id);
                if (result == null)
                    return NotFound($"House advert with Id = {Id} not found");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting data");
            }
        }

        /// <summary>
        /// Create house advert
        /// </summary>
        /// <param name="houseAdvert"></param>
        /// <param name="currentUserId"></param>
        /// <returns>Created advert</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateHouseAdvert(HouseAdvertModel houseAdvert, string? currentUserId)
        {
            try
            {
                if (string.IsNullOrEmpty(currentUserId))
                    return NotFound("Not found current user");

                if (houseAdvert != null)
                {
                    HouseAdvert advert = new();
                    advert = _mapper.Map<HouseAdvertModel, HouseAdvert>(houseAdvert);

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

                    await _context.HouseAdverts.AddAsync(advert);
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

        /// <summary>
        /// Update house advert
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="houseAdvert"></param>
        /// <returns>updated house advert</returns>
        [HttpPut("{Id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateHouseAdvert(int Id, HouseAdvertModel houseAdvert, string currentUserId)
        {
            try
            {
                if (Id != houseAdvert.Id)
                    return BadRequest("Flat advert ID is mismatch");

                if (string.IsNullOrEmpty(currentUserId))
                    return NotFound("Not found current user");

                HouseAdvert houseAdvert_ = new();
                houseAdvert_ = _mapper.Map<HouseAdvertModel, HouseAdvert>(houseAdvert);

                var houseAdvertToUpdate = await _context.HouseAdverts.FindAsync(Id);

                if (houseAdvertToUpdate == null)
                    return NotFound($"Flat advert with Id = {Id} not found");

                if (houseAdvertToUpdate.UserID != currentUserId)
                    return BadRequest("This advert isn't belong current user");

                houseAdvertToUpdate.MainInfo.Region = houseAdvert_.MainInfo.Region;
                houseAdvertToUpdate.MainInfo.City = houseAdvert_.MainInfo.City;
                houseAdvertToUpdate.MainInfo.Street = houseAdvert_.MainInfo.Street;
                houseAdvertToUpdate.MainInfo.HouseNumber = houseAdvert_.MainInfo.HouseNumber;
                houseAdvertToUpdate.MainInfo.FlatNumber = houseAdvert_.MainInfo.FlatNumber;
                houseAdvertToUpdate.MainInfo.RegistrationDate = houseAdvert_.MainInfo.RegistrationDate;

                foreach (var photo in houseAdvert_.Photos)
                    houseAdvertToUpdate.Photos.Add(photo);

                houseAdvertToUpdate.Description = houseAdvert_.Description.Replace("\n", "<br/>");
                houseAdvertToUpdate.Type = houseAdvert_.Type;
                houseAdvertToUpdate.TypeOfWalls = houseAdvert_.TypeOfWalls;
                houseAdvertToUpdate.TotalArea = houseAdvert_.TotalArea;
                houseAdvertToUpdate.LivingArea = houseAdvert_.LivingArea;
                houseAdvertToUpdate.LandArea = houseAdvert_.LandArea;
                houseAdvertToUpdate.TotalCountFloors = houseAdvert_.TotalCountFloors;
                houseAdvertToUpdate.Heating = houseAdvert_.Heating;
                houseAdvertToUpdate.YearBuilt = houseAdvert_.YearBuilt;
                houseAdvertToUpdate.RegistrationNumber = houseAdvert_.RegistrationNumber;
                houseAdvertToUpdate.CadastralNumber = houseAdvert_.CadastralNumber;

                if (houseAdvert_.TypePrice == TypeOfPrice.TotalPrice)
                    houseAdvertToUpdate.PricePerSquareMeter = (ulong)(houseAdvert_.TotalPrice / houseAdvert_.TotalArea);
                else
                {
                    houseAdvertToUpdate.PricePerSquareMeter = houseAdvert_.TotalPrice;
                    houseAdvertToUpdate.TotalPrice = (ulong)(houseAdvert_.TotalPrice * houseAdvert_.TotalArea);
                }

                houseAdvertToUpdate.Currency = houseAdvert_.Currency;
                houseAdvertToUpdate.TypePrice = houseAdvert_.TypePrice;

                _context.HouseAdverts.Update(houseAdvertToUpdate);
                await _context.SaveChangesAsync();
                return Ok(houseAdvertToUpdate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        /// <summary>
        /// Delete house advert from db
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>deleted house advert</returns>
        [HttpDelete("{Id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteHouseAdvert(int Id, string currentUserId)
        {
            try
            {
                if (string.IsNullOrEmpty(currentUserId))
                    return NotFound("Not found current user");

                var houseAdvertToDelete = await _context.HouseAdverts.FindAsync(Id);

                if (houseAdvertToDelete.UserID != currentUserId)
                    return BadRequest("This advert isn't belong current user");

                _context.HouseAdverts.Remove(houseAdvertToDelete);
                await _context.SaveChangesAsync();

                return Ok(houseAdvertToDelete);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
    }
}
