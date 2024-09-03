using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Micro.Center.Entities;
using Micro.Center.Web.Data;
using AutoMapper;
using Micro.Center.Web.Models.Order;

namespace Micro.Center.Web.Controllers
{
    public class OrdersController : Controller
    {
        #region Data And Conts
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        } 
        #endregion


        #region Actions
        public async Task<IActionResult> Index()
        {
            var order = await _context
                                     .Orders
                                     .Include(o => o.Customer)
                                     .ToListAsync();

            var orderVM = _mapper.Map<List<Order>,List<OrderViewModel>>(order);

            return View(orderVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context
                                     .Orders
                                     .Include(o => o.Customer)
                                     .Include(o => o.Products)
                                     .Where(o => o.Id == id)
                                     .SingleOrDefaultAsync();
            if (order == null)
            {
                return NotFound();
            }

            var orderDetailsVM = _mapper.Map<Order, OrderDetailsViewModel>(order);

            return View(orderDetailsVM);
        }

        public IActionResult Create()
        {
            var createUpdateOrderVM = new CreateUpdateOrderViewModel();

            LoadLookups(createUpdateOrderVM);

            return View(createUpdateOrderVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateOrderViewModel createUpdateOrderVM)
        {
            if (ModelState.IsValid)
            {

                var order = _mapper.Map<CreateUpdateOrderViewModel,Order>(createUpdateOrderVM);

                order.DateTime = DateTime.Now;

                await UpdateOrderProduct(order, createUpdateOrderVM.ProductIds);


                order.TotalPrice = GetOrderTotalPrice(order.Products);

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadLookups(createUpdateOrderVM);

            return View(createUpdateOrderVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context
                                    .Orders
                                    .Include(o => o.Products)
                                    .Where(o => o.Id == id)
                                    .SingleOrDefaultAsync();
            if (order == null)
            {
                return NotFound();
            }
            var createUpdateOrderVM = _mapper.Map<Order, CreateUpdateOrderViewModel>(order);

            createUpdateOrderVM.ProductIds = GetProductIds(order.Products);

            LoadLookups(createUpdateOrderVM);

            return View(createUpdateOrderVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateOrderViewModel createUpdateOrderVM)
        {
            if (id != createUpdateOrderVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var order = await _context
                                        .Orders
                                        .Include(o => o.Products)
                                        .Where(o => o.Id == id)
                                        .SingleAsync();

                    _mapper.Map(createUpdateOrderVM, order);

                    await UpdateOrderProduct(order, createUpdateOrderVM.ProductIds);

                    order.TotalPrice = GetOrderTotalPrice(order.Products);


                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(createUpdateOrderVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            LoadLookups(createUpdateOrderVM);

            return View(createUpdateOrderVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } 
        #endregion


        #region Private Method
        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        } 
        
        private void LoadLookups(CreateUpdateOrderViewModel createUpdateOrderVM)

        {

            createUpdateOrderVM.CustomerSelectList = new SelectList(_context.Customers, "Id", "FullName");
            createUpdateOrderVM.ProductMultiSelectList = new MultiSelectList(_context.Products, "Id", "Name");

        }
        private async Task UpdateOrderProduct(Order order, List<int> productId)
        {
            
            order.Products.Clear();

            var product = await _context
                                   .Products
                                   .Where(p => productId.Contains(p.Id))
                                   .ToListAsync();
            
            order.Products.AddRange(product);
        }
        private decimal GetOrderTotalPrice(List<Product> products)
        {
            return products.Sum(p => p.Price);
        }

        private List<int> GetProductIds(List<Product> products)
        {
            return products.Select(m => m.Id).ToList();
        }
        #endregion
    }
}
