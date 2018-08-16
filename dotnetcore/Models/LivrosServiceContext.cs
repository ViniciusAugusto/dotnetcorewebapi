using System;
using Microsoft.EntityFrameworkCore;

namespace dotnetcore.Models
{
	public class LivrosServiceContext : DbContext
    {
		public LivrosServiceContext(DbContextOptions<LivrosServiceContext> options) : base(options)
        {
        }

		public DbSet<Livros> Livros { get; set; }
    }
}
