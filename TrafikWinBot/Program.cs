using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TrafikWinBot
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var options = new ChromeOptions();
            options.AddArgument("--autoplay-policy=no-user-gesture-required");
            IWebDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://trafikwin.com/2020/index.php");
            Globals.WaitForPageLoad(driver);
            
            Thread.Sleep(TimeSpan.FromSeconds(3));
            
            var user = Globals.IsElementPresent(driver,By.Name("user"));
            user.SendKeys("forlife34@yandex.com");
            var pass = Globals.IsElementPresent(driver,By.Name("password"));
            pass.SendKeys("F893y010");
            var btnConnect = Globals.IsElementPresent(driver,By.Name("connect"));
            btnConnect.Click();

            Globals.WaitForPageLoad(driver);

            while (true)
            {
                driver.Navigate().GoToUrl("https://trafikwin.com/2020/?page=module&md=youtube");

                var allVideoLinks = driver.FindElements(By.XPath("//a[@class='visit_button']"));
                if (allVideoLinks.Count > 0)
                {
                    foreach (var videoLink in allVideoLinks)
                    {
                        driver.Navigate().GoToUrl(videoLink.GetAttribute("href"));

                        Globals.WaitForPageLoad(driver);

                        Globals.RunJSCommand(driver, "played = 20; roundedPlayed = 20;");

                        Thread.Sleep(TimeSpan.FromSeconds(3));

                        Globals.RunJSCommand(driver, "YouTubePlayed();");
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(10));
                }
                else
                {
                    Console.WriteLine("Video Kalmadı. 1 Saat beklenicek");
                    Console.WriteLine(DateTime.Now.ToShortTimeString());
                    Thread.Sleep(TimeSpan.FromHours(1));
                }
            }
        }
    }
}