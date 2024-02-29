using ImageProcess;
using Microsoft.Win32;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PictureFind
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        PictureFinder pictureFinder;
        PictureInformation mainPicInfo;
        PictureInformation tartgetPicInfo;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pictureFinder = new PictureFinder();
            mainPicInfo = new PictureInformation();
            tartgetPicInfo = new PictureInformation();
            MainImage_radio.Checked += ImageGroupe_radio_changed;
            TargetImage_radio.Checked += ImageGroupe_radio_changed;

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
            string fileName = OpenFile();
            if (!string.IsNullOrEmpty(fileName))
            {
                MainFileName_textbox.Text = fileName;
                MainFileName_textbox.Focus();
                MainFileName_textbox.CaretIndex = fileName.Length;
                if (MainImage_radio.IsChecked==true)
                {
                    Uri uri = new Uri(fileName);
                    Display_image.Source = new BitmapImage(uri);
                    mainPicInfo.width = Convert.ToInt32(Display_image.Source.Width);
                    mainPicInfo.height = Convert.ToInt32(Display_image.Source.Height);
                }
                else
                {
                    MainImage_radio.IsChecked = true;
                }
            }
        }

        private void TargetFileSelect_button_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenFile();
            if (!string.IsNullOrEmpty(fileName))
            {
                TargetFileName_textbox.Text = fileName;
                TargetFileName_textbox.Focus();
                TargetFileName_textbox.CaretIndex = fileName.Length;
                if (TargetImage_radio.IsChecked == true)
                {
                    Uri uri = new Uri(fileName);
                    Display_image.Source = new BitmapImage(uri);
                    tartgetPicInfo.width = Convert.ToInt32(Display_image.Source.Width);
                    tartgetPicInfo.height = Convert.ToInt32(Display_image.Source.Height);
                }
                else
                {
                    TargetImage_radio.IsChecked = true;
                }
            }
        }

        private void FindImage_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(MainFileName_textbox.Text) || string.IsNullOrEmpty(TargetFileName_textbox.Text)
                    ||string.IsNullOrEmpty(Threshhold_textbox.Text))
                {
                    MessageBox.Show("重新檢查設定");
                    return;
                }
                double threshhold = double.Parse(Threshhold_textbox.Text) / 100;
                int x = -1;
                int y = -1;
                if (SIFT_radio.IsChecked==true)
                {
                    if(pictureFinder.FindImageSIFT(MainFileName_textbox.Text, TargetFileName_textbox.Text, threshhold,out x,out y))
                    {
                        LeftUpCoordinate_label.Content = "左上座標: x = " + (x-(tartgetPicInfo.width/2)) + " , y = " + (y-(tartgetPicInfo.height/2));
                        MidCoordinate_label.Content = "中央座標: x = " + x + " , y = " + y;
                    }
                }
                else if (IP_radio.IsChecked==true)
                {
                    if (pictureFinder.FindImageNoPink(MainFileName_textbox.Text, TargetFileName_textbox.Text, threshhold, out x, out y))
                    {
                        LeftUpCoordinate_label.Content = "左上座標: x = " + (x - (tartgetPicInfo.width / 2)) + " , y = " + (y - (tartgetPicInfo.height / 2));
                        MidCoordinate_label.Content = "中央座標: x = " + x + " , y = " + y;
                    }
                }
                else if(GB_radio.IsChecked==true)
                {
                    if (pictureFinder.FindImageGB(MainFileName_textbox.Text, TargetFileName_textbox.Text, threshhold, out x, out y))
                    {
                        LeftUpCoordinate_label.Content = "左上座標: x = " + (x - (tartgetPicInfo.width / 2)) + " , y = " + (y - (tartgetPicInfo.height / 2));
                        MidCoordinate_label.Content = "中央座標: x = " + x + " , y = " + y;
                    }
                }
                else
                {
                    MessageBox.Show("請選擇搜尋條件");
                    return;
                }
                if (MainImage_radio.IsChecked==false)
                {
                    MainImage_radio.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("發生錯誤：" + ex.Message);
            }
        }
        private void ImageGroupe_radio_changed(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MainImage_radio.IsChecked == true && !string.IsNullOrEmpty(MainFileName_textbox.Text))
                {
                    Uri uri = new Uri(MainFileName_textbox.Text);
                    Display_image.Source = new BitmapImage(uri);
                    mainPicInfo.width = Convert.ToInt32(Display_image.Source.Width);
                    mainPicInfo.height = Convert.ToInt32(Display_image.Source.Height);
                }
                else if (TargetImage_radio.IsChecked == true && !string.IsNullOrEmpty(TargetFileName_textbox.Text))
                {
                    Uri uri = new Uri(TargetFileName_textbox.Text);
                    Display_image.Source = new BitmapImage(uri);
                    tartgetPicInfo.width = Convert.ToInt32(Display_image.Source.Width);
                    tartgetPicInfo.height = Convert.ToInt32(Display_image.Source.Height);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("發生錯誤：" + ex.Message);
            }
        }

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]");
        }
        private void TextBox_TextChanged_Percent(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                string strnum = txt.Text;
                if ("" == strnum || null == strnum)
                {
                    txt.Text = "1";
                    return;
                }
                int num = 0;
                if (!int.TryParse(txt.Text, out num))
                {
                    return;
                }
                if (num >= 1 && num <= 100)
                {
                    return;
                }
                else if(num<=0)
                {
                    txt.Text = "1";
                    txt.SelectionStart = txt.Text.Length;
                }
                else
                {
                    txt.Text = "100";
                    txt.SelectionStart = txt.Text.Length;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string OpenFile()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == true)
                {
                    return openFileDialog.FileName;

                    //Uri uri = new Uri(openFileDialog.FileName);
                    //setImage.Source = new BitmapImage(uri);
                }
                return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("發生錯誤：" + ex.Message);
                return "";
            }
        }

    }
}
