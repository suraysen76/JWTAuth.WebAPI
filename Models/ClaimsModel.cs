using System.Security.Claims;

namespace JWTAuth.WebAPI.Models
{
    public class ClaimsModel
    {
        public string? Token { get; set; }
        public List<ClaimModel>? Claims { get; set; }
    }
    public class ClaimModel
    {
        public string? Type { get; set; }
        public string? Value { get; set; }
    }
}
