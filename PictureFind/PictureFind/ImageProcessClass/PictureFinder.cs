
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace ImageProcess
{
    public class PictureFinder
    {
        Color ignoreColor = Color.FromArgb(255, 0, 255);
        Scalar ignoreColor_S = new Scalar(255, 0, 255);

        #region opencv nopink

        public bool FindImageNoPink(string MainImagePath,string imagePathB, double similarityThreshold)
        {
            using (Mat matA = Cv2.ImRead(MainImagePath, ImreadModes.Unchanged))
            using (Mat matB = Cv2.ImRead(imagePathB, ImreadModes.Unchanged))
            {

                Mat mask = new Mat();
                Cv2.InRange(matB, ignoreColor_S, ignoreColor_S, mask);
                matB.SetTo(new Scalar(0, 0, 0), mask);

                Mat result = new Mat();
                Cv2.MatchTemplate(matA, matB, result, TemplateMatchModes.CCoeffNormed);
                Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

                if (maxVal >= similarityThreshold)
                {
                    return true;
                }
            }

            return false;
        }

        public bool FindImageNoPink(string MainImagePath, string imagePathB, double similarityThreshold, out int x, out int y)
        {
            x = -1;
            y = -1;
            using (Mat matA = Cv2.ImRead(MainImagePath, ImreadModes.Unchanged))
            using (Mat matB = Cv2.ImRead(imagePathB, ImreadModes.Unchanged))
            {

                Mat mask = new Mat();
                Cv2.InRange(matB, ignoreColor_S, ignoreColor_S, mask);
                matB.SetTo(new Scalar(0, 0, 0), mask);

                Mat result = new Mat();
                Cv2.MatchTemplate(matA, matB, result, TemplateMatchModes.CCoeffNormed);
                Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

                if (maxVal >= similarityThreshold)
                {
                    x = maxLoc.X + (matB.Cols / 2);
                    y = maxLoc.Y + (matB.Rows / 2);
                    return true;
                }
            }

            return false;
        }

        public bool FindImageNoPink(string MainImagePath, string imagePathB, double similarityThreshold, int startX, int startY, int endX, int endY)
        {
            using (Mat matA = Cv2.ImRead(MainImagePath, ImreadModes.Unchanged))
            using (Mat matB = Cv2.ImRead(imagePathB, ImreadModes.Unchanged))
            {
                OpenCvSharp.Rect searchRegion = new OpenCvSharp.Rect(startX, startY, endX - startX, endY - startY);
                Mat croppedA = new Mat(matA, searchRegion);

                Mat mask = new Mat();
                Cv2.InRange(matB, ignoreColor_S, ignoreColor_S, mask);
                matB.SetTo(new Scalar(0, 0, 0), mask);

                Mat result = new Mat();
                Cv2.MatchTemplate(croppedA, matB, result, TemplateMatchModes.CCoeffNormed);
                Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

                if (maxVal >= similarityThreshold)
                {
                    return true;
                }
            }

            return false;
        }
        public bool FindImageNoPink(string MainImagePath, string imagePathB, double similarityThreshold, int startX, int startY, int endX, int endY, out int x, out int y)
        {
            x = -1;
            y = -1;

            using (Mat matA = Cv2.ImRead(MainImagePath, ImreadModes.Unchanged))
            using (Mat matB = Cv2.ImRead(imagePathB, ImreadModes.Unchanged))
            {
                OpenCvSharp.Rect searchRegion = new OpenCvSharp.Rect(startX, startY, endX - startX, endY - startY);
                Mat croppedA = new Mat(matA, searchRegion);

                Mat mask = new Mat();
                Cv2.InRange(matB, ignoreColor_S, ignoreColor_S, mask);
                matB.SetTo(new Scalar(0, 0, 0), mask);

                Mat result = new Mat();
                Cv2.MatchTemplate(croppedA, matB, result, TemplateMatchModes.CCoeffNormed);
                Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

                if (maxVal >= similarityThreshold)
                {
                    x = maxLoc.X + startX + (matB.Cols / 2);
                    y = maxLoc.Y + startY + (matB.Rows / 2);
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region opencv SIFT 
        public bool FindImageSIFT(string MainImagePath,string imagePathB, double threshold)
        {
            using (var imageA =  new Mat(MainImagePath, ImreadModes.Color))
            using (var imageB = new Mat(imagePathB, ImreadModes.Color))
            using (var result = new Mat())
            {
                Cv2.MatchTemplate(imageA, imageB, result, TemplateMatchModes.CCoeffNormed);

                double minVal, maxVal;
                OpenCvSharp.Point minLoc, maxLoc;
                Cv2.MinMaxLoc(result, out minVal, out maxVal, out minLoc, out maxLoc);

                if (maxVal >= threshold)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool FindImageSIFT(string MainImagePath, string imagePathB, double threshold, out int centerX, out int centerY)
        {
            using (var imageA = new Mat(MainImagePath, ImreadModes.Color))
            using (var imageB = new Mat(imagePathB, ImreadModes.Color))
            using (var result = new Mat())
            {
                Cv2.MatchTemplate(imageA, imageB, result, TemplateMatchModes.CCoeffNormed);

                double minVal, maxVal;
                OpenCvSharp.Point minLoc, maxLoc;
                Cv2.MinMaxLoc(result, out minVal, out maxVal, out minLoc, out maxLoc);

                if (maxVal >= threshold)
                {
                    centerX = maxLoc.X + imageB.Width / 2;
                    centerY = maxLoc.Y + imageB.Height / 2;
                    return true;
                }
                else
                {
                    centerX = 0;
                    centerY = 0;
                    return false;
                }
            }
        }
        public bool FindImageSIFT(string MainImagePath, string imagePathB, double threshold, int startX, int startY, int endX, int endY)
        {
            using (var imageA = new Mat(MainImagePath, ImreadModes.Color))
            using (var imageB = new Mat(imagePathB, ImreadModes.Color))
            using (var result = new Mat())
            {
                startX = Math.Max(startX, 0);
                startY = Math.Max(startY, 0);
                endX = Math.Min(endX, imageA.Width);
                endY = Math.Min(endY, imageA.Height);

                var searchRect = new OpenCvSharp.Rect(startX, startY, endX - startX, endY - startY);
                var searchRegion = new Mat(imageA, searchRect);

                Cv2.MatchTemplate(searchRegion, imageB, result, TemplateMatchModes.CCoeffNormed);

                double minVal, maxVal;
                OpenCvSharp.Point minLoc, maxLoc;
                Cv2.MinMaxLoc(result, out minVal, out maxVal, out minLoc, out maxLoc);


                if (maxVal >= threshold)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FindImageSIFT(string MainImagePath, string imagePathB, double threshold, int startX, int startY, int endX, int endY, out int centerX, out int centerY)
        {
            using (var imageA =  Cv2.ImRead(MainImagePath, ImreadModes.Color))
            using (var imageB = Cv2.ImRead(imagePathB, ImreadModes.Color))
            using (var result = new Mat())
            {
                startX = Math.Max(startX, 0);
                startY = Math.Max(startY, 0);
                endX = Math.Min(endX, imageA.Width);
                endY = Math.Min(endY, imageA.Height);

                var searchRect = new OpenCvSharp.Rect(startX, startY, endX - startX, endY - startY);
                var searchRegion = new Mat(imageA, searchRect);

                Cv2.MatchTemplate(searchRegion, imageB, result, TemplateMatchModes.CCoeffNormed);

                double minVal, maxVal;
                OpenCvSharp.Point minLoc, maxLoc;
                Cv2.MinMaxLoc(result, out minVal, out maxVal, out minLoc, out maxLoc);

                maxLoc.X += searchRect.Left;
                maxLoc.Y += searchRect.Top;

                if (maxVal >= threshold)
                {
                    centerX = maxLoc.X + imageB.Width / 2;
                    centerY = maxLoc.Y + imageB.Height / 2;
                    return true;
                }
                else
                {
                    centerX = 0;
                    centerY = 0;
                    return false;
                }
            }
        }

        #endregion
        #region opencv  Gaussian Blur

        public bool FindImageGB(string MainImagePath,string imagePathB, double threshold)
        {
            using (var imageA = new Mat(MainImagePath, ImreadModes.Color))
            using (var imageB = new Mat(imagePathB, ImreadModes.Color))
            using (var result = new Mat())
            {
                var ProcessedsearchRegion = GaussianBlurProcess(imageA);
                var ProcessedimageB = GaussianBlurProcess(imageB);
                // 進行圖像匹配
                Cv2.MatchTemplate(ProcessedsearchRegion, ProcessedimageB, result, TemplateMatchModes.CCoeffNormed);

                // 尋找最佳匹配位置
                double minVal, maxVal;
                OpenCvSharp.Point minLoc, maxLoc;
                Cv2.MinMaxLoc(result, out minVal, out maxVal, out minLoc, out maxLoc);

                // 判斷是否找到符合閾值的匹配
                if (maxVal >= threshold)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool FindImageGB(string MainImagePath, string imagePathB, double threshold, out int centerX, out int centerY)
        {
            using (var imageA = new Mat(MainImagePath, ImreadModes.Color))
            using (var imageB = new Mat(imagePathB, ImreadModes.Color))
            using (var result = new Mat())
            {
                var ProcessedsearchRegion = GaussianBlurProcess(imageA);
                var ProcessedimageB = GaussianBlurProcess(imageB);
                // 進行圖像匹配
                Cv2.MatchTemplate(ProcessedsearchRegion, ProcessedimageB, result, TemplateMatchModes.CCoeffNormed);

                // 尋找最佳匹配位置
                double minVal, maxVal;
                OpenCvSharp.Point minLoc, maxLoc;
                Cv2.MinMaxLoc(result, out minVal, out maxVal, out minLoc, out maxLoc);

                // 判斷是否找到符合閾值的匹配
                if (maxVal >= threshold)
                {
                    centerX = maxLoc.X + imageB.Width / 2;
                    centerY = maxLoc.Y + imageB.Height / 2;
                    return true;
                }
                else
                {
                    centerX = 0;
                    centerY = 0;
                    return false;
                }
            }
        }
        public bool FindImageGB(string MainImagePath,string imagePathB, double threshold, int startX, int startY, int endX, int endY)
        {
            using (var imageA = new Mat(MainImagePath, ImreadModes.Color))
            using (var imageB = new Mat(imagePathB, ImreadModes.Color))
            using (var result = new Mat())
            {
                // 確保起始和終點座標在圖像 A 的範圍內
                startX = Math.Max(startX, 0);
                startY = Math.Max(startY, 0);
                endX = Math.Min(endX, imageA.Width);
                endY = Math.Min(endY, imageA.Height);

                // 定義搜索範圍
                var searchRect = new OpenCvSharp.Rect(startX, startY, endX - startX, endY - startY);
                var searchRegion = new Mat(imageA, searchRect);
                var ProcessedsearchRegion = GaussianBlurProcess(searchRegion);
                var ProcessedimageB = GaussianBlurProcess(imageB);

                // 進行圖像匹配
                Cv2.MatchTemplate(ProcessedsearchRegion, ProcessedimageB, result, TemplateMatchModes.CCoeffNormed);

                // 尋找最佳匹配位置
                double minVal, maxVal;
                OpenCvSharp.Point minLoc, maxLoc;
                Cv2.MinMaxLoc(result, out minVal, out maxVal, out minLoc, out maxLoc);


                // 判斷是否找到符合閾值的匹配
                if (maxVal >= threshold)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool FindImageGB(string MainImagePath, string imagePathB, double threshold, int startX, int startY, int endX, int endY, out int centerX, out int centerY)
        {
            using (var imageA = Cv2.ImRead(MainImagePath, ImreadModes.Color))
            using (var imageB = new Mat(imagePathB, ImreadModes.Color))
            using (var result = new Mat())
            {
                // 確保起始和終點座標在圖像 A 的範圍內
                startX = Math.Max(startX, 0);
                startY = Math.Max(startY, 0);
                endX = Math.Min(endX, imageA.Width);
                endY = Math.Min(endY, imageA.Height);

                // 定義搜索範圍
                var searchRect = new OpenCvSharp.Rect(startX, startY, endX - startX, endY - startY);
                var searchRegion = new Mat(imageA, searchRect);
                var ProcessedsearchRegion = GaussianBlurProcess(searchRegion);
                var ProcessedimageB = GaussianBlurProcess(imageB);

                // 進行圖像匹配
                Cv2.MatchTemplate(ProcessedsearchRegion, ProcessedimageB, result, TemplateMatchModes.CCoeffNormed);

                // 尋找最佳匹配位置
                double minVal, maxVal;
                OpenCvSharp.Point minLoc, maxLoc;
                Cv2.MinMaxLoc(result, out minVal, out maxVal, out minLoc, out maxLoc);

                // 轉換最佳匹配位置到圖像 A 的座標系統
                maxLoc.X += searchRect.Left;
                maxLoc.Y += searchRect.Top;

                // 判斷是否找到符合閾值的匹配
                if (maxVal >= threshold)
                {
                    centerX = maxLoc.X + imageB.Width / 2;
                    centerY = maxLoc.Y + imageB.Height / 2;
                    return true;
                }
                else
                {
                    centerX = 0;
                    centerY = 0;
                    return false;
                }
            }
        }
        public bool FindMutilpleImageGB(string MainImagePath, string imagePathB, double threshold, int startX, int startY, int endX, int endY, bool Dir, out List<int> centerX, out List<int> centerY)
        {
            using (var imageA = new Mat(MainImagePath, ImreadModes.Color))
            using (var imageB = new Mat(imagePathB, ImreadModes.Color))
            using (var result = new Mat())
            {
                // 確保起始和終點座標在圖像 A 的範圍內
                endX = Math.Min(endX, imageA.Width);
                endY = Math.Min(endY, imageA.Height);
                bool found = false;
                int xc = Math.Max(startX, 0);
                int yc = Math.Max(startY, 0);
                List<int> coordx = new List<int>();
                List<int> coordy = new List<int>();
                if (Dir)
                {
                    for (; (yc + imageB.Height) <= endY; yc += imageB.Height)
                    {
                        // 定義搜索範圍
                        var searchRect = new OpenCvSharp.Rect(xc, yc, endX - xc, endY - yc);
                        var searchRegion = new Mat(imageA, searchRect);
                        var ProcessedsearchRegion = GaussianBlurProcess(searchRegion);
                        var ProcessedimageB = GaussianBlurProcess(imageB);

                        // 進行圖像匹配
                        Cv2.MatchTemplate(ProcessedsearchRegion, ProcessedimageB, result, TemplateMatchModes.CCoeffNormed);

                        // 尋找最佳匹配位置
                        double minVal, maxVal;
                        OpenCvSharp.Point minLoc, maxLoc;
                        Cv2.MinMaxLoc(result, out minVal, out maxVal, out minLoc, out maxLoc);

                        // 轉換最佳匹配位置到圖像 A 的座標系統
                        maxLoc.X += searchRect.Left;
                        maxLoc.Y += searchRect.Top;

                        // 判斷是否找到符合閾值的匹配
                        if (maxVal >= threshold)
                        {
                            coordx.Add(maxLoc.X + imageB.Width / 2);
                            coordy.Add(maxLoc.Y + imageB.Height / 2);
                            yc = maxLoc.Y + imageB.Height + 2;
                            yc -= imageB.Height;
                            found = true;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    for (; (xc + imageB.Width) <= endX;)
                    {
                        // 定義搜索範圍
                        var searchRect = new OpenCvSharp.Rect(xc, yc, endX - xc, endY - yc);
                        var searchRegion = new Mat(imageA, searchRect);
                        var ProcessedsearchRegion = GaussianBlurProcess(searchRegion);
                        var ProcessedimageB = GaussianBlurProcess(imageB);

                        // 進行圖像匹配
                        Cv2.MatchTemplate(ProcessedsearchRegion, ProcessedimageB, result, TemplateMatchModes.CCoeffNormed);

                        // 尋找最佳匹配位置
                        double minVal, maxVal;
                        OpenCvSharp.Point minLoc, maxLoc;
                        Cv2.MinMaxLoc(result, out minVal, out maxVal, out minLoc, out maxLoc);

                        // 轉換最佳匹配位置到圖像 A 的座標系統
                        maxLoc.X += searchRect.Left;
                        maxLoc.Y += searchRect.Top;

                        // 判斷是否找到符合閾值的匹配
                        if (maxVal >= threshold)
                        {
                            coordx.Add(maxLoc.X + imageB.Width / 2);
                            coordy.Add(maxLoc.Y + imageB.Height / 2);
                            xc = maxLoc.X + imageB.Width + 2;
                            found = true;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                centerX = coordx;
                centerY = coordy;
                return found;

            }
        }


        //高斯模糊
        private Mat GaussianBlurProcess(Mat image)
        {
            Mat processedImage = new Mat();
            Cv2.GaussianBlur(image, processedImage, new OpenCvSharp.Size(5, 5), 0);
            return processedImage;
        }
        #endregion
    }
}
