using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShabuPOS
{
    public static class ThemeConfig
    {
        public static void FormatMinimalistDataGridView(DataGridView dgv)
        {
            if (dgv == null) return;

            // 1. เปลี่ยนพื้นหลังตาราง (ส่วนที่ว่าง) เป็นสีเทาอ่อนมินิมอล
            dgv.BackgroundColor = Color.FromArgb(245, 245, 245); // สีเทาอ่อน
            dgv.BorderStyle = BorderStyle.None;

            // 2. เส้นคั่นตารางเอาแค่แนวนอน
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(230, 230, 230);

            // ⭐ เพิ่มเติม: สีของเซลล์ข้อมูล (เทาอ่อน สลับ ขาว ให้อ่านง่าย)
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); // เซลล์ปกติสีเทาอ่อนกลืนกับพื้นหลัง
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White; // บรรทัดสลับเป็นสีขาว

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            // 3. สีไฮไลท์เวลาคลิกเลือกแถว (ใช้โทนสีส้มอ่อน คล้ายปุ่มในหน้าเว็บ)
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 240, 230); // สีส้มพีชอ่อนๆ
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            // 4. หัวตารางสีขาว ตัวอักษรสีดำหนา
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(254, 188, 70); // เปลี่ยนเป็นสีส้มอ่อน
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black; // ให้ตัวหนังสือยังคงเป็นสีดำ
            dgv.ColumnHeadersHeight = 40;

            // 5. ปรับความสูงบรรทัดให้โปร่งๆ มีพื้นที่หายใจ
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowTemplate.Height = 45; // เพิ่มความสูงให้ดูเหมือน List ในมือถือ

            // 6. ปิดส่วนที่ไม่จำเป็น
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.ReadOnly = true;
        }

        // ==========================================
        // ฟังก์ชันตกแต่งปุ่ม เพิ่ม, แก้ไข, ลบ (พร้อม Hover Effect)
        // ==========================================

        public static void StyleButtonAdd(Button btn)
        {
            // สีเขียวพาสเทล -> เมาส์ชี้เป็นสีเขียวเข้มขึ้น
            ApplyButtonStyle(btn, Color.FromArgb(40, 167, 69), Color.FromArgb(33, 136, 56));
        }

        public static void StyleButtonEdit(Button btn)
        {
            // สีส้มชาบู (ตามธีมหลัก) -> เมาส์ชี้เป็นสีส้มเข้ม
            ApplyButtonStyle(btn, Color.FromArgb(255, 152, 0), Color.FromArgb(230, 138, 0));
        }

        public static void StyleButtonDelete(Button btn)
        {
            // สีแดงซอฟต์ๆ -> เมาส์ชี้เป็นสีแดงเข้ม
            ApplyButtonStyle(btn, Color.FromArgb(220, 53, 69), Color.FromArgb(200, 35, 51));
        }

        // ฟังก์ชันเบื้องหลังที่คอยจัดการตั้งค่า Property ให้ปุ่ม
        // ฟังก์ชันหลักที่จัดการทั้งสีปุ่มและขอบมนแบบเนียนๆ
        // ฟังก์ชันหลักที่จัดการทั้งสีปุ่มและขอบมนแบบเนียนๆ (เวอร์ชันแก้ปุ่มแหว่ง)
        private static void ApplyButtonStyle(Button btn, Color bgColor, Color hoverColor)
        {
            if (btn == null) return;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = bgColor;
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;

            btn.FlatAppearance.MouseOverBackColor = hoverColor;
            btn.FlatAppearance.MouseDownBackColor = hoverColor;

            // ฟังก์ชันช่วยวาดขอบโค้ง (เขียนไว้ให้เรียกซ้ำได้)
            System.Drawing.Drawing2D.GraphicsPath GetRoundPath()
            {
                int radius = 12; // ปรับความโค้งตรงนี้
                int d = radius * 2;
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

                // กัน Error กรณีปุ่มโหลดมาเล็กเกินไป
                if (btn.Width < d || btn.Height < d) return path;

                path.AddArc(0, 0, d, d, 180, 90);
                path.AddArc(btn.Width - d, 0, d, d, 270, 90);
                path.AddArc(btn.Width - d, btn.Height - d, d, d, 0, 90);
                path.AddArc(0, btn.Height - d, d, d, 90, 90);
                path.CloseFigure();
                return path;
            }

            // 1. ตัดขอบครั้งแรกตอนโหลด
            btn.Region = new Region(GetRoundPath());

            // ⭐ 2. ไม้ตาย: ถ้าปุ่มถูกยืด/หด (เช่น ตอนเปลี่ยนฟอนต์) ให้ตัดขอบใหม่ทันที!
            btn.SizeChanged += (s, e) =>
            {
                btn.Region = new Region(GetRoundPath());
            };

            // 3. วาดเส้นขอบลบรอยหยักให้เนียนกริบ (ใช้วิธีเรียก GetRoundPath() เพื่อให้ขนาดอัปเดตเสมอ)
            btn.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Color parentColor = btn.Parent != null ? btn.Parent.BackColor : Color.White;
                using (Pen pen = new Pen(parentColor, 2))
                {
                    e.Graphics.DrawPath(pen, GetRoundPath());
                }
            };
        }

        // ฟังก์ชันเปลี่ยนเฉพาะชื่อฟอนต์ แต่คงขนาดและสไตล์เดิมไว้
        public static void ApplyGlobalFont(Control control)
        {
            if (control.Font != null)
            {
                float originalSize = control.Font.Size;
                FontStyle originalStyle = control.Font.Style;

                // 👇 เอาขนาดเดิม ลบออกสัก 1.5 หรือ 2 เพื่อให้ตัวเล็กลงพอดีกับกรอบเดิม 👇
                float newSize = originalSize - 1.5f;

                // กันเหนียว เผื่อลบแล้วเล็กไป ให้ต่ำสุดอยู่ที่ขนาด 8.5
                if (newSize < 8.5f) newSize = 8.5f;

                control.Font = new Font("Kanit", newSize, originalStyle);
            }

            foreach (Control c in control.Controls)
            {
                ApplyGlobalFont(c);
            }
        }
    }
}