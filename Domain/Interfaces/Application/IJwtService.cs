namespace Domain.Interfaces.Application
{
    public interface IJwtService
    {
        string GenerateToken(string user);
    }
}
