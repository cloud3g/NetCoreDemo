using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.IO;
using System.Reflection;
using System.ComponentModel;

namespace Blog.Common
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Utils
    {

        /// <summary> 
        /// 获取枚举值的描述文本 
        /// </summary> 
        /// <param name="value"></param> 
        /// <returns></returns> 
        public static string GetEnumDesc(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString()); if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false); if (attrs != null && attrs.Length > 0) return ((System.ComponentModel.DataAnnotations.DisplayAttribute)attrs[0]).Name;
            }
            return en.ToString();
        }
        /// <summary>
        /// 使用MD5加密字符串
        /// </summary>
        /// <param name="str">待加密的字符</param>
        /// <returns></returns>
        public static string MD5(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] arr = UTF8Encoding.Default.GetBytes(str);
            byte[] bytes = md5.ComputeHash(arr);
            str = BitConverter.ToString(bytes);
            return str;
        }
        /// <summary>
        /// 获取ModelState的错误信息
        /// </summary>
        /// <param name="ModelState"></param>
        /// <returns></returns>
        public static string ModelStateMessage(ModelStateDictionary modelState)
        {
            StringBuilder errinfo = new StringBuilder();
            foreach (var m in modelState.Values)
            {
                foreach (var e in m.Errors)
                {
                    errinfo.AppendLine(e.ErrorMessage);
                }
            }
            return errinfo.ToString();
        }
        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FirstLetterToUp(this string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }
        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="originalPicture">原图地址</param>
        /// <param name="thumbnail">缩略图地址</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns>是否成功</returns>
        public static bool CreateThumbnail(string originalPicture, string thumbnail, int width, int height)
        {
            //原图
            Image _original = Image.FromFile(originalPicture);
            // 原图使用区域
            RectangleF _originalArea = new RectangleF();
            //宽高比
            float _ratio = (float)width / height;
            if (_ratio > ((float)_original.Width / _original.Height))
            {
                _originalArea.X = 0;
                _originalArea.Width = _original.Width;
                _originalArea.Height = _originalArea.Width / _ratio;
                _originalArea.Y = (_original.Height - _originalArea.Height) / 2;
            }
            else
            {
                _originalArea.Y = 0;
                _originalArea.Height = _original.Height;
                _originalArea.Width = _originalArea.Height * _ratio;
                _originalArea.X = (_original.Width - _originalArea.Width) / 2;
            }
            Bitmap _bitmap = new Bitmap(width, height);
            Graphics _graphics = Graphics.FromImage(_bitmap);
            //设置图片质量
            _graphics.InterpolationMode = InterpolationMode.High;
            _graphics.SmoothingMode = SmoothingMode.HighQuality;
            //绘制图片
            _graphics.Clear(Color.Transparent);
            _graphics.DrawImage(_original, new RectangleF(0, 0, _bitmap.Width, _bitmap.Height), _originalArea, GraphicsUnit.Pixel);
            //保存
            _bitmap.Save(thumbnail);
            _graphics.Dispose();
            _original.Dispose();
            _bitmap.Dispose();
            return true;
        }

        /// <summary>
        /// 等比例压缩图片
        /// </summary>
        public static void SaveImageByWidthHeight(int intImgCompressWidth, int intImgCompressHeight, Stream stream, string strFileSavePath)
        {
            //从输入流中获取上传的image对象
            using (Image img = Image.FromStream(stream))
            {
                //根据压缩比例求出图片的宽度
                int intWidth = intImgCompressWidth / intImgCompressHeight * img.Height;
                int intHeight = img.Width * intImgCompressHeight / intImgCompressWidth;
                //画布
                using (Bitmap bitmap = new Bitmap(img, new Size(intImgCompressWidth, intImgCompressHeight)))
                {
                    //在画布上创建画笔对象
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        //将图片使用压缩后的宽高,从0，0位置画在画布上
                        graphics.DrawImage(img, 0, 0, intImgCompressWidth, intImgCompressHeight);
                        //保存图片
                        bitmap.Save(strFileSavePath);
                    }
                }
            }
        }

        /// <summary> 
        /// 按照比例缩小图片 
        /// </summary> 
        /// <param name="srcImage">要缩小的图片</param> 
        /// <param name="percent">缩小比例</param> 
        /// <returns>缩小后的结果</returns> 
        public static void PercentImage(Stream stream, double percent, string strFileSavePath)
        {
            Image img = Image.FromStream(stream);
            // 缩小后的高度 
            int newH = int.Parse(Math.Round(img.Height * percent).ToString());
            // 缩小后的宽度 
            int newW = int.Parse(Math.Round(img.Width * percent).ToString());
            try
            {
                // 要保存到的图片 
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量 
                g.InterpolationMode = InterpolationMode.Default;
                g.DrawImage(img, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
                g.Dispose();
                b.Save(strFileSavePath);
            }
            catch (Exception)
            {
                
            }
        }
    }
}
