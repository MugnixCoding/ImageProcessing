using ImageProcess;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TextIdentify
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        TextIdentifer textIdentifer;
        PictureInformation mainPicInfo;
        public MainWindow()
        {
            InitializeComponent();
            textIdentifer = new TextIdentifer();
            mainPicInfo = new PictureInformation();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Xcoordi_texbox.TextChanged += Xcoordi_texbox_TextChanged;
            Ycoordi_texbox.TextChanged += Ycoordi_texbox_TextChanged;
            Width_texbox.TextChanged += Width_texbox_TextChanged;
            Height_texbox.TextChanged += Height_texbox_TextChanged;
        }

        private void MainFileSelect_button_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenFile();
            if (!string.IsNullOrEmpty(fileName))
            {
                MainFileName_textbox.Text = fileName;
                MainFileName_textbox.Focus();
                MainFileName_textbox.CaretIndex = fileName.Length;
                Uri uri = new Uri(fileName);
                BitmapImage bitmap = new BitmapImage(uri);
                mainPicInfo.width = Convert.ToInt32(bitmap.PixelWidth);
                mainPicInfo.height = Convert.ToInt32(bitmap.PixelHeight);
                Display_image.Source = bitmap;
                RedRect_canvas.Width = Display_image.ActualWidth;
                RedRect_canvas.Height = Display_image.ActualHeight;

            }

        }
        private void IdentifyText_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int x = int.Parse(Xcoordi_texbox.Text);
                int y = int.Parse(Ycoordi_texbox.Text);
                int width = int.Parse(Width_texbox.Text);
                int height = int.Parse(Height_texbox.Text);

                string result = textIdentifer.ImageText(MainFileName_textbox.Text,x,y,x+width,y+height);
                if (string.IsNullOrEmpty(result))
                {
                    OCR_Result_textobx.Text = "無文字或辨認失敗!";
                }
                else
                {
                    OCR_Result_textobx.Text = result;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("辨認時發生錯誤: " + ex.Message);
                return;
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
        private void Display_image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RedRect_canvas.Width = Display_image.ActualWidth;
            RedRect_canvas.Height = Display_image.ActualHeight;


            double scale = Display_image.ActualWidth / mainPicInfo.width;
            int x = Convert.ToInt32(Convert.ToDouble(Xcoordi_texbox.Text)*scale);
            int y = Convert.ToInt32(Convert.ToDouble(Xcoordi_texbox.Text) * scale);
            int width = Convert.ToInt32(Convert.ToDouble(Width_texbox.Text) * scale);
            int height = Convert.ToInt32(Convert.ToDouble(Height_texbox.Text) * scale);
            Canvas.SetLeft(IdentifyRange_rect, x);
            Canvas.SetTop(IdentifyRange_rect, y);
            IdentifyRange_rect.Width = width;
            IdentifyRange_rect.Height = height;
        }

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]");
        }

        private void Xcoordi_texbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                if (string.IsNullOrEmpty(txt.Text))
                {
                    txt.Text = "0";
                }
                int num = 0;
                if (!int.TryParse(txt.Text, out num))
                {
                    return;
                }
                double scale = Display_image.ActualWidth / mainPicInfo.width;
                Canvas.SetLeft(IdentifyRange_rect, num * scale);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Ycoordi_texbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                if (string.IsNullOrEmpty(txt.Text))
                {
                    txt.Text = "0";
                }
                int num = 0;
                if (!int.TryParse(txt.Text, out num))
                {
                    return;
                }
                double scale = Display_image.ActualHeight / mainPicInfo.height;
                Canvas.SetTop(IdentifyRange_rect, num * scale);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Width_texbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                if (string.IsNullOrEmpty(txt.Text))
                {
                    txt.Text = "0";
                }
                int num = 0;
                if (!int.TryParse(txt.Text, out num))
                {
                    return;
                }
                double scale = Display_image.ActualWidth / mainPicInfo.width;
                IdentifyRange_rect.Width = num * scale;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Height_texbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                if (string.IsNullOrEmpty(txt.Text))
                {
                    txt.Text = "0";
                }
                int num = 0;
                if (!int.TryParse(txt.Text,out num))
                {
                    return;
                }
                double scale = Display_image.ActualHeight / mainPicInfo.height;
                IdentifyRange_rect.Height = num * scale;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
