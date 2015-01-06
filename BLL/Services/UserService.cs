using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Exseptions;
using BLL.Interfaces.Services;
using BLL.Mappers;
using DAL.Exceptions;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;
        private readonly IProfileRepository profileRepository;

        public UserService(IUnitOfWork uow, IUserRepository userRepository, IProfileRepository profileRepository)
        {
            this.uow = uow;
            this.userRepository = userRepository;
            this.profileRepository = profileRepository;
        }

        public IEnumerable<UserEntity> GetAllUsers()
        {
            try
            {
                return userRepository.GetAll().Select(user => user.ToBllUser());
            }
            catch (Exception ex)
            {

                throw new UserServiceException(ex);
            }
        }

        public UserEntity CreateUser(UserEntity user)
        {
            user.ArgumentisNull();
            try
            {
                if (GetUserByLogin(user.Login) != null)
                {
                    throw new DuplicateLoginException() {Login = user.Login};
                } 
            }
            catch (UserNotFoundException)
            {
                userRepository.Create(user.ToDalUser());
                uow.Commit();
            }
            catch (Exception ex)
            {
                throw new UserServiceException(ex);
            }

            return user;
        }


        public void DeleteUser(int id)
        {
            id.ArgumentisNull();
            DalUser user;
            try
            {
                user = userRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new DataException("Error data access", ex);
            }

            if (user == null)
            {
                throw new UserNotFoundException() { Id = id };
            }
            userRepository.Delete(id);
            uow.Commit();
        }

        public UserEntity GetUserByLogin(string login)
        {
            login.ArgumentisNull();
            DalUser getUser;
            try
            {
                getUser = userRepository.GetByLogin(login);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error data access",ex);
            }
            if (getUser == null)
            {
                throw new UserNotFoundException();
            }
            else
            {
                return getUser.ToBllUser();
            }
        }

        public UserEntity GetUserById(int id)
        {
            UserEntity user;
            try
            {
                user = userRepository.Get(id).ToBllUser();
            }
            catch (Exception ex)
            {
                throw new UserServiceException(ex);
            }
            if (user == null)
            {
                throw new UserNotFoundException(){Id=id};
            }
            return user;
        }

        public UserEntity GetUserByAlbumId(int id)
        {
            id.ArgumentisNull();
            try
            {
                return userRepository.GetByAlbumId(id).ToBllUser();
            }
            catch (Exception ex)
            {
                throw new UserServiceException(ex);
            }
            
        }

        public void UpdateUser(UserEntity user)
        {
            user.ArgumentisNull();
            try
            {
                userRepository.Update(user.ToDalUser());
                uow.Commit();
            }
            catch (Exception ex)
            {
                throw new UserServiceException(ex);
            }
        }

        public bool UserExistsByLogin(string login)
        {
            login.ArgumentisNull();
            DalUser user;
            try
            {
                user = userRepository.GetByLogin(login);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error data access", ex);
            }
           
            if (user == null)
                return false;
            return true;
        }

        public RoleEntity GetRoleByName(string name)
        {
            name.ArgumentisNull();
            try
            {
                return userRepository.GetRoleByName(name).ToRoleEntity();
            }
            catch (Exception ex )
            {
                throw new UserServiceException(ex);
            }  
        }

        public RoleEntity GetRoleById(int id)
        {
            id.ArgumentisNull();
            try
            {
                return userRepository.GetRoleById(id).ToRoleEntity();
            }
            catch (Exception ex)
            {
                
                throw new UserServiceException(ex);
            }
        }

        public void CreateRole(RoleEntity role)
        {
            role.ArgumentisNull();
            try
            {
                userRepository.CreateRole(role.ToDalRole());
                uow.Commit();
            }
            catch (Exception ex)
            {

                throw new DatabaseException("Error data access", ex);
            }
           
        }

        public ProfileEntity GetProfileUser(int id)
        {
            id.ArgumentisNull();
            DalProfile profile;
            try
            {
                profile = profileRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error data access", ex);
            }
            if (profile == null) throw new ProfileNotFoundException();
            else
            {
                return profile.ToBllProfile();
            }
        }

        public void UpdateProfile(ProfileEntity entity)
        {
            entity.ArgumentisNull();
            try
            {
                profileRepository.Update(entity.ToDalProfile());
                uow.Commit();
            }
            catch (Exception ex)
            {
                throw new UserServiceException(ex);
            }
          
        }


        public void CreateProfile(ProfileEntity entity)
        {
            entity.ArgumentisNull();
            try
            {
                profileRepository.Create(entity.ToDalProfile());
                uow.Commit();
            }
            catch (Exception ex)
            {
                throw new UserServiceException();
            }
        }
    }
}
