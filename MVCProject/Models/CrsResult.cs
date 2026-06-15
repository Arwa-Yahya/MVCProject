using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class CrsResult
    {
        public int Degree {  get; set; }

        [ForeignKey("Course")]
        public int Crs_Id {  get; set; }

        [ForeignKey("Trainee")]
        public int Trainee_Id {  get; set; }

        public Course Course { get; set; }
        public Trainee Trainee { get; set; }
    }
}
