using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PennyWise.Interfaces;
using PennyWise.ViewModels;

namespace PennyWise.Controllers
{
    [Authorize]
    public class BudgetController : Controller
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        // Category management
        public async Task<IActionResult> Categories()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var categories = await _budgetService.GetCategoriesAsync(userId);
            var vm = categories.Select(c => new BudgetCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            
            ViewBag.CurrencySymbol = User.FindFirst("CurrencySymbol")?.Value ?? "$";
            
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView(vm);
            }
            
            return View(vm);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View(new BudgetCategoryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(BudgetCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _budgetService.CreateCategoryAsync(userId, model.Name);

            return RedirectToAction(nameof(Categories));
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var category = await _budgetService.GetCategoryByIdAsync(id, userId);
            if (category == null)
            {
                return NotFound();
            }

            var vm = new BudgetCategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(BudgetCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var category = await _budgetService.GetCategoryByIdAsync(model.Id, userId);
            if (category == null)
            {
                return NotFound();
            }

            await _budgetService.UpdateCategoryAsync(category, model.Name);
            return RedirectToAction(nameof(Categories));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var category = await _budgetService.GetCategoryByIdAsync(id, userId);
            if (category == null)
            {
                return NotFound();
            }

            await _budgetService.DeleteCategoryAsync(category);
            return RedirectToAction(nameof(Categories));
        }

        // Budget allocations
        [HttpGet]
        public async Task<IActionResult> Allocations(int? month, int? year)
        {
            var now = DateTime.Now;
            var m = month ?? now.Month;
            var y = year ?? now.Year;

            // Validation: Prevent future months
            if (y > now.Year || (y == now.Year && m > now.Month))
            {
                return RedirectToAction("Allocations", new { month = now.Month, year = now.Year });
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var categories = await _budgetService.GetCategoriesAsync(userId);
            var allocations = await _budgetService.GetAllocationsForMonthAsync(userId, m, y);

            var vm = new BudgetAllocationEditViewModel
            {
                Month = m,
                Year = y,
                Items = categories.Select(c =>
                {
                    var allocation = allocations.FirstOrDefault(a => a.BudgetCategoryId == c.Id);
                    return new BudgetAllocationItem
                    {
                        CategoryId = c.Id,
                        CategoryName = c.Name,
                        AllocatedAmount = allocation?.AllocatedAmount ?? 0
                    };
                }).ToList()
            };

            ViewBag.CurrencySymbol = User.FindFirst("CurrencySymbol")?.Value ?? "$";

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView(vm);
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Allocations(BudgetAllocationEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var dict = model.Items.ToDictionary(i => i.CategoryId, i => i.AllocatedAmount);
            await _budgetService.SaveAllocationsForMonthAsync(userId, model.Month, model.Year, dict);

            TempData["Message"] = "Budget allocations saved.";
            return RedirectToAction("Index", "Dashboard", new { month = model.Month, year = model.Year });
        }
    }
}

