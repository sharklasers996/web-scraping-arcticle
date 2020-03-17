using System.Collections.Generic;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SeleniumScraper
{
    internal static class Program
    {
        private static RemoteWebDriver _webDriver;
        private static readonly List<string> CssSelectors = new List<string>
        {
            "div > table > tbody > tr",
            "tr[class]",
            "tr[class='odd']",
            "*[class='odd']",
            "tr[class*='e']"
        };

        private static void Main(string[] args)
        {
            if (args.Length > 0
                && args[0].Equals("chrome", StringComparison.InvariantCultureIgnoreCase))
            {
                _webDriver = CreateChromeDriver();
            }
            else
            {
                _webDriver = CreateFirefoxDriver();
            }

            TestCssSelectors();
            TestElementWaiting();

            _webDriver.Quit();
        }

        private static void TestElementWaiting()
        {
            _webDriver.Navigate().GoToUrl("http://localhost:5000/scraper/button");

            static IWebElement FindButton()
            {
                return _webDriver.FindElementByCssSelector(".btn");
            }

            Console.WriteLine("Waiting for button...");
            new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10)).Until(_ =>
            {
                try
                {
                    var button = FindButton();
                    return button.Displayed;
                }
                catch
                {
                    return false;
                }
            });

            var button = FindButton();
            var secretLink = button.GetAttribute("href");
            Console.WriteLine($"Buttons href value is {secretLink}");
        }

        private static void TestCssSelectors()
        {
            _webDriver.Navigate().GoToUrl("http://localhost:5000/scraper/table");

            foreach (var selector in CssSelectors)
            {
                var elements = _webDriver.FindElementsByCssSelector(selector);

                Console.WriteLine($"Results for {selector}");
                foreach (var element in elements)
                {
                    Console.WriteLine(element.Text);
                }
                Console.WriteLine();
            }
        }

        private static RemoteWebDriver CreateFirefoxDriver()
        {
            var options = new FirefoxOptions();
            // options.AddArgument("--headless");

            return new FirefoxDriver(options);
        }

        private static RemoteWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            // options.AddArgument("--headless");

            return new ChromeDriver(options);
        }
    }
}
