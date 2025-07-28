namespace ScribeTracker.Data
{
    using Microsoft.EntityFrameworkCore;
    using ScribeTracker.Models;
    using System.Collections.Generic;

    public class ScribeContext : DbContext
    {
        public ScribeContext(DbContextOptions<ScribeContext> options)
            : base(options)
        {
        }

        public DbSet<PenName> PenNames => Set<PenName>();
        public DbSet<Work> Works => Set<Work>();
        public DbSet<Market> Markets => Set<Market>();
        public DbSet<Submission> Submissions => Set<Submission>();
    }
}
