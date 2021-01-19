using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class PlayerStatisticConfiguration : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> playerStatistic)
        {
            playerStatistic
                .HasKey(ps => new
                {
                    ps.GameId,
                    ps.PlayerId
                });

            playerStatistic
                .HasOne(ps => ps.Game)
                .WithMany(g => g.PlayerStatistics)
                .HasForeignKey(ps => ps.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            playerStatistic
                .HasOne(ps => ps.Player)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(ps => ps.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            playerStatistic
                .Property(ps => ps.ScoredGoals)
                .IsRequired(true);

            playerStatistic
                .Property(ps => ps.Assists)
                .IsRequired(true);

            playerStatistic
                .Property(ps => ps.MinutesPlayed)
                .IsRequired(true);
        }
    }
}