using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Exseptions;
using BLL.Interfaces.Services;
using BLL.Mappers;
using DAL.Exceptions;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;

namespace BLL.Services
{
    public class AlbumService:IAlbumService
    {
        private readonly IUnitOfWork uow;
        private readonly IAlbumRepository albumRepository;
        private readonly IUserRepository userRepository;
        private readonly IPhotoRepository photoRepository;

        public AlbumService(IUnitOfWork uow, IAlbumRepository albumRepository, IUserRepository userRepository, IPhotoRepository photoRepository)
        {
            this.uow = uow;
            this.albumRepository = albumRepository;
            this.userRepository = userRepository;
            this.photoRepository = photoRepository;
        }
        public IEnumerable<AlbumEntity> GetAllUserAlbums(string userName)
        {
            userName.ArgumentisNull();
            var user = userRepository.GetByLogin(userName);
            if (user==null) throw new UserNotFoundException();
            var albums=albumRepository.GetAllUserAlbums(user.Id);
            if (albums == null) throw new AlbumsNotFoundException();
            try
            {
                return albums.Select(album => album.ToBllAlbum());
            }
            catch (Exception ex)
            {
                
                throw new AlbumServiceException(ex);
            }    
        }

        public AlbumEntity GetAlbumById(int id)
        {
            id.ArgumentisNull();
            AlbumEntity album;
            try
            {
                album = albumRepository.Get(id).ToBllAlbum();
            }
            catch (Exception ex)
            {
                throw new AlbumServiceException(ex);
            }
            if (album == null)
            {
                throw new AlbumsNotFoundException();
            }
            return album;
        }

        public void CreateAlbum(AlbumEntity album)
        {
            album.ArgumentisNull();
            try
            {
                albumRepository.Create(album.ToDalAlbum());
                uow.Commit();
            }
            catch (Exception ex)
            {
                throw new AlbumServiceException(ex);
            }
           
        }

        public void EditAlbum(AlbumEntity album)
        {
            album.ArgumentisNull();
            try
            {
                albumRepository.Update(album.ToDalAlbum());
                uow.Commit();
            }
            catch (Exception ex)
            {
                throw new AlbumServiceException(ex);
            }
        }

        public void DeleteAlbum(int id)
        {
            id.ArgumentisNull();
            DalAlbum album;
            try
            {
                album = albumRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error data access",ex);
            }

            if (album == null)
            {
                throw new AlbumsNotFoundException();
            }
            albumRepository.Delete(id);
            uow.Commit();
        }

        public void CreatePhoto(PhotoEntity photo)
        {
            photo.ArgumentisNull();
            try
            {
                photoRepository.Create(photo.ToDalPhoto());
                uow.Commit();
            }
            catch (Exception ex)
            {
                throw new AlbumServiceException(ex);
            }  
        }

        public PhotoEntity GetPhotoById(int id)
        {
            id.ArgumentisNull();
            PhotoEntity photo;
            try
            {
                photo = photoRepository.Get(id).ToPhotoEntity();
            }
            catch (Exception ex)
            {
                throw new AlbumServiceException(ex);
            }
            if (photo == null) throw new PhotoNotFoundException();
            return photo;
        }

        public void DeletePhoto(int id)
        {
            id.ArgumentisNull();
            DalPhoto photo;
            try
            {
                photo = photoRepository.Get(id);
            }
            catch (Exception ex)
            { 
                throw new DatabaseException("Error data access",ex);
            }

            if (photo == null)
            {
                throw new PhotoNotFoundException();
            }
            photoRepository.Delete(id);
            uow.Commit();
        }

        public void UpdatePhoto(PhotoEntity photo)
        {
            photo.ArgumentisNull();
            try
            {
                photoRepository.Update(photo.ToDalPhoto());
                uow.Commit();
            }
            catch (Exception ex)
            {
                throw new AlbumServiceException(ex);
            }
        }

        public AlbumEntity GetAlbumByName(string name)
        {
            name.ArgumentisNull();
            DalAlbum album;
            try
            {
                album = albumRepository.GetByName(name);
            }
            catch (Exception ex)
            {
                
                throw new DatabaseException("Error data access",ex);
            }
            if (album == null) throw new AlbumsNotFoundException();
            return album.ToBllAlbum();
        }

        public IEnumerable<PhotoEntity> GetAllAlbumPhotos(int albumId)
        {
            albumId.ArgumentisNull();
            IEnumerable<DalPhoto> photos;
            try
            {
                photos = photoRepository.GetAllAlbumPhoto(albumId);
            }
            catch (Exception ex)
            {

                throw new DatabaseException("Error data access", ex);
            }
            if (photos == null) throw new PhotoNotFoundException();
            return photos.Select(photo => photo.ToPhotoEntity());
        }


        public IEnumerable<CommentEntity> GetAllPhotoComments(int id)
        {
            id.ArgumentisNull();
            IEnumerable<DalComment> comments;
            try
            {
                comments = photoRepository.GetAllPhotoComments(id);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error data access", ex);
            }
            if (comments == null) return null;
            return comments.Select(comment => comment.ToCommentEntity());
        }

        public void CreateComment(CommentEntity comment)
        {
            comment.ArgumentisNull();
            try
            {
                photoRepository.CreateComment(comment.ToDalComment());
                uow.Commit();
            }
            catch (Exception ex)
            {
                throw new AlbumServiceException(ex);
            }
        }


        public IEnumerable<LikeEntity> GetAllLikesPhoto(int id)
        {
            id.ArgumentisNull();
            IEnumerable<DalLike> likes;
            try
            {
                likes = photoRepository.GetAllLikesPhoto(id);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error data access", ex);
            }
            if (likes == null) return null;
            return likes.Select(like => like.ToLikeEntity());
        }

        public void CreateLike(LikeEntity like)
        {
            like.ArgumentisNull();
            try
            {
                photoRepository.CreateLike(like.ToDalLike());
                uow.Commit();
            }
            catch (Exception ex)
            {
                throw new AlbumServiceException(ex);
            }
        }

        public void CreateTag(TagEntity tag)
        {
            tag.ArgumentisNull();
            try
            {
                photoRepository.CreateTag(tag.ToDalTag());
                uow.Commit();
            }
            catch (Exception ex)
            {
                
                throw new AlbumServiceException(ex);
            }
        }

        public TagEntity GetTagById(int id)
        {
            id.ArgumentisNull();
            TagEntity tag;
            try
            {
                tag = photoRepository.GetTagById(id).ToTagEntity();
            }
            catch (Exception ex)
            {
                throw new AlbumServiceException(ex);
            }
            if (tag == null) throw new TagNotFoundException();
            return tag;
        }

        public TagEntity GetTagByName(string name)
        {
            name.ArgumentisNull();
            TagEntity tag;
            try
            {
                tag = photoRepository.GetTagByName(name).ToTagEntity();
            }
            catch (Exception ex)
            {
                throw new AlbumServiceException(ex);
            }
            if (tag == null) throw new TagNotFoundException();
            return tag;
        }

       public  void DeleteTagById(int id)
        {
           id.ArgumentisNull();
            DalTag tag;
           try
           {
               tag = photoRepository.GetTagById(id);
           }
           catch (TagNotFoundException)
           {
               throw new TagNotFoundException();
           }
           catch (Exception ex)
           {
               
               throw new DatabaseException("Error data access",ex);
           }
            if (tag == null)
            {
                throw new TagNotFoundException();
            }
            photoRepository.DeleteTag(id);
            uow.Commit();
        }


       public IEnumerable<PhotoEntity> GetAllPhotosByTag(int id)
       {
           id.ArgumentisNull();
           IEnumerable<DalPhoto> photos;
           try
           {
               photos = photoRepository.GetAllPhotosByTag(id);
           }
           catch (Exception ex)
           {
               throw new DatabaseException("Error data access",ex);
           }
          
           if (photos == null) throw new PhotoNotFoundException();
           return photos.Select(photo => photo.ToPhotoEntity());
       }

        public IEnumerable<PhotoEntity> GetSomePhotos()
        {
            IEnumerable<DalPhoto> photos;
            try
            {
                photos = photoRepository.GetSomePhotos();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error data access",ex);
            }
            if (photos == null) throw new PhotoNotFoundException();
            return photos.Select(photo => photo.ToPhotoEntity());
        }
    }
}
