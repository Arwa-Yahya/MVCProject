using MVCProject.Models;

namespace MVCProject.ViewModels
{
    public class CrsInfoExtraInfoViewModel
    {
        public int CrsId { get; set; }
        public string CrsName { get; set; }
        public int CrsDegree { get; set; }
        public int CrsMinDegree { get; set; }
        public int CrsHours { get; set; }

        public int DepartmentId { get; set; }
        public List<Department> DeptList { get; set; }

        public int InstructorId { get; set; }
        public List<Instructor> InstructorList { get; set; }
       




    }
}
