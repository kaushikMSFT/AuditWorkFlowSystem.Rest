using Microsoft.AspNetCore.Identity;

namespace ClientAPI.Domain
{
    public class ClientUser:IdentityUser
    {
        public string Name { get; set; }
        //public string Username;
    }
}