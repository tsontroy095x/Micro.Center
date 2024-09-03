using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Micro.Center.Entities;
using Micro.Center.Web.Data;
using AutoMapper;
using Micro.Center.Web.Models.Product;

namespace Micro.Center.Web.Controllers
{
    public class ProductsController : Controller
    {
        #region Data And Const
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        } 
        #endregion


        #region Actions
        public async Task<IActionResult> Index()
        {
            var product = await _context
                                                .Products
                                                .Include(p => p.ProductType)
                                                .ToListAsync();

            var productVM = _mapper.Map<List<Product> , List<ProductViewModel>>(product);
            
            return View(productVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context
                                   .Products
                                   .Include(p => p.ProductType)
                                   .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var productDetailsVM = _mapper.Map<Product, ProductDetailsViewModel>(product);

            return View(productDetailsVM);
        }

        public IActionResult Create()
        {
            var createUpdateProductVM = new CreateUpdateProductViewModel();
            createUpdateProductVM.ProductTypesMultiSelectList = new MultiSelectList(_context.ProductTypes, "Id", "Name");

            return View(createUpdateProductVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CreateUpdateProductViewModel CreateUpdateproductVM)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<CreateUpdateProductViewModel, Product>(CreateUpdateproductVM);
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           // ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Description", product.ProductTypeId);
            return View(CreateUpdateproductVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var createUpdateProductVM = _mapper.Map<Product, CreateUpdateProductViewModel>(product);

            createUpdateProductVM.ProductTypesMultiSelectList = new MultiSelectList(_context.ProductTypes, "Id", "Name");

            return View(createUpdateProductVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateProductViewModel CreateUpdateproductVM)
        {
            if (id != CreateUpdateproductVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<CreateUpdateProductViewModel, Product>(CreateUpdateproductVM);
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(CreateUpdateproductVM.Id))
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
           // ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Description", product.ProductTypeId);
            return View(CreateUpdateproductVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } 
        #endregion


        #region Private Method
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        } 
        #endregion

    }
}
