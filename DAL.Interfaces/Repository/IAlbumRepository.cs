using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repository
{
    public interface IAlbumRepository:IRepository<DalAlbum>
    {
        IEnumerable<DalAlbum> GetAllUserAlbums(int id);
        DalAlbum GetByName(string name);
    }
}
