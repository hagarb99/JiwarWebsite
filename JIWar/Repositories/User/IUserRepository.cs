using GEWAR.Models;

namespace Jiwar.Repositories
{

    public interface IUserRepository
    {
        Task<int> GetTotalUsersAsync();
        Task<int> GetNewUsersTodayAsync();
        Task<int> GetNewUsersThisWeekAsync();
        Task<int> GetNewUsersThisMonthAsync();
        Task<int> GetActiveUsersAsync();

  

        Task<Dictionary<string, int>> GetUsersCountByRoleAsync();

        Task<List<User>> GetAllUsersForAdminAsync();
        Task DeleteUserAsync(string userId);
    }


}
