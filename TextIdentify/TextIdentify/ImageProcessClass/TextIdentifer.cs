
using System;
using PaddleOCRSharp;
using System.Drawing;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ImageProcess
{
    public class TextIdentifer
    {
        static OCRModelConfig config;
        static OCRParameter oCRParameter;
        static PaddleOCREngine engine;

        public TextIdentifer()
        {
            config = new OCRModelConfig();
            string modelPathroot = System.IO.Path.GetDirectoryName(typeof(OCRModelConfig).Assembly.Location) + @"\inference";
            config.cls_infer = modelPathroot + @"\ch_ppocr_mobile_v2.0_cls_infer";
            config.det_infer = modelPathroot + @"\ch_ppocr_mobile_v2.0_det_infer";
            config.rec_infer = modelPathroot + @"\ch_PP-OCRv4_rec_infer";
            config.keys = modelPathroot + @"\ppocr_keys_cht.txt";
            oCRParameter = new OCRParameter();
            engine = new PaddleOCREngine(config, oCRParameter);
        }
        public Bitmap LoadImage(string imagePath)
        {
            while (true)
            {
                try
                {
                    // 打開圖片檔案
                    using (FileStream fileStream = File.OpenRead(imagePath))
                    {
                        // 創建MemoryMappedFile
                        using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(fileStream, null, 0, MemoryMappedFileAccess.Read, null, HandleInheritability.None, true))
                        {
                            using (MemoryMappedViewStream stream = mmf.CreateViewStream(0, 0, MemoryMappedFileAccess.Read))
                            {
                                // 將MemoryMappedViewStream轉換為MemoryStream
                                MemoryStream memoryStream = new MemoryStream();
                                stream.CopyTo(memoryStream);
                                memoryStream.Position = 0;

                                // 使用MemoryStream載入圖片
                                Bitmap bitmap = new Bitmap(memoryStream);

                                // 將MemoryStream關閉
                                memoryStream.Close();

                                // 返回載入的圖片
                                return bitmap;
                            }
                        }
                    }
                }
                catch (IOException)
                {

                }
            }
        }


        public string ImageText(string mainImagePath)
        {
            using (var image = LoadImage(mainImagePath))
            {
                OCRResult oCRResult = engine.DetectText(image);
                if (oCRResult != null)
                {
                    return oCRResult.Text;
                }
                else
                {
                    return "";
                }
            }

        }
        public string ImageText(string mainImagePath, int StartX, int StartY, int EndX, int EndY)
        {
            using (var image = LoadImage(mainImagePath))
            {
                using (Bitmap croppedImage = new Bitmap(EndX - StartX, EndY - StartY))
                {
                    using (Graphics g = Graphics.FromImage(croppedImage))
                    {
                        g.DrawImage(image, new Rectangle(0, 0, croppedImage.Width, croppedImage.Height), new Rectangle(StartX, StartY, croppedImage.Width, croppedImage.Height), GraphicsUnit.Pixel);
                    }
                    OCRResult oCRResult = engine.DetectText(croppedImage);
                    if (oCRResult != null)
                    {
                        return oCRResult.Text;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
        }
        public int Count(string ImageText, string substr)
        {
            return Regex.Matches(ImageText, substr).Count;
        }
        public bool Similar(string s, string t, int maxEditDistance)
        {

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];
            if (Math.Abs(n - m) > maxEditDistance)
            {
                return false;
            }

            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 1; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    int min1 = d[i - 1, j] + 1;
                    int min2 = d[i, j - 1] + 1;
                    int min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }
            return d[n, m] <= maxEditDistance;
        }
    }
}
