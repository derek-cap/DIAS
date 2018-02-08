using DIAS.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIAS.Infrasturcture
{
    public class StudyContext : DbContext
    {
        public StudyContext(DbContextOptions<StudyContext> options)
            : base(options)
        { }

        public DbSet<DIAS.Data.StudyRecord> Study { get; set; }
        public DbSet<SeriesRecord> Series { get; set; }
        public DbSet<ImageRecord> Image { get; set; }
    }
}
