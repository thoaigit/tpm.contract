using Autofac.Features.Indexed;
using CoC.Business.DTO;
using Core.DataAccess.Interface;
using Core.DTO.Response;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using tpm.dto.admin.Response;

namespace tpm.business
{
    public class OutputFastSaleService : IOutputFastSaleService
    {
        protected Lazy<IRepository> _objRepositoryONL;
        protected Lazy<IReadOnlyRepository> _objReadOnlyRepositoryONL;
        protected Lazy<IRepository> _objRepositorySCM;
        protected IPriceService _priceService;
        private bool _disposedValue;

        #region Constructor
        public OutputFastSaleService(
            Lazy<IRepository> objRepository, Lazy<IReadOnlyRepository> objReadOnlyRepository,
            IIndex<string, Lazy<IRepository>> indexRepository)
        {
            _objRepositoryONL = objRepository;
            _objReadOnlyRepositoryONL = objReadOnlyRepository;
            _objRepositorySCM = indexRepository[ConnectionEnum.SCM.ToString()];
            //_socket = Core.Infrastructure.Engine.ContainerManager.Resolve<Quobject.SocketIoClientDotNet.Client.Socket>();
        }
        #endregion 

        public CRUDResult<OutputFastSaleReadByIDRes> ReadByID(int id, int userID, bool isSearchByCreatedUser)
        {
            OutputFastSaleReadByIDRes result = null;
            using (var multi = _objReadOnlyRepositoryONL.Value.Connection.QueryMultiple("[SCM].[OutputFastSale_ReadByID_Partners]", new
            {
                OFSID = id,
                UserID = userID,
                IsSearchByCreatedUser = isSearchByCreatedUser
            }, commandType: CommandType.StoredProcedure))
            {
                result = multi.ReadSingleOrDefault<OutputFastSaleReadByIDRes>();
                if (result == null) return new CRUDResult<OutputFastSaleReadByIDRes> { StatusCode = CRUDStatusCodeRes.ResourceNotFound };
                else
                {
                    result.Details = multi.Read<OutputFastSaleDetailReadByIDRes>();
                    int countCombo = result.Details.Where(t => !string.IsNullOrEmpty(t.ComboID) && !string.IsNullOrEmpty(t.ComboName) && t.ComboQuantity > 0).Count();
                    result.IsCombo = countCombo == result.Details.Count();
                    return new CRUDResult<OutputFastSaleReadByIDRes> { StatusCode = CRUDStatusCodeRes.Success, Data = result, };
                }
            }
        }
        public PagingResponse<OutputFastSaleSearchRes> Search(int userID, OutputFastSaleSearchReq Obj, bool isSearchByCreatedUser)
        {
            var param = new DynamicParameters();
            param.Add("@UserID", userID);
            param.Add("@Keyword", Obj.Keyword);
            param.Add("@OutputFastStatusIDsQuery", Obj.OutputFastStatusIDs.ToSQLSelectStatement<int>());
            //param.Add("@strStoreID", Obj.StoreIDs.ToSQLSelectStatement<int>());
            param.Add("@CreatedDateFrom", Obj.CreatedDateFrom);
            param.Add("@CreatedDateTo", Obj.CreatedDateTo);
            param.Add("@PageIndex", Obj.PageIndex);
            param.Add("@PageSize", Obj.PageSize);
            param.Add("@IsSearchByCreatedUser", isSearchByCreatedUser);

            var data = _objReadOnlyRepositoryONL.Value.StoreProcedureQuery<OutputFastSaleSearchRes>("SCM.OutputFastSale_Search4Partner", param)?.ToList();

            if (data == null || !data.Any())
            {
                return new PagingResponse<OutputFastSaleSearchRes>() { StatusCode = CRUDStatusCodeRes.ResourceNotFound };
            }
            return new PagingResponse<OutputFastSaleSearchRes>()
            {
                CurrentPageIndex = Obj.PageIndex,
                PageSize = Obj.PageSize,
                Records = data,
                TotalRecord = data?.FirstOrDefault().TotalRecord ?? 0,
                StatusCode = CRUDStatusCodeRes.Success
            };
        }
        public CRUDResult<bool> Cancel(int userID, string username, string fullname, int OFSID, string revokeTypeName)
        {
            string stepLog = "\r\nKhởi tạo tham số OutputFastSale_Cancel4Partner";
            var param = new DynamicParameters();
            param.Add("@OFSID", OFSID);
            param.Add("@UpdatedUserID", userID);
            param.Add("@RevokeUser", userID);
            param.Add("@RevokeUsername", username);
            param.Add("@RevokeFullname", fullname);
            param.Add("@RevokeTypeName", revokeTypeName);
            param.Add("@ManagerCodes", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            IDbTransaction tranSCM = null;
            using (var tran = _objRepositoryONL.Value.Connection.BeginTransaction())
            {
                try
                {
                    stepLog += "\r\nExecute OutputFastSale_Cancel4Partner";
                    var result = _objRepositoryONL.Value.Connection.Execute("[SCM].[OutputFastSale_Cancel4Partner]", param, tran,
                        commandType: CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        stepLog += "\r\nGET sManagerCodes";
                        var sManagerCodes = param.Get<string>("@ManagerCodes");
                        if (!string.IsNullOrEmpty(sManagerCodes))
                        {
                            CRUDResult<int> rstranSCM = null;
                            tranSCM = _objRepositorySCM.Value.Connection.BeginTransaction();
                            stepLog += "\r\nCập nhật PQT onl: updateIsPending4ProductGift";
                            rstranSCM = updateIsPending4ProductGift(ref tranSCM, userID, new List<string>(), false, sManagerCodes);
                            if (rstranSCM.Data <= 0)
                            {
                                stepLog += "\r\nRollback: tran";
                                tran.Rollback();
                                return new CRUDResult<bool> { StatusCode = CRUDStatusCodeRes.ResetContent, Data = false, ErrorMessage = "Không cập nhật được phiếu quà tặng" };
                            }
                            else
                            {
                                stepLog += "\r\nCommit: tranSCM";
                                tranSCM.Commit();
                            }
                        }
                        stepLog += "\r\nCommit: tran";
                        tran.Commit();
                        return new CRUDResult<bool> { StatusCode = CRUDStatusCodeRes.Success, Data = true };
                    }
                    else
                    {
                        stepLog += "\r\nRollback: tranSCM";
                        tranSCM?.Rollback();
                        stepLog += "\r\nRollback: tran";
                        tran.Rollback();

                        return new CRUDResult<bool> { StatusCode = CRUDStatusCodeRes.ResetContent, ErrorMessage = "Dữ liệu chưa được cập nhật", Data = false };
                    }
                }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    tranSCM?.Rollback();
                    return new CRUDResult<bool>() { StatusCode = CRUDStatusCodeRes.ResetContent, Data = false, ErrorMessage = ex.Message };
                }
                catch (Exception objEx)
                {
                    tran.Rollback();
                    tranSCM?.Rollback();
                    throw (objEx);
                }
            }
        }

        private CRUDResult<int> updateIsPending4ProductGift(ref IDbTransaction tran, int userID, List<string> codeCards, bool isPending = true, string managerCodes = "")
        {
            try
            {
                string GiftCodes = string.Join(",", codeCards);
                var rowExec = _objRepositorySCM.Value.Connection.Execute("PRO.ProductGift_UpdateIsPendingFromONL", new
                {
                    ManagerCodes = managerCodes,
                    GiftCodes = GiftCodes,
                    IsPending = isPending,
                    UserID = userID,
                }, tran, commandType: CommandType.StoredProcedure);
                if (rowExec <= 0)
                {
                    tran?.Rollback();
                    return new CRUDResult<int> { StatusCode = CRUDStatusCodeRes.ResetContent, Data = -1, ErrorMessage = "Dữ liệu chưa được cập nhật" };
                }
                return new CRUDResult<int> { StatusCode = CRUDStatusCodeRes.ResetContent, Data = 1, };
            }
            catch (SqlException ex)
            {
                tran?.Rollback();
                return new CRUDResult<int>() { StatusCode = CRUDStatusCodeRes.ResetContent, Data = -1, ErrorMessage = ex.Message };
            }
            catch (Exception e)
            {
                tran?.Rollback();
                throw e;
            }
        }

        #region Destructor
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_objRepositoryONL.IsValueCreated)
                        _objRepositoryONL.Value.Dispose();

                    if (_objReadOnlyRepositoryONL.IsValueCreated)
                        _objReadOnlyRepositoryONL.Value.Dispose();

                    if (_objRepositorySCM.IsValueCreated)
                        _objRepositorySCM.Value.Dispose();
                    _priceService.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~OutputFastSaleService()
        {
            Dispose(false);
        }
        #endregion
    }
}
