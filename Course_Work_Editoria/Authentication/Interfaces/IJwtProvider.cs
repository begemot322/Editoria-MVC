using Editoria.Models.Entities;

namespace Course_Work_Editoria.Authentication.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
