﻿using Microsoft.EntityFrameworkCore;
using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Infra.Data.Context
{
	public class ToyDbContext: DbContext
	{
		public ToyDbContext(DbContextOptions<ToyDbContext> options) : base(options)
		{
		}
		public DbSet<Toy> Toys { get; set; }
	}
}
