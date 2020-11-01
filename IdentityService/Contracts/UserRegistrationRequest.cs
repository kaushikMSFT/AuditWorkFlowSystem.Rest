namespace IdentityService.Contracts
{
    using System.ComponentModel.DataAnnotations;

    namespace IdentityService.Contracts.V1
    {
        public class UserRegistrationRequest
        {
            [EmailAddress]
            public string Email { get; set; }

            public string Password { get; set; }

            public string userType { get; set; }
        }

       
    }
}