using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.dto.admin;
using tpm.dto.admin.Response;

namespace tpm.business
{
    public interface IEmployeeService : IDisposable
    {
        bool Create(EmployeeCreateReq objReq, out int EmployeeID);
        bool Update(EmployeeCreateReq objReq, int EmployeeID);
        bool Delete(int EmployeeID);
        IEnumerable<EmployeeRes> ReadAll();
        IEnumerable<EmployeeRes> GetEmployeesByID(int EmployeeID);
        IEnumerable<EmployeeRes> GetEmployeesWithTypeName();
    }
}
