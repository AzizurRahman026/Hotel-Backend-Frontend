using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hotels_Crud.Controllers
{
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
            return Ok("Alhamdulillah! Home page working...");
        }

        [HttpGet("gethotels")]
        public IActionResult GetHotels()
        {
            var result = _hotelServices.GetHotels();
            return Ok(result);
        }

        [HttpPost("addhotel")]
        public IActionResult AddHotel([FromBody] hotel newHotel)
        {
            _hotelServices.AddHotel(newHotel);
            Console.WriteLine(newHotel.id + " " + newHotel.name);
            return Ok(new { Message = "Hotel added successfully!" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHotel(string id, [FromBody] hotel updateHotel)
        {
            var result = _hotelServices.UpdateHotel(id, updateHotel);
            if (!result)
            {
                return NotFound("Hotel not found!");
            }
            return Ok("Hotel updated successfully!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHotel(string id)
        {
            var result = _hotelServices.DeleteHotel(id);
            if (!result)
            {
                return NotFound("Hotel not found...");
            }
            return Ok("Hotel delete Successfully...");
        }
    }
}
