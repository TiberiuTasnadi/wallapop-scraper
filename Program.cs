using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        using (var driver = new ChromeDriver())
        {
            string classCard = "ItemCardList__item";
            string classTitle = "ItemCard__title";
            string classPrice = "ItemCard__price";
            string classData = "ItemCard__data";
            string url = "https://es.wallapop.com/app/search?filters_source=recent_searches&keywords=tarjetas%20graficas&category_ids=15000&latitude=41.61495&longitude=0.62755";

            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            //var element = driver.FindElement(By.XPath("//button[@class ='btn btn-primary']"));
            //element.Click();

            IList<IWebElement> graphicCardList = driver.FindElements(By.ClassName(classCard));

            if (graphicCardList.Count == 0)
            {
                Console.WriteLine($"No data recived ");
            }

            foreach (var graphicCard in graphicCardList)
            {
                string title = string.Empty;
                string price = string.Empty;
                string itemUrl = graphicCard.GetAttribute("href");
                string id = GenerateIdFromUrl(itemUrl);

                IList<IWebElement> graphicCardDataList = graphicCard.FindElements(By.ClassName(classData));

                foreach (var data in graphicCardDataList)
                {
                    title = data.FindElement(By.ClassName(classTitle)).Text;
                    price = data.FindElement(By.ClassName(classPrice)).Text;
                }

                Console.WriteLine($"Title: {title}, Price: {price}, ID: {id}");
            }

            driver.Quit();
        }
    }

    private static string GenerateIdFromUrl(string url)
    {
        try
        {
            string[] urlSplitted = url.Split("-");

            if (urlSplitted.Length < 2)
            {
                throw new Exception($"Splitter returned only {urlSplitted.Length} positions.");
            }

            return urlSplitted.Last();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error extracting ID from the URL: {url}. Message: {ex.Message}");
            throw ex;
        }
    }
}