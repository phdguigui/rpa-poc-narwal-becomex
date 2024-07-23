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

            if (IsFieldAvailable(element))
            {
                element?.SendKeys(text);
                return true;
            }
            return false;
        }

        private static bool IsFieldAvailable (IWebElement? element)
        {
            return element != null && string.IsNullOrEmpty(element.GetAttribute("value")) && element.GetAttribute("disabled") == null;
        }

        public static bool SelectOption(ChromeDriver driver, string xPath, string option, int timeout = 30)
        {
            if (GetElement(driver, xPath, timeout)?.GetAttribute("disabled") != null)
            {
                return false;
            }

            Click(driver, xPath, timeout);

            var optionsList = driver.FindElements(By.XPath($"{xPath}/option"));

            if (optionsList.Count == 0)
            {
                return false;
            }

            foreach (var opt in optionsList)
            {
                if (opt.Text.Contains(option, StringComparison.CurrentCultureIgnoreCase))
                {
                    opt.Click();
                    return true;
                }
            }

            return false;
        }

        public static bool ClickOptionButton(ChromeDriver driver, string xPath, string option, int timeout = 30)
        {
            var optionsList = driver.FindElements(By.XPath($"{xPath}"));

            foreach (var button in optionsList)
            {
                if (button.GetAttribute("value").Contains(option, StringComparison.CurrentCultureIgnoreCase))
                {
                    button.Click();
                    return true;
                }
            }

            return false;
        }
    }
}
