using System.Security.Cryptography;
using System.Text;

namespace QuanLyCuaHangBanQuanAo.Common
{
	public static class PasswordHelper
	{
		// Hàm này "hash" mật khẩu
		public static string HashPassword(string password)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] bytes = Encoding.UTF8.GetBytes(password);
				byte[] hash = sha256.ComputeHash(bytes);
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < hash.Length; i++)
				{
					builder.Append(hash[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}

		// Hàm này so sánh mật khẩu nhập vào với hash trong CSDL
		public static bool VerifyPassword(string inputPassword, string hashedPassword)
		{
			string hashOfInput = HashPassword(inputPassword);
			return string.Equals(hashOfInput, hashedPassword, StringComparison.OrdinalIgnoreCase);
		}
	}
}