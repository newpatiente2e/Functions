using System.Collections.Generic;
using Newtonsoft.Json;

namespace Contoso.Healthcare.Api.Models
{
    public record Patient
    {
        [JsonProperty("id")]
        public string Id { get; set; } = "";
        public string? Iso { get; set; }
        public string? FamilyName { get; set; }
        public string? GivenNames { get; set; }
        public string? DateOfBirth { get; set; }
        public string? AddressStreet { get; set; }
        public string? AddressCity { get; set; }
        public string? AddressState { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? EmergencyName { get; set; }
        public string? EmergencyRelationship { get; set; }
        public string? EmergencyPhone { get; set; }
        public string? EmergencyEmail { get; set; }
        public List<Allergy> Allergies { get; set; } = new();
        public string? Date { get; set; }
        public string? AddressPostcode { get; internal set; }
        public string? Gender { get; set; }
        public string? Pronouns { get; set; }
        public bool IsApproved { get; set; }
        public bool PreferEmail { get; set; }
        public bool PreferText { get; set; }
        public bool PreferPhone { get; set; }
        public string? InsuranceNumber { get; set; }

        public static string GetId(string patiendId) => $"{patiendId}:complete";
    }
}
