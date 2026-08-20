using CocoApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CocoApp.API.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }

		// Bảng mới để lưu trữ lịch sử quẹt và tương hợp
		public DbSet<Swipe> Swipes { get; set; }

		// Bảng lưu trữ lịch sử tin nhắn
		public DbSet<Message> Messages { get; set; }
	}
}