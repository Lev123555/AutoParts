using AutoParts.Models;
using AutoParts.Models.Data;
using AutoParts.ViewModels.Spares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoParts.Controllers
{
    public class SparesController : Controller
    {
        private readonly AppCtx _context;

        public SparesController(AppCtx context)
        {
            _context = context;
        }

        // GET: Spares
        public async Task<IActionResult> Index(int? pageNumber, string searchString, SpareSortState sortOrder = SpareSortState.SpareAsc)
        {
            ViewData["CurrentFilter"] = searchString;

            var spares = from s in _context.Spares
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                spares = spares.Where(s => s.TitleSpare.Contains(searchString));
            }

            ViewData["TitleSort"] = sortOrder == SpareSortState.SpareAsc ? SpareSortState.SpareDesc : SpareSortState.SpareAsc;
            ViewData["PriceSort"] = sortOrder == SpareSortState.PriceAsc ? SpareSortState.PriceDesc : SpareSortState.PriceAsc;
            ViewData["CategorySort"] = sortOrder == SpareSortState.CategoryAsc ? SpareSortState.CategoryDesc : SpareSortState.CategoryAsc;
            ViewData["CarBrandSort"] = sortOrder == SpareSortState.CarBrandAsc ? SpareSortState.CarBrandDesc : SpareSortState.CarBrandAsc;
            ViewData["CarModelSort"] = sortOrder == SpareSortState.CarModelAsc ? SpareSortState.CarModelDesc : SpareSortState.CarModelAsc;

            spares = sortOrder switch
            {
                SpareSortState.SpareDesc => spares.OrderByDescending(s => s.TitleSpare),
                SpareSortState.PriceAsc => spares.OrderBy(s => s.Price),
                SpareSortState.PriceDesc => spares.OrderByDescending(s => s.Price),
                SpareSortState.CategoryAsc => spares.OrderBy(s => s.Category),
                SpareSortState.CategoryDesc => spares.OrderByDescending(s => s.Category),
                SpareSortState.CarBrandAsc => spares.OrderBy(s => s.CarBrand),
                SpareSortState.CarBrandDesc => spares.OrderByDescending(s => s.CarBrand),
                SpareSortState.CarModelAsc => spares.OrderBy(s => s.CarModel),
                SpareSortState.CarModelDesc => spares.OrderByDescending(s => s.CarModel),
                _ => spares.OrderBy(s => s.TitleSpare),
            };

            int pageSize = 3;
            return View(await PaginatedList<Spare>.CreateAsync(spares.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Spares/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Spares == null)
            {
                return NotFound();
            }

            var spare = await _context.Spares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spare == null)
            {
                return NotFound();
            }

            return View(spare);
        }

        // GET: Spares/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSpareModel model)
        {
            if (_context.Spares
                .Where(f => f.TitleSpare == model.TitleSpare)
                .Where(f => f.Description == model.Description)
                .Where(f => f.Price == model.Price)
                .Where(f => f.Category == model.Category)
                .Where(f => f.Article == model.Article)
                .Where(f => f.CarBrand == model.CarBrand)
                .Where(f => f.CarModel == model.CarModel)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введённая запчасть уже существует");
            }

            if (ModelState.IsValid)
            {
                Spare spare = new()
                {
                    TitleSpare = model.TitleSpare,
                    Description = model.Description,
                    Price = model.Price,
                    Category = model.Category,
                    Article = model.Article,
                    CarBrand = model.CarBrand,
                    CarModel = model.CarModel,

                };

                _context.Add(spare);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Spares/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Spares == null)
            {
                return NotFound();
            }

            var spare = await _context.Spares.FindAsync(id);
            if (spare == null)
            {
                return NotFound();
            }

            EditSpareModel model = new()
            {
                TitleSpare = spare.TitleSpare,
                Description = spare.Description,
                Price = spare.Price,
                Category = spare.Category,
                Article = spare.Article,
                CarBrand = spare.CarBrand,
                CarModel = spare.CarModel,
            };
            return View(model);
        }

        // POST: Spares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, EditSpareModel model)
        {
            Spare spare = await _context.Spares.FindAsync(id);

            if (_context.Spares
                .Where(f => f.TitleSpare == model.TitleSpare)
                .Where(f => f.Description == model.Description)
                .Where(f => f.Price == model.Price)
                .Where(f => f.Category == model.Category)
                .Where(f => f.Article == model.Article)
                .Where(f => f.CarBrand == model.CarBrand)
                .Where(f => f.CarModel == model.CarModel)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введённая запчасть уже существует");
            }

            if (id != spare.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    spare.TitleSpare = model.TitleSpare;
                    spare.Description = model.Description;
                    spare.Price = model.Price;
                    spare.Category = model.Category;
                    spare.Article = model.Article;
                    spare.CarBrand = model.CarBrand;
                    spare.CarModel = model.CarModel;
                    _context.Update(spare);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpareExists(spare.Id))
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
            return View(model);
        }

        // GET: Spares/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Spares == null)
            {
                return NotFound();
            }

            var spare = await _context.Spares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spare == null)
            {
                return NotFound();
            }

            return View(spare);
        }

        // POST: Spares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Spares == null)
            {
                return Problem("Entity set 'AppCtx.Spares'  is null.");
            }
            var spare = await _context.Spares.FindAsync(id);
            if (spare != null)
            {
                _context.Spares.Remove(spare);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpareExists(short id)
        {
          return (_context.Spares?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
