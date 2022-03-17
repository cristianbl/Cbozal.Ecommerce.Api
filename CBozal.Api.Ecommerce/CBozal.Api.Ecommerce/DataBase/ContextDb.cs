using CBozal.Api.Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBozal.Api.Ecommerce.DataBase
{
    public class ContextDb : DbContext
    {
        public DbSet<Shopcart> Shopcart { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public ContextDb()
        {
        }
        public ContextDb(DbContextOptions<ContextDb> options) : base(options)
        {

        }
    }
}
