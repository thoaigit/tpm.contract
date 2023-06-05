using CoC.Business.DTO;
using Core.DataAccess.Interface;
using Core.DTO.Response;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.dto.admin.MBM;
using tpm.dto.admin.Response.CTR;

namespace tpm.business
{
    public class ServiceService : IServiceService
    {
        private readonly Lazy<IRepository> _objRepository;
        private readonly Lazy<IReadOnlyRepository> _objReadOnlyRepository;
        private bool _disposedValue;

        public ServiceService(Lazy<IRepository> objRepository, Lazy<IReadOnlyRepository> objReadOnlyRepository)
        {
            _objRepository = objRepository;
            _objReadOnlyRepository = objReadOnlyRepository;
        }

        #region Create
        public CRUDResult<ServiceRes> Create(ServiceCreateReq obj)
        {
            try
            {
                // Tạo một đối tượng DynamicParameters để lưu trữ các tham số truyền vào stored procedure
                var param = new DynamicParameters();

                // Thêm các tham số với giá trị từ các thuộc tính của đối tượng obj truyền vào
                param.Add("@Service_ID", obj.Service_ID);
                param.Add("@Service_Name", obj.Service_Name);
                param.Add("@Unit", obj.Unit);
                param.Add("@Quantity", obj.Quantity);
                param.Add("@Unit_Price", obj.Unit_Price);
                param.Add("@Total_Amount", obj.Total_Amount);
                param.Add("@Service_Type_ID", obj.Service_Type_ID);

                // Còn các tham số khác nữa

                // Gọi stored procedure "[IDS].[Client_Create]" với các tham số đã được khai báo ở trên
                var storedProcedureResult = _objReadOnlyRepository.Value.StoreProcedureQuery<ServiceRes>("CTR.Service_Create", param);

                // Kiểm tra xem stored procedure có trả về kết quả và không rỗng
                if (storedProcedureResult != null && storedProcedureResult.Any())
                {
                    var createdClient = storedProcedureResult.First();

                    // Trả về kết quả thành công và đối tượng khách hàng vừa được tạo
                    return new CRUDResult<ServiceRes> { StatusCode = CRUDStatusCodeRes.Success, Data = createdClient };
                }

                // Trả về lỗi "Dữ liệu truyền vào không hợp lệ" nếu không tìm thấy đối tượng khách hàng trong kết quả trả về
                return new CRUDResult<ServiceRes> { StatusCode = CRUDStatusCodeRes.InvalidData, ErrorMessage = "Dữ liệu truyền vào không hợp lệ" };
            }
            catch (Exception ex)
            {
                // Trả về lỗi "Có lỗi xảy ra" nếu có lỗi trong quá trình thực thi stored procedure hoặc xảy ra lỗi khác
                return new CRUDResult<ServiceRes> { StatusCode = CRUDStatusCodeRes.Deny, ErrorMessage = "Có lỗi xảy ra" };
            }
        }

        #endregion
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_objRepository.IsValueCreated)
                        _objRepository.Value.Dispose();
                    if (_objReadOnlyRepository.IsValueCreated)
                        _objReadOnlyRepository.Value.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ServiceService()
        {
            Dispose(false);
        }
    }
}
