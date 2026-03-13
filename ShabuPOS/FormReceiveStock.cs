using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using BuffetAPI.Data;
using BuffetAPI.Models;

namespace ShabuPOS
{
    public partial class FormReceiveStock : Form
    {
        BuffetDbContext db = DbConfig.GetDbContext();

        public FormReceiveStock()
        {
            InitializeComponent();

            dgvReceive.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReceive.AllowUserToAddRows = false;
            dgvReceive.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchPo = txtSearchPO.Text.Trim();
            if (string.IsNullOrEmpty(searchPo))
            {
                MessageBox.Show("กรุณากรอกเลขที่ใบสั่งซื้อ", "แจ้งเตือน");
                return;
            }

            try
            {
                var po = db.PurchaseOrders
                           .Include(p => p.PurchaseOrderDetails)
                           .ThenInclude(d => d.Product)
                           .FirstOrDefault(p => p.Po_id == searchPo);

                if (po == null)
                {
                    MessageBox.Show("ไม่พบใบสั่งซื้อนี้ในระบบ", "ค้นหาไม่พบ");
                    return;
                }

                // ✅ เงื่อนไขสำคัญ: ตรวจสอบสถานะการอนุมัติ
                // สมมติว่าถ้ายังไม่อนุมัติ สถานะจะเป็น "รออนุมัติ" หรือค่าว่าง
                if (po.Po_status != "รอรับสินค้า" && po.Po_status != "อนุมัติแล้ว")
                {
                    MessageBox.Show("ไม่สามารถรับสินค้าได้เนื่องจากใบสั่งซื้อนี้ 'ยังไม่ได้รับการอนุมัติ' จากฝ่ายการเงิน",
                                    "ระงับการรับสินค้า", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (po.Po_status == "รับสินค้าเรียบร้อย")
                {
                    MessageBox.Show("ใบสั่งซื้อนี้ถูกรับสินค้าเข้าสต๊อกไปเรียบร้อยแล้ว", "แจ้งเตือน");
                    return;
                }

                dgvReceive.Columns.Clear();
                dgvReceive.Columns.Add("Pro_id", "รหัสสินค้า");
                dgvReceive.Columns.Add("Pro_name", "ชื่อสินค้า");
                dgvReceive.Columns.Add("Order_Qty", "จำนวนที่สั่ง");

                var receiveCol = new DataGridViewTextBoxColumn();
                receiveCol.Name = "Receive_Qty";
                receiveCol.HeaderText = "จำนวนที่รับจริง (คีย์ที่นี่)";
                dgvReceive.Columns.Add(receiveCol);

                dgvReceive.Columns["Pro_id"].ReadOnly = true;
                dgvReceive.Columns["Pro_name"].ReadOnly = true;
                dgvReceive.Columns["Order_Qty"].ReadOnly = true;
                dgvReceive.Columns["Receive_Qty"].ReadOnly = false;
                dgvReceive.Columns["Receive_Qty"].DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;

                foreach (var item in po.PurchaseOrderDetails)
                {
                    dgvReceive.Rows.Add(
                        item.Pro_id,
                        item.Product.Pro_name,
                        item.Order_Qty,
                        item.Order_Qty
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // ==================================================
        // 🛠️ แก้ไขปุ่มบันทึก: ย้ายการ Save เข้าไปใน Loop และจัดการ Tracking
        // ==================================================
        private void btnSaveReceive_Click(object sender, EventArgs e)
        {
            if (dgvReceive.Rows.Count == 0) return;

            string poId = txtSearchPO.Text.Trim();

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (DataGridViewRow row in dgvReceive.Rows)
                    {
                        if (row.Cells["Pro_id"].Value == null) continue;

                        string proId = row.Cells["Pro_id"].Value.ToString();
                        int receiveQty = Convert.ToInt32(row.Cells["Receive_Qty"].Value);

                        if (receiveQty > 0)
                        {
                            // 1. อัปเดตยอดสต๊อกในตาราง Product
                            var product = db.Products.Find(proId);
                            if (product != null)
                            {
                                product.Pro_stock = (product.Pro_stock ?? 0) + receiveQty;
                            }

                            // 2. บันทึกประวัติการรับเข้า (เรียก GenerateID ทุกรอบ)
                            var stockLog = new Stock
                            {
                                Stock_ID = GenerateStock_ID(), // จะได้ ID ใหม่เสมอเพราะเรา Save ทันทีด้านล่าง
                                Pro_id = proId,
                                Po_id = poId,
                                Stock_Date = DateTime.Now,
                                Transaction_Type = "IN",
                                Stock_Qty = receiveQty
                            };
                            db.Stocks.Add(stockLog);

                            // ✅ สำคัญ: ต้องบันทึกทีละรายการเพื่อให้ GenerateID รอบถัดไปเห็นข้อมูลล่าสุดใน DB
                            db.SaveChanges();
                        }
                    }

                    // 3. อัปเดตสถานะใบ PO
                    var po = db.PurchaseOrders.Find(poId);
                    if (po != null)
                    {
                        po.Po_status = "รับสินค้าเรียบร้อย";
                        db.SaveChanges();
                    }

                    transaction.Commit();
                    MessageBox.Show("บันทึกรับสินค้าเข้าสต๊อกเรียบร้อยแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadPendingPOs(); // ✅ รีเฟรชรายการฝั่งซ้ายใหม่
                    dgvReceive.Rows.Clear(); // ล้างตารางฝั่งขวา
                    dgvReceive.Columns.Clear();
                    txtSearchPO.Clear();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // ✅ ล้างค่าที่ค้างอยู่ในหน่วยความจำเพื่อป้องกัน Error ในการกดครั้งถัดไป
                    db.ChangeTracker.Clear();

                    string errorMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    MessageBox.Show("เกิดข้อผิดพลาด: " + errorMsg, "Error");
                }
            }
        }

        private string GenerateStock_ID()
        {
            // ดึงจากฐานข้อมูลโดยตรงเพื่อให้ได้ค่าล่าสุดจริงๆ
            var lastStock = db.Stocks.OrderByDescending(s => s.Stock_ID).FirstOrDefault();
            if (lastStock == null) return "STK-00001";

            int running = int.Parse(lastStock.Stock_ID.Substring(4));
            running++;
            return "STK-" + running.ToString("D5");
        }

        private void FormReceiveStock_Load(object sender, EventArgs e)
        {
            // ✅ โหลดรายการ PO ที่ "รอรับสินค้า" มาโชว์ในตารางฝั่งซ้ายทันที
            LoadPendingPOs();

            // ✅ เรียกใช้ ThemeConfig เพื่อตกแต่งตาราง!
            ThemeConfig.FormatMinimalistDataGridView(dgvPOList);
            ThemeConfig.FormatMinimalistDataGridView(dgvReceive);


            // ✅ ตกแต่งปุ่มพร้อมใส่ Hover Effect
            ThemeConfig.StyleButtonAdd(btnSaveReceive);

            ThemeConfig.ApplyGlobalFont(this);
        }

        private void LoadPendingPOs()
        {
            try
            {
                // ดึงใบสั่งซื้อที่มีสถานะ "รอรับสินค้า" (ที่ผ่านการอนุมัติจากการเงินแล้ว)
                var pendingPOs = db.PurchaseOrders
                                   .Where(p => p.Po_status == "รอรับสินค้า")
                                   .Select(p => new {
                                       PO_ID = p.Po_id,
                                       Date = p.Po_date,
                                       Supplier = p.Po_Buyreq // หรือฟิลด์อื่นๆ ที่ต้องการโชว์
                                   })
                                   .ToList();
                dgvPOList.DataSource = pendingPOs;

                // ตั้งชื่อหัวคอลัมน์ให้สวยงาม
                if (dgvPOList.Columns.Count > 0)
                {
                    dgvPOList.Columns["PO_ID"].HeaderText = "เลขที่ใบสั่งซื้อ";
                    dgvPOList.Columns["Date"].HeaderText = "วันที่สั่ง";
                    dgvPOList.Columns["Supplier"].HeaderText = "อ้างอิง PR";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถโหลดรายการใบสั่งซื้อได้: " + ex.Message);
            }
        }

        // ✅ เมื่อคลิกที่รายการ PO ในตารางฝั่งซ้าย ให้ดึงสินค้ามาโชว์ฝั่งขวา
        private void dgvPOList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // ดึงเลขที่ PO จากแถวที่เลือก (ระวังชื่อคอลัมน์ต้องตรงกับใน LoadPendingPOs)
            string poId = dgvPOList.Rows[e.RowIndex].Cells["PO_ID"].Value.ToString();
            txtSearchPO.Text = poId; // ใส่เลขในช่องค้นหาให้อัตโนมัติ

            // เรียกฟังก์ชันค้นหาสินค้า (ใช้โค้ดเดิมที่คุณมีในปุ่มค้นหาได้เลย)
            LoadPODetails(poId);
        }

        private void LoadPODetails(string poId)
        {
            try
            {
                var po = db.PurchaseOrders
                           .Include(p => p.PurchaseOrderDetails)
                           .ThenInclude(d => d.Product)
                           .FirstOrDefault(p => p.Po_id == poId);

                if (po == null) return;

                dgvReceive.Columns.Clear();
                // ... (โค้ดสร้าง Column และ Row เหมือนที่คุณเคยทำไว้ใน btnSearch_Click) ...
                // ตัวอย่าง:
                dgvReceive.Columns.Add("Pro_id", "รหัสสินค้า");
                dgvReceive.Columns.Add("Pro_name", "ชื่อสินค้า");
                dgvReceive.Columns.Add("Order_Qty", "จำนวนที่สั่ง");
                dgvReceive.Columns.Add("Receive_Qty", "จำนวนที่รับจริง");

                foreach (var item in po.PurchaseOrderDetails)
                {
                    dgvReceive.Rows.Add(item.Pro_id, item.Product.Pro_name, item.Order_Qty, item.Order_Qty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}