using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Micro.Center.Entities;
using Micro.Center.Web.Data;
using AutoMapper;
using Micro.Center.Web.Models.ProductType;

namespace Micro.Center.Web.Controllers
{
    public class ProductTypesController : Controller
    {
        #region Data And Const
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductTypesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        } 
        #endregion


        #region Actions
        public async Task<IActionResult> Index()
        {
           var ProductType = await _context
                                        .ProductTypes
                                        .ToListAsync();

            var ProductTypeVM = _mapper.Map<List<ProductType>, List<ProductTypeViewModel>>(ProductType);
            
            return View(ProductTypeVM);

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            var producttypeVM = _mapper.Map<ProductType, ProductTypeDetailsViewModel>(productType);

            return View(producttypeVM);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CreateUpdateProductTypeViewModel CreateUpdateproductTypeVM)
        {
            if (ModelState.IsValid)
            {
                var producttype = _mapper.Map<CreateUpdateProductTypeViewModel, ProductType>(CreateUpdateproductTypeVM);
                _context.Add(producttype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(CreateUpdateproductTypeVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            var producttypeVM = _mapper.Map<ProductType, CreateUpdateProductTypeViewModel>(productType);

            return View(producttypeVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateProductTypeViewModel CreateUpdateproductTypeVM)
        {
            if (id != CreateUpdateproductTypeVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var producttype = _mapper.Map<CreateUpdateProductTypeViewModel , ProductType>(CreateUpdateproductTypeVM);
                    _context.Update(producttype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypeExists(CreateUpdateproductTypeVM.Id))
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
            return View(CreateUpdateproductTypeVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType != null)
            {
                _context.ProductTypes.Remove(productType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } 
        #endregion


        #region Private Method
        private bool ProductTypeExists(int id)
        {
            return _context.ProductTypes.Any(e => e.Id == id);
        } 
        #endregion
    }
}
