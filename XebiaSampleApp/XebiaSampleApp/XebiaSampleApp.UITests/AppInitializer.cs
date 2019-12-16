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
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}