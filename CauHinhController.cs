using PTL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PTL.Lib;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Web_GiaSu.Controllers;

namespace Web_GiaSu.Areas.Admin.Controllers
{
    public class CauHinhController : Controller
    {
        private CauHinhLib _service = new CauHinhLib();
        #region Cấu hình
        [CustomAuthorize]
        public ActionResult DanhSachCauHinh()
        {
            ViewBag.Title = "Cấu hình";
            return View("DanhSachCauHinh");
        }

        private IEnumerable<CauHinhViewModel> GetOrders(CauHinhSearch filter)
        {
            var model = _service.CauHinhGrid(filter);
            return model;
        }

        public ActionResult ReadDanhSachCauHinh([DataSourceRequest]DataSourceRequest request, int? IdNhomCauHinhSearch, string MaCauHinhSearch, string TenCauHinhSearch)
        {
            CauHinhSearch filter = new CauHinhSearch();
            filter.IdNhomCauHinhSearch = IdNhomCauHinhSearch;
            filter.MaCauHinhSearch = MaCauHinhSearch;
            filter.TenCauHinhSearch = TenCauHinhSearch;
            return Json(GetOrders(filter).ToDataSourceResult(request));
        }
        [CustomAuthorize]

        public ActionResult Add_CauHinh()
        {
            CauHinhViewModel model = new CauHinhViewModel();
            var viewname = "AddEdit_CauHinh";
            ViewBag.IsUpdate = (int)EnumAddEdit.Add;
            ViewBag.Title = "Thêm cấu hình";
            return View(viewname, model);
        }
        [CustomAuthorize]
        public ActionResult AddEdit_CauHinh(int ID)
        {
            CauHinhViewModel model = new CauHinhViewModel();
            var query = _service.GetCauHinhTheoId(ID);
            ViewBag.IsUpdate = (int)EnumAddEdit.Edit;
            ViewBag.Title = "Cập nhật cấu hình";
            return View(query);
        }
        [HttpPost]
        public ActionResult AddEdit_CauHinh(CauHinhViewModel model, int IsUpdate)
        {
            string mess = "";
            ViewBag.IsUpdate = IsUpdate;
            ViewBag.Title = IsUpdate == (int)EnumAddEdit.Add ? "Thêm cấu hình" : "Cập nhật cấu hình";
            ResultModel rs = new ResultModel();
            try
            {

                if (ModelState.IsValid)
                {
                    string kq = _service.Add_EditCauHinh(model, IsUpdate);
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
        public ActionResult DeleteCauHinh(int ID)
        {
            ResultModel rs = new ResultModel();
            string kq = _service.XoaCauHinh(ID);
            string mess = "";
            if (string.IsNullOrEmpty(kq))
            {
                rs.success = true;
                rs.message = "Xóa cấu hình thành công";
            }
            else
            {
                rs.message = "Xóa cấu hình thất bại";
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
                    string mess = _service.DeleteAllCauHinh(pID);
                    if (!string.IsNullOrEmpty(mess))
                    {
                        rs.error = true;
                        rs.message = mess;
                    }
                    else
                    {
                        rs.success = true;
                        rs.message = "Xóa cấu hình thành công";
                    }
                }
                else
                {
                    rs.error = true;
                    rs.message = "Chưa cấu hình";
                }
            }
            catch (Exception ex)
            {
                rs.message = ex.Message;
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Cài đặt cấu hình
        public ActionResult CaiDatCauHinh()
        {
            ViewBag.Title = "Cài đặt cấu hình";
            var model = _service.CaiDatCauHinh();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddEdit_CaiDatCauHinh(FormCollection request)
        {
            string mess = "";
            ViewBag.Title = "Cài đặt cấu hình";
            List<CaiDatCauHinhViewModel> lst = new List<CaiDatCauHinhViewModel>();
            foreach (var key in request.AllKeys)
            {
                CaiDatCauHinhViewModel model = new CaiDatCauHinhViewModel();
                model.MaCauHinh = key;
                model.Value = request[key];
                lst.Add(model);
            }
            ResultModel rs = new ResultModel();
            try
            {
                if (ModelState.IsValid)
                {
                    string kq = _service.AddEdit_CaiDatCauHinh(lst);
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
        #endregion
    }
}