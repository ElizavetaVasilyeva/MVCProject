using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using Site.Models;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAlbumService albumService;
        public HomeController(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            IEnumerable<PhotoEntity> photos;
            try
            {
                photos = albumService.GetSomePhotos();
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
            if (photos != null)
            {
                var modelPhotos = photos.Select(photo => new PhotoViewModel()
                {
                    Id = photo.Id,
                    Name = photo.Name,
                    Description = photo.Description,
                    Image = "data:image/png;base64," + Convert.ToBase64String(photo.Image.ToArray()),
                    ImageType = photo.ImageType,
                    CreatedDate = photo.CreatedDate
                });
                return View(modelPhotos);
            }
            return View();
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string textsearch)
        {
            IEnumerable<PhotoViewModel> modelPhotos;
            IEnumerable<PhotoEntity> photos;
            int tagId;
            try
            {
                tagId = albumService.GetTagByName(textsearch).Id;
                photos = albumService.GetAllPhotosByTag(tagId);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
          
            if (photos != null)
            {
                modelPhotos = photos.Select(photo => new PhotoViewModel()
                {
                    Id = photo.Id,
                    Name = photo.Name,
                    Description = photo.Description,
                    Image = "data:image/png;base64," + Convert.ToBase64String(photo.Image.ToArray()),
                    ImageType = photo.ImageType,
                    CreatedDate = photo.CreatedDate
                });
                if (Request.IsAjaxRequest())
                {
                    return new JsonResult()
                    {
                        Data = modelPhotos,
                    };
                }
                return View(modelPhotos);
            }
            return View();
            }
    }
}
