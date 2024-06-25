using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

namespace RPABecomex
{
    public static class FindElement
    {
        public static IWebElement? GetElement(ChromeDriver driver, string xPath, int validateTime = 30)
        {
            Stopwatch sw = new();
            sw.Start();

            IWebElement element;
            while (sw.Elapsed.TotalSeconds < validateTime)
            {
                try
                {
                    element = driver.FindElement(By.XPath(xPath));
                }
                catch
                {
                    continue;
                }
                return element;
            }
            return null;
        }

        public static bool Click (ChromeDriver driver, string xPath, int validateTime = 30)
        {
            var element = GetElement(driver, xPath, validateTime);
            
            if (element != null)
            {
                element.Click();
                return true;
            }
            return false;
        }

        public static bool Type (ChromeDriver driver, string xPath, string text, int validateTime = 30)
        {
            var element = GetElement(driver, xPath, validateTime);

            if (element != null)
            {
                element.SendKeys(text);
                return true;
            }
            return false;
        }
    }
}
