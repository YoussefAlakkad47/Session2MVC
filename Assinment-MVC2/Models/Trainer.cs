namespace Assinment_MVC2.Models
{
    public class Trainer
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public Address Address { get; set; } = null!;

        public DateTime HireDate { get; set; }

        public string Specialties { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
