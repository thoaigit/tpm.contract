using CoC.Business.DTO;
using Core.DataAccess.Interface;
using Core.DTO.Response;
using Dapper;
using Microsoft.AspNetCore.Mvc;
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
            var result = _objReadOnlyRepository.Value.StoreProcedureQuery<ServiceRes>("CTR.GetServicesWithTypeName");
            if (result == null)
            {
                result = new List<ServiceRes>();
            }
            return result;
        }

        public IEnumerable<ServiceRes> GetServicesByID(int Service_ID)
        {
            var result = _objReadOnlyRepository.Value.StoreProcedureQuery<ServiceRes>("CTR.GetServicesByID", new { Service_ID });
            if (result == null)
            {
                result = new List<ServiceRes>();
            }
            return result;
        }




        #region Create
        public bool Create(ServiceCreateReq objReq, out int newServiceID)
        {
            try
            {
                // Tạo một đối tượng DynamicParameters để lưu trữ các tham số truyền vào stored procedure
                var param = new DynamicParameters();

                // Thêm các tham số với giá trị từ các thuộc tính của đối tượng obj truyền vào             
                param.Add("@Unit_ID", objReq.Unit_ID);
                param.Add("@Quantity", objReq.Quantity);
                param.Add("@Unit_Price", objReq.Unit_Price);
                param.Add("@Total_Amount", objReq.Total_Amount);
                param.Add("@Service_Type_ID", objReq.Service_Type_ID);

                // Thực hiện gọi stored procedure để thêm dữ liệu vào database
                newServiceID = _objReadOnlyRepository.Value.Connection.ExecuteScalar<int>("CTR.Service_Create", param, commandType: CommandType.StoredProcedure);

                // Kiểm tra Service_ID mới
                if (newServiceID > 0)
                {
                    // Trả về kết quả thành công
                    return true;
                }

                // Trả về false nếu không tạo mới được dữ liệu
                return false;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và ném ra ngoại lệ
                throw new Exception("Có lỗi xảy ra trong quá trình thực thi stored procedure.", ex);
            }
        }
        #endregion





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
