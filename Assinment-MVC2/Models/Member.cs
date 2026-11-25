namespace Assinment_MVC2.Models
{
    public class Member
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public Address Address { get; set; } = null!;

        public DateTime JoinDate { get; set; }

        public string? Photo { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
    }
}
