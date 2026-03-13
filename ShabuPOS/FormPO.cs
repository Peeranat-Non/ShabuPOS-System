using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using BuffetAPI.Data;
using BuffetAPI.Models;

namespace ShabuPOS
{
    public partial class FormPO : Form
    {
        BuffetDbContext db = DbConfig.GetDbContext();
        private List<POCartItem> _cart = new List<POCartItem>();

        public FormPO()
        {
            InitializeComponent();
            SetupGrids();
        }

        private void SetupGrids()
        {
            dgvPODetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPODetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPODetails.ReadOnly = true;
            dgvPODetails.AllowUserToAddRows = false;

            // ตั้งค่าตารางรายการ PR (ฝั่งซ้าย)
            dgvPRList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPRList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPRList.ReadOnly = true;
            dgvPRList.AllowUserToAddRows = false;
        }

        private void FormPO_Load(object sender, EventArgs e)
        {
            InitPOFilters();
            // ✅ นำรหัสพนักงานที่ Login อยู่มาใส่ในช่องอัตโนมัติ
            txtEmploy_ID.Text = UserSession.CurrentEmployId;

            // ✅ (แนะนำ) ล็อกช่องไว้ไม่ให้พนักงานแก้รหัสตัวเอง
            txtEmploy_ID.ReadOnly = true;

            cboStatus.Items.Clear();
            cboStatus.Items.Add("รอรับสินค้า");
            cboStatus.SelectedIndex = 0;
            cboStatus.Enabled = false;

            txtPO_ID.Text = GeneratePO_ID();
            txtPO_ID.ReadOnly = true;
            lblTotalAmount.Text = "0.00 บาท";

            // ✅ เรียกใช้ ThemeConfig เพื่อตกแต่งตาราง!
            ThemeConfig.FormatMinimalistDataGridView(dgvPRList);
            ThemeConfig.FormatMinimalistDataGridView(dgvPODetails);

            // ✅ ตกแต่งปุ่มพร้อมใส่ Hover Effect
            ThemeConfig.StyleButtonAdd(btnSavePO);

            // ✅ โหลดรายการ PR ที่ "รออนุมัติ" มาโชว์ทันที
            LoadPendingPRs();

            ThemeConfig.ApplyGlobalFont(this);
        }

        private void InitPOFilters()
        {
            // ตั้งค่าสถานะใบสั่งซื้อ
            cbPOStatusFilter.Items.Clear();
            cbPOStatusFilter.Items.AddRange(new string[] { "ทั้งหมด", "รอรับสินค้า", "รับสินค้าเรียบร้อย", "ยกเลิก" });
            cbPOStatusFilter.SelectedIndex = 0;

            // ตั้งวันที่เริ่มต้นเป็นต้นเดือนปัจจุบัน
            dtpPOStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpPOEnd.Value = DateTime.Now;
        }

        private void LoadPOMaster()
        {
            using (var db = DbConfig.GetDbContext())
            {
                // 1. ดึงข้อมูล PO พร้อมความสัมพันธ์ [cite: 2026-03-08]
                var query = db.PurchaseOrders.Include(p => p.Employee).AsQueryable();

                // 2. กรองตามวันที่ [cite: 2026-03-08]
                DateTime start = dtpPOStart.Value.Date;
                DateTime end = dtpPOEnd.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(p => p.Po_date >= start && p.Po_date <= end);

                // 3. กรองตามสถานะ [cite: 2026-03-08]
                if (cbPOStatusFilter.Text != "ทั้งหมด")
                {
                    query = query.Where(p => p.Po_status == cbPOStatusFilter.Text);
                }

                // 4. กรองตามรหัสใบขอซื้อ (PR_ID) ถ้ามีการพิมพ์ค้นหา [cite: 2026-03-08]
                if (!string.IsNullOrWhiteSpace(txtSearchPR.Text))
                {
                    query = query.Where(p => p.Po_id.Contains(txtSearchPR.Text.Trim()));
                }

                // 5. นำข้อมูลลง DataGridView
                dgvPRList.DataSource = query.Select(p => new
                {
                    ID = p.Po_id,               // ✅ ตรงกับ Po_id ใน Model
                    Date = p.Po_date,           // ✅ ตรงกับ Po_date ใน Model
                    Ref_PR = p.Po_Buyreq,       // ✅ ตรงกับ Po_Buyreq ใน Model
                    Status = p.Po_status,       // ✅ ตรงกับ Po_status ใน Model
                    By = p.Employee.EmployName
                }).OrderByDescending(p => p.Date).ToList();
            }
        }

        private void LoadPendingPRs()
        {
            var pendingPRs = db.PurchaseRequisitions
                               .Where(p => p.PR_Status == "รออนุมัติ")
                               .Select(p => new
                               {
                                   PR_ID = p.PR_ID, // ✅ ตั้งชื่อคอลัมน์เป็น PR_ID
                                   Date = p.PR_Date,
                                   Employee = p.Employ_ID
                               })
                               .ToList();
            dgvPRList.DataSource = pendingPRs;
        }

        // ✅ Event เมื่อคลิกเลือกรายการในตารางฝั่งซ้าย

        private void dgvPRList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // ✅ โหมดสร้างใหม่: ถ้าคลิกตารางที่แสดงใบ PR (มีคอลัมน์ PR_ID)
            if (dgvPRList.Columns.Contains("PR_ID"))
            {
                string prId = dgvPRList.Rows[e.RowIndex].Cells["PR_ID"].Value.ToString();
                txtSearchPR.Text = prId;
                FetchPRData(prId); // ดึงข้อมูล PR ลงตะกร้าเตรียมออก PO
            }
            // ✅ โหมดดูประวัติ: ถ้าคลิกตารางที่แสดงใบ PO (มีคอลัมน์ ID จาก LoadPOMaster)
            else if (dgvPRList.Columns.Contains("ID"))
            {
                string poId = dgvPRList.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                FetchPODetails(poId); // ดึงรายละเอียด PO เดิมมาโชว์
            }
        }

        private void FetchPODetails(string poId)
        {
            using (var db = DbConfig.GetDbContext())
            {
                var details = db.PurchaseOrderDetails
                    .Include(d => d.Product)
                    .Where(d => d.Po_id == poId) // ✅ ใช้ Po_id ตาม Model PurchaseOrderDetail
                    .Select(d => new
                    {
                        รหัสสินค้า = d.Pro_id,
                        รายการสินค้า = d.Product.Pro_name,
                        จำนวนที่สั่ง = d.Order_Qty,
                        ราคาต่อหน่วย = d.Unit_Price, // ✅ ปลดคอมเมนต์และใช้ชื่อตาม Model
                        ราคารวม = d.Order_Qty * d.Unit_Price // ✅ คำนวณยอดรวม
                    }).ToList();

                dgvPODetails.DataSource = details; // ตารางด้านล่าง
            }
        }


        //private void dgvPRList_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex < 0) return;

        //    // ✅ เปลี่ยนจาก "เลขที่ใบขอซื้อ" เป็น "PR_ID" ตามที่ตั้งไว้ในข้อ 1
        //    string prId = dgvPRList.Rows[e.RowIndex].Cells["PR_ID"].Value.ToString();
        //    txtSearchPR.Text = prId;

        //    FetchPRData(prId);
        //}

        private void FetchPRData(string prId)
        {
            try
            {
                var pr = db.PurchaseRequisitions
                           .Include(p => p.PurchaseRequisitionDetails)
                           .ThenInclude(d => d.Product)
                           .FirstOrDefault(p => p.PR_ID == prId);

                if (pr == null) return;

                _cart.Clear();
                foreach (var item in pr.PurchaseRequisitionDetails)
                {
                    _cart.Add(new POCartItem
                    {
                        Pro_id = item.Pro_id,
                        Pro_name = item.Product.Pro_name,
                        Order_Qty = item.Request_Qty,
                        Unit_Price = item.Product.Pro_price ?? 0
                    });
                }
                RefreshGrid();
            }
            catch (Exception ex)
            {
                db.ChangeTracker.Clear();
                MessageBox.Show("เกิดข้อผิดพลาดในการดึงข้อมูลสินค้า: " + ex.Message);
            }
        }

        private void RefreshGrid()
        {
            dgvPODetails.DataSource = null;
            dgvPODetails.DataSource = _cart;

            if (dgvPODetails.Columns.Count > 0)
            {
                dgvPODetails.Columns["Pro_id"].HeaderText = "รหัสสินค้า";
                dgvPODetails.Columns["Pro_name"].HeaderText = "รายการสินค้า";
                dgvPODetails.Columns["Order_Qty"].HeaderText = "จำนวนที่สั่ง";
                dgvPODetails.Columns["Unit_Price"].HeaderText = "ราคา/หน่วย";
                dgvPODetails.Columns["Total_Price"].HeaderText = "ราคารวม";
            }

            decimal netTotal = _cart.Sum(c => c.Total_Price);
            lblTotalAmount.Text = $"{netTotal.ToString("N2")} บาท";
        }

        private void btnSavePO_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show("กรุณาเลือกรายการใบขอซื้อก่อนบันทึก", "แจ้งเตือน");
                return;
            }

            try
            {
                var po = new PurchaseOrder
                {
                    Po_id = txtPO_ID.Text,
                    Po_date = dtpPO_Date.Value,
                    Po_time = DateTime.Now.TimeOfDay,
                    Po_employee = UserSession.CurrentEmployId,
                    Po_status = "รออนุมัติการเงิน",
                    Po_Buyreq = txtSearchPR.Text,
                    Po_approver = "System_Auto", // ระบุชื่อเพื่อแก้ปัญหาค่า NULL
                    PurchaseOrderDetails = new List<PurchaseOrderDetail>()
                };

                foreach (var item in _cart)
                {
                    po.PurchaseOrderDetails.Add(new PurchaseOrderDetail
                    {
                        Pro_id = item.Pro_id,
                        Order_Qty = item.Order_Qty,
                        Unit_Price = item.Unit_Price,
                        Total_Price = item.Total_Price
                    });
                }

                //// เพิ่มโค้ดส่วนนี้ก่อน transaction.Commit() หรือหลัง db.SaveChanges()
                //var pr = db.PurchaseRequisitions.Find(txtPR_Ref.Text);
                //if (pr != null)
                //{
                //    pr.PR_Status = "ทำใบสั่งซื้อแล้ว"; // เปลี่ยนสถานะเพื่อไม่ให้โชว์ในรายการรอทำ PO
                //}

                db.PurchaseOrders.Add(po);

                // ✅ เปลี่ยนสถานะใบ PR เดิม เพื่อให้หายจากตารางฝั่งซ้ายของหน้า PO
                var pr = db.PurchaseRequisitions.Find(txtSearchPR.Text);
                if (pr != null)
                {
                    pr.PR_Status = "ทำใบสั่งซื้อแล้ว";
                }

                db.SaveChanges();

                MessageBox.Show("บันทึกใบสั่งซื้อสำเร็จ!", "สำเร็จ");

                // ล้างค่าและรีเฟรชรายการ
                _cart.Clear();
                RefreshGrid();
                LoadPendingPRs(); // รีเฟรชรายการ PR ฝั่งซ้าย
                txtPO_ID.Text = GeneratePO_ID();
                txtSearchPR.Clear();
            }
            catch (Exception ex)
            {
                db.ChangeTracker.Clear();
                MessageBox.Show("เกิดข้อผิดพลาด: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }

        private string GeneratePO_ID()
        {
            try
            {
                var lastPO = db.PurchaseOrders.OrderByDescending(p => p.Po_id).FirstOrDefault();
                if (lastPO == null) return "PO-00001";

                string lastId = lastPO.Po_id;
                int runningNumber = int.Parse(lastId.Substring(3));
                runningNumber++;
                return "PO-" + runningNumber.ToString("D5");
            }
            catch { return "PO-ERROR"; }
        }

        public class POCartItem
        {
            public string Pro_id { get; set; }
            public string Pro_name { get; set; }
            public int Order_Qty { get; set; }
            public decimal Unit_Price { get; set; }
            public decimal Total_Price => Order_Qty * Unit_Price;
        }

        private void btnSearchPR_Click(object sender, EventArgs e)
        {

        }

        private void txtSearchPR_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbPOStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPOMaster();
        }

        private void dtpPOStart_ValueChanged(object sender, EventArgs e)
        {
            LoadPOMaster();
        }

        private void dtpPOEnd_ValueChanged(object sender, EventArgs e)
        {
            LoadPOMaster();
        }

        private void btnNewPO_Click(object sender, EventArgs e)
        {
            // 1. ล้างตะกร้าสินค้าเดิมและรีเฟรชตารางรายละเอียดด้านล่าง
            _cart.Clear();
            RefreshGrid();

            // 2. รันรหัสใบสั่งซื้อ (PO) ใหม่มารอ
            txtPO_ID.Text = GeneratePO_ID();

            // 3. ล้างช่องค้นหาและช่องข้อมูลอ้างอิง
            txtSearchPR.Clear();
            lblTotalAmount.Text = "0.00 บาท";

            // 4. สลับโหมดตารางฝั่งซ้าย: โหลดเฉพาะ PR ที่รออนุมัติกลับมา
            LoadPendingPRs();

            MessageBox.Show("เข้าสู่โหมดสร้างใบสั่งซื้อใหม่ พร้อมเลือกใบขอซื้อ (PR) จากตารางฝั่งซ้ายครับ", "ระบบพร้อมใช้งาน");
        }
    }
}