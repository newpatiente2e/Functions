namespace Contoso.Healthcare.Api.Models
{
    public class Allergy
    {
        public string? Medication { get; set; }
        public string? Reaction { get; set; }
        public string? Severity { get; set; }

        public Allergy(string? medication, string? reaction, string? severity)
        {
            Medication = medication;
            Reaction = reaction;
            Severity = severity;
        }
    }
}
