using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Configurations;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {

        }

        public FootballBettingContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(ConnectionConfiguration.CONNECTION_STRING);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new BetConfiguration());

            modelBuilder
                .ApplyConfiguration(new ColorConfiguration());

            modelBuilder
                .ApplyConfiguration(new CountryConfiguration());

            modelBuilder
                .ApplyConfiguration(new GameConfiguration());

            modelBuilder
                .ApplyConfiguration(new PlayerConfiguration());

            modelBuilder
                .ApplyConfiguration(new PlayerStatisticConfiguration());

            modelBuilder
                .ApplyConfiguration(new PositionConfiguration());

            modelBuilder
                .ApplyConfiguration(new TeamConfiguration());

            modelBuilder
                .ApplyConfiguration(new TownConfiguration());

            modelBuilder
                .ApplyConfiguration(new UserConfiguration());
        }
    }
}