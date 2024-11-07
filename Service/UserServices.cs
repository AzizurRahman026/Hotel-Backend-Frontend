using Entities;
using Hotels_Crud.DTO;
using System.Security.Cryptography;
using System.Text;
using Data;
using Entities.DTO;
using MongoDB.Bson;
using MongoDB.Driver;

public interface IUserServices
{
    Task<User> RegisterUser(RegisterDTO user);
    Task<bool> UserExist(string username);
    Task<User> Login(LoginDTO loginDto);
    Task<User> getUser(string userId);
}

namespace Service
{
    public class UserServices : IUserServices
    {
        private readonly MongoDbContext _context;

        public UserServices(MongoDbContext context)
        {
            _context = context;
        }

        // get user code...
        public async Task<User> getUser(string userId)
        {
            ObjectId id = new ObjectId(userId.ToString());
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var user = await _context.user.Find(filter).FirstOrDefaultAsync();
            return user;
        }




        // user registration code...
        public async Task<User> RegisterUser(RegisterDTO user)
        {
            using var hmac = new HMACSHA512();
            var registerUser = new User
            {
                Username = user.username,
                Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.password)),
                PasswordSalt = hmac.Key
            };

            // Save the user to the database
            await _context.user.InsertOneAsync(registerUser);

            return registerUser;
        }

        public async Task<User> Login(LoginDTO loginDto)
        {
            var user = await _context.user.Find(u => u.Username == loginDto.username).FirstOrDefaultAsync();

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

            for (int i = 0; i < computeHash.Length; ++i)
            {
                if (computeHash[i] != user.Password[i])
                {
                    return null;
                }
            }

            return user;
        }


        public async Task<bool> UserExist(string username)
        {
            var info = await _context.user.Find(u => u.Username == username).FirstOrDefaultAsync();

            if (info != null)
            {
                return true;
            }
            return false;
        }
    }
}
