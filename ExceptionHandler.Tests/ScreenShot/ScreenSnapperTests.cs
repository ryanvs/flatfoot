using System;
using System.IO;
using ExceptionHandler.ScreenShot;
using Machine.Specifications;

// ReSharper disable InconsistentNaming 
// ReSharper disable CheckNamespace
namespace ExceptionHandler.Tests.ScreenShot
{
    [Subject(typeof(ScreenSnapper), "It Will create a png screen shot")]
    class when_taking_a_png_screen_shot 
    {
        private static String ScreenShotPath;

        private It will_create_screenshot_file = () => File.Exists(ScreenShotPath).ShouldBeTrue();
        private Cleanup remove_screenshot = () => { if (File.Exists(ScreenShotPath))File.Delete(ScreenShotPath); };
        private Because of = () => ScreenShotPath = ScreenSnapper.TakeScreenshot(Path.Combine(Environment.CurrentDirectory, "ScreenShot"));
    }

    [Subject(typeof(ScreenSnapper), "It Will create a Jpeg screen shot")]
    class when_taking_a_jpeg_screen_shot 
    {
        private static String ScreenShotPath;

        private It will_create_screenshot_file = () => File.Exists(ScreenShotPath).ShouldBeTrue();
        private Cleanup remove_screenshot = () => { if (File.Exists(ScreenShotPath))File.Delete(ScreenShotPath); };
        private Because of = () => ScreenShotPath = ScreenSnapper.TakeScreenshot(Path.Combine(Environment.CurrentDirectory, "ScreenShot"));
    }
}
