using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class JsonHelper
    {
        /// <summary>
        /// 将对象序列化成为Json格式字符串
        /// </summary>
        /// <param name="obj">待序列化对象</param>
        /// <returns></returns>
        public static string Serialize(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// 将json格式字符串反序列化成为 指定T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">待反序列化Json格式字符串</param>
        /// <returns></returns>
        public static T Deserialize<T>(this string json)
        {
            return json == null ? default(T) : JsonConvert.DeserializeObject<T>(json);
        }
        /// <summary>
        /// 将json格式字符串反序列化成为 指定T 对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">待反序列化Json格式字符串</param>
        /// <returns></returns>
        public static List<T> DeserializeToList<T>(this string json)
        {
            return json == null ? default(List<T>) : JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}
