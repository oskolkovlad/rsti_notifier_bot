namespace RstiNotifierBot.Model
{
    using Microsoft.EntityFrameworkCore;

    internal class NewsContext : DbContext
    {
        private const string ConectionString =
            @"Server=(localdb)\mssqllocaldb;Database=rsti_news;Trusted_Connection=True;";

        public DbSet<News> News { get; set; }

        public DbSet<NewsHistory> NewsHistories { get; set; }

        public NewsContext()
        {
            Database.EnsureCreated();
        }

        #region DbContext Members

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConectionString);
        }

        #endregion
    }
}
