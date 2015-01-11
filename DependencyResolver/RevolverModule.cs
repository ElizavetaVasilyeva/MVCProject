using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Services;
using BLL.Services;
using DAL.Concrete;
using DAL.Interfaces.Repository;
using Ninject.Modules;
using ORM;

namespace DependencyResolver
{
    public class RevolverModule:NinjectModule
    {

        public override void Load()
        {
            Bind<DbContext>().To<SiteModel>().InThreadScope();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IProfileRepository>().To<ProfileRepository>();
            Bind<IAlbumRepository>().To<AlbumRepository>();
            Bind<IPhotoRepository>().To<PhotoRepository>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IUserService>().To<UserService>();
            Bind<IAlbumService>().To<AlbumService>();
        }
    }
}
