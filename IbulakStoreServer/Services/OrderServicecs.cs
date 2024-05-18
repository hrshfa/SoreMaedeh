using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Order;

namespace IbulakStoreServer.Services
{
    public class OrderService
    {
        private readonly StoreDbContext _context;
        private List<Order> order;

        public int ProductId { get; private set; }

        public OrderService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<Order?> GetAsync(int id)
        {
            Order? order = await _context.Orders.FindAsync(id);
            return order;
        }
        public async Task<List<Order>> GetsAsync()
        {
            List<Order> orders = await _context.Orders.ToListAsync();
            return orders;
        }
        public async Task<List<Order>> GetsByProductAsync(int productId)
        {
            List<Order> orders = await _context.Orders.Where(order => order.productId == productId).ToListAsync();
            return order;
        }
        public async Task<List<Order>> GetsByUserAsync(int userId)
        {
            List<Order> orders = await _context.Orders.Where(order => order.userId == userId).ToListAsync();
            return order;
        }
        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(List<OrderAddRequestDto> models)
        {
            var orders = models.Select(orderDto=>new Order
            {
                Count = orderDto.Count,
                Price=orderDto.Price,
                ProductId=orderDto.ProductId,
                UserId = orderDto.UserId,
                CreatedAt=DateTime.Now
            });
            _context.Orders.AddRange(orders);
            await _context.SaveChangesAsync();
        }
        public async Task EditAsync(Order order)
        {
            Order? oldOrder = await _context.Orders.FindAsync(order.Id);
            if (oldOrder is null)
            {
                throw new Exception("سفارشی  با این شناسه پیدا نشد.");
            }
            oldOrder.Count = order.Count;
            oldOrder.Count = order.Price;
            oldOrder.ProductId = order.ProductId;
            oldOrder.UserId = order.UserId;
            
            _context.Orders.Update(oldOrder);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Order? order = await _context.Orders.FindAsync(id);
            if (order is null)
            {
                throw new Exception("سفارشی  با این شناسه پیدا نشد.");
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderReportByProductResponseDto>> OrdersReportByProductAsync(OrderReportByProductRequestDto model)
        {
            var ordersQuery = _context.Orders.Where(a =>
                                (model.FromDate == null || a.CreatedAt >= model.FromDate)
                               && (model.ToDate == null || a.CreatedAt <= model.ToDate)
                                )
                .GroupBy(a => a.ProductId)
                .Select(a => new
                {
                    ProductId = a.Key,
                    TotalSum = a.Sum(s => s.Price)
                });

            var productsQuery = from product in _context.Products
                           from order in ordersQuery.Where(a => a.ProductId == product.Id).DefaultIfEmpty()
                           select new OrderReportByProductResponseDto
                           {
                               ProductName=product.Name,
                               ProductCategoryName=product.Category.Name,
                               ProductId=product.Id,
                               TotalSum=(int?) order.TotalSum
                           };

            productsQuery = productsQuery.Skip(model.PageNo * model.PageSize)
                                .Take(model.PageSize);
            var result = await productsQuery.ToListAsync();
            return result;
        }
    }
}
