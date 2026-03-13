namespace ShabuPOS
{
    public static class UserSession
    {
        public static string CurrentEmployId { get; set; } = string.Empty;
        public static string CurrentUsername { get; set; } = string.Empty;
        public static string CurrentFullName { get; set; } = string.Empty;
        public static string CurrentRole { get; set; } = string.Empty;

        // เช็คว่าล็อกอินอยู่หรือไม่ (ถ้ามีรหัสพนักงานถือว่าล็อกอินแล้ว)
        public static bool IsLoggedIn => !string.IsNullOrEmpty(CurrentEmployId);

        public static void Logout()
        {
            CurrentEmployId = string.Empty;
            CurrentUsername = string.Empty;
            CurrentFullName = string.Empty;
            CurrentRole = string.Empty;
        }
    }
}