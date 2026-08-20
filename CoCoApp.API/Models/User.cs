namespace CocoApp.API.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Email { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty;

		// --- HỒ SƠ CƠ BẢN (FR-05) ---
		public string AvatarUrl { get; set; } = string.Empty;
		public string University { get; set; } = string.Empty;
		public string Faculty { get; set; } = string.Empty;
		public string Major { get; set; } = string.Empty;
		public string AcademicYear { get; set; } = string.Empty;
		public string Introduction { get; set; } = string.Empty;
		public string FacebookLink { get; set; } = string.Empty;
		public string GithubLink { get; set; } = string.Empty;

		// --- HỒ SƠ HỌC TẬP (FR-06) ---
		public string SkillsGoodAt { get; set; } = string.Empty;
		public string SkillsToLearn { get; set; } = string.Empty;

		// --- HỒ SƠ SINH HOẠT (FR-07) ---
		public string SleepingTime { get; set; } = string.Empty;
		public string Gender { get; set; } = string.Empty;

		// Dùng kiểu bool cho các trạng thái Có/Không (True/False)
		public bool IsSmoker { get; set; }
		public bool HasPet { get; set; }

		// Dùng kiểu decimal? (có dấu ?) để cho phép giá trị null nếu họ chưa nhập ngân sách thuê trọ
		public decimal? RentalBudget { get; set; }
	}
}