using System;
using System.Collections.Generic;
using System.Data;
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
    public class AlbumRepository : IAlbumRepository
    {
        private readonly DbContext context;

        public AlbumRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
        }

        public IEnumerable<DalAlbum> GetAll()
        {
            throw new NotImplementedException();
        }

        public DalAlbum Get(int key)
        {
            key.ArgumentisNull();
            try
            {
                var getalbum = context.Set<Album>().FirstOrDefault(album => album.Id == key);
                return getalbum.ToDllAlbum();
            }
            catch (Exception ex)
            {
                throw new DataException("Error data access", ex);
            }
        }

        public void Create(DalAlbum entity)
        {
            entity.ArgumentisNull();
            try
            {
                var album = entity.ToAlbum();
                context.Set<Album>().Add(album);
            }
            catch (Exception ex)
            {
                throw new DataException("Error data access", ex);
            }
        }

        public void Update(DalAlbum entity)
        {
            entity.ArgumentisNull();
            try
            {
                var oldEntity = context.Set<Album>().Find(entity.Id);
                if (oldEntity != null)
                {
                    var contextOldEntry = context.Entry(oldEntity);
                    contextOldEntry.CurrentValues.SetValues(new Album()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Description = entity.Description,
                        UserId = entity.UserId,
                        CreatedDate = entity.CreatedDate
                    });
                    contextOldEntry.State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
                throw new DataException("Error data access", ex);
            }
        }

        public void Delete(int key)
        {
            key.ArgumentisNull();
            try
            {
                var entity = context.Set<Album>().FirstOrDefault(album => album.Id == key);
                if (entity != null)
                {
                    context.Set<Album>().Remove(entity);
                }
            }
            catch (Exception ex)
            {
                throw new DataException("Error data access", ex);
            }
        }

        public IEnumerable<DalAlbum> GetAllUserAlbums(int id)
        {
            id.ArgumentisNull();
            try
            {
                var albums = context.Set<Album>().Where(album => album.UserId == id).Select(album => album).ToList();
                if (albums.Count == 0) return null;
                return albums.ToList().Select(alb => alb.ToDllAlbum());
            }
            catch (Exception ex)
            {
                throw new DataException("Error data access", ex);
            }
        }

        public DalAlbum GetByName(string name)
        {
            name.ArgumentisNull();
            try
            {
                var getalbum = context.Set<Album>().FirstOrDefault(album => album.Name.Equals(name));
                if (getalbum == null) return null;
                return getalbum.ToDllAlbum();
            }
            catch (Exception ex)
            {
                throw new DataException("Error data access", ex);
            }
        }
    }
}
