using AutoMapper;
using BuyHouse.BLL.Services.Providers.DateTimeProvider;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Entities.HelperEnum;
using BuyHouse.WEB.Models.AdvertModel;
using BuyHouse.WEB.Models.HttpClientModel;
using Microsoft.AspNetCore.Mvc;

namespace BuyHouse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/")]
    public class FlatAdvertController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;

        public FlatAdvertController(ApplicationDbContext context, IMapper mapper, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// Get Flat advert by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Flat advert</returns>
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetFlatAdvertById(int Id)
        {
            try
            {
                var result = await _context.FlatAdverts.FindAsync(Id);
                if (result == null)
                    return NotFound($"Flat advert with Id = {Id} not found");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting data");
            }
        }

        /// <summary>
        /// Create flat advert
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="currentUserId"></param>
        /// <returns>created flat advert</returns>
        [HttpPost]
        public async Task<IActionResult> CreateFlatAdvert(CreateRequestModel requestModel, string? currentUserId)
        {
            try
            {
                if (string.IsNullOrEmpty(currentUserId))
                    return NotFound("Not found current user");

                if (requestModel.FlatAdvert != null)
                {
                    FlatAdvert advert = new FlatAdvert();
                    advert = _mapper.Map<FlatAdvertModel, FlatAdvert>(requestModel.FlatAdvert);

                    advert.UserID = currentUserId;
                    advert.CreationDate = _dateTimeProvider.Now();
                    advert.Photos = (ICollection<RealtyPhoto>)requestModel.RealtyPhotos;

                    if (advert.TypePrice == TypeOfPrice.TotalPrice)
                        advert.PricePerSquareMeter = (ulong?)(advert.TotalPrice / advert.TotalArea);
                    else
                    {
                        advert.PricePerSquareMeter = advert.TotalPrice;
                        advert.TotalPrice = (ulong?)(advert.TotalPrice * advert.TotalArea);
                    }

                    await _context.FlatAdverts.AddAsync(advert);
                    await _context.SaveChangesAsync();
                    return Ok(advert);
                }
                return BadRequest();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data");
            }
        }

        /// <summary>
        /// Update flat advert
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="requestModel"></param>
        /// <returns>updated flat advert</returns>
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> UpdateFlatAdvert(int Id, CreateRequestModel requestModel)
        {
            try
            {
                if (Id != requestModel.FlatAdvert.Id)
                    return BadRequest("Flat advert ID is mismatch");

                FlatAdvert flatAdvert_ = new FlatAdvert();
                flatAdvert_ = _mapper.Map<FlatAdvertModel, FlatAdvert>(requestModel.FlatAdvert);

                var flatAdvertToUpdate = await _context.FlatAdverts.FindAsync(Id);

                if (flatAdvertToUpdate == null)
                    return NotFound($"Flat advert with Id = {Id} not found");

                flatAdvertToUpdate.MainInfo.Region = flatAdvert_.MainInfo.Region;
                flatAdvertToUpdate.MainInfo.City = flatAdvert_.MainInfo.City;
                flatAdvertToUpdate.MainInfo.Street = flatAdvert_.MainInfo.Street;
                flatAdvertToUpdate.MainInfo.HouseNumber = flatAdvert_.MainInfo.HouseNumber;
                flatAdvertToUpdate.MainInfo.FlatNumber = flatAdvert_.MainInfo.FlatNumber;
                flatAdvertToUpdate.MainInfo.RegistrationDate = flatAdvert_.MainInfo.RegistrationDate;
                flatAdvertToUpdate.Photos = (ICollection<RealtyPhoto>)requestModel.RealtyPhotos;
                flatAdvertToUpdate.Description = flatAdvert_.Description;
                flatAdvertToUpdate.Type = flatAdvert_.Type;
                flatAdvertToUpdate.TypeOfWalls = flatAdvert_.TypeOfWalls;
                flatAdvertToUpdate.TotalArea = flatAdvert_.TotalArea;
                flatAdvertToUpdate.LivingArea = flatAdvert_.LivingArea;
                flatAdvertToUpdate.Floor = flatAdvert_.Floor;
                flatAdvertToUpdate.Heating = flatAdvert_.Heating;
                flatAdvertToUpdate.YearBuilt = flatAdvert_.YearBuilt; 
                flatAdvertToUpdate.RegistrationNumber = flatAdvert_.RegistrationNumber;
                flatAdvertToUpdate.TotalPrice = flatAdvert_.TotalPrice;
                flatAdvertToUpdate.PricePerSquareMeter = flatAdvert_.PricePerSquareMeter;
                flatAdvertToUpdate.Currency = flatAdvert_.Currency;
                flatAdvertToUpdate.TypePrice = flatAdvert_.TypePrice;
                flatAdvertToUpdate.CreationDate = flatAdvert_.CreationDate;
                flatAdvertToUpdate.LikeCount = flatAdvert_.LikeCount;

                _context.FlatAdverts.Update(flatAdvertToUpdate);
                await _context.SaveChangesAsync();
                return Ok(flatAdvertToUpdate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        /// <summary>
        /// Delete falt advert from db
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>deleted flat advert</returns>
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteFlatAdvert(int Id)
        {
            try
            {
                var flatAdvertToDelete = await _context.FlatAdverts.FindAsync(Id);

                if (flatAdvertToDelete == null)
                    return NotFound($"Flat advert with Id = {Id} not found");

                 _context.FlatAdverts.Remove(flatAdvertToDelete);
                await _context.SaveChangesAsync();

                return Ok(flatAdvertToDelete);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
    }
}
