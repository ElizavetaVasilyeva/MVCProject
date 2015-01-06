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
    public class ProfileRepository : IProfileRepository
    {
        private readonly DbContext context;

        public ProfileRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
        }

        public IEnumerable<DalProfile> GetAll()
        {
            throw new NotImplementedException();
        }

        public DalProfile Get(int userId)
        {
            userId.ArgumentisNull();
            try
            {
                var getprofile = context.Set<Profile>().FirstOrDefault(profile => profile.UserId == userId);
                if (getprofile == null) return null;
                return getprofile.ToDllProfile();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Eror data access", ex);
            }
        }

        public void Create(DalProfile entity)
        {
            entity.ArgumentisNull();
            try
            {
                var profile = entity.ToProfile();
                context.Set<Profile>().Add(profile);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Eror data access", ex);
            }
        }

        public void Update(DalProfile entity)
        {
            entity.ArgumentisNull();
            try
            {
                var oldEntity = context.Set<Profile>().FirstOrDefault(profile => profile.UserId == entity.UserId);
                if (oldEntity != null)
                {
                    var contextOldEntry = context.Entry(oldEntity);
                    contextOldEntry.CurrentValues.SetValues(new Profile()
                    {
                        Id = oldEntity.Id,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Age = entity.Age,
                        Email = entity.Email,
                        Country = entity.Country,
                        City = entity.City,
                        PhoneNumber = entity.PhoneNumber,
                        AboutYourself = entity.AboutYourself,
                        Interests = entity.Interests,
                        LastUpdateDate = entity.LastUpdateDate,
                        UserId = oldEntity.UserId
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
            throw new NotImplementedException();
        }
    }
}
