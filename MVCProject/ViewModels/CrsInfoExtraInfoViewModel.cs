using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using MVCProject.Models.Validation_Attribute;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels
{
    public class CrsInfoExtraInfoViewModel
    {
        public int CrsId { get; set; }

        [Unique]
        [Required]
        [StringLength(50,MinimumLength =2)]
        public string CrsName { get; set; }

        [Range(50,100)]
        public int CrsDegree { get; set; }

        //[LessThan(51)]
        [Remote(action:"CheckMinDegree",controller:"Course",AdditionalFields ="CrsDegree")]
        public int CrsMinDegree { get; set; }

        [Remote(action: "DividedBy3", controller: "Course")]
        public int CrsHours { get; set; }
   
        public int DepartmentId { get; set; }
        public List<Department>? DeptList { get; set; }
  
        public int InstructorId { get; set; }
        public List<Instructor>? InstructorList { get; set; }
       




    }
}
