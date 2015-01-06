using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IAlbumService
    {
        IEnumerable<AlbumEntity> GetAllUserAlbums(string userName);
        AlbumEntity GetAlbumById(int id);
        void CreateAlbum(AlbumEntity album);
        void EditAlbum(AlbumEntity album);
        void DeleteAlbum(int id);
        void CreatePhoto(PhotoEntity photo);
        PhotoEntity GetPhotoById(int id);
        void DeletePhoto(int id);
        void UpdatePhoto(PhotoEntity photo);
        IEnumerable<PhotoEntity> GetAllAlbumPhotos(int albumId);
        AlbumEntity GetAlbumByName(string name);
        IEnumerable<CommentEntity> GetAllPhotoComments(int id);
        void CreateComment(CommentEntity comment);
        IEnumerable<LikeEntity> GetAllLikesPhoto(int id);
        void CreateLike(LikeEntity like);
        void CreateTag(TagEntity tag);
        TagEntity GetTagById(int id);
        TagEntity GetTagByName(string name);
        void DeleteTagById(int id);
        IEnumerable<PhotoEntity> GetAllPhotosByTag(int id);
        IEnumerable<PhotoEntity> GetSomePhotos();
    }
}
