using ReName.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.VisualBasic.Devices;
using Common.Model;
using Common;

namespace ReName.Controllers
{
    public class HomeController : WebController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetFolderRoute(string path)
        {
            try
            {
                //var files = Directory.GetFiles(path, "*");

                //foreach (var file in files)
                //    Console.WriteLine(file);

                //DirectoryInfo root = new DirectoryInfo(path);
                //string dicName = root.Name;
                string json;
                string url = "https://api.bilibili.com/x/web-interface/view?aid=";

                string[] dirs = Directory.GetDirectories(path);
                List<string> list = new List<string>();

                foreach (string item in dirs)
                {
                    list.Add(Path.GetFileNameWithoutExtension(item));
                }


                foreach (string item in list)
                {
                    using (WebClient web = new WebClient())
                    {
                        web.Encoding = System.Text.Encoding.UTF8;
                        json = web.DownloadString(url + item);
                    }
                    //序列化
                    Root obj = JsonHelper.Deserialize<Root>(json);
                    if (obj.data != null)
                    {
                        //重命名外文件夹
                        Computer MyComputer = new Computer();
                        //MyComputer.FileSystem.RenameFile(path +"\\"+ item, "新名字");
                        MyComputer.FileSystem.RenameDirectory(path + "\\" + item, obj.data.title);
                    }
                }

                return Success("重命名成功");
            }
            catch (Exception e)
            {
                return Failed(e.Message);
            }
        }

        public JsonResult GetFolderAndChildFolderRoute(string path)
        {
            try
            {
                string json;
                string url = "https://api.bilibili.com/x/web-interface/view?aid=";

                string[] dirs = Directory.GetDirectories(path);

                foreach (string item in dirs)
                {
                    string FileName = Path.GetFileNameWithoutExtension(item);
                    using (WebClient web = new WebClient())
                    {
                        web.Encoding = System.Text.Encoding.UTF8;
                        json = web.DownloadString(url + FileName);
                    }
                    //序列化
                    Root obj = JsonHelper.Deserialize<Root>(json);
                    if (obj.data != null)
                    {
                        //重命名
                        Computer MyComputer = new Computer();
                        //重命名内文件
                        var files = Directory.GetFiles(item, "*mp4", SearchOption.AllDirectories).Concat(Directory.GetFiles(item, "*flv", SearchOption.AllDirectories));
                        foreach (var file in files)
                        {
                            //拓展名
                            string extension = Path.GetExtension(file);
                            MyComputer.FileSystem.RenameFile(file, obj.data.title + extension);
                        }
                        //重命名外文件夹
                        MyComputer.FileSystem.RenameDirectory(path + "\\" + FileName, obj.data.title);
                    }
                }
                return Success("重命名成功");
            }
            catch (Exception e)
            {
                return Failed(e.Message);
            }
        }
    }
}