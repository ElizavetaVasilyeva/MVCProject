using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IUserService
    {
        UserEntity CreateUser(UserEntity entity);
        UserEntity GetUserById(int id);
        UserEntity GetUserByLogin(string login);
        UserEntity GetUserByAlbumId(int id);
        bool UserExistsByLogin(string login);
        IEnumerable<UserEntity> GetAllUsers();
        void DeleteUser(int id);
        void UpdateUser(UserEntity user);
        RoleEntity GetRoleByName(string name);
        RoleEntity GetRoleById(int id);
        void CreateRole(RoleEntity role);
        ProfileEntity GetProfileUser(int id);
        void UpdateProfile(ProfileEntity entity);
        void CreateProfile(ProfileEntity entity);
    }
}
