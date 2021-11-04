using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WebTuyenSinh.Data.Entityes
{
    public class WebTuyenSinhFactory : IDesignTimeDbContextFactory<HeThongTuyenSinhDB>
    {
       
public HeThongTuyenSinhDB CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            var connectionString = configuration.GetConnectionString("Web_TuyenSinh");

            var optionsBuilder = new DbContextOptionsBuilder<HeThongTuyenSinhDB>();
            optionsBuilder.UseSqlServer(connectionString);

            return new HeThongTuyenSinhDB(optionsBuilder.Options);
        }
    }
}
