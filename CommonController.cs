using PTL.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTL.Controllers
{
    public class CommonController
    {
        // GET: Common
        // Biến dùng chung
        #region Const
        public string login()
        {
            return "login".ToString();
        }

        #endregion

        public string getCurrentUsername()
        {
            return HttpContext.Current.Request.Cookies[this.login()].Value;
        }

        private BaseLib _bll = new BaseLib();
        public string GetCauHinh(string MaCauHinh)
        {
            string Value = "";
            try
            {
                var rs = _bll.Context.CaiDatCauHinhs.Where(x => x.MaCauHinh == MaCauHinh)?.FirstOrDefault();
                if (rs != null)
                {
                    Value = rs.Value;
                }
            }
            catch (Exception ex)
            {

            }
            return Value;
        }
        
    }
}