using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Exseptions;
using BLL.Interfaces.Services;
using Microsoft.Ajax.Utilities;
using Ninject;
using Site.Models;

namespace Site.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly IUserService userService;

        public GalleryController(IAlbumService albumService, IUserService userService)
        {
            this.albumService = albumService;
            this.userService = userService;
        }

        public ActionResult Index(string name)
        {
            IEnumerable<AlbumEntity> albums;
            try
            {
                albums = albumService.GetAllUserAlbums(name);
            }
            catch (AlbumsNotFoundException)
            {
                ViewBag.Login = name;
                return View();
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400,"Bad request");
            }
            if (albums != null)
            {
                var modelAlbums = albums.Select(album => new AlbumViewModel()
                {
                    Id = album.Id,
                    Name = album.Name,
                    Description = album.Description,
                    CreatedDate = album.CreatedDate
                });
                ViewBag.Login = name;
                return View(modelAlbums);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            AlbumEntity album;
            try
            {
                album = albumService.GetAlbumById(id);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
            AlbumViewModel model = new AlbumViewModel()
            {
                Id = album.Id,
                Name = album.Name,
                Description = album.Description,
                CreatedDate = album.CreatedDate
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AlbumViewModel album)
        {
            AlbumEntity editAlbum = new AlbumEntity()
            {
                Id = album.Id,
                Name = album.Name,
                Description = album.Description,
                UserId = userService.GetUserByLogin(System.Web.HttpContext.Current.User.Identity.Name).Id,
                CreatedDate = DateTime.Now
            };
            try
            {
                albumService.EditAlbum(editAlbum);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
            return RedirectToAction("Index", "Gallery", new { name = System.Web.HttpContext.Current.User.Identity.Name });
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AlbumViewModel album)
        {
            AlbumEntity newAlbum = new AlbumEntity()
            {
                Name = album.Name,
                Description = album.Description,
                UserId = userService.GetUserByLogin(System.Web.HttpContext.Current.User.Identity.Name).Id,
                CreatedDate = DateTime.Now
            };
            try
            {
                albumService.CreateAlbum(newAlbum);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
          
            return RedirectToAction("Index", "Gallery", new { name = System.Web.HttpContext.Current.User.Identity.Name });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                albumService.DeleteAlbum(id);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
          
            return RedirectToAction("Index", "Gallery", new { name = System.Web.HttpContext.Current.User.Identity.Name });
        }

        [HttpGet]
        public ActionResult Album(string name)
        {
            AlbumEntity album;
            IEnumerable<PhotoEntity> photos;
            try
            {
                album = albumService.GetAlbumByName(name);
                TempData["Name"] = name;
                TempData["Description"] = album.Description;
                ViewBag.Login = userService.GetUserById(album.UserId).Login;
                photos = albumService.GetAllAlbumPhotos(album.Id);
            }
            catch (PhotoNotFoundException)
            {
                return View();
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

        public ActionResult AddPhoto(string album, HttpPostedFileBase image)
        {
            PhotoEntity photo = new PhotoEntity();
            AlbumEntity alb;
            try
            {
                alb = albumService.GetAlbumByName(album);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
            if (image != null)
            {
                photo.Image = new byte[image.ContentLength];
                image.InputStream.Read(photo.Image, 0, image.ContentLength);
                photo.Name = image.FileName;
                photo.ImageType = image.ContentType;
                photo.CreatedDate = DateTime.Now;
                photo.AlbumId = alb.Id;
                albumService.CreatePhoto(photo);
            }

            return RedirectToAction("Album", new { alb.Name });
        }


        [HttpGet]
        public ActionResult ShowPhoto(int id)
        {
            PhotoEntity photo=null;
            IEnumerable<CommentEntity> comments=null;
            CommentModel modelcomment;

            try
            {
                photo = albumService.GetPhotoById(id);
                comments = albumService.GetAllPhotoComments(id);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
                if (comments != null)
                {
                    var commentsModel = comments.Select(comment => new CommentViewModel()
                    {
                        Id = comment.Id,
                        Text = comment.Text,
                        CreatedDate = comment.CreatedDate,
                        UserName = userService.GetUserById(comment.UserId).Login
                    });
                    ViewBag.Comments = commentsModel;
                }
                IEnumerable<LikeEntity> likes;
                try
                {
                    likes = albumService.GetAllLikesPhoto(id);
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(400, "Bad request");
                }
                if (likes != null)
                {
                    var likesModel = likes.Select(like => new LikeViewModel()
                    {
                        Id = like.Id,
                        UserName = userService.GetUserById(like.UserId).Login
                    });
                    ViewBag.Likes = likesModel;
                }

                modelcomment = new CommentModel()
                {
                    IdPhoto = photo.Id,
                    Description = photo.Description,
                    Image = "data:image/png;base64," + Convert.ToBase64String(photo.Image.ToArray()),
                    CreatedDatePhoto = photo.CreatedDate,
                    PhotoUser = userService.GetUserByAlbumId(photo.AlbumId).Login,
                    CurrentUserName = System.Web.HttpContext.Current.User.Identity.Name
                };
            return View(modelcomment);
        }

        [HttpPost]
        public ActionResult ShowPhoto(CommentModel comment)
        {
            var newComment = new CommentEntity()
            {
                Text = comment.Text,
                UserId = userService.GetUserByLogin(System.Web.HttpContext.Current.User.Identity.Name).Id,
                PhotoId = comment.IdPhoto,
                CreatedDate = DateTime.Now
            };
            try
            {
                albumService.CreateComment(newComment);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");  
            }
            if (Request.IsAjaxRequest())
            {
                return new JsonResult()
                {
                    Data = comment,
                };
            }
            return RedirectToAction("ShowPhoto", "Gallery", new { id = comment.IdPhoto });
        }

        public ActionResult Like(CommentModel comment)
        {
            var like = new LikeEntity()
            {
                UserId = userService.GetUserByLogin(comment.CurrentUserName).Id,
                PhotoId = comment.IdPhoto,
                SetDate = DateTime.Now
            };
            try
            {
                albumService.CreateLike(like);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
            if (Request.IsAjaxRequest())
            {
                return new JsonResult()
                {
                    Data = comment,
                };
            }
            return RedirectToAction("ShowPhoto", "Gallery", new { id = comment.IdPhoto });
        }
        [HttpGet]
        public ActionResult EditPhoto(int id)
        {
            PhotoEntity photo;
            try
            {
                photo = albumService.GetPhotoById(id);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
            var modelPhoto = new PhotoViewModel()
            {
                Id = photo.Id,
                Name = photo.Name,
                Description = photo.Description,
                Image = "data:image/png;base64," + Convert.ToBase64String(photo.Image.ToArray()),
                ImageType = photo.ImageType,
                CreatedDate = photo.CreatedDate
            };
            return View(modelPhoto);
        }

        [HttpPost]
        public ActionResult EditPhoto(PhotoViewModel photo)
        {
            PhotoEntity editPhoto;
            try
            {
                editPhoto = albumService.GetPhotoById(photo.Id);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
            PhotoEntity modelPhoto = new PhotoEntity()
            {
                Id = editPhoto.Id,
                Name = photo.Name,
                Description = photo.Description,
                Image = editPhoto.Image,
                ImageType = editPhoto.ImageType,
                CreatedDate = editPhoto.CreatedDate,
                AlbumId = editPhoto.AlbumId,

            };
            if (photo.Description != null)
            {
                int poz1 = photo.Description.IndexOf('#');


                if (poz1 != -1)
                {
                    int poz2 = photo.Description.IndexOf(' ', poz1);
                    if(poz2!=-1) poz2-=poz1;
                    if (poz2 == -1)
                    {
                        poz2 = photo.Description.Length - poz1;
                    }
                    string textTag = photo.Description.Substring(poz1 + 1, poz2 - 1);
                    TagEntity newtag = new TagEntity()
                    {
                        Name = textTag
                    };
                    TagEntity tag;
                    try
                    {
                        albumService.CreateTag(newtag);
                        tag = albumService.GetTagByName(textTag);
                    }
                    catch (Exception)
                    {
                        return new HttpStatusCodeResult(400, "Bad request");
                    }
                   
                    modelPhoto.TagId = tag.Id;
                }
            }
            try
            {
                albumService.UpdatePhoto(modelPhoto);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
            return RedirectToAction("Album", "Gallery", new { name = albumService.GetAlbumById(editPhoto.AlbumId).Name });
        }

        public ActionResult DeletePhoto(int id)
        {
            PhotoEntity editPhoto;
            string nameAlbum;
            try
            {
                editPhoto = albumService.GetPhotoById(id);
                nameAlbum = albumService.GetAlbumById(editPhoto.AlbumId).Name;
                albumService.DeletePhoto(id);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
            return RedirectToAction("Album", "Gallery", new { name = nameAlbum });
        }
    }
}
