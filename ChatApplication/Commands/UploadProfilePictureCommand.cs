using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatClient.ViewModel;

namespace ChatClient.Commands
{
    public class UploadProfilePictureCommand : CommandBase
    {
        public UploadProfilePictureCommand() 
        {
        
        }

        public override void Execute(object parameter)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Image files|*.bmp;*.jpg;*.jpeg;*.png";
            dlg.FilterIndex = 1;

            bool? result = dlg.ShowDialog();
            if(result == true)
            {
                string filename = dlg.FileName;
                Debug.WriteLine(filename);
                try
                {
                    Bitmap image1 = (Bitmap) Image.FromFile(filename);
                    
                    Debug.WriteLine("Loaded image into bitmap");
                    byte[] data;

                    using(var memoryStream = new MemoryStream())
                    {
                        image1.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        data = memoryStream.ToArray();
                        
                        
                    }
                }
                catch
                {

                }
            }

           
        }
    }
}
