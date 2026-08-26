using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CocoAppAPI.Controllers
{
	// Lớp dùng để hứng dữ liệu JSON từ Flutter gửi lên
	public class SwipeRequestDto
	{
		public int TargetUserId { get; set; }
		public bool IsLike { get; set; }
	}

	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class SwipeController : ControllerBase
	{
		// POST: api/swipe
		[HttpPost]
		public IActionResult Swipe([FromBody] SwipeRequestDto request)
		{
			if (request == null)
			{
				return BadRequest("Dữ liệu không hợp lệ.");
			}

			// TODO: Lấy ID của người dùng đang đăng nhập từ Token (thông qua Claim)
			// Lệnh lấy ID: var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (request.IsLike)
			{
				// Hành động Thả tim (Quẹt phải)
				// TODO: Lưu vào bảng Likes (Database) bằng Entity Framework

				// Trả về báo thành công
				return Ok(new { message = "Đã lưu lượt Thích thành công!" });
			}
			else
			{
				// Hành động Bỏ qua (Quẹt trái)
				// TODO: Lưu vào bảng Passes (Database)

				return Ok(new { message = "Đã bỏ qua người dùng." });
			}
		}
	}
}