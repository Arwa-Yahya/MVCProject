using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Trainee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string Address { get; set; }
        public int Grade { get; set; }

        [ForeignKey("Department")]
        public int Dept_Id { get; set; }
        public Department Department { get; set; }

        public List<CrsResult> CrsResults { get; set; } = new List<CrsResult>();

    }
}
