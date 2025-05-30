﻿using Learn.C.Sharp.Database;
using Learn.C.Sharp.Dtos;
using Learn.C.Sharp.Helper;
using Learn.C.Sharp.Models;
using Microsoft.EntityFrameworkCore;

namespace Learn.C.Sharp.Services
{
    public class TouristRouteRepository(AppDbContext context, IPropertyMappingService propertyMappingService) : ITouristRouteRepository
    {
        private readonly AppDbContext _context = context;
        private readonly IPropertyMappingService _propertyMappingService = propertyMappingService;

        public async Task<TouristRoute> GetTouristRouteAsync(Guid touristRouteId)
        {
            //立即执行
            return await _context.TouristRouts.Include(t => t.TouristRoutePictures).AsNoTracking().FirstOrDefaultAsync(n => n.Id == touristRouteId);
        }

        public async Task<PaginationList<TouristRoute>> GetTouristRoutesAsync(
            string keyword,
            string ratingOperator,
            int? ratingValue,
            int pageSize,
            int pageNumber,
            string orderBy
            )
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
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                var touristRouteMappingDictionary = _propertyMappingService.GetPropertyMapping<TouristRouteDto, TouristRoute>();

                result = result.ApplySort(orderBy, touristRouteMappingDictionary);

            }
            //var skip = (pageNumber - 1) * pageSize;
            //result = result.Skip(skip);
            //result = result.Take(pageSize);
            ////立即执行
            return await PaginationList<TouristRoute>.CreateAsync(pageNumber, pageSize, result);
        }
        public async Task<bool> TouristRouteExistsAsync(Guid touristRouteId)
        {
            return await _context.TouristRouts.AnyAsync(t => t.Id == touristRouteId);
        }
        public async Task<IEnumerable<TouristRoutePicture>> GetPicturesByTouristRouteIdAsync(Guid touristRouteId)
        {
            return await _context.TouristRoutePictures.Where(p => p.TouristRouteId == touristRouteId).ToListAsync();
        }

        public async Task<TouristRoutePicture> GetPictureAsync(int pictureId)
        {
            return await _context.TouristRoutePictures.Where(p => p.Id == pictureId).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TouristRoute>> GetTouristRoutesByIDListAsync(IEnumerable<Guid> ids)
        {
            return await _context.TouristRouts.Where(t => ids.Contains(t.Id)).ToListAsync();
        }
        public void AddTouristRoute(TouristRoute touristRoute)
        {
            if (touristRoute == null)
            {
                throw new ArgumentNullException(nameof(touristRoute));
            }
            _context.TouristRouts.Add(touristRoute);
        }
        public void AddTouristRoutePicture(Guid touristRouteId, TouristRoutePicture touristRoutePicture)
        {
            if (touristRouteId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(touristRouteId));
            }
            if (touristRoutePicture == null)
            {
                throw new ArgumentNullException(nameof(touristRoutePicture));
            }
            touristRoutePicture.TouristRouteId = touristRouteId;
            _context.TouristRoutePictures.Add(touristRoutePicture);
        }

        public void DeleteTouristRoute(TouristRoute touristRoute)
        {
            _context.TouristRouts.Remove(touristRoute);
        }
        public void DeleteTouristRoutes(IEnumerable<TouristRoute> touristRoutes)
        {
            _context.TouristRouts.RemoveRange(touristRoutes);
        }
        public void DeleteTouristRoutePicture(TouristRoutePicture touristRoutePicture)
        {
            _context.TouristRoutePictures.Remove(touristRoutePicture);
        }
        public async Task<ShoppingCart> GetShoppingCartByUserId(string userId)
        {
            return await _context.ShoppingCarts
                .Include(s => s.User)
                .Include(s => s.ShoppingCartItems)
                .ThenInclude(li => li.TouristRoute)
                .ThenInclude(tr => tr.TouristRoutePictures)
                .Where(s => s.UserId == userId)
                .FirstOrDefaultAsync();
        }
        public async Task CreateShoppingCart(ShoppingCart shoppingCart)
        {
            await _context.ShoppingCarts.AddAsync(shoppingCart);
        }
        public async Task AddShoppingCartItem(LineItem lineItem)
        {
            await _context.LineItems.AddAsync(lineItem);
        }
        public async Task<LineItem> GetShoppingCartItemByItemId(int itemId)
        {
            return await _context.LineItems.Where(li => li.Id == itemId).FirstOrDefaultAsync();
        }
        public void DeleteShoppingCartItem(LineItem lineItem)
        {
            _context.LineItems.Remove(lineItem);
        }
        public async Task<IEnumerable<LineItem>> GetShoppingCartsByIdListAsync(IEnumerable<int> ids)
        {
            return await _context.LineItems.Where(li => ids.Contains(li.Id)).ToListAsync();
        }
        public void DeleteShoppingCartItems(IEnumerable<LineItem> lineItems)
        {
            _context.LineItems.RemoveRange(lineItems);
        }
        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }
        public async Task<PaginationList<Order>> GetOrdersByUserId(string userId, int pageSize, int pageNumber)
        {
            IQueryable<Order> result = _context.Orders.Include(o => o.OrderItems).Where(o => o.UserId == userId);
            string sql = result.ToQueryString();
            Console.WriteLine("++++++++++++++++++++++++++++++" + sql + "++++++++++++++++++++++++++++++");

            return await PaginationList<Order>.CreateAsync(pageNumber, pageSize, (IQueryable<Order>)result);

        }
        public async Task<Order> GetOrderById(Guid orderId)
        {
            return await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.TouristRoute).Where(o => o.Id == orderId).FirstOrDefaultAsync();
        }
        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

    }
}
