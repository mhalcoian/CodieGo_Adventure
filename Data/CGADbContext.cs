using CodieGo_Adventure.Models;
using Microsoft.EntityFrameworkCore;

namespace CodieGo_Adventure.Data
{
    public class CGADbContext : DbContext
    {
        public CGADbContext(DbContextOptions<CGADbContext> options) : base(options)
        {

        }

        // DbSets for entity
        public DbSet<PasswordResetTokens> PasswordResetTokens { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<LoginRecords> LoginRecords { get; set; }
        public DbSet<Profiles> Profiles { get; set; }
        public DbSet<Challenges> Challenges { get; set; }
        public DbSet<ChallengesProgress> ChallengesProgress { get; set; }
        public DbSet<AchievementBadge> AchievementBadges { get; set; }
        public DbSet<Leaderboard> Leaderboard { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Modules> Modules { get; set; }
        public DbSet<ModulesProgress> ModulesProgress { get; set; }
        public DbSet<Puzzles> Puzzles { get; set; }
    }
}
