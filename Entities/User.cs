using MongoDB.Bson;

namespace Entities
{
    public class User
    {
        public ObjectId Id { get; set; } // MongoDB generates Id auto...
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
