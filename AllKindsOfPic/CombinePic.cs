using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllKindsOfPic
{
    /// <summary>
    /// 图片合并，适用于两张图片的合并，或者在图片上添加文字
    /// nyc 2018-2-2 16:00:14
    /// </summary>
  public  class CombinePic
    {
      public static string Combine(string name)
      {
          //背景图片
          string sourceImg = System.Web.HttpContext.Current.Server.MapPath("~/upload/2.png");
          //内置图片
          string destimg = System.Web.HttpContext.Current.Server.MapPath("~/upload/1.png");
          Image imgBack = System.Drawing.Image.FromFile(sourceImg);
          Image img = System.Drawing.Image.FromFile(destimg);
          using (Graphics g = Graphics.FromImage(imgBack))
          {
              Font font = new Font("黑体", 28);
              SolidBrush sbrush = new SolidBrush(Color.White);
              //这里是写文字，两数字是x,y坐标
              g.DrawString(name, font, sbrush, new PointF(212, 914));
              //这里是合并图片
              g.DrawImage(img, 72, 306, 615, 553);
              GC.Collect();
              System.IO.MemoryStream ms = new System.IO.MemoryStream();
              string Folder = System.Web.HttpContext.Current.Server.MapPath("~/upload/combine/");
              if (!Directory.Exists(Folder))
              {
                  Directory.CreateDirectory(Folder);
              }
              var data = DateTime.Now.ToString("yyyyMMddHHmmss");
              var file = System.Web.HttpContext.Current.Server.MapPath("~/upload/combine/") + data + ".jpg";
              imgBack.Save(file, ImageFormat.Jpeg);
              System.Drawing.Image myimg = imgBack;
              myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
              return file;
          }
      }
    }
}
