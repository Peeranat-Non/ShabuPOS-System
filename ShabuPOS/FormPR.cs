using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BuffetAPI.Data;
using BuffetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ShabuPOS // เปลี่ยนให้ตรงกับ Namespace โปรเจกต์คุณ
{
    public partial class FormPR : Form
    {
        BuffetDbContext db = DbConfig.GetDbContext();

        // 1. สร้างตัวแปรตะกร้าสินค้าจำลองสำหรับใบขอซื้อ
        private List<PRCartItem> _cart = new List<PRCartItem>();

        public FormPR()
        {
            InitializeComponent();

            // ตั้งค่า DataGridView เบื้องต้น
            dgvPRDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPRDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPRDetails.MultiSelect = false;
            dgvPRDetails.ReadOnly = true;
            dgvPRDetails.AllowUserToAddRows = false; // ปิดการสร้างแถวว่างๆ ด้านล่าง
        }

        private void FormPR_Load(object sender, EventArgs e)
        {
            InitFilters();
            LoadPRMaster();
            ThemeConfig.ApplyGlobalFont(this);
            // ✅ นำรหัสพนักงานที่ Login อยู่มาใส่ในช่องอัตโนมัติ
            txtEmploy_ID.Text = UserSession.CurrentEmployId;

            // ✅ (แนะนำ) ล็อกช่องไว้ไม่ให้พนักงานแก้รหัสตัวเอง
            txtEmploy_ID.ReadOnly = true;

            // ตั้งค่าสถานะ
            cboStatus.Items.Clear();
            cboStatus.Items.Add("รออนุมัติ");
            cboStatus.SelectedIndex = 0;
            cboStatus.Enabled = false;

            // รันรหัสใบขอซื้ออัตโนมัติ
            txtPR_ID.Text = GeneratePR_ID();
            txtPR_ID.ReadOnly = true;

            // ✅ เรียกใช้ ThemeConfig เพื่อตกแต่งตาราง!dgvPRMaster
            ThemeConfig.FormatMinimalistDataGridView(dgvPRDetails);
            ThemeConfig.FormatMinimalistDataGridView(dgvPRMaster);


            // ✅ ตกแต่งปุ่มพร้อมใส่ Hover EffectbtnSave
            ThemeConfig.StyleButtonAdd(btnAdd);
            //ThemeConfig.StyleButtonAdd(btnSave);
            ThemeConfig.StyleButtonDelete(btnDelete);

            // ตั้งค่ารายการแผนกสำหรับใบขอซื้อใหม่ [cite: 2026-03-08]
            cboPRDept.Items.Clear();
            cboPRDept.Items.AddRange(new string[] { "ครัวร้อน", "บาร์น้ำ", "ของสด" });
            cboPRDept.SelectedIndex = 0; // ตั้งค่าเริ่มต้น [cite: 2026-03-08]

            InitFilters(); // โหลดรายการเข้าตัวกรองด้านบน [cite: 2026-03-08]

            // โหลดข้อมูลสินค้าเข้า ComboBox (cboProducts)
            try
            {
                var products = db.Products.ToList();
                cboProducts.DataSource = products;
                cboProducts.DisplayMember = "Pro_name";  // แสดงชื่อสินค้า
                cboProducts.ValueMember = "Pro_id";      // เก็บค่าเป็นรหัสสินค้า
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถโหลดข้อมูลสินค้าได้: " + ex.Message);
            }
        }

        // ฟังก์ชันโหลดรายชื่อใบ PR ทั้งหมดตามตัวกรอง [cite: 2026-03-08]
        private void LoadPRMaster()
        {
            using (var db = DbConfig.GetDbContext())
            {
                var query = db.PurchaseRequisitions.Include(r => r.Employee).AsQueryable();

                // 1. กรองตามวันที่ [cite: 2026-03-08]
                DateTime startDate = dtpStart.Value.Date;
                DateTime endDate = dtpEnd.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(r => r.PR_Date >= startDate && r.PR_Date <= endDate);

                // 2. กรองตามสถานะ (ถ้าไม่ได้เลือก "ทั้งหมด") [cite: 2026-03-08]
                if (cbStatusFilter.Text != "ทั้งหมด" && !string.IsNullOrEmpty(cbStatusFilter.Text))
                {
                    query = query.Where(r => r.PR_Status == cbStatusFilter.Text);
                }

                // 3. กรองตามแผนก (ถ้าไม่ได้เลือก "ทั้งหมด") [cite: 2026-03-08]
                if (cbDeptFilter.Text != "ทั้งหมด" && !string.IsNullOrEmpty(cbDeptFilter.Text))
                {
                    query = query.Where(r => r.PR_Department == cbDeptFilter.Text);
                }

                var list = query.Select(r => new
                {
                    ID = r.PR_ID,
                    Date = r.PR_Date,
                    Dept = r.PR_Department,
                    Status = r.PR_Status
                }).ToList();

                dgvPRMaster.DataSource = list;
            }
        }

        private void InitFilters()
        {
            // 1. ตั้งค่าสถานะ (Status Filter) [cite: 2026-03-08]
            cbStatusFilter.Items.Clear();
            cbStatusFilter.Items.AddRange(new string[] {
                "ทั้งหมด",
                "รออนุมัติ",
                "อนุมัติแล้ว",
                "ทำใบสั่งซื้อแล้ว",
                "ถูกปฏิเสธ"
            });
            cbStatusFilter.SelectedIndex = 0; // เลือก "ทั้งหมด" เป็นค่าเริ่มต้น

            // 2. ตั้งค่าแผนก (Department Filter) [cite: 2026-03-08]
            // คุณสามารถพิมพ์เอง หรือดึงจาก Database ก็ได้ครับ (แนะนำแบบพิมพ์เองถ้าแผนกคงที่)
            cbDeptFilter.Items.Clear();
            cbDeptFilter.Items.AddRange(new string[] {
                "ทั้งหมด",
                "ครัวร้อน",
                "บาร์น้ำ",
                "ของสด"
            });
            cbDeptFilter.SelectedIndex = 0; // เลือก "ทั้งหมด" เป็นค่าเริ่มต้น
        }

        // เมื่อคลิกเลือกใบ PR ในตาราง Master [cite: 2026-02-26]
        private void dgvPRMaster_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // ถ้ามีของค้างในตะกร้า ให้ถามก่อนว่าจะล้างตะกร้าเพื่อดูใบเก่าไหม
            if (_cart.Count > 0)
            {
                var result = MessageBox.Show("คุณมีรายการที่ยังไม่ได้บันทึก ต้องการทิ้งรายการนี้เพื่อดูข้อมูลใบอื่นใช่หรือไม่?",
                                             "ยืนยัน", MessageBoxButtons.YesNo);
                if (result == DialogResult.No) return;
                _cart.Clear(); // ล้างตะกร้าเพื่อให้พร้อมโชว์รายละเอียดใบเก่า
            }

            // ดึงค่า ID จากตารางฝั่งซ้ายที่มีคอลัมน์ ID อยู่จริง
            string prId = dgvPRMaster.Rows[e.RowIndex].Cells["ID"].Value.ToString();

            // โหลดรายละเอียดมาโชว์ที่ตารางฝั่งขวา
            LoadDetailsFromDB(prId);
        }

        // ฟังก์ชันสำหรับโหลดรายละเอียดสินค้าจาก Database มาแสดงผลที่ตารางฝั่งขวา [cite: 2026-03-08]
        private void LoadDetailsFromDB(string prId)
        {
            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    var details = db.PurchaseRequisitionDetails
                        .Include(d => d.Product)
                        .Where(d => d.PR_ID == prId)
                        .ToList(); // ดึงมาเป็น List ก่อน

                    // ผูกข้อมูลเข้ากับตารางฝั่งขวา
                    dgvPRDetails.DataSource = details.Select(d => new
                    {
                        รายการสินค้า = d.Product?.Pro_name ?? "ไม่ระบุ",
                        จำนวน = d.Request_Qty
                    }).ToList();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void RefreshCartGrid()
        {
            // บังคับล้าง DataSource เดิมออกก่อน
            dgvPRDetails.DataSource = null;

            // นำตะกร้าสินค้าปัจจุบัน (ซึ่งอาจจะเป็นค่าว่างหลังจาก Clear) มาใส่
            dgvPRDetails.DataSource = _cart.ToList();

            if (dgvPRDetails.Columns.Count > 0)
            {
                // เช็คเผื่อกรณีชื่อคอลัมน์จาก Class PRCartItem
                if (dgvPRDetails.Columns["Pro_name"] != null)
                    dgvPRDetails.Columns["Pro_name"].HeaderText = "รายการสินค้า";

                if (dgvPRDetails.Columns["Request_Qty"] != null)
                    dgvPRDetails.Columns["Request_Qty"].HeaderText = "จำนวนที่ขอ";

                // ซ่อนรหัสสินค้าไว้เบื้องหลัง (ถ้าไม่ต้องการให้รกตา)
                if (dgvPRDetails.Columns["Pro_id"] != null)
                    dgvPRDetails.Columns["Pro_id"].Visible = false;
            }
        }

        //private void LoadPRData()
        //{
        //    using (var db = DbConfig.GetDbContext())
        //    {
        //        // ดึงข้อมูล PR Header พร้อมชื่อพนักงานที่ขอซื้อ [cite: 2026-03-08]
        //        var query = db.PurchaseRequisitions.Include(r => r.Employee).AsQueryable();

        //        // 1. กรองตามช่วงวันที่ [cite: 2026-03-08]
        //        DateTime startDate = dtpStart.Value.Date;
        //        DateTime endDate = dtpEnd.Value.Date.AddDays(1).AddTicks(-1);
        //        query = query.Where(r => r.PR_Date >= startDate && r.PR_Date <= endDate);

        //        // 2. กรองตามสถานะ (Pending, Approved, Rejected) [cite: 2026-03-08]
        //        if (cbStatusFilter.SelectedIndex > 0)
        //            query = query.Where(r => r.PR_Status == cbStatusFilter.Text);

        //        // 3. กรองตามแผนก [cite: 2026-03-08]
        //        if (cbDeptFilter.SelectedIndex > 0)
        //            query = query.Where(r => r.PR_Department == cbDeptFilter.Text);

        //        dgvPRDetails.DataSource = query.Select(r => new
        //        {
        //            ID = r.PR_ID,
        //            Date = r.PR_Date,
        //            Dept = r.PR_Department,
        //            Status = r.PR_Status,
        //            Requester = r.Employee.EmployName
        //        }).ToList();
        //    }
        //}

        // ==========================================
        // ส่วนจัดการปุ่ม เพิ่ม / ลบ / ล้าง
        // ==========================================


        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 1. ตรวจสอบค่าก่อน (ถ้าคุณเผลอกดย้ำตอนค่าเป็น 0 มันจะเด้ง Error นี้)
            if (cboProducts.SelectedIndex < 0 || numQty.Value <= 0)
            {
                MessageBox.Show("กรุณาเลือกสินค้าและระบุจำนวนที่มากกว่า 0", "แจ้งเตือน");
                return;
            }

            // 2. Logic การเพิ่ม/บวกจำนวนเดิม (ที่คุณแก้ไปก่อนหน้านี้) [cite: 2026-03-08]
            string proId = cboProducts.SelectedValue.ToString();
            var existingItem = _cart.FirstOrDefault(c => c.Pro_id == proId);

            if (existingItem != null)
            {
                existingItem.Request_Qty += (int)numQty.Value;
            }
            else
            {
                _cart.Add(new PRCartItem
                {
                    Pro_id = proId,
                    Pro_name = cboProducts.Text,
                    Request_Qty = (int)numQty.Value
                });
            }

            // 3. 🚩 จุดที่ทำให้เกิดปัญหา: การรีเซ็ตค่า [cite: 2026-03-08]
            // ถ้าคุณอยากกดย้ำๆ ได้ ให้คอมเมนต์บรรทัดด้านล่างนี้ออกครับ
            // numQty.Value = 0; 

            RefreshCartGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPRDetails.CurrentRow != null)
            {
                var item = (PRCartItem)dgvPRDetails.CurrentRow.DataBoundItem;
                _cart.Remove(item);
                RefreshGrid();
                RefreshCartGrid();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (_cart.Count > 0)
            {
                _cart.Clear();
                RefreshGrid();
                RefreshCartGrid();
            }
        }

        // ฟังก์ชันช่วยรีเฟรชตาราง
        private void RefreshGrid()
        {
            dgvPRDetails.DataSource = null;
            dgvPRDetails.DataSource = _cart;

            // เปลี่ยนชื่อหัวคอลัมน์ให้เป็นภาษาไทย
            if (dgvPRDetails.Columns.Count > 0)
            {
                dgvPRDetails.Columns["Pro_id"].HeaderText = "รหัสสินค้า";
                dgvPRDetails.Columns["Pro_name"].HeaderText = "รายการสินค้า";
                dgvPRDetails.Columns["Request_Qty"].HeaderText = "จำนวนที่ขอ";
            }
        }

        // ==========================================
        // ส่วนของการบันทึกข้อมูล
        // ==========================================

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show("กรุณาเพิ่มรายการสินค้าอย่างน้อย 1 รายการ", "แจ้งเตือน");
                return;
            }

            try
            {
                //// ตรวจสอบรหัสพนักงาน
                //var empExists = db.Employees.Any(emp => emp.Employ_ID == txtEmploy_ID.Text);
                //if (!empExists)
                //{
                //    MessageBox.Show($"ไม่พบรหัสพนักงาน '{txtEmploy_ID.Text}' ในระบบ", "แจ้งเตือน");
                //    return;
                //}

                // เริ่มสร้างใบขอซื้อ (Header)
                var pr = new PurchaseRequisition
                {
                    PR_ID = txtPR_ID.Text,
                    Employ_ID = txtEmploy_ID.Text,
                    PR_Department = cboPRDept.Text, // ✅ บันทึกแผนกตามตัวกรองที่เลือก [cite: 2026-03-08]
                    PR_Date = dtpPR_Date.Value,
                    PR_Status = cboStatus.Text,
                    PurchaseRequisitionDetails = new List<PurchaseRequisitionDetail>()
                };

                // วนลูปอ่านจากตะกร้า (_cart) มาสร้างเป็น Detail
                foreach (var item in _cart)
                {
                    var detail = new PurchaseRequisitionDetail
                    {
                        Pro_id = item.Pro_id,
                        Request_Qty = item.Request_Qty
                    };
                    pr.PurchaseRequisitionDetails.Add(detail);
                }

                db.PurchaseRequisitions.Add(pr);
                db.SaveChanges(); // Entity Framework จะจัดการ Save ลง 2 ตารางพร้อมกันแบบ Transaction ให้อัตโนมัติ

                MessageBox.Show("บันทึกใบขอซื้อสำเร็จ!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ล้างหน้าจอเตรียมทำรายการใหม่
                LoadPRMaster();
                _cart.Clear();
                RefreshGrid();
                numQty.Value = 0;
                txtPR_ID.Text = GeneratePR_ID(); // รันรหัสใหม่มารอ
            }
            catch (Exception ex)
            {
                db.ChangeTracker.Clear(); // ล้าง Cache กัน Error ซ้อน
                string errorMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show("เกิดข้อผิดพลาดในการบันทึก:\n" + errorMsg, "Error");
            }
        }

        // ==========================================
        // ฟังก์ชันสร้างรหัสอัตโนมัติ
        // ==========================================
        private string GeneratePR_ID()
        {
            try
            {
                var lastPR = db.PurchaseRequisitions.OrderByDescending(p => p.PR_ID).FirstOrDefault();
                if (lastPR == null) return "PR-00001";

                string lastId = lastPR.PR_ID;
                int runningNumber = int.Parse(lastId.Substring(3));
                runningNumber++;

                return "PR-" + runningNumber.ToString("D5");
            }
            catch
            {
                return string.Empty;
            }
        }

        private void dgvPR_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string prId = dgvPRDetails.Rows[e.RowIndex].Cells["ID"].Value.ToString();

            using (var db = DbConfig.GetDbContext())
            {
                var details = db.PurchaseRequisitionDetails
                    .Include(d => d.Product)
                    .Where(d => d.PR_ID == prId)
                    .Select(d => new
                    {
                        ProductName = d.Product.Pro_name,
                        Quantity = d.Request_Qty
                    }).ToList();

                dgvPRDetails.DataSource = details;
            }
        }

        private void btnNewPR_Click(object sender, EventArgs e)
        {
            // 1. ล้างตะกร้าสินค้าเดิมให้ว่าง
            _cart.Clear();

            // 2. รันรหัส PR ใหม่มารอ (เช่น PR-00003)
            txtPR_ID.Text = GeneratePR_ID();

            // 3. ล้างตารางฝั่งขวาให้กลับมาผูกกับตะกร้าว่างๆ
            RefreshCartGrid();

            // 4. ตั้งค่าหน้าจอให้พร้อมกรอกข้อมูลใหม่
            numQty.Value = 0;
            cboProducts.SelectedIndex = -1; // ล้างช่องเลือกสินค้า

            MessageBox.Show("พร้อมสำหรับการสร้างใบขอซื้อใหม่แล้วครับ", "ระบบพร้อมใช้งาน");
        }

        private void cbDeptFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPRMaster();
        }

        private void cbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPRMaster();
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            LoadPRMaster();
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            LoadPRMaster();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }

    // ==========================================
    // สร้าง Class จำลองสำหรับพักข้อมูลในตาราง
    // (วางไว้นอกคลาส FormPR แต่ยังอยู่ในไฟล์เดียวกันได้)
    // ==========================================
    public class PRCartItem
    {
        public string Pro_id { get; set; }
        public string Pro_name { get; set; }
        public int Request_Qty { get; set; }
    }
}