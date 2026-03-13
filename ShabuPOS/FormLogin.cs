using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuffetAPI.Data;
using Microsoft.EntityFrameworkCore;
using ShabuPOS.Helpers;

namespace ShabuPOS
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textUsername.Text.Trim();
            string password = textPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน");
                return;
            }

            // 1. แปลงเป็น Hash แบบ SHA256 ตาม SecurityHelper
            string passwordHash = SecurityHelper.HashPassword(password);

            // 1. สร้างตัวกำหนดค่า (Options) แล้วดึง Connection String จาก DbConfig มาใช้
            var optionsBuilder = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<BuffetDbContext>();
            optionsBuilder.UseSqlServer(DbConfig.ConnectionString);

            // 2. โยน Options ที่ตั้งค่าแล้วเข้าไปในวงเล็บตอนสร้าง Context
            using (var db = new BuffetDbContext(optionsBuilder.Options))
            {
                // 2. ค้นหาพนักงานที่ Username และ PasswordHash ตรงกัน
                // หมายเหตุ: ตรวจสอบว่าใน Model Employee มีคอลัมน์ Username และ PasswordHash แล้ว
                var user = db.Employees.FirstOrDefault(u =>
                    u.Username == username &&
                    u.PasswordHash == passwordHash);

                if (user != null)
                {
                    // 3. เก็บลง Session ตามคลาสที่เราสร้างไว้
                    UserSession.CurrentEmployId = user.EmployId;
                    UserSession.CurrentUsername = user.Username;
                    UserSession.CurrentFullName = user.EmployName;
                    UserSession.CurrentRole = user.Role;

                    // 4. แจ้งผลลัพธ์สำเร็จ
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง", "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("คุณต้องการออกจากโปรแกรมใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                // ✅ ส่งค่า Cancel กลับไปที่ Program.cs เพื่อให้มันเข้าเงื่อนไข else และ return จบโปรแกรม
                this.DialogResult = DialogResult.Cancel;
                //this.Close();
                Application.Exit(); // ปิดโปรแกรมทันที
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            ThemeConfig.ApplyGlobalFont(this);
        }
    }
}
