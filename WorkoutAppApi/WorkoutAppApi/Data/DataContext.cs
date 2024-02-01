using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using WorkoutAppApi.Models;

namespace WorkoutAppApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workout>()
                .HasDiscriminator<string>("workout_type")
                .HasValue<Workout>("outlined")
                .HasValue<ScheduledWorkout>("scheduled");
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<RepsOfExercise> RepsOfExercises { get; set; }
        public DbSet<ScheduledWorkout> ScheduledWorkouts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        

    }
}
