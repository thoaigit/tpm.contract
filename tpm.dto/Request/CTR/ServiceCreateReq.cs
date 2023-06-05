using tpm.dto.admin.Common;
using tpm.dto.admin.Request.SCM;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace CoC.Business.DTO
{
    public class ServiceCreateReq : BaseDTO
    {
        public int Service_ID { get; set; }
        public string Service_Name { get; set; }
        public string Unit { get; set; }   
        public int Quantity { get; set; }   
        public decimal Unit_Price { get; set; }

        public decimal Total_Amount { get; set; }
        public int Service_Type_ID { get; set; }
        public int CreatedUser { get; set; }    
        public DateTime CreatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

    }
    public class ServiceCreateReqValidator : AbstractValidator<ServiceCreateReq>
    {
        public ServiceCreateReqValidator()
        {
            RuleFor(x => x.Service_Name).NotEmpty().WithMessage("Tên dịch vụ không được để trống");
            RuleFor(x => x.Unit).NotEmpty().WithMessage("Đơn vị không được để trống");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Số lượng phải lớn hơn 0");
            RuleFor(x => x.Unit_Price).GreaterThan(0).WithMessage("Đơn giá phải lớn hơn 0");
            RuleFor(x => x.Total_Amount).GreaterThan(0).WithMessage("Tổng giá trị phải lớn hơn 0");
            RuleFor(x => x.Service_Type_ID).GreaterThan(0).WithMessage("ID loại dịch vụ phải lớn hơn 0");
            RuleFor(x => x.CreatedUser).GreaterThan(0).WithMessage("Người tạo phải lớn hơn 0");
            RuleFor(x => x.CreatedDate).NotEmpty().WithMessage("Ngày tạo không được để trống");
            RuleFor(x => x.UpdatedUser).GreaterThan(0).WithMessage("Người cập nhật phải lớn hơn 0");
            RuleFor(x => x.UpdatedDate).NotEmpty().WithMessage("Ngày cập nhật không được để trống");
        }
    }
}
