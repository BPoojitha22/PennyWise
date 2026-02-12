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
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly IIncomeService _incomeService;
        private readonly IBudgetService _budgetService;

        public DashboardController(IDashboardService dashboardService, IIncomeService incomeService, IBudgetService budgetService)
        {
            _dashboardService = dashboardService;
            _incomeService = incomeService;
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

            var vm = await _dashboardService.GetMonthlySummaryAsync(userId, m, y);
            
            ViewBag.CurrencySymbol = User.FindFirst("CurrencySymbol")?.Value ?? "$";
            
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView(vm);
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> SaveIncome(int month, int year, decimal amount)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _incomeService.SaveForMonthAsync(userId, month, year, amount);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SaveBudget(int month, int year, BudgetAllocationItem[] items)
        {
            if (items == null || !items.Any())
            {
                return BadRequest();
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var dict = items.ToDictionary(i => i.CategoryId, i => i.AllocatedAmount);
            await _budgetService.SaveAllocationsForMonthAsync(userId, month, year, dict);
            
            return Ok();
        }
    }
}

