using System.Reflection.Metadata;
using Entities;

namespace IServices
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
