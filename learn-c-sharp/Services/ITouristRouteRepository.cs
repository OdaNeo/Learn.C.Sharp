using learn_c_sharp.Models;

namespace learn_c_sharp.Services
{
    public interface ITouristRouteRepository
    {
        IEnumerable<TouristRoute> GetTouristRoutes();
        TouristRoute GetTouristRoute(Guid touristRouteId);
        bool TouristRouteExists(Guid touristRouteId);

        IEnumerable<TouristRoutePicture> GetPicturesByTouristRouteId(Guid touristRouteId);
        TouristRoutePicture GetPicture(int pictureId);
    }
}
 