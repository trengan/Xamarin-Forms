using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace RockPaperScissors.UITests
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
                    .ApkFile(@"C:\Mavericks\RockPaperScissors\RockPaperScissors\RockPaperScissors.Android\bin\Release\com.companyname.RockPaperScissors.apk")
                    .StartApp();

                // Verify the APK File path if your are try run UI Test in another machine
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}