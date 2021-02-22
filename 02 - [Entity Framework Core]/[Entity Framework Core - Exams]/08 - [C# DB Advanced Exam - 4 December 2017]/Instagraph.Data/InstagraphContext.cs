using Instagraph.Models;
using Microsoft.EntityFrameworkCore;

namespace Instagraph.Data
{
    public class InstagraphContext : DbContext
    {
        public InstagraphContext()
        {
        }

        public InstagraphContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserFollower> UsersFollowers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>(user =>
                {
                    user
                        .HasAlternateKey(u => u.Username);

                    user
                        .HasIndex(u => u.Username)
                        .IsUnique(true);
                });

            builder
                .Entity<UserFollower>(userFollower =>
                {
                    userFollower
                        .HasKey(uf => new
                        {
                            uf.UserId,
                            uf.FollowerId
                        });

                    userFollower
                        .HasOne(uf => uf.Follower)
                        .WithMany(f => f.UserFollowing)
                        .HasForeignKey(uf => uf.FollowerId)
                        .OnDelete(DeleteBehavior.Restrict);

                    userFollower
                        .HasOne(uf => uf.User)
                        .WithMany(u => u.Followers)
                        .HasForeignKey(uf => uf.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

            builder
                .Entity<Post>(post =>
                {
                    post.HasOne(p => p.User)
                        .WithMany(u => u.Posts)
                        .HasForeignKey(p => p.UserId)
                        .OnDelete(DeleteBehavior.Restrict);

                    post.HasOne(p => p.Picture)
                        .WithMany(pic => pic.Posts)
                        .HasForeignKey(p => p.PictureId)
                        .OnDelete(DeleteBehavior.Restrict);
                });
        }
    }
}