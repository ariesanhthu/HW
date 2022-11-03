namespace HW.Services
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}
