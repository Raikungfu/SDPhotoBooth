using BussinessObject.Models;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class DepositProductRepository : Repository<DepositProduct>, IDepositProductRepository
    {
        private readonly AIPhotoboothDbContext _context;
        public DepositProductRepository(AIPhotoboothDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
