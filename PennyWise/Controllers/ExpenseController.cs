using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PennyWise.Interfaces;
using PennyWise.ViewModels;

namespace PennyWise.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        private readonly IBudgetService _budgetService;

        public ExpenseController(IExpenseService expenseService, IBudgetService budgetService)
        {
            _expenseService = expenseService;
            _budgetService = budgetService;
        }

        public async Task<IActionResult> Index(int? month, int? year)
        {
            var now = DateTime.Now;
            var m = month ?? now.Month;
            var y = year ?? now.Year;

            // Validation: Prevent future months
            if (y > now.Year || (y == now.Year && m > now.Month))
            {
                return RedirectToAction("Index", new { month = now.Month, year = now.Year });
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var expenses = await _expenseService.GetForMonthAsync(userId, m, y);

            var vm = expenses.Select(e => new ExpenseListItemViewModel
            {
                Id = e.Id,
                Date = e.Date,
                CategoryName = e.BudgetCategory.Name,
                Amount = e.Amount,
                Description = e.Description
            }).ToList();

            ViewBag.Month = m;
            ViewBag.Year = y;
            ViewBag.CurrencySymbol = User.FindFirst("CurrencySymbol")?.Value ?? "$";
            
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView(vm);
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var categories = await _budgetService.GetCategoriesAsync(userId);

            var vm = new ExpenseCreateViewModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenseCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var categories = await _budgetService.GetCategoriesAsync(userId);
                model.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });
                return View(model);
            }

            var userIdPost = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _expenseService.AddAsync(userIdPost, model.BudgetCategoryId, model.Date, model.Amount, model.Description);

            return RedirectToAction(nameof(Index), new { month = model.Date.Month, year = model.Date.Year });
        }
    }
}

