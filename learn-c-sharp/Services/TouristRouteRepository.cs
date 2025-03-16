using learn_c_sharp.Database;
using learn_c_sharp.Models;
using Microsoft.EntityFrameworkCore;

namespace learn_c_sharp.Services
{
    public class TouristRouteRepository : ITouristRouteRepository
    {
        private readonly AppDbContext _context;

        public TouristRouteRepository(AppDbContext context)
        {
            _context = context;
        }

        public TouristRoute GetTouristRoute(Guid touristRouteId)
        {
            return _context.TouristRouts.Include(t => t.TouristRoutePictures).FirstOrDefault(n => n.Id == touristRouteId);
        }

        public IEnumerable<TouristRoute> GetTouristRoutes()
        {
            // include vs join
            return _context.TouristRouts.Include(t => t.TouristRoutePictures);
        }
        public bool TouristRouteExists(Guid touristRouteId)
        {
            return _context.TouristRouts.Any(t => t.Id == touristRouteId);
        } 
        public IEnumerable<TouristRoutePicture> GetPicturesByTouristRouteId(Guid touristRouteId)
        { 
            return _context.TouristRoutePictures.Where(p=>p.TouristRouteId == touristRouteId).ToList();
        }

        public TouristRoutePicture GetPicture(int pictureId)
        {
            return _context.TouristRoutePictures.Where(p => p.Id == pictureId).FirstOrDefault();
        }

    }
}
