using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public int Hours { get; set; }

        [ForeignKey("Department")]
        public int Dept_Id { get; set; }
        public Department Department { get; set; }

        public List<Instructor> Instructors { get; set; } = new List<Instructor>();
        public List<CrsResult> CrsResults { get; set; } =new List<CrsResult>();

    }
}
