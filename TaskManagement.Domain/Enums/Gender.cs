using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Enums
{
    public enum Gender
    {
        Male ,
        Female
    }
    public static class GenderExtension
    {
        public static string GetStringValue(this Gender gender)
        {
            return gender switch
            {
                Gender.Male => "Male",
                Gender.Female => "Female",
                _ => null
            };

        }
    }
}
