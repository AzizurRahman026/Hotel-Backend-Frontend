using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hotels_Crud.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IHotelServices _hotelServices;

        public HomeController(IHotelServices hotelServices)
        {
            _hotelServices = hotelServices;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            Console.WriteLine("ok now hitting this root api...");
            return Ok( new {Message = "Home page"});
        }

        [HttpGet("gethotels")]
        public async Task<IActionResult> GetHotels()
        {
            Console.WriteLine("ok now hitting this gethotel api...");
            var result = await _hotelServices.GetHotels();
            return Ok(result);
        }

        [HttpPost("addhotel")]
        public async Task<IActionResult> AddHotel([FromBody] Hotel newHotel)
        {
            await _hotelServices.AddHotel(newHotel);
            Console.WriteLine(newHotel.id + " " + newHotel.name);
            return Ok(new { Message = "Hotel added successfully!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(string id, [FromBody] Hotel updateHotel)
        {
            var result = await _hotelServices.UpdateHotel(id, updateHotel);
            if (!result)
            {
                return NotFound("Hotel not found!");
            }
            return Ok("Hotel updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(string id)
        {
            var result = await _hotelServices.DeleteHotel(id);
            if (!result)
            {
                return NotFound("Hotel not found...");
            }
            return Ok("Hotel delete Successfully...");
        }

        [HttpGet("{cityName}")]
        public async Task<IActionResult> SearchHotelsByCity(string cityName)
        {
            List<Hotel> result = await _hotelServices.SearchHotelsByCity(cityName);
            if (result.Count == 0) return NotFound("Hotels not found");
            return Ok(result);
        }
    }
}
