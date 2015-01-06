using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class DllEntityMapper
    {
        public static DalUser ToDllUser(this User user)
        {
            return new DalUser()
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                CreatedDate = user.CreatedDate,
                RoleId = user.RoleId
            };
        }

        public static User ToUser(this DalUser dalUser)
        {
            return new User()
            {
                Id = dalUser.Id,
                Login = dalUser.Login,
                Email = dalUser.Email,
                Password = dalUser.Password,
                CreatedDate = dalUser.CreatedDate,
                RoleId = dalUser.RoleId
            };
        }

        public static DalRole ToDllRole(this Role role)
        {
            return new DalRole()
            {
                Id = role.Id,
                Name = role.Name
            };
        }
        public static Role ToRole(this DalRole role)
        {
            return new Role()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static Profile ToProfile(this DalProfile profile)
        {
            return new Profile()
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Age,
                Email = profile.Email,
                Country = profile.Country,
                City = profile.City,
                PhoneNumber = profile.PhoneNumber,
                AboutYourself = profile.AboutYourself,
                Interests = profile.Interests,
                LastUpdateDate = profile.LastUpdateDate,
                UserId = profile.UserId
            };
        }

        public static DalProfile ToDllProfile(this Profile profile)
        {
            return new DalProfile()
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Age,
                Email = profile.Email,
                Country = profile.Country,
                City = profile.City,
                PhoneNumber = profile.PhoneNumber,
                AboutYourself = profile.AboutYourself,
                Interests = profile.Interests,
                LastUpdateDate = profile.LastUpdateDate,
                UserId = profile.UserId
            };
        }

        public static DalAlbum ToDllAlbum(this Album album)
        {
            return new DalAlbum()
            {
                Id = album.Id,
                Name = album.Name,
                Description = album.Description,
                UserId = album.UserId,
                CreatedDate = album.CreatedDate
            };
        }

        public static Album ToAlbum(this DalAlbum album)
        {
            return new Album()
            {
                Id = album.Id,
                Name = album.Name,
                Description = album.Description,
                UserId = album.UserId,
                CreatedDate = album.CreatedDate
            };
        }

        public static Photo ToPhoto(this DalPhoto photo)
        {
            return new Photo()
            {
                Id = photo.Id,
                Name = photo.Name,
                Description = photo.Description,
                Image = photo.Image,
                ImageType = photo.ImageType,
                CreatedDate = photo.CreatedDate,
                AlbumId = photo.AlbumId,
                TagId = photo.TagId
            };
        }

        public static DalPhoto ToDllPhoto(this Photo photo)
        {
            return new DalPhoto()
            {
                Id = photo.Id,
                Name = photo.Name,
                Description = photo.Description,
                Image = photo.Image,
                ImageType = photo.ImageType,
                CreatedDate = photo.CreatedDate,
                AlbumId = photo.AlbumId,
                TagId = photo.TagId
            };
        }

        public static DalLike ToDllLike(this Like like)
        {
            return new DalLike()
            {
                Id = like.Id,
                SetDate = like.SetDate,
                UserId = like.UserId,
                PhotoId = like.PhotoId
            };
        }

        public static Like ToLike(this DalLike like)
        {
            return new Like()
            {
                Id = like.Id,
                SetDate = like.SetDate,
                UserId = like.UserId,
                PhotoId = like.PhotoId
            };
        }

        public static Tag ToTag(this DalTag tag)
        {
            return  new Tag()
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public static DalTag ToDllTag(this Tag tag)
        {
            return new DalTag()
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public static DalComment ToDllComment(this Comment comment)
        {
            return new DalComment()
            {
                Id = comment.Id,
                Text = comment.Text,
                UserId = comment.UserId,
                PhotoId = comment.PhotoId,
                CreatedDate = comment.CreatedDate
            };
        }

        public static Comment ToComment(this DalComment comment)
        {
            return new Comment()
            {
                Id = comment.Id,
                Text = comment.Text,
                UserId = comment.UserId,
                PhotoId = comment.PhotoId,
                CreatedDate = comment.CreatedDate
            };
        }
    }
}
