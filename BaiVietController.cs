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
    public class BaiVietController : Controller
    {
        private BaiVietLib _service = new BaiVietLib();
        #region Load danh sách
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [CustomAuthorize]
        public ActionResult DanhSachBaiViet()
        {
            ViewBag.Title = "Danh sách bài viết";
            return View();
        }

        [CustomAuthorize]
        public ActionResult DanhSachBaiVietDropdown()
        {
            var data = _service.DanhSachBaiVietDropdown();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize]
        private IQueryable<BaiVietViewModel> GetOrders(BaiVietSearchModel filter)
        {
            IQueryable<BaiVietViewModel> model;
            model = _service.DanhSachBaiViet(filter);
            return model;
        }
        [CustomAuthorize]
        public ActionResult ReadDanhSachBaiViet([DataSourceRequest]DataSourceRequest request, string TieuDeSearch, int? IsHienThiSearch)
        {
            BaiVietSearchModel filter = new BaiVietSearchModel();
            filter.TieuDeSearch = TieuDeSearch;
            filter.IsHienThiSearch = IsHienThiSearch;
            return Json(GetOrders(filter).ToDataSourceResult(request));
        }
        #endregion

        #region Thêm + sửa
        [CustomAuthorize]
        public ActionResult AddBaiViet()
        {
            BaiVietViewModel model = new BaiVietViewModel();
            var viewname = "AddEdit_BaiViet";
            ViewBag.IsUpdate = (int)EnumAddEdit.Add;
            return View(viewname, model);
        }

        [CustomAuthorize]
        public ActionResult EditBaiViet(int pId)
        {
            BaiVietViewModel model = new BaiVietViewModel();
            var query = _service.GetBaiViet(pId);
            ViewBag.IsUpdate = (int)EnumAddEdit.Edit;
            return View(query);
        }
        [HttpPost]
        public ActionResult AddEdit_BaiViet(int IsUpdate, BaiVietViewModel model)
        {
            string mess = "";
            ViewBag.IsUpdate = IsUpdate;
            ViewBag.Title = IsUpdate == (int)EnumAddEdit.Add ? "Thêm bài viết" : "Cập nhật bài viết";
            ResultModel rs = new ResultModel();
            try
            {
                if (ModelState.IsValid)
                {
                    string kq = _service.Add_EditBaiViet(model, IsUpdate);
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

        #region Xóa
        [CustomAuthorize]
        public ActionResult DeleteBaiViet(int ID)
        {
            ResultModel rs = new ResultModel();
            try
            {
                string kq =  _service.XoaBaiViet(ID);
                string mess = "";
                if (kq == null)
                {
                    rs.success = true;
                    rs.message = "Xóa  bài viết thành công";
                }
                else
                {
                    rs.message = "Xóa bài viết thất bại";
                }
            }
            catch (Exception ex)
            {
                rs.message = ex.Message;

            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize]
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
                        rs.message = "Xóa bài viết thành công";
                    }
                }
                else
                {
                    rs.error = true;
                    rs.message = "Chưa chọn bài viết";
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