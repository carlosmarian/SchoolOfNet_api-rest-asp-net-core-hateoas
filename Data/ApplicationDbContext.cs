using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SchoolOfNet_API_Rest_com_ASPNET_Core_2.Models;

namespace SchoolOfNet_API_Rest_com_ASPNET_Core_2.Data
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Produto> Produtos { get; set ;}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {            
        }
    }
}