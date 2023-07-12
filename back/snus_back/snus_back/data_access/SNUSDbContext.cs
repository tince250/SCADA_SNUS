using Microsoft.EntityFrameworkCore;
using snus_back.Models;

namespace snus_back.data_access
{
    public class SNUSDbContext : DbContext
    {
        public SNUSDbContext(DbContextOptions<SNUSDbContext> options) : base(options) { }

        public DbSet<User> Users { get;set; }
        public DbSet<Alarm> Alarms { get;set; }
        public DbSet<AlarmRecord> AlarmRecords { get;set; }
        public DbSet<AnalogInput> AnalogInputs { get;set; }
        public DbSet<AnalogOutput> AnalogOutputs { get;set; }
        public DbSet<DigitalOutput> DigitalOutputs { get;set; }
        public DbSet<DigitalInput> DigitalInputs { get;set; }
        public DbSet<TagRecord> TagRecords { get;set; }

    }
}
