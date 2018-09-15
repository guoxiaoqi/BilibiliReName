using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ReName.Common
{
    /// <summary>
    /// 控制器基类，各控制器都集成该类
    /// </summary>
    public abstract class BaseController : Controller
    {
        static readonly Type TypeOfCurrent = typeof(BaseController);
        static readonly Type TypeOfDisposableAttribute = typeof(DisposableAttribute);

        #region 资源释放
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this.DisposeMembers();
        }
        /// <summary>
        /// 扫描对象内所有带有 DisposableAttribute 标记并实现了 IDisposable 接口的属性和字段，执行其 Dispose() 方法
        /// </summary>
        void DisposeMembers()
        {
            Type type = this.GetType();

            List<PropertyInfo> properties = new List<PropertyInfo>();
            List<FieldInfo> fields = new List<FieldInfo>();

            Type searchType = type;

            while (true)
            {
                properties.AddRange(searchType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(a =>
                {
                    if (typeof(IDisposable).IsAssignableFrom(a.PropertyType))
                    {
                        return a.CustomAttributes.Any(c => c.AttributeType == TypeOfDisposableAttribute);
                    }

                    return false;
                }));

                fields.AddRange(searchType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public |
                    BindingFlags.DeclaredOnly).Where(a =>
                    {
                        if (typeof(IDisposable).IsAssignableFrom(a.FieldType))
                        {
                            return a.CustomAttributes.Any(c => c.AttributeType == TypeOfDisposableAttribute);
                        }

                        return false;
                    }));

                if (searchType == TypeOfCurrent)
                    break;
                else
                    searchType = searchType.BaseType;
            }

            foreach (var pro in properties)
            {
                IDisposable val = pro.GetValue(this) as IDisposable;
                if (val != null)
                    val.Dispose();
            }

            foreach (var field in fields)
            {
                IDisposable val = field.GetValue(this) as IDisposable;
                if (val != null)
                    val.Dispose();
            }
        }
        #endregion

        #region 数据返回封装
        /// <summary>
        /// 返回成功状态
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected JsonResult Success(object data)
        {
            ExecutionResult<object> result = ExecutionResult<object>.Success("success", data);
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 返回成功状态
        /// </summary>
        /// <param name="msg">信息</param>
        /// <returns></returns>
        protected JsonResult Success(string msg = "success")
        {
            ExecutionResult result = ExecutionResult.Success(msg);
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 返回成功状态
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected JsonResult Success(string msg, object data,object Summary=null)
        {
            ExecutionResult<object> result = ExecutionResult<object>.Success(msg, data,Summary);
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 返回失败状态
        /// </summary>
        /// <param name="msg">信息</param>
        /// <returns></returns>
        protected JsonResult Failed(string msg = "error")
        {
            ExecutionResult retResult = ExecutionResult.Fail(msg);
            return this.Json(retResult, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 返回失败状态
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected JsonResult Failed(object data)
        {
            ExecutionResult<object> retResult = ExecutionResult<object>.Fail("error", data);
            return this.Json(retResult, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 返回失败状态
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected JsonResult Failed(string msg, object data)
        {
            ExecutionResult<object> retResult = ExecutionResult<object>.Fail(msg, data);
            return this.Json(retResult, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}