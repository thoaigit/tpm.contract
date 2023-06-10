using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpm.dto.admin.Response
{
    public class EmployeeRes
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public int PositionID { get; set; }
        public int DepartmentID { get; set; }
        public int NationalityID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int EmployeeTypeID { get; set; }
        public bool IsActived { get; set; }
    }
}
