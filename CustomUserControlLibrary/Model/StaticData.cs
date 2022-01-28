using CustomUserControlLibrary.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CustomUserControlLibrary.Model
{
   public static class StaticData
    {
        public static SolidColorBrush GetColor(string color)
        {
            return new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(color));
        }

        public static BitmapImage GetBitmapImage(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    BitmapImage source = new BitmapImage();
                    source.BeginInit();
                    source.StreamSource = new MemoryStream(File.ReadAllBytes(path));
                    source.EndInit();
                    return source;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                LogServer.Error(e);
                return null;
            }

        }
    }
}
