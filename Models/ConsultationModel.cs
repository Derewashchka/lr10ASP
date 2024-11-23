using System.ComponentModel.DataAnnotations;

namespace lr10.Models
{
    public class ConsultationModel
    {
        [Required(ErrorMessage = "Будь ласка, введіть ім'я та прізвище")]
        [Display(Name = "Ім'я та прізвище")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть email")]
        [EmailAddress(ErrorMessage = "Некоректний формат email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Будь ласка, виберіть дату консультації")]
        [Display(Name = "Дата консультації")]
        [DataType(DataType.Date)]
        [FutureWorkingDayValidation(ErrorMessage = "Дата має бути майбутньою робочою датою")]
        public DateTime ConsultationDate { get; set; }

        [Required(ErrorMessage = "Будь ласка, виберіть продукт")]
        [Display(Name = "Продукт")]
        public string Product { get; set; }

        public static readonly string[] AvailableProducts = new[]
        {
            "JavaScript",
            "C#",
            "Java",
            "Python",
            "Основи"
        };
    }

    public class FutureWorkingDayValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                if (date.Date <= DateTime.Now.Date)
                    return false;

                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    return false;

                return true;
            }
            return false;
        }
    }

    public class NoBasicsOnMondayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (ConsultationModel)validationContext.ObjectInstance;

            if (model.Product == "Основи" && model.ConsultationDate.DayOfWeek == DayOfWeek.Monday)
            {
                return new ValidationResult("Консультації з 'Основи' не проводяться по понеділках");
            }

            return ValidationResult.Success;
        }
    }
}