using System.Security.Claims;

namespace tpm.dto.admin
{
    public class UserPrincipal : ClaimsPrincipal
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

    }
}
