using LearnEnglish.Models.BankCard;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LearnEnglish.Models.CustomValidationAttribute
{
    public class BankCardExpirationAttribute : ValidationAttribute
    {
        const int CardExpiryYearMax = 5;

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Срок действия карты {name} указан некорректно"
                : ErrorMessage;
        }

        public override bool IsValid(object value)
        {
            if (value is not BankCardAddViewModel viewModel)
            {
                throw new FormatException("Данный атрибут применим только для банковской карты.");
            }

            var month = viewModel.ValidityMonth;
            var year = viewModel.ValidityYear;

            return CheckCardExpiration(month, year);
        }

        private static bool CheckCardExpiration(int month, int year)
        {
            var monthCheck = new Regex(@"^(0?[1-9]|1[0-2])$");
            var yearCheck = new Regex(@"^20[0-9]{2}$");

            if (!monthCheck.IsMatch(month.ToString())
                || !yearCheck.IsMatch(year.ToString()))
            {
                return false;
            }

            var daysInMonthExpiry = DateTime.DaysInMonth(year, month);
            var cardExpiry = new DateTime(year, month, daysInMonthExpiry, 23, 59, 59);

            return cardExpiry > DateTime.Now
                && cardExpiry < DateTime.Now.AddYears(CardExpiryYearMax);
        }
    }
}
