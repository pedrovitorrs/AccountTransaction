using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AccountTransaction.Account.API.DTO.Request
{
    public class AccountAddRequestDTO : BaseRequestDTO, IValidatableObject
    {
        [Required(ErrorMessage = "O nome do titular é obrigatório")]
        public string? Nome_Titular { get; set; }
        [Required]
        [StringLength(18, ErrorMessage = "O {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 14)]
        public string? Identificador_Titular { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if(!Regex.IsMatch(Nome_Titular, @"^((\b[A-zÀ-ú']{2,40}\b)\s*){2,}$"))
            {
                results.Add(new ValidationResult("Favor informar no mínimo nome e sobrenome", new string[] { nameof(Nome_Titular) }));
            }
            return results;
        }
    }
}
