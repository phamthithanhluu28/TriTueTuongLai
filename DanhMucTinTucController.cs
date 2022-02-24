using Kendo.Mvc.UI;
using PTL.Lib;
using PTL.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Web_GiaSu.Controllers;

namespace Web_GiaSu.Areas.Admin.Controllers
{
    public class DanhMucTinTucController : Controller
    {
        private TinTucLib _dmtintuc = new TinTucLib();
        // GET: Admin/DanhMucTinTuc
        [CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [CustomAuthorize]
        public ActionResult DanhSachDanhMucTinTuc()
        {
            ViewBag.Title = "Danh mục tin tức";
            return View("DanhSachDMTinTuc");
        }

        public ActionResult DanhSachDMTinTucForDropdown()
        {
            var data = _dmtintuc.LoadDanhSachDMTinTucForDropdown();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<DanhMucTinTucViewModel> GetOrders(DanhMucTinTucSearchModel filter)
        {
            var model = _dmtintuc.DanhSachDMTinTuc(filter);
            return model;
        }

        public ActionResult ReadDanhSachDMTinTuc([DataSourceRequest]DataSourceRequest request, DanhMucTinTucSearchModel filter)
        {
            return Json(GetOrders(filter).ToDataSourceResult(request));
        }
        [CustomAuthorize]
        public ActionResult Add_DMTinTuc()
        {
            DanhMucTinTucViewModel model = new DanhMucTinTucViewModel();
            var viewname = "AddEdit_DMTinTuc";
            ViewBag.IsUpdate = (int)EnumAddEdit.Add;
            return View(viewname, model);
        }
        [CustomAuthorize]
        public ActionResult AddEdit_DMTinTuc(int ID)
        {
            DanhMucTinTucViewModel model = new DanhMucTinTucViewModel();
            var query = _dmtintuc.GetDanhMucTinTucTheoId(ID);
            ViewBag.IsUpdate = (int)EnumAddEdit.Edit;
            return View(query);
        }
        [HttpPost]
        public ActionResult AddEdit_DMTinTuc(DanhMucTinTucViewModel model, int IsUpdate)
        {
            string mess = "";
            ViewBag.IsUpdate = IsUpdate;
            ViewBag.Title = IsUpdate == (int)EnumAddEdit.Add ? "Thêm danh mục tin tức" : "Cập nhật danh mục tin tức";
            ResultModel rs = new ResultModel();
            try
            {

                if (ModelState.IsValid)
                {
                    string kq = _dmtintuc.Add_EditDMTinTuc(model, IsUpdate);
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
        public ActionResult DeleteDMTinTuc(int ID)
        {
            ResultModel rs = new ResultModel();
            string kq = _dmtintuc.XoaDMTinTuc(ID);
            string mess = "";
            if (string.IsNullOrEmpty(kq))
            {
                rs.success = true;
                rs.message = "Xóa danh mục tin tức thành công";
            }
            else
            {
                rs.message = "Xóa danh mục tin thất bại";
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
                    string mess = _dmtintuc.DeleteAll(pID);
                    if (!string.IsNullOrEmpty(mess))
                    {
                        rs.error = true;
                        rs.message = mess;
                    }
                    else
                    {
                        rs.success = true;
                        rs.message = "Xóa danh mục tin tức thành công";
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