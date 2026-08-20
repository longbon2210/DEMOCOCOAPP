using System.Text;
using CocoApp.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();

// 1. NÂNG CẤP SWAGGER: Thêm nút "Authorize" (ổ khóa) để nhập Token
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. CẤU HÌNH BẢO VỆ (JWT AUTHENTICATION)
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false, // Tạm tắt kiểm tra người phát hành
			ValidateAudience = false, // Tạm tắt kiểm tra người nhận
			ValidateLifetime = true, // CÓ kiểm tra thẻ hết hạn chưa
			ValidateIssuerSigningKey = true, // CÓ kiểm tra chữ ký bí mật
			IssuerSigningKey = new SymmetricSecurityKey(key)
		};
	});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection(); // Vẫn đang tạm tắt để tránh lỗi kết nối

// 3. BẬT CHẾ ĐỘ KIỂM TRA BẢO MẬT (Thứ tự 2 dòng này rất quan trọng, phải nằm trước MapControllers)
app.UseAuthentication(); // Xác định "Bạn là ai?" (Kiểm tra thẻ)
app.UseAuthorization();  // Xác định "Bạn được làm gì?" (Quyền hạn)

app.MapControllers();
app.MapHub<CocoApp.API.Hubs.ChatHub>("/chatHub");

app.Run();