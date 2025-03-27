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
            //立即执行
            return _context.TouristRouts.Include(t => t.TouristRoutePictures).FirstOrDefault(n => n.Id == touristRouteId);
        }

        public IEnumerable<TouristRoute> GetTouristRoutes(string keyword, string ratingOperator, int? ratingValue)
        {
            // 延迟执行
            IQueryable<TouristRoute> result = _context
                .TouristRouts
                .Include(t => t.TouristRoutePictures);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                result = result.Where((t) => t.Title.Contains(keyword));
            }
            if (ratingValue >= 0)
            {
                switch (ratingOperator)
                {
                    case "largerThan":
                        result = result.Where(t => t.Rating >= ratingValue);
                        break;
                    case "lessThan":
                        result = result.Where(t => t.Rating <= ratingValue);
                        break;
                    case "equalTo":
                    default:
                        result = result.Where(t => t.Rating == ratingValue);
                        break;
                }

            }
            //立即执行
            return result.ToList();
        }
        public bool TouristRouteExists(Guid touristRouteId)
        {
            return _context.TouristRouts.Any(t => t.Id == touristRouteId);
        }
        public IEnumerable<TouristRoutePicture> GetPicturesByTouristRouteId(Guid touristRouteId)
        {
            return _context.TouristRoutePictures.Where(p => p.TouristRouteId == touristRouteId).ToList();
        }

        public TouristRoutePicture GetPicture(int pictureId)
        {
            return _context.TouristRoutePictures.Where(p => p.Id == pictureId).FirstOrDefault();
        }
        public void AddTouristRoute(TouristRoute touristRoute)
        {
            if (touristRoute == null)
            {
                throw new ArgumentNullException(nameof(touristRoute));
            }
            _context.TouristRouts.Add(touristRoute);
        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
