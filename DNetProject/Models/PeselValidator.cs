using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace DNetProject.Models
{
    public class PeselValidator: IPeselValidator 
    {
        /// <summary>
        /// Mnozniki dla PESEL
        /// </summary>
        private readonly int[] mnozniki = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

        /// <summary>
        /// Sprawdza PESEL pod kątem poprawności
        /// </summary>
        /// <param name="pesel">PESEL string</param>
        /// <returns>true = OK; false = NOK</returns>
        public bool ValidatePesel(string pesel)
        {
            bool toRet = false;
            try
            {
                if (pesel.Length == 11)
                {
                    toRet = CountSum(pesel).Equals(pesel[10].ToString());
                }
            }
            catch (Exception)
            {
                toRet = false;
            }

            return toRet;
        }

        public bool ValidateAge(DateTime birthDate)
        {
            var age = GetAge(birthDate);
            if (age < 15)
                return false;

            var date = birthDate.Date;
            string birth = birthDate.ToShortDateString();
            return true;
        }

        int GetAge(DateTime bornDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - bornDate.Year;
            if (bornDate > today.AddYears(-age))
                age--;

            return age;
        }


        /// <summary>
        /// Liczy sumę cyfr znaczących PESEL
        /// </summary>
        /// <param name="pesel">PESEL string</param>
        /// <returns>SUMA string</returns>
        private string CountSum(string pesel)
        {
            int sum = 0;
            for (int i = 0; i < mnozniki.Length; i++)
            {
                sum += mnozniki[i] * int.Parse(pesel[i].ToString());
            }

            int reszta = sum % 10;
            return reszta == 0 ? reszta.ToString() : (10 - reszta).ToString();
        }

        public bool ValidatePeselAndBirthDate(string pesel, DateTime birthDate)
        {
            pesel = pesel.Trim();

            int rok = 0;
            int mies = 0;
            int dzien = Convert.ToInt32(pesel[4].ToString()) * 10 + Convert.ToInt32(pesel[5].ToString());

            if (pesel[2] == '0' || pesel[2] == '1')
            {
                rok = 1900;
                mies = Convert.ToInt32(pesel[2].ToString()) * 10 + Convert.ToInt32(pesel[3].ToString());
            }
            else if (pesel[2] == '2' || pesel[2] == '3')
            {
                rok = 2000;
                mies = (Convert.ToInt32(pesel[2].ToString()) * 10 + Convert.ToInt32(pesel[3].ToString()) - 20);
            }
            else if (pesel[2] == '4' || pesel[2] == '5')
            {
                rok = 2100;
                mies = (Convert.ToInt32(pesel[2].ToString()) * 10 + Convert.ToInt32(pesel[3].ToString()) - 40);
            }
            else if (pesel[2] == '6' || pesel[2] == '7')
            {
                rok = 2200;
                mies = (Convert.ToInt32(pesel[2].ToString()) * 10 + Convert.ToInt32(pesel[3].ToString()) - 60);
            }
            else if (pesel[2] == '8' || pesel[2] == '9')
            {
                rok = 1800;
                mies = (Convert.ToInt32(pesel[2].ToString()) * 10 + Convert.ToInt32(pesel[3].ToString()) - 80);
            }

            rok += Convert.ToInt32(pesel[0].ToString()) * 10 + Convert.ToInt32(pesel[1].ToString());
            String peselDate = rok.ToString() + "-" + (mies < 10 ? "0" + mies.ToString() : mies.ToString()) + "-" +
                               (dzien < 10 ? "0" + dzien.ToString() : dzien.ToString());
            String birth = birthDate.Year.ToString() + "-" +
                           (birthDate.Month < 10 ? "0" + birthDate.Month.ToString() : birthDate.Month.ToString()) +
                           "-" + (birthDate.Day < 10 ? "0" + birthDate.Day.ToString() : birthDate.Day.ToString());


            if (birth == peselDate)
                return true;
            else return false;
        }
    }
}