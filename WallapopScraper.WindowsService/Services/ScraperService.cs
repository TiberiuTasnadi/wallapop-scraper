using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WallapopScraper.Application.Contracts;
using WallapopScraper.Application.DTOs;
using WallapopScraper.WindowsService;

namespace WallapopScrapper.Service.Service
{
    public sealed class ScraperService : IScraperService
    {
        private readonly ConfigurationSettings _configurationSettings;
        private readonly IScraperApplicationService _scraperApplicationService;

        public ScraperService(
            IOptions<ConfigurationSettings> options,
            IScraperApplicationService scraperApplicationService)
        {
            _configurationSettings = options.Value;
            _scraperApplicationService = scraperApplicationService;
        }

        public async Task Execute()
        {
            try
            {
                WorkerConfigurationDto configuration = await GetConfiguration();

                using (var driver = new ChromeDriver())
                {
                    string classCard = configuration.CardClass; //"ItemCardList__item";
                    string classTitle = configuration.TitleClass; //"ItemCard__title";
                    string classPrice = configuration.PriceClass; //"ItemCard__price";
                    string classData = configuration.DataClass; //"ItemCard__data";
                    string url = configuration.Url; //"https://es.wallapop.com/app/search?filters_source=recent_searches&keywords=tarjetas%20graficas&category_ids=15000&latitude=41.61495&longitude=0.62755";

                    driver.Navigate().GoToUrl(url);
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

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

                if (configuration == null)
                {
                    throw new Exception("Configuration not recived from the DB.");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} ERROR: " + ex.Message);
            }
        }

        private string GenerateIdFromUrl(string url)
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

        private async Task<WorkerConfigurationDto> GetConfiguration()
        {
            WorkerConfigurationDto configuration = null;

            if (Guid.TryParse(_configurationSettings.ConfigurationId, out Guid configurationId))
            {
                configuration = await _scraperApplicationService.GetConfiguration(configurationId);

                if (configuration == null)
                {
                    configuration = await _scraperApplicationService.CreateConfiguration(configurationId);
                }
            }
            else
            {
                Console.WriteLine("Configuration ID is not a valid GUID value");
            }

            return configuration;
        }
    }
}
