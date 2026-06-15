namespace MVCProject.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }

        public List<Instructor> Instructors {  get; set; }=new List<Instructor>();
        public List<Course> Courses {  get; set; } =new List<Course>();
        public List<Trainee> Trainees {  get; set; } =new List<Trainee>();
    }
}
