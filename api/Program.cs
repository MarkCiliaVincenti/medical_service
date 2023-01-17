using Microsoft.EntityFrameworkCore;

using Domain;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ServiceContext>(options =>
options.UseSqlite($"Host=localhost;Port=5000;Database=repo;Username=fsanvr;Password=12345"));

builder.Services.AddTransient<IUserRepository, UserRepositoryImpl>();
builder.Services.AddTransient<UserService>();

builder.Services.AddTransient<IDoctorRepository, DoctorRepositoryImpl>();
builder.Services.AddTransient<DoctorService>();

builder.Services.AddTransient<IAppointmentRepository, AppointmentRepositoryImpl>();
builder.Services.AddTransient<AppointmentService>();

builder.Services.AddTransient<IScheduleRepository, ScheduleRepositoryImpl>();
builder.Services.AddTransient<ScheduleService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
