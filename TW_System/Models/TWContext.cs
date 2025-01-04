
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace TW.Models
{
	public class TWContext : DbContext
	{
		public TWContext(DbContextOptions<TWContext> options) : base(options)
		{
		}

		///TW
		public DbSet<TW_User> TW_Users { get; set; }
		public DbSet<TW_TitleMenu> TW_TitleMenus { get; set; }
		public DbSet<TW_Role> TW_Roles { get; set; }
		public DbSet<TW_Controller> TW_Controllers { get; set; }
		public DbSet<TW_Authorize> TW_Authorizes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TW_TitleMenu>().HasNoKey();
			modelBuilder.Entity<TW_Role>().HasNoKey();
			modelBuilder.Entity<TW_Controller>().HasNoKey();
			modelBuilder.Entity<TW_Authorize>().HasNoKey();
		}
	}

	///TW_User
	[Table("TW_User")]
	public class TW_User
	{
		[Key]
		public int Id { get; set; }
		public string No { get; set; }
		public string Account { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public int Role { get; set; }
		public DateTime? BirthDate { get; set; }
		public string Password { get; set; }
		public DateTime CreatedAt { get; set; }
		public bool IsDeleted { get; set; }
	}

	[Table("TW_TitleMenu")]
	public class TW_TitleMenu
	{
		[Key]
		public int Id { get; set; }
		public int ParentId { get; set; }
		public string VNText { get; set; }
		public string ENText { get; set; }
		public int Type { get; set; }
		public string Action { get; set; }
		public string Area { get; set; }
		public string Controller { get; set; }
		public int DisplayOrder { get; set; }
		public string Icon { get; set; }
	}

	[Table("TW_Role")]
	public class TW_Role
	{
		[Key]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Menu { get; set; }
	}

	[Table("TW_Controller")]
	public class TW_Controller
	{
		[Key]
		public Guid Id { get; set; }
		public string Area { get; set; }
		public string Controller { get; set; }
		public string Action { get; set; }
		public string ReturnType { get; set; }
		public string Attributes { get; set; }
	}

	[Table("TW_Authorize")]
	public class TW_Authorize
	{
		[Key]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Method { get; set; }
		public string Description { get; set; }
	}
}