using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using WallapopScraper.Persistence.Model;

namespace WallapopScraper.Persistence
{
    public class ScraperDbContext : DbContext
    {
        public DbSet<WorkerConfiguration> WorkerConfiguration { get; set; }

        public string DbPath { get; }

        public ScraperDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "WallapopScraper\\scraper.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
