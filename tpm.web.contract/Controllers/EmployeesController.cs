﻿using Core.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using tpm.business;
using tpm.dto.admin;

namespace tpm.web.contract.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeCreateReqValidator _validator;
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeTypeService _employeeTypeService;
        private readonly IPositionService _positionService;
        private readonly IDepartmentService _departmentService;
        private readonly IGenderService _genderService;

        public EmployeesController(EmployeeCreateReqValidator validator,IEmployeeService employeeService,IEmployeeTypeService employeeTypeService,IPositionService positionService, IDepartmentService departmentService, IGenderService genderService)
        {
            _validator = validator;
            _employeeService = employeeService;
            _employeeTypeService = employeeTypeService;
            _positionService = positionService;
            _departmentService = departmentService;
            _genderService = genderService;

        }
        public IActionResult Index()
        {
            var employees = _employeeService.GetEmployeesWithTypeName();
            var employeeTypes = _employeeTypeService.GetAllEmployees();
            var positions = _positionService.GetAllPositions();
            var departments = _departmentService.GetAllDepartments();
            var genders = _genderService.GetAllGenders();


            ViewBag.Employees = employees;
            ViewBag.EmployeeTypes = employeeTypes;
            ViewBag.Positions = positions;
            ViewBag.Departments = departments;
            ViewBag.Genders = genders;
            return View();
        }

        [HttpGet]
        public IActionResult GetEmployee(int EmployeeID)
        {
            var getEmployee = _employeeService.GetEmployeesByID(EmployeeID);

            return Json(new { Employee = getEmployee });
        }


        #region Create Post
        [HttpPost]
        public JsonResult Create(EmployeeCreateReq objReq)
        {
            try
            {
                int newEmployeeID = 0;

                bool result = _employeeService.Create(objReq, out newEmployeeID);

                if (result)
                {
                    // Gọi phương thức GetServiceById để lấy thông tin dịch vụ mới
                    var newEmployee = _employeeService.GetEmployeesByID(newEmployeeID);

                    return Json(new
                    {
                        objCodeStep = new
                        {
                            Status = CRUDStatusCodeRes.Success,
                            Message = "Tạo mới thành công"
                        },
                        Employee = newEmployee // Trả về thông tin dịch vụ mới
                    });
                }
                else
                {
                    return Json(new
                    {
                        objCodeStep = new
                        {
                            Status = CRUDStatusCodeRes.Deny,
                            Message = "Tạo mới không thành công"
                        }
                    });
                }
            }
            catch (Exception objEx)
            {
                return Json(new
                {
                    success = false,
                    message = "Có lỗi xảy ra khi thực hiện tạo mới: " + objEx.Message
                });
            }

        }
        #endregion

        #region Update
        [HttpPost]
        public IActionResult Update(EmployeeCreateReq objReq, int EmployeeID)
        {
            try
            {
                bool result = _employeeService.Update(objReq, EmployeeID);

                if (result)
                {
                    // Gọi phương thức GetServiceById để lấy thông tin dịch vụ mới
                    var updateEmployee = _employeeService.GetEmployeesByID(EmployeeID);

                    return Json(new
                    {
                        objCodeStep = new
                        {
                            Status = CRUDStatusCodeRes.Success,
                            Message = "Tạo mới thành công"
                        },
                        Employee = updateEmployee // Trả về thông tin dịch vụ mới
                    });
                }
                else
                {
                    return Json(new
                    {
                        objCodeStep = new
                        {
                            Status = CRUDStatusCodeRes.Deny,
                            Message = "Tạo mới không thành công"
                        }
                    });
                }
            }
            catch (Exception objEx)
            {
                return Json(new
                {
                    success = false,
                    message = "Có lỗi xảy ra khi thực hiện tạo mới: " + objEx.Message
                });
            }
        }


        #endregion

        #region Delete
        [HttpDelete]
        public JsonResult Delete(int EmployeeID)
        {
            try
            {
                // Gọi phương thức xóa dịch vụ từ service
                bool deleteResult = _employeeService.Delete(EmployeeID);

                if (deleteResult)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Xóa dịch vụ không thành công!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra trong quá trình xóa dịch vụ!", error = ex.Message });
            }
        }
        #endregion

        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }

    }
}
