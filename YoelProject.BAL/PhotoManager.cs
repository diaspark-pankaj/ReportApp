using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoelProject.DAL.Repository;
using YoelProject.Models;
using YoelProject.ViewModel.Admin;

namespace YoelProject.BAL
{
    public class PhotoManager
    {
        public IEnumerable<PhotoViewModel> GetAllPhotos(string filter = null, int page = 1, int pageSize = 20)
        {
            PhotoRepository photoRepository = new PhotoRepository();
            return photoRepository.GetAll()
                        .Where(x => filter == null || (x.Description.Contains(filter)))
                        .OrderByDescending(x => x.PhotoId)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList().Select(photo=> 
                            new PhotoViewModel()
                            {
                                PhotoId = photo.PhotoId,
                                Description = photo.Description,
                                ThumbPath = photo.ThumbPath,
                                ImagePath = photo.ImagePath,
                            });
        }

        public void CreatePhoto(PhotoViewModel photoViewModel)
        {
            PhotoRepository photoRepository = new PhotoRepository();
            var photo = new Photo();
            photo.Description = photoViewModel.Description;
            photo.ImagePath = photoViewModel.ImagePath;
            photo.ThumbPath = photoViewModel.ThumbPath;
            photo.CreatedOn = DateTime.Now;

            photoRepository.Add(photo);
            photoRepository.SaveChanges();
        }
    }
}
