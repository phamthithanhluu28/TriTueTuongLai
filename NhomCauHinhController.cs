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
    public class NhomCauHinhController : Controller
    {
        private CauHinhLib _service = new CauHinhLib();

        [CustomAuthorize]
        public ActionResult DanhSachNhomCauHinh()
        {
            ViewBag.Title = "Nhóm cấu hình";
            return View("DanhSachNhomCauHinh");
        }
        public ActionResult LoadNhomCauHinhCha(int? excIdNhom)
        {
            var data = _service.LoadNhomCauHinhCha(excIdNhom);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<NhomCauHinhViewModel> GetOrders(NhomCauHinhSearch filter)
        {
            var model = _service.NhomCauHinhGrid(filter);
            return model;
        }

        public ActionResult ReadDanhSachNhomCauHinh([DataSourceRequest]DataSourceRequest request, string TenNhomCauHinhSearch, int? IdNhomCauHinhChaSearch)
        {
            NhomCauHinhSearch filter = new NhomCauHinhSearch();
            filter.TenNhomCauHinhSearch = TenNhomCauHinhSearch;
            filter.IdNhomCauHinhChaSearch = IdNhomCauHinhChaSearch;

            return Json(GetOrders(filter).ToDataSourceResult(request));
        }
        [CustomAuthorize]

        public ActionResult Add_NhomCauHinh()
        {
            NhomCauHinhViewModel model = new NhomCauHinhViewModel();
            var viewname = "AddEdit_NhomCauHinh";
            ViewBag.IsUpdate = (int)EnumAddEdit.Add;
            ViewBag.Title = "Thêm mhóm cấu hình";
            return View(viewname, model);
        }
        [CustomAuthorize]
        public ActionResult AddEdit_NhomCauHinh(int ID)
        {
            NhomCauHinhViewModel model = new NhomCauHinhViewModel();
            var query = _service.GetNhomCauHinhTheoId(ID);
            ViewBag.IsUpdate = (int)EnumAddEdit.Edit;
            ViewBag.Title = "Cập nhật cấu hình";
            return View(query);
        }
        [HttpPost]
        public ActionResult AddEdit_NhomCauHinh(NhomCauHinhViewModel model, int IsUpdate)
        {
            string mess = "";
            ViewBag.IsUpdate = IsUpdate;
            ViewBag.Title = IsUpdate == (int)EnumAddEdit.Add ? "Thêm nhóm cấu hình" : "Cập nhật nhóm cấu hình";
            ResultModel rs = new ResultModel();
            try
            {

                if (ModelState.IsValid)
                {
                    string kq = _service.Add_EditNhomCauHinh(model, IsUpdate);
                    if (string.IsNullOrEmpty(kq))
                    {
                        rs.success = true;
                        rs.message = ViewBag.Title + " thành công";
                    }
                    else
                    {
                        rs.error = true;
                        rs.message = ViewBag.Title + " thất bại";
                    }
                }
                else
                {
                    rs.error = true;
                    rs.message = ViewBag.Title + " thất bại";
                }
            }
            catch (Exception ex)
            {
                rs.message = ex.Message;
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult DeleteNhomCauHinh(int ID)
        {
            ResultModel rs = new ResultModel();
            string kq = _service.XoaNhomCauHinh(ID);
            string mess = "";
            if (string.IsNullOrEmpty(kq))
            {
                rs.success = true;
                rs.message = "Xóa nhóm cấu hình thành công";
            }
            else
            {
                rs.message = kq;
            }
            ModelState.AddModelError(CommonModel.error, mess);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAll(List<int> pID)
        {
            ResultModel rs = new ResultModel();
            try
            {
                if (pID != null)
                {
                    string mess = _service.DeleteAllNhomCauHinh(pID);
                    if (!string.IsNullOrEmpty(mess))
                    {
                        rs.error = true;
                        rs.message = mess;
                    }
                    else
                    {
                        rs.success = true;
                        rs.message = "Xóa nhóm cấu hình thành công";
                    }
                }
                else
                {
                    rs.error = true;
                    rs.message = "Chưa chọn danh mục tin tức";
                }
            }
            catch (Exception ex)
            {
                rs.message = ex.Message;
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
    }
}