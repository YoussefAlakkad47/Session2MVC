using Assinment_MVC2.Data;
using Assinment_MVC2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assinment_MVC2.Controllers
{
    public class PlanController : Controller
    {
        private readonly GymDbContext _context;

        public PlanController(GymDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var plans = _context.Plans
                .AsNoTracking()
                .ToList();

            return View(plans);
        }

        public IActionResult Details(int id)
        {
            var plan = _context.Plans
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);

            if (plan is null)
            {
                return NotFound();
            }

            return View(plan);
        }

        public IActionResult Edit(int id)
        {
            var plan = _context.Plans.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (plan is null)
            {
                return NotFound();
            }

            if (!plan.IsActive)
            {
                TempData["ErrorMessage"] = "Inactive plans can't be edited.";
                return RedirectToAction(nameof(Index));
            }

            if (IsLocked(id))
            {
                TempData["ErrorMessage"] = "A plan with active or upcoming memberships is locked.";
                return RedirectToAction(nameof(Index));
            }

            return View(new PlanEditViewModel
            {
                Id = plan.Id,
                PlanName = plan.Name,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
                Description = plan.Description
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PlanEditViewModel model)
        {
            var plan = _context.Plans.FirstOrDefault(p => p.Id == model.Id);
            if (plan is null)
            {
                return NotFound();
            }

            if (!plan.IsActive)
            {
                TempData["ErrorMessage"] = "Inactive plans can't be edited.";
                return RedirectToAction(nameof(Index));
            }

            if (IsLocked(plan.Id))
            {
                TempData["ErrorMessage"] = "A plan with active or upcoming memberships is locked.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            plan.DurationDays = model.DurationDays;
            plan.Price = model.Price;
            plan.Description = model.Description;
            plan.UpdatedAt = DateTime.Now;
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Plan updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Activate(int id)
        {
            var plan = _context.Plans.FirstOrDefault(p => p.Id == id);
            if (plan is null)
            {
                return NotFound();
            }

            if (plan.IsActive && IsLocked(id))
            {
                TempData["ErrorMessage"] = "A plan with active or upcoming memberships is locked.";
                return RedirectToAction(nameof(Index));
            }

            plan.IsActive = !plan.IsActive;
            plan.UpdatedAt = DateTime.Now;
            _context.SaveChanges();

            TempData["SuccessMessage"] = plan.IsActive ? "Plan activated." : "Plan deactivated.";
            return RedirectToAction(nameof(Index));
        }

        private bool IsLocked(int planId)
        {
            return _context.Memberships.Any(m => m.PlanId == planId && m.EndDate > DateTime.Now);
        }
    }
}
