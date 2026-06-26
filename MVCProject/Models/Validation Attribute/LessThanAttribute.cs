using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models.Validation_Attribute
{
    public class LessThanAttribute:ValidationAttribute
    {
        public int DegreeCrs { get; set; }

        public LessThanAttribute(int degreeCrs)
        {
            DegreeCrs = degreeCrs;
        }

        public override bool IsValid(object? value)
        {

        int mindegree = int.Parse(value.ToString());

            if(mindegree > DegreeCrs) return false;
            else return true;
        }
    }
}
