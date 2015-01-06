using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Exceptions;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;
using DAL.Mappers;
using ORM;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
        }

        public IEnumerable<DalUser> GetAll()
        {
            try
            {
                var list = context.Set<User>().ToList();
                return list.Select(user => user.ToDllUser());
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Eror data access", ex);
            }

        }

        public DalUser Get(int key)
        {
            key.ArgumentisNull();
            try
            {
                var getuser = context.Set<User>().FirstOrDefault(user => user.Id == key);
                return getuser.ToDllUser();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Eror data access", ex);
            }
        }

        public void Create(DalUser entity)
        {
            entity.ArgumentisNull();
            try
            {
                var user = entity.ToUser();
                context.Set<User>().Add(user);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Eror data access", ex);
            }
        }

        public void Update(DalUser entity)
        {
            entity.ArgumentisNull();
            try
            {
                var oldEntity = context.Set<User>().Find(entity.Id);
                if (oldEntity != null)
                {
                    var contextOldEntry = context.Entry(oldEntity);
                    contextOldEntry.CurrentValues.SetValues(new User()
                    {
                        Id = oldEntity.Id,
                        Login = entity.Login,
                        Email = entity.Email,
                        Password = oldEntity.Password,
                        CreatedDate = oldEntity.CreatedDate,
                        RoleId = entity.RoleId
                    });
                    contextOldEntry.State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Eror data access", ex);
            }
        }

        public void Delete(int key)
        {
            key.ArgumentisNull();
            try
            {
                var entity = context.Set<User>().FirstOrDefault(user => user.Id == key);
                if (entity != null)
                {
                    context.Set<User>().Remove(entity);
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Eror data access", ex);
            }

        }


        public DalUser GetByLogin(string login)
        {
            login.ArgumentisNull();
            try
            {
                var getuser = context.Set<User>().FirstOrDefault(user => user.Login.Equals(login, StringComparison.OrdinalIgnoreCase));
                if (getuser == null) return null;
                return getuser.ToDllUser();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Eror data access", ex);
            }
        }

        public DalUser GetByAlbumId(int key)
        {
            key.ArgumentisNull();
            try
            {
                var getalbum = context.Set<Album>().FirstOrDefault(album => album.Id == key);
                var getuser = context.Set<User>().FirstOrDefault(user => user.Id == getalbum.UserId);
                if (getuser == null) return null;

                return getuser.ToDllUser();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Eror data access", ex);
            }
        }

        public void CreateRole(DalRole entity)
        {
            entity.ArgumentisNull();
            try
            {
                var role = entity.ToRole();
                context.Set<Role>().Add(role);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Eror data access", ex);
            }
        }

        public DalRole GetRoleByName(string name)
        {
            name.ArgumentisNull();
            try
            {
                var getrole = context.Set<Role>().FirstOrDefault(role => role.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                return getrole.ToDllRole();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Eror data access", ex);
            }
        }


        public DalRole GetRoleById(int roleId)
        {
            roleId.ArgumentisNull();
            try
            {
                var getrole = context.Set<Role>().FirstOrDefault(role => role.Id == roleId);
                return getrole.ToDllRole();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Eror data access", ex);
            }
        }
    }
}
