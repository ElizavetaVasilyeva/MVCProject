using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repository
{
    public interface IPhotoRepository:IRepository<DalPhoto>
    {
        IEnumerable<DalComment> GetAllPhotoComments(int key);
        void CreateComment(DalComment comment);
        IEnumerable<DalPhoto> GetAllAlbumPhoto(int albumId);
        IEnumerable<DalLike> GetAllLikesPhoto(int key);
        void CreateLike(DalLike like);
        void CreateTag(DalTag tag);
        DalTag GetTagById(int key);
        DalTag GetTagByName(string name);
        void DeleteTag(int? key);
        IEnumerable<DalPhoto> GetAllPhotosByTag(int key);
        IEnumerable<DalPhoto> GetSomePhotos();
    }
}
