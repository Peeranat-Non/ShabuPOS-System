using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BuffetAPI.Data;
using BuffetAPI.Models;

namespace ShabuPOS
{
    public partial class FormApprovePR : Form
    {
        BuffetDbContext db = DbConfig.GetDbContext();

        public FormApprovePR()
        {
            InitializeComponent();
        }

        private void FormApprovePR_Load(object sender, EventArgs e)
        {
            LoadDataToApprove();

            // ✅ นำรหัสพนักงานที่ Login อยู่มาใส่ในช่องอัตโนมัติ
            txtApprover.Text = UserSession.CurrentFullName;

            // ✅ (แนะนำ) ล็อกช่องไว้ไม่ให้พนักงานแก้รหัสตัวเอง
            txtApprover.ReadOnly = true;

            // ✅ เรียกใช้ ThemeConfig เพื่อตกแต่งตาราง!
            ThemeConfig.FormatMinimalistDataGridView(dgvPendingPR);

            // ✅ ตกแต่งปุ่มพร้อมใส่ Hover Effect
            ThemeConfig.StyleButtonAdd(btnApprove);
            ThemeConfig.StyleButtonDelete(btnReject);

            ThemeConfig.ApplyGlobalFont(this);
        }

        // ฟังก์ชันสำหรับโหลดรายการที่รออนุมัติ
        private void LoadDataToApprove()
        {
            try
            {
                // ✅ เปลี่ยนมาดึงข้อมูลจากตาราง PurchaseOrders (PO) 
                // โดยเลือกเฉพาะใบสั่งซื้อที่มีสถานะเป็น "รออนุมัติการเงิน"
                var pendingList = db.PurchaseOrders
                                    .Where(p => p.Po_status == "รออนุมัติการเงิน")
                                    .Select(p => new
                                    {
                                        เลขที่ใบสั่งซื้อ = p.Po_id,
                                        วันที่สั่งซื้อ = p.Po_date,
                                        รหัสพนักงานจัดซื้อ = p.Po_employee,
                                        อ้างอิงใบขอซื้อ = p.Po_Buyreq, // PR_Ref
                                        สถานะปัจจุบัน = p.Po_status
                                    })
                                    .ToList();

                dgvPendingPR.DataSource = null;
                dgvPendingPR.DataSource = pendingList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถโหลดข้อมูลได้: " + ex.Message);
            }

            // ✅ เพิ่มบรรทัดนี้เพื่อให้ชื่อผู้อนุมัติกลับมาแสดงเสมอ
            txtApprover.Text = UserSession.CurrentFullName;
            txtApprover.ReadOnly = true; // ล็อกไว้ให้ใช้ชื่อคน Login เท่านั้น
        }
        private string GenerateExp_ID()
        {
            try
            {
                // ดึงรายการค่าใช้จ่ายล่าสุด โดยเรียงจากรหัสมากไปน้อย
                var lastExp = db.Expenses.OrderByDescending(e => e.Exp_id).FirstOrDefault();

                // ถ้ายังไม่มีข้อมูลในตารางเลย ให้เริ่มที่ EXP-00001
                if (lastExp == null) return "EXP-00001";

                // ถ้ามีข้อมูลแล้ว ให้ดึงรหัสล่าสุดมาตัดคำว่า "EXP-" ออกเพื่อเอาตัวเลขมาบวกเพิ่ม
                string lastId = lastExp.Exp_id; // สมมติได้ "EXP-00010"
                int runningNumber = int.Parse(lastId.Replace("EXP-", ""));
                runningNumber++;

                // ส่งกลับรหัสใหม่ในรูปแบบ EXP- ตามด้วยตัวเลข 5 หลัก (เช่น EXP-00011)
                return "EXP-" + runningNumber.ToString("D5");
            }
            catch
            {
                // กรณีเกิดข้อผิดพลาด ให้ส่งค่า default ที่ไม่ซ้ำกับใคร
                return "EXP-" + DateTime.Now.ToString("yyyyMMddHHmmss");
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (dgvPendingPR.CurrentRow == null) return;

            string poId = dgvPendingPR.CurrentRow.Cells["เลขที่ใบสั่งซื้อ"].Value.ToString();
            string prId = dgvPendingPR.CurrentRow.Cells["อ้างอิงใบขอซื้อ"].Value.ToString();

            try
            {
                var po = db.PurchaseOrders.Find(poId);
                if (po != null)
                {
                    // --- ส่วนที่ 1: อัปเดตสถานะใบสั่งซื้อ ---
                    po.Po_status = "รอรับสินค้า";
                    po.Po_approver = UserSession.CurrentFullName;

                    // --- ส่วนที่ 2: คำนวณยอดเงินรวม ---
                    decimal totalAmount = db.PurchaseOrderDetails
                                            .Where(d => d.Po_id == poId)
                                            .Sum(d => d.Order_Qty * d.Unit_Price);

                    // --- ส่วนที่ 3: บันทึกลงตาราง Expense (ใช้ GenerateExp_ID ตรงนี้!) ---
                    var expense = new Expense
                    {
                        Exp_id = GenerateExp_ID(), // ✅ เรียกใช้งานฟังก์ชันที่เขียนไว้
                        Exp_date = DateTime.Now.Date,
                        Exp_time = DateTime.Now.TimeOfDay,
                        Exp_amount = totalAmount
                    };
                    db.Expenses.Add(expense);

                    // --- ส่วนที่ 4: บันทึกทุกอย่าง ---
                    db.SaveChanges();

                    MessageBox.Show($"อนุมัติเรียบร้อย! สร้างรหัสค่าใช้จ่าย: {expense.Exp_id}", "สำเร็จ");
                    LoadDataToApprove(); // รีเฟรชตาราง
                }
            }
            catch (Exception ex)
            {
                db.ChangeTracker.Clear();
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dgvPendingPR.CurrentRow == null) return;

            string poId = dgvPendingPR.CurrentRow.Cells["เลขที่ใบสั่งซื้อ"].Value.ToString();

            var result = MessageBox.Show($"คุณต้องการปฏิเสธใบสั่งซื้อ {poId} ใช่หรือไม่?", "ยืนยันการปฏิเสธ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var po = db.PurchaseOrders.Find(poId);
                if (po != null)
                {
                    po.Po_status = "ไม่อนุมัติ";
                    db.SaveChanges();
                    LoadDataToApprove();
                }
            }
        }
    }
}