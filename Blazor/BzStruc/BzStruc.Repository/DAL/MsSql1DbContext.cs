using BzStruc.Repository.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;


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

        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Conversation> Conversation { get; set; }
        public virtual DbSet<Interlocutor> Interlocutor { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            #region MapManyToMany Contact


            builder.Entity<Contact>(entity =>
            {
                entity
                    .HasOne(d => d.ContactReceiver)
                    .WithMany(p => p.ContactReceiver)
                    .HasForeignKey(d => d.ContactReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity
                    .HasOne(d => d.ContactSender)
                    .WithMany(p => p.ContactSender)
                    .HasForeignKey(d => d.ContactSenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            #endregion

            #region MapManyToMany Conversation

            builder.Entity<Conversation>(entity =>
            {
                entity
                    .HasOne(ho => ho.ConversationReceiver)
                    .WithMany(wm => wm.ConversationReceiver)
                    .HasForeignKey(hfk => hfk.ConversationReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity
                    .HasOne(ho => ho.ConversationSender)
                    .WithMany(wm => wm.ConversationSender)
                    .HasForeignKey(hfk => hfk.ConversationSenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            });


            #endregion

            #region OneToOne 

            builder.Entity<GenericUser>()
                .HasOne(ho => ho.Interlocutor)
                .WithOne(wo => wo.User)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Interlocutor>()
                .HasOne(ho => ho.User)
                .WithOne(wo => wo.Interlocutor)
                .OnDelete(DeleteBehavior.ClientSetNull);

            #endregion

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