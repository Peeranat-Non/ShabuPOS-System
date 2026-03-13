namespace ShabuPOS
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ✅ เพิ่มบรรทัดนี้: กำหนดฟอนต์เริ่มต้นให้ทุกหน้าต่างในโปรเจกต์!
            Application.SetDefaultFont(new Font("Kanit", 10F, FontStyle.Regular));
            // (เปลี่ยนคำว่า Tahoma เป็นฟอนต์ที่คุณต้องการได้เลย เช่น Kanit, Prompt, Segoe UI)

            // ✅ ปรับส่วนนี้เพื่อความปลอดภัย
            using (FormLogin login = new FormLogin())
            {
                // ถ้ากดปุ่ม "ออก" หรือกด X ที่หน้า Login ให้จบการทำงานทันที
                if (login.ShowDialog() == DialogResult.OK)
                {
                    // ถ้า Login สำเร็จ (DialogResult.OK) ถึงจะไปรันหน้าหลักต่อ
                    Application.Run(new FormMain());
                }
                else
                {
                    // ถ้ากดปุ่มอื่นที่ไม่ใช่ OK (เช่น Cancel หรือ Exit) ให้จบ Main ทันที
                    return;
                }
            }
        }
    }
}