using TowerDefenseAPI.Domain.Models;
using TowerDefenseAPI.Domain.Response;

namespace TowerDefenseAPI.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<IEnumerable<UserViewModel>>> GetLeaderboard();

        Task<IBaseResponse<IEnumerable<User>>> GetAll();

        Task<IBaseResponse<User>> Get(string id);

        Task<IBaseResponse<bool>> Create(User entity);

        Task<IBaseResponse<bool>> Delete(string id);

        Task<IBaseResponse<User>> Update(User entity);

    }
}
