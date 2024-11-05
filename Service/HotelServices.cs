using Entities;
using MongoDB.Driver;
using Data;

public interface IHotelServices
{
    Task<List<hotel>> GetHotels();
    Task AddHotel(hotel obj);

    Task<bool> UpdateHotel(string id, hotel updatedHotel);
    Task<bool> DeleteHotel(string id);
    // add search hotel function...
    Task<List<hotel>> SearchHotelsByCity(string cityName);
}

namespace Service
{
    public class HotelServices : IHotelServices
    {
        private readonly MongoDbContext _context;

        public HotelServices(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<hotel>> GetHotels()
        {
            return await _context.hotel.Find(_ => true).ToListAsync();
        }

        public async Task AddHotel(hotel obj)
        {
            await _context.hotel.InsertOneAsync(obj);
        }

        public async Task<bool> UpdateHotel(string id, hotel updatedHotel)
        {
            int Id = Int32.Parse(id);
            var filter = Builders<hotel>.Filter.Eq(h => h.id, Id);
            var updateResult = await _context.hotel.ReplaceOneAsync(filter, updatedHotel);
            return updateResult.ModifiedCount > 0;
        }


        // Delete a hotel by ID from the database
        public async Task<bool> DeleteHotel(string id)
        {
            int Id = Int32.Parse(id);
            var filter = Builders<hotel>.Filter.Eq(h => h.id, Id);
            var deleteResult = await _context.hotel.DeleteOneAsync(filter);
            return deleteResult.DeletedCount > 0;
        }

        // Search hotels by city name in the database
        public async Task<List<hotel>> SearchHotelsByCity(string cityName)
        {
            var filter = Builders<hotel>.Filter.Eq(h => h.city, cityName);
            return await _context.hotel.Find(filter).ToListAsync();
        }

    }
}
