using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 服务接口返回结构类(无数据返回)
    /// </summary>
    public class ExecutionResult
    {
        /// <summary>
        /// 执行结果构造函数
        /// </summary>
        public ExecutionResult()
        {
            IsSuccess = false;
            Message = "";
        }

        /// <summary>
        /// 执行是否成功,true为成功,false为失败
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 执行结果信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 登录状态
        /// </summary>
        //[DefaultValue(true)]
        //public bool LoginStatus { get; set; }
        private bool _loginStatus = true;
        public bool LoginStatus
        {
            get { return _loginStatus; }
            set { _loginStatus = value; }
        }
        /// <summary>
        /// 拼装执行结果
        /// </summary>
        /// <param name="isSuccess">执行成功 true，执行失败 false</param>
        /// <param name="message">执行结果信息</param>
        public ExecutionResult(bool isSuccess, string message = "")
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        /// <summary>
        /// 返回执行失败
        /// </summary>
        /// <param name="message">执行结果信息</param>
        /// <returns></returns>
        public static ExecutionResult Fail(string message = "error")
        {
            return new ExecutionResult(false, message);
        }
        /// <summary>
        /// 返回执行成功
        /// </summary>
        /// <param name="message">执行结果信息</param>
        /// <returns></returns>
        public static ExecutionResult Success(string message = "success")
        {
            return new ExecutionResult(true, message);
        }
    }
}
