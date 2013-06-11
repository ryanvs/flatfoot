#region

using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace ExceptionHandler.ScreenShot
{
    public class ScreenSnapper
    {
        public static string TakeScreenshot(string screenShotPath)
        {
            var bitmap = ScreenCapture.CaptureWindow(Process.GetCurrentProcess().MainWindowHandle);

            var formatExtension = ImageFormat.Png.ToString().ToLower();
            if (Path.GetExtension(screenShotPath) != "." + formatExtension)
            {
                screenShotPath += "." + formatExtension;
            }
            bitmap.Save(screenShotPath, ImageFormat.Png);
            return screenShotPath;
        }
    }
}