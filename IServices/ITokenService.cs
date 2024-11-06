using System.Reflection.Metadata;

namespace IServices
{
    public interface ITokenService
    {
        string CreateToken(UserStringHandle user);
    }
}
