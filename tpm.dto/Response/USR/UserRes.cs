using tpm.dto.admin.Common;
using System;

namespace tpm.dto.admin.Response
{
    public class UserRes
    {
        public int UserID { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
