using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllKindsOfPic
{
    /// <summary>
    /// 两张图片的叠加，不规则形状的叠加
    /// 比如替换脸操作
    /// nyc 2018-2-2 16:01:31
    /// </summary>
   public class CreateOverPic
    {
       public static string COverPic()
       {
           //要合并图片部分
           string sourceImg = System.Web.HttpContext.Current.Server.MapPath("~/upload/s.png");
           //透明图片来源（整体图片）
           string destImg = System.Web.HttpContext.Current.Server.MapPath("~/upload/img.png");
           Image imgPhoto = Image.FromFile(sourceImg);

           //取整体图片的宽高
           Image imgPhotoa = Image.FromFile(destImg);
           int phWidth = imgPhotoa.Width;
           int phHeight = imgPhotoa.Height;

           //创建原始图的bitmap
           Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);

           //设置此 Bitmap 的分辨率。 
           bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

           //load the Bitmap into a Graphics object 
           Graphics grPhoto = Graphics.FromImage(bmPhoto);
           //Set the rendering quality for this Graphics object
           grPhoto.SmoothingMode = SmoothingMode.AntiAlias;//清除锯齿的呈现

           //Draws the photo Image object at original size to the graphics object.
           grPhoto.DrawImage(
               imgPhoto,                               // Photo Image object
               new Rectangle(21, 109, phWidth, phHeight), //在透明图片的位置
               0,          
               0,        
               phWidth,   
               phHeight, 
               GraphicsUnit.Pixel);                    


           //------------------------------------------------------------
           //Step #2 - Insert Property image,For example:hair,skirt,shoes etc.
           //------------------------------------------------------------
           //create a image object containing the watermark
           Image imgWatermark = new Bitmap(destImg);
           int wmWidth = imgWatermark.Width;
           int wmHeight = imgWatermark.Height;


           //Create a Bitmap based on the previously modified photograph Bitmap
           Bitmap bmWatermark = new Bitmap(bmPhoto);
           bmWatermark.MakeTransparent(); //使默认的透明颜色对此 Bitmap 透明。

           //bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
           //Load this Bitmap into a new Graphic Object
           Graphics grWatermark = Graphics.FromImage(bmWatermark);


           int xPosOfWm = 0;
           int yPosOfWm = 0;

           //叠加
           grWatermark.DrawImage(imgWatermark, new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight),  //Set the detination Position
           0,                  // x-coordinate of the portion of the source image to draw. 
           0,                  // y-coordinate of the portion of the source image to draw. 
           wmWidth,            // Watermark Width
           wmHeight,            // Watermark Height
           GraphicsUnit.Pixel, // Unit of measurment
           null);   //ImageAttributes Object


           //Replace the original photgraphs bitmap with the new Bitmap
           imgPhoto = bmWatermark;

           //grWatermark.Dispose();
           System.IO.MemoryStream ms = new System.IO.MemoryStream();
           string Folder = System.Web.HttpContext.Current.Server.MapPath("~/upload/combine/");
           if (!Directory.Exists(Folder))
           {
               Directory.CreateDirectory(Folder);
           }
           var data = DateTime.Now.ToString("yyyyMMddHHmmss");
           var file = System.Web.HttpContext.Current.Server.MapPath("~/upload/combine/") + data + ".jpg";
           imgPhoto.Save(file, ImageFormat.Jpeg);
           System.Drawing.Image myimg = imgPhoto;
           myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
           return file;
       }
    }
}
