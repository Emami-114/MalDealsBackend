using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MalDealsBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace MalDealsBackend.Utils
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using AppDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();
        }

    }
}