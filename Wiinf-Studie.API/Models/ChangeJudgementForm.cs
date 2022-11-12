using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wiinf_Studie.API.Models
{
    public class ChangeJudgementForm : IValidatableObject
    {
        public string? Judgement { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();

            // Validate Judgement
            if (string.IsNullOrEmpty(Judgement) || !IsValidJudgement(Judgement))
                result.Add(new ValidationResult("The provided value for judgment is invalid."));

            return result;
        }

        private static bool IsValidJudgement(string judgement)
        {
            return Judgments.Allowed.Contains(judgement);
        }
    }
}