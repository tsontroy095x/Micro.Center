using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Micro.Center.Entities;
using Micro.Center.Web.Data;
using AutoMapper;
using Micro.Center.Web.Models.Customer;

namespace Micro.Center.Web.Controllers
{
    public class CustomersController : Controller
    {
        #region Data And Const
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        } 
        #endregion


        #region Actions
        public async Task<IActionResult> Index()
        {
         var customer = await _context
                                        .Customers
                                        .ToListAsync();

            var customerVM = _mapper.Map<List<Customer>, List<CustomerViewModel>>(customer);

            return View(customerVM);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context
                                    .Customers
                                    .Include(o => o.Orders)
                                    .SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            var customerVM = _mapper.Map<Customer, CustomerDetailsViewModel>(customer);

            return View(customerVM);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CreateUpdateCustomerViewModel CreateUpdatecustomerVM)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<CreateUpdateCustomerViewModel, Customer>(CreateUpdatecustomerVM);
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(CreateUpdatecustomerVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            
            var customerVM = _mapper.Map<Customer, CreateUpdateCustomerViewModel>(customer);

            return View(customerVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateCustomerViewModel CreateUpdatecustomerVM)
        {
            if (id != CreateUpdatecustomerVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var customer = _mapper.Map<CreateUpdateCustomerViewModel, Customer>(CreateUpdatecustomerVM);
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(CreateUpdatecustomerVM.Id))
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
            return View(CreateUpdatecustomerVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } 
        #endregion


        #region Private Methods
        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        } 
        #endregion
    }
}
