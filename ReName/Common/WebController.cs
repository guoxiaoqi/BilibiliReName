using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ReName.Common
{

    public abstract class WebController : BaseController
    {
        #region 属性
        private string CurrentUserKey = ConfigurationManager.AppSettings["CurrentUserKey"];
        #endregion
    }
}
