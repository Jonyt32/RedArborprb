using System.ComponentModel.DataAnnotations;

namespace WebApiRedArbor.Validations
{
    public class AgeValidationAttribute: ValidationAttribute
    {
        private readonly int _minAge;
        private readonly int _maxAge;

        public AgeValidationAttribute()
        {
            // Leer las variables de entorno o valores de configuración
            _minAge = int.Parse(Environment.GetEnvironmentVariable("MAX_AGE") ?? "-65");
            _maxAge = int.Parse(Environment.GetEnvironmentVariable("MIN_AGE") ?? "-18");
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime birthdate) 
            {
                int age = DateTime.Now.Year - birthdate.Year;
                if (birthdate > DateTime.Now.AddYears(_minAge) || birthdate < DateTime.Now.AddYears(_maxAge)) 
                {
                    return new ValidationResult("El empleado debe ser mayor de 18 años y menor de 65 años.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
