using SancionadosSAGB2025.Shared.Moral;
using SancionadosSAGB2025.Shared.Sanciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SancionadosSAGB2025.Shared.Validadores
{

    public class CurpValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var curp = value as string;

            if (string.IsNullOrWhiteSpace(curp))
                return ValidationResult.Success;
            // O return new ValidationResult("La CURP es obligatoria."); 
            // pero lo normal es que [Required] se encargue de eso.

            if (curp.Length != 18)
            {
                return new ValidationResult(
                    ErrorMessage ?? "La CURP debe tener exactamente 18 caracteres.",
                    new[] { validationContext.MemberName! });
            }

            // Aquí podrías meter regex oficial de CURP
            // if (!Regex.IsMatch(curp, "^[A-Z]{4}[0-9]{6}[HM][A-Z]{5}[0-9]{2}$")) ...

            return ValidationResult.Success;
        }
    }


    public class RfcValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            var data = (DatosGeneralesMorales)validationContext.ObjectInstance;


            if (data.Rfc!.Length != 12 && !string.IsNullOrEmpty(data.Rfc))
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName! });
            }

            return ValidationResult.Success;
        }
    }

    public class RfcValidationCharactersMoral : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null) return ValidationResult.Success;

            string? rfc = value.ToString();
            var rfcRegex = new Regex(@"\b[A-Za-zÑñ&]{3}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|[12][0-9]|3[01])");

            if (string.IsNullOrWhiteSpace(rfc) || rfc.Length != 12 || !rfcRegex.IsMatch(rfc))
            {
                return new ValidationResult(ErrorMessage ?? "El RFC no es válido");
            }

            return ValidationResult.Success;           
        }
    }

    public class RfcValidationCharactersFisica : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null) return ValidationResult.Success;

            string? rfc = value.ToString();
            var rfcRegex = new Regex(@"^[A-ZÑ&]{3,4}\d{6}[A-Z0-9]{3}$");

            if (string.IsNullOrWhiteSpace(rfc) || rfc.Length != 13 || !rfcRegex.IsMatch(rfc))
            {
                return new ValidationResult(ErrorMessage ?? "El RFC no es válido");
            }

            return ValidationResult.Success;
        }
    }
    public class LetrasNumerosEspaciosAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success; // Permite nulos, si quieres cambiarlo, agrega Required aparte

            string? texto = value.ToString();

            if (string.IsNullOrWhiteSpace(texto))
                return ValidationResult.Success; // Permite cadenas vacías si lo deseas

            // Regex que acepta solo letras (mayúsculas y minúsculas, incluyendo acentos y Ñ), números y espacios
            var regex = new Regex(@"^[a-zA-ZÁÉÍÓÚáéíóúÑñ0-9 ]+$");

            if (!regex.IsMatch(texto))
            {
                return new ValidationResult(ErrorMessage ?? "El campo contiene caracteres inválidos. Solo se permiten letras, números y espacios.");
            }

            return ValidationResult.Success;
        }
    }
}
