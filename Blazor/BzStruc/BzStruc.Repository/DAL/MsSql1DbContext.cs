using BzStruc.Repository.Enums;
using BzStruc.Repository.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;


namespace BzStruc.Repository.DAL
{
    // Add-Migration CreateMigrationDatabaseMsSql1 -Context MsSql1DbContext
    // Update-Database CreateMigrationDatabaseMsSql1 -Context MsSql1DbContext
    // # NOTE
    // < CreateMigrationDatabaseMsSql1 > It's mean store in custom directory name
    // < MsSql1DbContext > It's mean use this file target by class name

    //Need to add this to IOC
    //services.AddScoped(typeof(IEntityFrameworkRepository<,>), typeof(EntityFrameworkRepository<,>));
    //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    public class MsSql1DbContext :
        IdentityDbContext<GenericUser, GenericRole, int, GenericUserClaim, GenericUserRole, GenericUserLogin, GenericRoleClaim, GenericUserToken>

    {
        private readonly string _connectionString;
        public MsSql1DbContext(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<DbContextOptions<MsSql1DbContext>>();
            _connectionString = options.FindExtension<SqlServerOptionsExtension>().ConnectionString;
        }

        // Add DbSet<OtherTable> here
        public virtual DbSet<GenericUser> User { get; set; }
        public virtual DbSet<GenericRole> Role { get; set; }

        public virtual DbSet<GenericUserClaim> UserClaim { get; set; }
        public virtual DbSet<GenericUserRole> UserRole { get; set; }
        public virtual DbSet<GenericUserLogin> UserLogin { get; set; }
        public virtual DbSet<GenericRoleClaim> RoleClaim { get; set; }
        public virtual DbSet<GenericUserToken> UserToken { get; set; }

        public virtual DbSet<Conversation> Conversation { get; set; }
        public virtual DbSet<Logs> Interlocutor { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<MessageReadBy> MessageReadBy { get; set; }
        public virtual DbSet<Messages> Message { get; set; }
        public virtual DbSet<Participants> Participant { get; set; }




        public virtual DbSet<Logs> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            var MediaTypesConverter = new ValueConverter<MediaTypes, string>(
                v => v.ToString(),
                v => (MediaTypes)Enum.Parse(typeof(MediaTypes), v));

            builder
                .Entity<Media>()
                .Property(e => e.MediaType)
                .HasConversion(MediaTypesConverter);

            //
            var UserOnlineStatusConverter = new ValueConverter<UserOnlineStatus, string>(
               v => v.ToString(),
               v => (UserOnlineStatus)Enum.Parse(typeof(UserOnlineStatus), v));

            builder
              .Entity<GenericUser>()
              .Property(e => e.OnlineStatus)
              .HasConversion(UserOnlineStatusConverter);

            //
            var MessageTypesConverter = new ValueConverter<MessageTypes, string>(
              v => v.ToString(),
              v => (MessageTypes)Enum.Parse(typeof(MessageTypes), v));

            builder
              .Entity<Messages>()
              .Property(e => e.MessageType)
              .HasConversion(MessageTypesConverter);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

#if DEBUG
            var logEntitySql = new LoggerFactory();
            logEntitySql.AddProvider(new SqlLoggerProvider());
            optionsBuilder.UseLoggerFactory(logEntitySql).UseSqlServer(_connectionString);
#else
            if (optionsBuilder.IsConfigured)
            {
                return;
            }
            optionsBuilder.UseSqlServer(_connectionString);
#endif

        }
    }


}