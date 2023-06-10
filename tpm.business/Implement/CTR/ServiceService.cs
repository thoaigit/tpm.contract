using CoC.Business.DTO;
using Core.DataAccess.Interface;
using Core.DTO.Response;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.dto.admin;
using tpm.dto.admin.MBM;
using tpm.dto.admin.Response;

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

        public IEnumerable<ServiceRes> List()
        {
            return ReadAll();
        }


        public IEnumerable<ServiceRes> ReadAll()
        {
            var result = _objReadOnlyRepository.Value.StoreProcedureQuery<ServiceRes>("CTR.Service_ReadAll");
            if (result == null)
            {
                result = new List<ServiceRes>();
            }
            return result;
        }


        public IEnumerable<ServiceRes> GetServicesWithTypeName()
        {
            return _objReadOnlyRepository.Value.StoreProcedureQuery<ServiceRes>("CTR.GetServicesWithTypeName");
        }


        



        #region Create
        public bool Create(ServiceCreateReq obj)
        {
            try
            {
                // Tạo một đối tượng DynamicParameters để lưu trữ các tham số truyền vào stored procedure
                var param = new DynamicParameters();

                // Thêm các tham số với giá trị từ các thuộc tính của đối tượng obj truyền vào             
                param.Add("@Unit_ID", obj.Unit_ID);
                param.Add("@Quantity", obj.Quantity);
                param.Add("@Unit_Price", obj.Unit_Price);
                param.Add("@Total_Amount", obj.Total_Amount);
                param.Add("@Service_Type_ID", obj.Service_Type_ID);

                // Thực hiện gọi stored procedure để thêm dữ liệu vào database
                var storedProcedureResult = _objReadOnlyRepository.Value.Connection.Execute("CTR.Service_Create", param, commandType: CommandType.StoredProcedure);

                // Kiểm tra số dòng trả về
                if (storedProcedureResult > 0)
                {
                    // Trả về kết quả thành công
                    return true;
                }

                // Trả về false nếu không tìm thấy dữ liệu trong kết quả trả về
                return false;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và ném ra ngoại lệ
                throw new Exception("Có lỗi xảy ra trong quá trình thực thi stored procedure.", ex);
            }
        }

        public bool Delete(int serviceId)
        {
            try
            {
                // Tạo một đối tượng DynamicParameters để lưu trữ các tham số truyền vào stored procedure
                var param = new DynamicParameters();

                // Thêm tham số với giá trị từ serviceId truyền vào
                param.Add("@Service_ID", serviceId);

                // Thực hiện gọi stored procedure để xóa dữ liệu trong database
                var storedProcedureResult = _objReadOnlyRepository.Value.Connection.Execute("CTR.Service_Delete", param, commandType: CommandType.StoredProcedure);

                // Kiểm tra số dòng trả về
                if (storedProcedureResult > 0)
                {
                    // Trả về kết quả thành công
                    return true;
                }

                // Trả về false nếu không tìm thấy dữ liệu trong kết quả trả về
                return false;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và ném ra ngoại lệ
                throw new Exception("Có lỗi xảy ra trong quá trình thực thi stored procedure.", ex);
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
