using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace intervention_management.Models
{
    public class Rocket_app_developmentContext : DbContext
    {
        public Rocket_app_developmentContext(DbContextOptions<Rocket_app_developmentContext> options)
            : base(options)
        {
        }

        public DbSet<Battery> batteries { get; set; }
        public DbSet<Elevator> elevators { get; set; }
        // public DbSet<ElevatorStatus> Elevators { get; set; }
        public DbSet<Building> buildings { get; set; }
         
        public DbSet<Column> columns { get; set; }

        public DbSet<Lead> leads { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<User> admin_users {get; set; }
        public DbSet<Intervention> interventions {get; set; }
        public DbSet<Employee> employees {get; set; }

        
    }
}