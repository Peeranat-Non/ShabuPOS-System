using System.Security.Cryptography;
using System.Text;

namespace ShabuPOS.Helpers
{
    public static class SecurityHelper
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // แปลงรหัสผ่านเป็น Byte แล้ว Hash
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // แปลงกลับเป็น String (Hexadecimal)
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}