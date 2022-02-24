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
    public class TinTucController : Controller
    {
        private TinTucLib _tintuc = new TinTucLib();

        #region Load danh sách
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [CustomAuthorize]
        public ActionResult DanhSachTinTuc()
        {

            return View("DanhSachTinTuc");
        }

        private List<TinTucViewModel> GetOrders()
        {
            List<TinTucViewModel> model = new List<TinTucViewModel>();
            model = _tintuc.DanhSachTinTuc();
            return model;
        }

        public ActionResult ReadDanhSachTinTuc([DataSourceRequest]DataSourceRequest request)
        {
            return Json(GetOrders().ToDataSourceResult(request));
        }
        #endregion

        #region Thêm + sửa
        [CustomAuthorize]
        public ActionResult Add_TinTuc()
        {
            TinTucViewModel model = new TinTucViewModel();
            var viewname = "AddEdit_TinTuc";
            ViewBag.IsUpdate = (int)EnumAddEdit.Add;
            ViewBag.Title = "Thêm tin tức";
            return View(viewname, model);
        }

        [CustomAuthorize]
        public ActionResult AddEdit_TinTuc(int ID)
        {
            TinTucViewModel model = new TinTucViewModel();
            var query = _tintuc.GetTinTucTheoId(ID);
            ViewBag.IsUpdate = (int)EnumAddEdit.Edit;
            ViewBag.Title = "Cập nhật tin tức";
            return View(query);
        }
        [HttpPost]
        public ActionResult AddEdit_TinTuc(int IsUpdate, TinTucViewModel model, HttpPostedFileBase HinhDaiDien)
        {
            string mess = "";
            ViewBag.IsUpdate = IsUpdate;
            ViewBag.Title = IsUpdate == (int)EnumAddEdit.Add ? "Thêm tin tức" : "Cập nhật tin tức";
            ResultModel rs = new ResultModel();

            try
            {
                if (ModelState.IsValid)
                {
                    string kq = _tintuc.Add_EditTinTuc(model, HinhDaiDien, IsUpdate);
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
            if (rs.success == true)
            {
                return Json(rs, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View(model);
            }
        }
        #endregion

        #region Xóa
        [CustomAuthorize]
        public ActionResult DeleteTinTuc(int ID)
        {
            ResultModel rs = new ResultModel();
            try
            {
                string kq = _tintuc.XoaTinTuc(ID);
                string mess = "";
                if (kq == null)
                {
                    rs.success = true;
                    rs.message = "Xóa  tin tức thành công";
                }
                else
                {
                    rs.message = "Xóa tin thất bại";
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
                    string mess = _tintuc.DeleteAll(pID);
                    if (!string.IsNullOrEmpty(mess))
                    {
                        rs.error = true;
                        rs.message = mess;
                    }
                    else
                    {
                        rs.success = true;
                        rs.message = "Xóa tin tức thành công";
                    }
                }
                else
                {
                    rs.error = true;
                    rs.message = "Chưa chọn tin tức";
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