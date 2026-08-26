using CocoApp.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace CocoApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly AppDbContext _context;

		public UsersController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/users
		[HttpGet]
		public IActionResult GetAllUsers()
		{
			// Lấy danh sách từ DB và chỉ trích xuất các thông tin công khai
			var users = _context.Users.Select(u => new
			{
				id = u.Id,
				email = u.Email, // Tạm dùng Email làm tên hiển thị trên thẻ
				university = u.University ?? "Chưa cập nhật trường",
				major = u.Major ?? "Chưa cập nhật ngành"
			}).ToList();

			return Ok(users);
		}
		// DTO hứng toàn bộ dữ liệu hồ sơ từ Flutter gửi lên
		public class UpdateProfileDto
		{
			// --- HỒ SƠ CƠ BẢN ---
			public string AvatarUrl { get; set; } = string.Empty;
			public string University { get; set; } = string.Empty;
			public string Faculty { get; set; } = string.Empty;
			public string Major { get; set; } = string.Empty;
			public string AcademicYear { get; set; } = string.Empty;
			public string Introduction { get; set; } = string.Empty;
			public string FacebookLink { get; set; } = string.Empty;
			public string GithubLink { get; set; } = string.Empty;

			// --- HỒ SƠ HỌC TẬP ---
			public string SkillsGoodAt { get; set; } = string.Empty;
			public string SkillsToLearn { get; set; } = string.Empty;

			// --- HỒ SƠ SINH HOẠT ---
			public string SleepingTime { get; set; } = string.Empty;
			public string Gender { get; set; } = string.Empty;
			public bool IsSmoker { get; set; }
			public bool HasPet { get; set; }
			public decimal? RentalBudget { get; set; }
		}

		// PUT: api/users/profile
		[HttpPut("profile")]
		[Microsoft.AspNetCore.Authorization.Authorize] // Bắt buộc phải có thẻ JWT
		public IActionResult UpdateProfile([FromBody] UpdateProfileDto request)
		{
			// 1. Kiểm tra danh tính người dùng qua JWT Token
			var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userIdString))
				return Unauthorized("Không tìm thấy thông tin xác thực!");

			int userId = int.Parse(userIdString);

			// 2. Lấy thông tin user từ Database
			var user = _context.Users.FirstOrDefault(u => u.Id == userId);
			if (user == null)
				return NotFound("Người dùng không tồn tại!");

			// 3. Đổ dữ liệu mới vào (Cập nhật FR-05)
			user.AvatarUrl = request.AvatarUrl;
			user.University = request.University;
			user.Faculty = request.Faculty;
			user.Major = request.Major;
			user.AcademicYear = request.AcademicYear;
			user.Introduction = request.Introduction;
			user.FacebookLink = request.FacebookLink;
			user.GithubLink = request.GithubLink;

			// (Cập nhật FR-06)
			user.SkillsGoodAt = request.SkillsGoodAt;
			user.SkillsToLearn = request.SkillsToLearn;

			// (Cập nhật FR-07)
			user.SleepingTime = request.SleepingTime;
			user.Gender = request.Gender;
			user.IsSmoker = request.IsSmoker;
			user.HasPet = request.HasPet;
			user.RentalBudget = request.RentalBudget;

			// 4. Lưu thay đổi xuống Database
			_context.SaveChanges();

			return Ok(new { message = "Cập nhật hồ sơ thành công!", user });
		}
	}
}