using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TrafikWinBot
{
    public static class Globals
    {
        public static void WaitForPageLoad(IWebDriver driver)
        {
            try
            {
                IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)driver;
                WebDriverWait webDriverWait = new WebDriverWait(driver, new TimeSpan(0, 0, 15));
                webDriverWait.Until<bool>((IWebDriver wd) => javaScriptExecutor.ExecuteScript("return document.readyState")
                    .Equals("complete"));
            }
            catch (Exception)
            {
            }
        }
        
        public static IWebElement IsElementPresent(IWebDriver driver, By by)
        {
            IWebElement Founded = null;

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
                IWebElement element = wait.Until(x => driver.FindElement(by));
                if (element.Displayed)
                {
                    Founded = element;
                }
                else
                {
                    Founded = null;
                }
            }
            catch (Exception)
            {
            }

            return Founded;
        }
        
        public static void RunJSCommand(IWebDriver driver, string jsCommand, object[] options = null)
        {
            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)driver;
            if (options != null)
            {
                javaScriptExecutor.ExecuteScript(jsCommand, options);
            }
            else
            {
                javaScriptExecutor.ExecuteScript(jsCommand);
            }
        }
    }
}