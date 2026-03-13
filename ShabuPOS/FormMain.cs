using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShabuPOS
{
    public partial class FormMain : Form
    {
        // สร้างตัวแปรไว้เก็บสถานะฟอร์มที่กำลังเปิดอยู่
        private Form activeForm = null;

        // ✅ กำหนดสีธีมให้ตรงตามรูปภาพที่คุณต้องการ
        Color panelBgColor = Color.FromArgb(16, 25, 45); // สีน้ำเงินเข้มสำหรับพื้นหลัง Panel
        Color defaultBtnBg = Color.White; // สีพื้นหลังปุ่มปกติ
        Color defaultBtnText = Color.Black; // สีตัวหนังสือปกติ
        Color activeBtnBg = Color.FromArgb(255, 240, 230); // สีพื้นหลังตอนกด (ส้มพีชอ่อนๆ)
        Color activeBtnText = Color.FromArgb(255, 111, 44); // สีตัวหนังสือตอนกด (ส้มสด)

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"ผู้ใช้งาน: {UserSession.CurrentFullName} ({UserSession.CurrentRole})";

            // เรียกจัดรูปแบบปุ่มและ Panel ตั้งแต่เปิดหน้าต่าง
            SetupMenuUI();

            ThemeConfig.ApplyGlobalFont(this);

            // ✅ เรียกฟังก์ชันตรวจสอบสิทธิ
            AuthorizeUser();

            // เปลี่ยนชื่อ panelMenuScroll เป็นชื่อ Panel เมนูของคุณ
            panelMenuScroll.AutoScroll = true;

            // บังคับซ่อน Scrollbar แนวนอน (เผื่อไว้)
            panelMenuScroll.HorizontalScroll.Maximum = 0;
            panelMenuScroll.HorizontalScroll.Visible = false;

            // เทคนิคซ่อนแถบแนวตั้งแต่ยังเลื่อนได้ (Vertical Scroll)
            panelMenuScroll.VerticalScroll.Visible = false;
        }

        private void AuthorizeUser()
        {
            string role = UserSession.CurrentRole;

            // 1. ซ่อนทุกปุ่มก่อนเป็นอันดับแรก (ยกเว้นปุ่มออกจากระบบ)
            foreach (Control ctrl in panelMenuScroll.Controls)
            {
                if (ctrl is Button btn && btn != buttonLogout)
                {
                    btn.Visible = false;
                }
            }

            // 2. ตรวจสอบสิทธิและเปิดปุ่มที่ต้องการตามตำแหน่ง
            switch (role)
            {
                case "ผู้ดูแลระบบ": // หรือ "Admin" ตามที่เก็บใน DB
                
                    // สิทธิสูงสุด: เห็นทุกปุ่ม
                    foreach (Control ctrl in panelMenuScroll.Controls) ctrl.Visible = true;
                    break;

                case "ผู้จัดการ":
                    btnManager.Visible = true; // ปุ่มรายงานต่างๆ
                    break;

                case "พนักงานแคชเชียร์":
                    buttonCashier.Visible = true;    // ปุ่มระบบขาย
                    //buttonManageMenu.Visible = true; // ปุ่มดูเมนู
                    break;

                case "เจ้าหน้าที่บัญชี":
                    buttonAccountFree.Visible = true; // ปุ่มรายรับ-รายจ่าย
                    btnManager.Visible = false; // ปุ่มรายงานต่างๆ
                    break;

                case "พนักงานการเงิน":
                    //buttonAccountFree.Visible = true;
                    button6.Visible = true; // ปุ่มอนุมัติการเงิน
                    break;

                case "พนักงานครัว":
                    btnKitchen.Visible = true; // ปุ่มจัดการออเดอร์ในครัว
                    break;

                case "เจ้าหน้าที่จัดซื้อ":
                    //button3.Visible = true; // ปุ่ม PR
                    button4.Visible = true; // ปุ่ม PO
                    break;

                case "เจ้าหน้าที่คลังสินค้า":
                    button5.Visible = true; // ปุ่มรับสินค้าเข้า Stock
                    button3.Visible = true; // ปุ่มใบขอซื้อ PR
                    button2.Visible = true; // ปุ่มจัดการสินค้า

                    break;

                case "เจ้าหน้าที่การตลาด":
                    btnSavePro.Visible = true; // ปุ่มบันทึกโปรโมชั่น
                    break;

                case "พนักงานต้อนรับ":
                    button1.Visible = true; // ปุ่มจัดการคิว/จองโต๊ะ
                    break;

                default:
                    // ถ้าเป็นตำแหน่งอื่นๆ เช่น "ลูกค้า" หรือตำแหน่งที่ไม่ได้ระบุไว้
                    // ให้เห็นเฉพาะปุ่มพื้นฐาน (ถ้ามี)
                    break;
            }
        }

        // ✅ 1. ฟังก์ชันตั้งค่าหน้าตากลาง (จัดปุ่มแบนราบ + สี Panel)
        private void SetupMenuUI()
        {
            // กำหนดสีพื้นหลังให้ Panel เมนู
            panelMenuScroll.BackColor = panelBgColor;

            foreach (Control ctrl in panelMenuScroll.Controls)
            {
                if (ctrl is Button btn)
                {
                    // ตั้งค่าหน้าตาปุ่มตามเดิม
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = defaultBtnBg;
                    btn.ForeColor = defaultBtnText;
                    btn.Font = new Font("Kanit", 14, FontStyle.Regular);
                    btn.Cursor = Cursors.Hand;

                    // ✅ บังคับให้ปุ่มขยายกว้างเกือบเต็มพื้นที่ Panel (ลบออกเล็กน้อยกัน Scrollbar บัง)
                    btn.Width = panelMenuScroll.ClientSize.Width - 10;

                    // ✅ กำหนดระยะห่างระหว่างปุ่ม (ซ้าย, บน, ขวา, ล่าง) 
                    // การตั้ง Margin.Bottom จะช่วยให้ปุ่มไม่ติดกันเกินไป
                    btn.Margin = new Padding(5, 0, 5, 8);
                }
            }
        }

        // ✅ 2. ฟังก์ชันเปลี่ยนสีปุ่มเวลากด (เขียนรวมกันที่เดียว)
        private void SetActiveButton(Button clickedButton)
        {
            // รีเซ็ตทุกปุ่มให้กลับเป็นสีขาวก่อน
            foreach (Control ctrl in panelMenuScroll.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = defaultBtnBg;
                    btn.ForeColor = defaultBtnText;
                    btn.Font = new Font("Kanit", btn.Font.Size, FontStyle.Regular);
                }
            }

            // ไฮไลท์เฉพาะปุ่มที่ถูกกดให้เป็นสีฟ้า
            if (clickedButton != null)
            {
                clickedButton.BackColor = activeBtnBg;
                clickedButton.ForeColor = activeBtnText;
                // เปลี่ยนเป็นตัวหนาให้เด่นขึ้น (ถ้าไม่ชอบตัวหนาลบบรรทัดล่างออกได้)
                clickedButton.Font = new Font("Kanit", clickedButton.Font.Size, FontStyle.Bold);
            }
        }

        // ✅ 3. ฟังก์ชันเปิดฟอร์มลูก (รวมการเปลี่ยนสีปุ่มไว้ในนี้เลย เพื่อไม่ให้ลืมเรียกใช้งาน)
        private void openChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null) activeForm.Close();

            // เปลี่ยนสีปุ่มทันทีเมื่อเปิดฟอร์ม
            if (btnSender is Button btn)
            {
                SetActiveButton(btn);
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // ========================================================
        // ส่วนของ Event Click ปุ่มต่างๆ ส่งแค่ new Form() กับ sender
        // ========================================================

        private void btnSavePro_Click(object sender, EventArgs e)
        {
            openChildForm(new FormManagePromotion(), sender);
        }

        private void buttonManageShop_Click(object sender, EventArgs e)
        {
            openChildForm(new FormShop(), sender);
        }

        private void buttonManageMenu_Click(object sender, EventArgs e)
        {
            openChildForm(new FormMenu(), sender);
        }

        private void buttonManageEmployee_Click(object sender, EventArgs e)
        {
            openChildForm(new FormEmployee(), sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openChildForm(new Form1(), sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new FormProduct(), sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new FormPR(), sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new FormPO(), sender);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openChildForm(new FormReceiveStock(), sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openChildForm(new FormApprovePR(), sender);
        }

        private void btnKitchen_Click(object sender, EventArgs e)
        {
            openChildForm(new FormKitchen(), sender);
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            // ล้างค่า Session และปิดโปรแกรมเพื่อกลับหน้า Login
            UserSession.Logout();
            Application.Restart(); // รีสตาร์ทแอปเพื่อกลับไปหน้า Login
        }

        private void btnManager_Click(object sender, EventArgs e)
        {
            openChildForm(new FormManager(), sender);
        }

        private void buttonCashier_Click(object sender, EventArgs e)
        {
            openChildForm(new FromCashier(), sender);
        }

        private void buttonAccountFree_Click(object sender, EventArgs e)
        {
            openChildForm(new FormAccountFee(), sender);
        }
    }
}