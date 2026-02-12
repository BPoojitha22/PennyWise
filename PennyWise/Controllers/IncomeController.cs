using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PennyWise.Interfaces;
using PennyWise.ViewModels;

namespace PennyWise.Controllers
{
    [Authorize]
    public class IncomeController : Controller
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? month, int? year)
        {
            var now = DateTime.Now;
            var m = month ?? now.Month;
            var y = year ?? now.Year;
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var income = await _incomeService.GetForMonthAsync(userId, m, y);

            var vm = new IncomeEditViewModel
            {
                Month = m,
                Year = y,
                Amount = income?.Amount ?? 0
            };

            ViewBag.CurrencySymbol = User.FindFirst("CurrencySymbol")?.Value ?? "$";

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView(vm);
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IncomeEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _incomeService.SaveForMonthAsync(userId, model.Month, model.Year, model.Amount);

            TempData["Message"] = "Income saved.";
            return RedirectToAction("Index", "Dashboard", new { month = model.Month, year = model.Year });
        }
    }
}

