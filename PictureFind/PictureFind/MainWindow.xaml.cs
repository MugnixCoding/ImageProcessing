using ImageProcess;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PictureFind
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        PictureFinder pictureFinder;
        public MainWindow()
        {
            InitializeComponent();
            pictureFinder = new PictureFinder();
        }

        private void FileOpen_button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void OpenFile(Image setImage)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    Uri uri = new Uri(openFileDialog.FileName);
                    setImage.Source = new BitmapImage(uri);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("發生錯誤：" + ex.Message);
                }
            }
        }

        private void Target_Picture_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(((Image)sender).Source.ToString()))
            {
                return;
            }
            Image image = (Image)sender;

            Point positionRelativeToImage = e.GetPosition(image);

            double x = positionRelativeToImage.X * (image.Source.Width / image.RenderSize.Width);
            double y = positionRelativeToImage.Y * (image.Source.Height / image.RenderSize.Height);
        }

        private void MainFileSelect_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TargetFileSelect_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FindImage_button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
