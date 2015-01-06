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
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DbContext context;

        public PhotoRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
        }

        public IEnumerable<DalPhoto> GetAll()
        {
            try
            {
                var list = context.Set<Photo>().ToList();
                return list.Select(photo => photo.ToDllPhoto());
            }
            catch (Exception ex)
            {

                throw new DataException("Error data access", ex);
            }

        }

        public DalPhoto Get(int key)
        {
            key.ArgumentisNull();
            try
            {
                var getPhoto = context.Set<Photo>().FirstOrDefault(photo => photo.Id == key);
                return getPhoto.ToDllPhoto();
            }
            catch (Exception ex)
            {

                throw new DataException("Error data access", ex);
            }
        }

        public void Create(DalPhoto entity)
        {
            entity.ArgumentisNull();
            try
            {
                var photo = entity.ToPhoto();
                context.Set<Photo>().Add(photo);
            }
            catch (Exception ex)
            {

                throw new DataException("Error data access", ex);
            }
        }

        public void Update(DalPhoto entity)
        {
            entity.ArgumentisNull();
            try
            {
                var oldEntity = context.Set<Photo>().Find(entity.Id);
                if (entity.Description == null && oldEntity.TagId != null)
                {
                    DeleteTag(oldEntity.TagId);
                }
                if (oldEntity != null)
                {
                    var contextOldEntry = context.Entry(oldEntity);
                    contextOldEntry.CurrentValues.SetValues(new Photo()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Description = entity.Description,
                        Image = entity.Image,
                        ImageType = entity.ImageType,
                        CreatedDate = entity.CreatedDate,
                        AlbumId = entity.AlbumId,
                        TagId = entity.TagId
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
                var entity = context.Set<Photo>().FirstOrDefault(photo => photo.Id == key);
                if (entity != null)
                {
                    context.Set<Photo>().Remove(entity);
                }
            }
            catch (Exception ex)
            {

                throw new DataException("Error data access", ex);
            }
        }

        public IEnumerable<DalPhoto> GetAllAlbumPhoto(int albumId)
        {
            albumId.ArgumentisNull();
            try
            {
                var photos = context.Set<Photo>().Where(photo => photo.AlbumId == albumId).ToList();
                if (photos.Count == 0) return null;
                return photos.Select(photo => photo.ToDllPhoto());
            }
            catch (Exception ex)
            {

                throw new DataException("Error data access", ex);
            }
        }


        public IEnumerable<DalLike> GetAllLikesPhoto(int key)
        {
            key.ArgumentisNull();
            try
            {
                var likes = context.Set<Like>().Where(like => like.PhotoId == key).ToList();
                if (likes.Count == 0) return null;
                return likes.Select(like => like.ToDllLike());
            }
            catch (Exception ex)
            {

                throw new DataException("Error data access", ex);
            }
        }

        public void CreateLike(DalLike entity)
        {
            entity.ArgumentisNull();
            try
            {
                var like = entity.ToLike();
                context.Set<Like>().Add(like);
            }
            catch (Exception ex)
            {

                throw new DataException("Error data access", ex);
            }

        }

        public void CreateTag(DalTag tag)
        {
            tag.ArgumentisNull();
            try
            {
                var entity = tag.ToTag();
                context.Set<Tag>().Add(entity);
            }
            catch (Exception ex)
            {

                throw new DataException("Error data access", ex);
            }
        }


        public DalTag GetTagById(int key)
        {
            key.ArgumentisNull();
            try
            {
                var getTag = context.Set<Tag>().FirstOrDefault(tag => tag.Id == key);
                return getTag.ToDllTag();
            }
            catch (Exception ex)
            {

                throw new DataException("Error data access", ex);
            }
        }


        public DalTag GetTagByName(string name)
        {
            name.ArgumentisNull();
            try
            {
                var getTag = context.Set<Tag>().FirstOrDefault(tag => tag.Name == name);
                return getTag.ToDllTag();
            }
            catch (Exception ex)
            {
                throw new DataException("Error data access", ex);
            }
        }

        public void DeleteTag(int? key)
        {
            try
            {
                var entity = context.Set<Tag>().FirstOrDefault(tag => tag.Id == key);
                if (entity != null)
                {
                    context.Set<Tag>().Remove(entity);
                }
            }
            catch (Exception ex)
            {
                throw new DataException("Error data access", ex);
            }

        }

        public IEnumerable<DalPhoto> GetAllPhotosByTag(int key)
        {
            key.ArgumentisNull();
            try
            {
                var photos = context.Set<Photo>().Where(photo => photo.TagId == key).ToList();
                if (photos.Count == 0) return null;
                return photos.Select(photo => photo.ToDllPhoto());
            }
            catch (Exception ex)
            {
                throw new DataException("Error data access", ex);
            }
        }

        public IEnumerable<DalPhoto> GetSomePhotos()
        {
            try
            {
                var photos = context.Set<Photo>().Select(photo => photo).Take(12).ToList();
                if (photos.Count == 0) return null;
                return photos.Select(photo => photo.ToDllPhoto());
            }
            catch (Exception ex)
            {
                throw new DataException("Error data access", ex);
            }
        }

        public IEnumerable<DalComment> GetAllPhotoComments(int key)
        {
            key.ArgumentisNull();
            try
            {
                var comments = context.Set<Comment>().Where(comment => comment.PhotoId == key).ToList();
                if (comments.Count == 0) return null;
                return comments.Select(comment => comment.ToDllComment());
            }
            catch (Exception ex)
            {
                throw new DataException("Error data access", ex);
            }
        }

        public void CreateComment(DalComment comment)
        {
            comment.ArgumentisNull();
            try
            {
                var newcomment = comment.ToComment();
                context.Set<Comment>().Add(newcomment);
            }
            catch (Exception ex)
            {
                throw new DataException("Error data access", ex);
            }
        }
    }
}
