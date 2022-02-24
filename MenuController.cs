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
    public class MenuController : Controller
    {
        private MenuLib _service = new MenuLib();
        private TinTucLib _dmtt = new TinTucLib();
        private TinTucLib _tintuc = new TinTucLib();
        private BaiVietLib _baiviet = new BaiVietLib();
        #region Load danh sách
        // GET: Admin/Menu
        [CustomAuthorize]
        public ActionResult DanhSachMenu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GridDanhSachMenuRead([DataSourceRequest] DataSourceRequest request)
        {
            var data = _service.lstMenuForGird();
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadMenuCha(int? idNhom, int? excpIdMenu)
        {
            var data = _service.LoadMenuCha(idNhom, excpIdMenu);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadLienKet(int? pEnumLoaiMenu)
        {
            List<DropdownModel> data = new List<DropdownModel>();

            try
            {
                bool IsHienThi = true;
                switch (pEnumLoaiMenu)
                {
                    case (int)global::PTL.Lib.EnumLoaiMenu.TinTuc:

                        data = _tintuc.DanhSachTinTuc(IsHienThi).Select(x => new DropdownModel
                        {
                            Value = x.Id,
                            Text = x.TieuDe
                        }).ToList();
                        break;
                    case (int)global::PTL.Lib.EnumLoaiMenu.BaiViet:
                        BaiVietSearchModel filter = new BaiVietSearchModel();
                        filter.IsHienThiSearch = true;
                        data = _baiviet.DanhSachBaiViet(/*filter*/).Select(x => new DropdownModel
                        {
                            Value = x.Id,
                            Text = x.TieuDe
                        }).ToList();
                        break;
                    case (int)global::PTL.Lib.EnumLoaiMenu.DanhMucTinTuc:
                        data = _dmtt.LoadDanhSachDMTinTucForDropdown().Select(x => new DropdownModel
                        {
                            Value = x.Id,
                            Text = x.TenDanhMuc
                        }).ToList();
                        break;

                    case (int)global::PTL.Lib.EnumLoaiMenu.DanhMucSanPham:
                        break;

                    case (int)global::PTL.Lib.EnumLoaiMenu.LienKet:
                        break;
                }

            }
            catch (Exception ex)
            {

            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Thêm + Sửa
        [CustomAuthorize]
        public ActionResult AddMenu(string prefix)
        {
            ViewBag.IsAdd = true;
            MenuViewModel model = new MenuViewModel();
            model.Id = 0;
            ViewData.TemplateInfo.HtmlFieldPrefix = prefix;
            return View("AddEdit_Menu", model);
        }

        [CustomAuthorize]
        public ActionResult EditMenu(int Id, string prefix)
        {
            ViewBag.IsAdd = false;
            ViewData.TemplateInfo.HtmlFieldPrefix = prefix;
            MenuViewModel model = _service.GetMenuForId(Id);
            return View("AddEdit_Menu", model);
        }
        [HttpPost]
        public ActionResult AddEdit_Menu([Bind(Prefix = "prefix")] MenuViewModel model)
        {
            ResultModel rs = new ResultModel();
            var title = model.Id == 0 ? "Thêm menu" : "Cập nhật menu";
            if (ModelState.IsValid)
            {
                string mess = _service.Add_EditMenu(model);
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
        public ActionResult DeleteOneMenu(int id)
        {
            ResultModel rs = new ResultModel();
            try
            {
                if (id != null)
                {
                    string mess = _service.DeleteMenu(id);
                    if (string.IsNullOrEmpty(mess))
                    {
                        rs.success = true;
                        rs.message = "Xóa menu thành công";
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
                    rs.message = "Chưa chọn menu";
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
                        int count = pID.Count();
                        rs.success = true;
                        rs.message = "Đã xóa " + count + " menu";
                    }
                }
                else
                {
                    rs.error = true;
                    rs.message = "Chưa chọn menu";
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