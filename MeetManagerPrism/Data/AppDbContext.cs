using MeetManagerPrism.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace MeetManagerPrism.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Room> Rooms { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Role>().HasData
            (
                new Role() { Id = "AdminRoleId-51sa9-sdd18", RoleName = "Admin" },
                new Role() { Id = "ManagerRoleId-21ga5-sda13", RoleName = "Manager" },
                new Role() { Id = "UserRoleId-54sa9-sda87", RoleName = "User" }
            );

            modelBuilder.Entity<User>().HasData
            (
                new User() { Id = 1, Name = "Juzba", Email = "Juzba@gmail.com", PasswordHash = "$argon2id$v=19$m=65536,t=3,p=1$+g3wENe8VfgrJvTd4E9YNQ$zvtCx0lwFdXwvR6DLKTOH6FJzm8rB6y54wSEpXbIkJk", RoleId = "AdminRoleId-51sa9-sdd18" },
                new User() { Id = 2, Name = "Katka", Email = "Katka@gmail.com", PasswordHash = "$argon2id$v=19$m=65536,t=3,p=1$VSs65qBpJTKEJSTb7qXkAw$fBlpCgya4Z9LRmKnhUFzXh7tqnXrWSl2vyHkNCwEKCg", RoleId = "ManagerRoleId-21ga5-sda13" },
                new User() { Id = 3, Name = "Karel", Email = "Karel@gmail.com", PasswordHash = "$argon2id$v=19$m=65536,t=3,p=1$tyqbuA8MlWrR6ZE7pWMioA$wjo5b2y+qFdDrbr23ymFvKi9xv2W55g1uvX/3T0z9/s", RoleId = "UserRoleId-54sa9-sda87" }
            );

            modelBuilder.Entity<User>().HasKey("Id");

            modelBuilder.Entity<EventType>().HasData
                (
                new EventType() { Id = 1, Name = "Koncert" },
                new EventType() { Id = 2, Name = "Oslava" },
                new EventType() { Id = 3, Name = "Divadlo" },
                new EventType() { Id = 4, Name = "Ples" },
                new EventType() { Id = 5, Name = "Vystoupení" },
                new EventType() { Id = 6, Name = "Sraz" }
                );

            modelBuilder.Entity<Room>().HasData
                (
                new Room() { ID = 1, Name = "chata pod smrkem", Capacity = 120, Location = "Vysočina" },
                new Room() { ID = 2, Name = "jeskyně", Capacity = 120, Location = "Brno" },
                new Room() { ID = 3, Name = "sokolovna", Capacity = 120, Location = "Lipník" },
                new Room() { ID = 4, Name = "areal u smutného psa", Capacity = 120, Location = "Pardubice" }
                );

            modelBuilder.Entity<Invitation>()
             .HasOne(i => i.User)
             .WithMany(u => u.Invitations) // přidej kolekci Invitations do User třídy (pokud tam není)
             .HasForeignKey(i => i.UserId)
             .OnDelete(DeleteBehavior.Restrict); // nebo DeleteBehavior.NoAction

            base.OnModelCreating(modelBuilder);
        }
    }
}
