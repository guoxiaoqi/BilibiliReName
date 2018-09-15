using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common
{
    /// <summary>
    /// 服务接口返回结构类
    /// </summary>
    /// <typeparam name="T">返回数据模型</typeparam>
    public class ExecutionResult<T> : ExecutionResult
    {
        /// <summary>
        /// 执行结果构造函数
        /// </summary>
        public ExecutionResult()
            :base()
        {
        }

        /// <summary>
        /// 执行结果数据
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 数据汇总信息
        /// </summary>
        public object Summary { get; set; }

        /// <summary>
        /// 拼装执行结果
        /// </summary>
        /// <param name="isSuccess">执行成功 true，执行失败 false</param>
        /// <param name="message">执行结果信息</param>
        /// <param name="data">执行结果数据</param>
        public ExecutionResult(bool isSuccess, string message = "", T data = default(T),object Summary=null)
            : base(isSuccess, message)
        {
            this.Data = data;
            this.Summary = Summary;
        }
        /// <summary>
        /// 返回执行失败
        /// </summary>
        /// <param name="message">执行结果信息</param>
        /// <returns></returns>
        public static ExecutionResult<T> Fail(string message = "error", T data = default(T),object Summary=null)
        {
            return new ExecutionResult<T>(false, message, data, Summary);
        }
        /// <summary>
        /// 返回执行成功
        /// </summary>
        /// <param name="message">执行结果信息</param>
        /// <param name="data">执行结果数据</param>
        /// <returns></returns>
        public static ExecutionResult<T> Success(string message = "success", T data = default(T),object Summary=null)
        {
            return new ExecutionResult<T>(true, message, data,Summary);
        }

    }
}