using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

namespace RPABecomex
{
    public class Browser
    {
        private ChromeDriver driver = new();

        public ChromeDriver? Open(string url, string xPathValidate, int validateTime = 30)
        {
            driver.Navigate().GoToUrl(url);

            Stopwatch sw = new();
            sw.Start();

            while (sw.Elapsed.TotalSeconds < validateTime)
            {
                try
                {
                    driver.FindElement(By.XPath(xPathValidate));
                }
                catch 
                {
                    continue;
                }
                return driver;
            }
            return null;
        }

        public void Close()
        {
            driver.Quit();
        }
    }
}
