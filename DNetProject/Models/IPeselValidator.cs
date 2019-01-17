using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNetProject.Models
{
    interface IPeselValidator
    {
        bool ValidatePesel(string pesel);
        bool ValidateAge(DateTime birthDate);
        bool ValidatePeselAndBirthDate(string pesel, DateTime birthDate);
    }
}
