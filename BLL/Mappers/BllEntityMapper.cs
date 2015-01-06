using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Interfaces.Entities;
using DAL.Interfaces.DTO;

namespace BLL.Mappers
{
    public static class BllEntityMapper
    {
        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            return new UserEntity()
            {
                Id=dalUser.Id,
                Login = dalUser.Login,
                Email = dalUser.Email,
                Password = dalUser.Password,
                CreatedDate = dalUser.CreatedDate,
                RoleId = dalUser.RoleId
            };
        }

        public static DalUser ToDalUser(this UserEntity user)
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

        public static DalRole ToDalRole(this RoleEntity role)
        {
            return new DalRole()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static RoleEntity ToRoleEntity(this DalRole dalrole)
        {
            return new RoleEntity()
            {
                Id=dalrole.Id,
                Name = dalrole.Name
            };
        }

        public static ProfileEntity ToBllProfile(this DalProfile profile)
        {
            return new ProfileEntity()
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
                UserId=profile.UserId
            };
        }

        public static DalProfile ToDalProfile(this ProfileEntity profile)
        {
            return new DalProfile()
            {
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

        public static AlbumEntity ToBllAlbum(this DalAlbum album)
        {
            return new AlbumEntity()
            {
                Id = album.Id,
                Name = album.Name,
                Description = album.Description,
                UserId = album.UserId,
                CreatedDate = album.CreatedDate
            };
        }

        public static DalAlbum ToDalAlbum(this AlbumEntity album)
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

        public static DalPhoto ToDalPhoto(this PhotoEntity photo)
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

        public static PhotoEntity ToPhotoEntity(this DalPhoto photo)
        {
            return new PhotoEntity()
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

        public static CommentEntity ToCommentEntity(this DalComment comment)
        {
            return new CommentEntity()
            {
                Id = comment.Id,
                Text = comment.Text,
                UserId = comment.UserId,
                PhotoId = comment.PhotoId,
                CreatedDate = comment.CreatedDate
            };
        }
        public static DalComment ToDalComment(this CommentEntity comment)
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

        public static DalLike ToDalLike(this LikeEntity like)
        {
            return new DalLike()
            {
                Id = like.Id,
                SetDate = like.SetDate,
                UserId = like.UserId,
                PhotoId = like.PhotoId
            };
        }

        public static LikeEntity ToLikeEntity(this DalLike like)
        {
            return new LikeEntity()
            {
                Id = like.Id,
                SetDate = like.SetDate,
                UserId = like.UserId,
                PhotoId = like.PhotoId
            };
        }

        public static DalTag ToDalTag(this TagEntity tag)
        {
            return new DalTag()
            {
                Id = tag.Id,
                Name = tag.Name,
            };
        }

        public static TagEntity ToTagEntity(this DalTag tag)
        {
            return new TagEntity()
            {
                Id = tag.Id,
                Name = tag.Name,
            };
        }
    }
}
