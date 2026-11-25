using System.Text.Json;
using Assinment_MVC2.Models;

namespace Assinment_MVC2.Data
{
    public static class PlanSeeder
    {
        public static void Seed(GymDbContext context, string contentRootPath)
        {
            if (context.Plans.Any())
            {
                return;
            }

            var path = Path.Combine(contentRootPath, "Data", "plans.json");
            var json = File.ReadAllText(path);
            var plans = JsonSerializer.Deserialize<List<PlanSeed>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? [];

            var now = DateTime.Now;
            context.Plans.AddRange(plans.Select(plan => new Plan
            {
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
                IsActive = plan.IsActive,
                CreatedAt = now,
                UpdatedAt = now
            }));

            context.SaveChanges();
        }

        private sealed class PlanSeed
        {
            public string Name { get; set; } = null!;

            public string Description { get; set; } = null!;

            public int DurationDays { get; set; }

            public decimal Price { get; set; }

            public bool IsActive { get; set; }
        }
    }
}
