namespace Course_Work_Editoria.Authentication.Interfaces
{
    public interface IPasswordHasher
    {
        string Generate(string password);
        bool Verify(string password, string HashPassword);
    }
}
