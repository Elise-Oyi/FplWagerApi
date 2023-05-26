using fplWagerApi.Models;
using FplWagerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FplWagerApi.Data
{
    public class FplWagerContext : DbContext
    {
        //--DbContext for wagers
        public DbSet<Wagers> Wagers { get; set; }

        //--DbContext for users
        public DbSet<Users> Users { get; set; }

        //---DbContext for wagerList
        public DbSet<WagerList> WagerList { get; set; }

        public FplWagerContext(DbContextOptions<FplWagerContext> dbContextOptions) : base(dbContextOptions)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    //---create database if cannot connect
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();

                    //---create tables if no tables exist
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Database error occurred during initialization", ex);
            }
        }
    }
}
