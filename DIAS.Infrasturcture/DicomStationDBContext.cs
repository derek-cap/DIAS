using DIAS.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIAS.Infrasturcture
{
    public partial class DicomStationDBContext : DbContext
    {
        public DbSet<ImageRecord> ImageRecords { set; get; }

        public DbSet<SeriesRecord> SeriesRecords { set; get; }

        public DbSet<StudyRecord> StudyRecords { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=test1;user=root;password=fmi-drooga");
        }

    }
}
