using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PTL.Lib;
using PTL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_GiaSu.Controllers;

namespace Web_GiaSu.Areas.Admin.Controllers
{
    public class NhomMenuController : Controller
    {
        private MenuLib _service = new MenuLib();

        #region Load danh sách nhóm menu
        [CustomAuthorize]
        public ActionResult Index()
        {
            ViewBag.TitlePage = "Nhóm menu";
            return View("DanhSachNhomMenu");
        }
        [HttpPost]
        public ActionResult GridDanhSachNhomMenuRead([DataSourceRequest] DataSourceRequest request)
        {
            var data = _service.lstNhomMenu();
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DanhSachNhomMenuForDropdown()
        {
            var data = _service.lstNhomMenuForDropDown();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Thêm + sửa
        [CustomAuthorize]
        public ActionResult AddNhomMenu()
        {
            ViewBag.IsAdd = true;
            NhomMenuViewModel model = new NhomMenuViewModel();
            model.Id = 0;
            return View("AddEdit_NhomMenu", model);
        }

        [CustomAuthorize]
        public ActionResult EditNhomMenu(int Id)
        {
            ViewBag.IsAdd = false;

            NhomMenuViewModel model = _service.GetNhomMenu(Id);
            return View("AddEdit_NhomMenu", model);
        }
        [HttpPost]
        public ActionResult AddEdit_NhomMenu(NhomMenuViewModel model)
        {
            ResultModel rs = new ResultModel();
            var title = model.Id == 0 ? "Thêm nhóm menu" : "Cập nhật nhóm menu";
            if (ModelState.IsValid)
            {
                string mess = _service.Add_EditNhomMenu(model);
                if (string.IsNullOrEmpty(mess))
                {
                    rs.success = true;
                    rs.message = title + " thành công";
                }
                else
                {
                    rs.error = true;
                    rs.message = title + " thất bại";
                }
            }
            else
            {
                rs.message = title + " thất bại";
                rs.error = true;
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Xóa
        [HttpPost]
        public ActionResult DeleteOneNhomMenu(int id)
        {
            ResultModel rs = new ResultModel();
            try
            {
                if (id != null)
                {
                    string mess = _service.DeleteNhomMenu(id);
                    if (string.IsNullOrEmpty(mess))
                    {
                        rs.success = true;
                        rs.message = "Xóa nhóm menu thành công";
                    }
                    else
                    {
                        rs.error = true;
                        rs.message = mess;
                    }
                }
                else
                {
                    rs.error = true;
                    rs.message = "Chưa chọn nhóm menu";
                }
            }
            catch (Exception ex)
            {
                rs.message = ex.Message;
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAll(List<int> pID)
        {
            ResultModel rs = new ResultModel();
            try
            {
                if (pID != null)
                {
                    string mess = _service.DeleteAll(pID);
                    if (!string.IsNullOrEmpty(mess))
                    {
                        rs.error = true;
                        rs.message = mess;
                    }
                    else
                    {
                        rs.success = true;
                        rs.message = "Xóa nhóm menu thành công";
                    }
                }
                else
                {
                    rs.error = true;
                    rs.message = "Chưa chọn nhóm menu";
                }
            }
            catch (Exception ex)
            {
                rs.message = ex.Message;
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}