using FaceRecogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FaceRecogApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Activity> Activities { get; set; }
        
        public DbSet<JobTitle> JobTitles { get; set; }

        public DbSet<Picture> Pictures { get; set; }


    }

}
