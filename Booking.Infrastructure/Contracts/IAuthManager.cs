namespace Booking.Infrastructure.Contracts
{
    public interface IAuthManager
    {
        Task<string?> LoginAsync(string email, string password);
    }
}
