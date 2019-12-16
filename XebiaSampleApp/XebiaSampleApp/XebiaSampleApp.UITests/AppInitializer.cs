using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace XebiaSampleApp.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .EnableLocalScreenshots()
                    .ApkFile(@"C:\Xebia\XebiaSampleApp\XebiaSampleApp\XebiaSampleApp.Android\bin\Release\com.companyname.xebiasampleapp.apk")
                    .StartApp();

                //C:\Users\trenganathan\AppData\Local\Xamarin\Mono for Android\Archives\2019-12-11\XebiaSampleApp.Android 12-11-19 4.13 PM.apkarchive
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}