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
   public class RotationAndCombine
    {
       public string CreateNewFace(string data, string sourceImg, string destImg, int x, int y)//"~/upload/img.png"
       {
           try
           {
               //合并部分
               //string sourceImg = System.Web.HttpContext.Current.Server.MapPath(source);
               //string destImg = System.Web.HttpContext.Current.Server.MapPath(dest);
               Image imgPhoto = Image.FromFile(sourceImg);
               Image imgPhotoa = Image.FromFile(destImg);
               int phWidth = imgPhotoa.Width;
               int phHeight = imgPhotoa.Height;

               Rectangle rotateRec = Rotation.GetRotateRectangle(phWidth, phHeight, 5);
               int rotateWidth = rotateRec.Width;
               int rotateHeight = rotateRec.Height;

               Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
               bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
               Graphics grPhoto = Graphics.FromImage(bmPhoto);
               grPhoto.SmoothingMode = SmoothingMode.AntiAlias;


               Point centerPoint = new Point(rotateWidth / 2, rotateHeight / 2);
               //将graphics坐标原点移到中心点
               grPhoto.TranslateTransform(centerPoint.X, centerPoint.Y);
               //graphics旋转相应的角度(绕当前原点)
               grPhoto.RotateTransform(5);
               //恢复graphics在水平和垂直方向的平移(沿当前原点)
               grPhoto.TranslateTransform(-centerPoint.X, -centerPoint.Y);
               //此时已经完成了graphics的旋转

               //计算:如果要将源图像画到画布上且中心与画布中心重合，需要的偏移量
               Point Offset = new Point((rotateWidth - phWidth) / 2 - 20, (rotateHeight - phHeight) / 2 + 100);
               //将源图片画到rect里（rotateRec的中心）
               grPhoto.DrawImage(imgPhoto, new Rectangle(Offset.X, Offset.Y, phWidth, phHeight));
               //重至绘图的所有变换
               grPhoto.ResetTransform();

               //grPhoto.DrawImage(imgPhoto, new Rectangle(x, y, phWidth, phHeight), 0, 0, phWidth, phHeight, GraphicsUnit.Pixel);
               Image imgWatermark = new Bitmap(destImg);
               int wmWidth = imgWatermark.Width;
               int wmHeight = imgWatermark.Height;
               Bitmap bmWatermark = new Bitmap(bmPhoto);
               bmWatermark.MakeTransparent();
               Graphics grWatermark = Graphics.FromImage(bmWatermark);
               int xPosOfWm = 0;
               int yPosOfWm = 0;
               grWatermark.DrawImage(imgWatermark, new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight), 0, 0, wmWidth, wmHeight, GraphicsUnit.Pixel, null);
               imgPhoto = bmWatermark;
               System.IO.MemoryStream ms = new System.IO.MemoryStream();
               string Folder = System.Web.HttpContext.Current.Server.MapPath("~/upload/combine/" + data + "/");
               if (!Directory.Exists(Folder))
               {
                   Directory.CreateDirectory(Folder);
               }
               var data1 = DateTime.Now.ToString("yyyyMMddHHmmss");
               var file = System.Web.HttpContext.Current.Server.MapPath("~/upload/combine/" + data + "/") + data1 + ".png";
               imgPhoto.Save(file, ImageFormat.Jpeg);
               System.Drawing.Image myimg = imgPhoto;
               myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
               return file;
           }
           catch (Exception ex)
           {
               return "";
           }
       }
    }
}
