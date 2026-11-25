namespace Assinment_MVC2.Models
{
    public class Session
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;

        public int Capacity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TrainerId { get; set; }

        public Trainer Trainer { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
