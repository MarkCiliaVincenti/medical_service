using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Repository;

public class ServiceContext : DbContext
{
    public DbSet<AppointmentModel> Appointments { get; set; }
    public DbSet<ScheduleModel> Schedules { get; set; }
    public DbSet<DoctorModel> Doctors { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public ServiceContext()
    {
        //Database.EnsureCreated();
    }
    public ServiceContext(DbContextOptions<ServiceContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        IConfigurationRoot config = builder.Build();
 
        string connectionString = config.GetConnectionString("DefaultConnection");
        options.UseSqlite(connectionString);
    }
}
