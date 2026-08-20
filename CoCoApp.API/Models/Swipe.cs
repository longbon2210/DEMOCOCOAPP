namespace CocoApp.API.Models
{
	public class Swipe
	{
		public int Id { get; set; }

		// Người thực hiện hành động quẹt
		public int SwiperId { get; set; }

		// Người bị quẹt (người xuất hiện trên màn hình)
		public int SwipedId { get; set; }

		// True = Quẹt phải (Thích/Quan tâm) | False = Quẹt trái (Bỏ qua)
		public bool IsLike { get; set; }

		// Đánh dấu xem hai người đã tương hợp chưa
		public bool IsMatch { get; set; } = false;

		// Thời gian thực hiện hành động
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}