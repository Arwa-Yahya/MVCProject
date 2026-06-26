using MVCProject.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MVCProject.Models.Validation_Attribute
{
    public class UniqueAttribute : ValidationAttribute
    {
        StepsContext context = new StepsContext();

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            String name = value.ToString();
            CrsInfoExtraInfoViewModel crsfromreq = validationContext.ObjectInstance as CrsInfoExtraInfoViewModel;

            var crsfromdb=context.Courses.FirstOrDefault(x => x.Name == name && x.Dept_Id==crsfromreq.DepartmentId && x.Id != crsfromreq.CrsId);

            if (crsfromdb == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Name is Already Exist");
            }

        }
    }
}
