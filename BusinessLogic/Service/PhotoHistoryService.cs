using BusinessLogic.Service.IService;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class PhotoHistoryService : IPhotoHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PhotoHistoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string[]> GetListNewestPhotos(string userId)
        {
            var photoHistory = await _unitOfWork.PhotoHistory.GetFirstOrDefaultAsync(includeProperties: "Photos", orderBy: o => o.OrderByDescending(o => o.CreatedAt), filter: o => o.CustomerId == userId);
            if (photoHistory == null)
            {
                return null;
            }

            return photoHistory.Photos.Select(p => p.Url).ToArray();
        }
    }
}
