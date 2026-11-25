namespace Assinment_MVC2.Models
{
    public class Membership
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int MemberId { get; set; }

        public Member Member { get; set; } = null!;

        public int PlanId { get; set; }

        public Plan Plan { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string Status => EndDate > DateTime.Now ? "Active" : "Expired";
    }
}
