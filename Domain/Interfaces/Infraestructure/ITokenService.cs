namespace Domain.Abstations.Infraestructure
{
    public interface ITokenService
    {
       string GenerateToken(string user);
    }
}
