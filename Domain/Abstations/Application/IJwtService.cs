namespace Domain.Abstations.Application
{
    public interface IJwtService
    {
        string GenerateToken(string user);
    }
}
