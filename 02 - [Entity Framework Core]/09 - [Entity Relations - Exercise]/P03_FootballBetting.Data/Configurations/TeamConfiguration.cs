using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> team)
        {
            team
                .HasKey(t => t.TeamId);

            team
                .Property(t => t.Name)
                .HasMaxLength(60)
                .IsRequired(true)
                .IsUnicode(true);

            team
                .Property(t => t.LogoUrl)
                .HasMaxLength(300)
                .IsRequired(false)
                .IsUnicode(false);

            team
                .Property(t => t.Initials)
                .HasMaxLength(10)
                .IsRequired(true)
                .IsUnicode(true);

            team
                .Property(t => t.Budget)
                .IsRequired(true);

            team
                .HasOne(t => t.PrimaryKitColor)
                .WithMany(pkc => pkc.PrimaryKitTeams)
                .HasForeignKey(t => t.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            team
                .HasOne(t => t.SecondaryKitColor)
                .WithMany(skc => skc.SecondaryKitTeams)
                .HasForeignKey(t => t.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            team
                .HasOne(t => t.Town)
                .WithMany(to => to.Teams)
                .HasForeignKey(t => t.TownId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}