using Gif.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllKindsOfPic
{
   public class CreateGif
    {
        //Step 1:引用Gif.Components.dll
        //Step 2:准备所有需要的图片,这里用directory表示
        //Step 3：生成gif,路径为 file
        //nyc 2018年2月2日15:52:04
       public static string  CreateGif()
       {
           string directory = @"F:\1";
           bool repeat = true;
           //一般文件名按顺序排
           string[] pngfiles = Directory.GetFileSystemEntries(directory, "*.png");

           AnimatedGifEncoder e = new AnimatedGifEncoder();
           string Folder = System.Web.HttpContext.Current.Server.MapPath("~/upload/combine/");
           if (!Directory.Exists(Folder))
           {
               Directory.CreateDirectory(Folder);
           }
           var data = DateTime.Now.ToString("yyyyMMddHHmmss");
           var file = System.Web.HttpContext.Current.Server.MapPath("~/upload/combine/") + data + ".gif";
           e.Start(file);

           //每帧播放时间
           e.SetDelay(500);

           //-1：不重复，0：重复
           e.SetRepeat(repeat ? 0 : -1);
           for (int i = 0, count = pngfiles.Length; i < count; i++)
           {
               e.AddFrame(System.Drawing.Image.FromFile(pngfiles[i]));
           }
           e.Finish();
           return file;
       }
    }
}
