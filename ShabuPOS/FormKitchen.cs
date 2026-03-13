using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace ShabuPOS
{
    public partial class FormKitchen : Form
    {
        private string connString = DbConfig.ConnectionString;

        public FormKitchen()
        {
            InitializeComponent();

            // ตั้งค่าพื้นฐานตาราง
            dgvOrderHeader.ReadOnly = true;
            dgvOrderHeader.AllowUserToAddRows = false;
            dgvOrderDetails.AllowUserToAddRows = false;

            // ผูกเหตุการณ์คลิกที่ปุ่มในตาราง
            dgvOrderDetails.CellContentClick += dgvOrderDetails_CellContentClick;
        }

        private void FormKitchen_Load(object sender, EventArgs e)
        {
            ThemeConfig.ApplyGlobalFont(this);
            ThemeConfig.FormatMinimalistDataGridView(dgvOrderHeader);
            ThemeConfig.FormatMinimalistDataGridView(dgvOrderDetails);
            ThemeConfig.StyleButtonAdd(btnRefresh);

            // ✅ ซ่อนปุ่ม Save และ ComboBox เดิม เพราะเราจะกดในตารางแทน
            btnRefresh.Visible = true;
            //cbStatus.Visible = false;

            LoadHeaderData();
        }

        // 1. ปรับปรุงฟังก์ชันโหลด Header ให้เพิ่ม "ปุ่มจัดการทั้งบิล" เข้าไป
        private void LoadHeaderData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = @"SELECT OrderHeaderId AS OrderId, OrderTable, OrderTime, TotalStatus AS OrderStatus 
                           FROM OrderHeader WHERE TotalStatus != 'Completed' ORDER BY OrderTime ASC";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvOrderHeader.DataSource = dt;

                    // ✅ ตรวจสอบและสร้างคอลัมน์ปุ่ม "จัดการบิล" ที่ตารางบน
                    if (!dgvOrderHeader.Columns.Contains("btnServeAll"))
                    {
                        DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
                        btnCol.Name = "btnServeAll";
                        btnCol.HeaderText = "จัดการบิล";
                        btnCol.Text = "เสิร์ฟทั้งหมด";
                        btnCol.UseColumnTextForButtonValue = true;
                        dgvOrderHeader.Columns.Add(btnCol);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void dgvOrderHeader_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrderHeader.CurrentRow != null && dgvOrderHeader.CurrentRow.Cells["OrderId"].Value != null)
            {
                string orderId = dgvOrderHeader.CurrentRow.Cells["OrderId"].Value.ToString();
                LoadDetailData(orderId); // โหลดรายการเมนูลง dgvOrderDetails [cite: 2026-03-07]
            }
        }

        // 4. จัดการเหตุการณ์เมื่อกดปุ่ม "เสิร์ฟทั้งหมด" [cite: 2026-03-07]
        private void dgvOrderHeader_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOrderHeader.Columns[e.ColumnIndex].Name == "btnServeAll" && e.RowIndex >= 0)
            {

                string orderId = dgvOrderHeader.Rows[e.RowIndex].Cells["OrderId"].Value.ToString();

                if (MessageBox.Show($"เสิร์ฟอาหารทั้งหมดของโต๊ะนี้ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    UpdateFullOrderStatus(orderId);
                }
            }
        }

        // 3. ฟังก์ชัน SQL สำหรับอัปเดตข้อมูลทุกตารางพร้อมกัน (Transaction)
        private void UpdateFullOrderStatus(string orderId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    // ใช้ Transaction เพื่อให้มั่นใจว่าข้อมูลอัปเดตครบทั้ง 2 ตาราง [cite: 2025-11-23]
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            // ✅ 1. อัปเดตรายการอาหารทุกจานในบิลนี้ให้เป็น 'Served' [cite: 2026-03-07]
                            string updateDetails = "UPDATE OrderDetail SET ItemStatus = 'Served' WHERE OrderHeaderId = @id";
                            SqlCommand cmd1 = new SqlCommand(updateDetails, conn, trans);
                            cmd1.Parameters.AddWithValue("@id", orderId);
                            cmd1.ExecuteNonQuery();

                            // ✅ 2. อัปเดตสถานะหัวบิลให้เป็น 'Completed' เพื่อให้หายไปจากหน้าครัว [cite: 2026-02-26]
                            string updateHeader = "UPDATE OrderHeader SET TotalStatus = 'Completed' WHERE OrderHeaderId = @id";
                            SqlCommand cmd2 = new SqlCommand(updateHeader, conn, trans);
                            cmd2.Parameters.AddWithValue("@id", orderId);
                            cmd2.ExecuteNonQuery();

                            trans.Commit(); // บันทึกการเปลี่ยนแปลงทั้งหมด [cite: 2025-11-23]

                            MessageBox.Show("อัปเดตสถานะทั้งบิลสำเร็จ!");
                            LoadHeaderData(); // รีเฟรชตารางบนเพื่อเอาบิลที่เสร็จแล้วออก
                            dgvOrderDetails.DataSource = null; // เคลียร์ตารางล่าง
                        }
                        catch
                        {
                            trans.Rollback(); // ถ้าพลาดให้ยกเลิกทั้งหมด [cite: 2025-11-23]
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message); }
        }

        // ✅ ฟังก์ชันโหลดข้อมูลที่เพิ่ม "ปุ่มเปลี่ยนสถานะ" เข้าไปในตาราง
        // 3. ฟังก์ชันโหลดรายการอาหารลงตารางล่าง
        private void LoadDetailData(string headerId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = @"SELECT d.DetailId, m.MenuName, d.OrderQuantity, d.ItemStatus 
                           FROM OrderDetail d JOIN Menu m ON d.MenuId = m.MenuId
                           WHERE d.OrderHeaderId = @headerId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@headerId", headerId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // ✅ แสดงรายการเมนูใน dgvOrderDetails ตามปกติ
                    dgvOrderDetails.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        // ✅ จัดการเมื่อมีการกดปุ่มในตาราง [cite: 2026-03-07]
        private void dgvOrderDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ตรวจสอบว่าเป็นคอลัมน์ปุ่มที่กดหรือไม่
            if (dgvOrderDetails.Columns[e.ColumnIndex].Name == "btnAction" && e.RowIndex >= 0)
            {
                string detailId = dgvOrderDetails.Rows[e.RowIndex].Cells["DetailId"].Value.ToString();
                string currentStatus = dgvOrderDetails.Rows[e.RowIndex].Cells["ItemStatus"].Value.ToString();
                string nextStatus = "";

                // Logic การเลื่อนสถานะอัตโนมัติ [cite: 2026-03-07]
                if (currentStatus == "Waiting") nextStatus = "Cooking";
                else if (currentStatus == "Cooking") nextStatus = "Served";
                else return; // ถ้า Served แล้วไม่ต้องทำอะไรต่อ

                UpdateItemStatus(detailId, nextStatus);
            }
        }

        private void UpdateItemStatus(string detailId, string nextStatus)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "UPDATE OrderDetail SET ItemStatus = @status WHERE DetailId = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@status", nextStatus);
                        cmd.Parameters.AddWithValue("@id", detailId);
                        cmd.ExecuteNonQuery();
                    }
                }
                // รีโหลดตารางล่างเพื่อดูผลลัพธ์ทันที
                var currentHeaderId = dgvOrderHeader.CurrentRow.Cells["OrderId"].Value.ToString();
                LoadDetailData(currentHeaderId);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dgvOrderHeader_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // 1. โหลดข้อมูลหัวบิลใหม่ลงตารางบน
            LoadHeaderData();

            // 2. ตรวจสอบว่ามีข้อมูลออเดอร์ในตารางบนไหม
            if (dgvOrderHeader.Rows.Count > 0)
            {
                // เลือกแถวแรกให้อัตโนมัติ
                dgvOrderHeader.Rows[0].Selected = true;

                // ดึง OrderId จากแถวแรกมาโหลดตารางล่างต่อทันที
                string firstOrderId = dgvOrderHeader.Rows[0].Cells["OrderId"].Value.ToString();
                LoadDetailData(firstOrderId);
            }
            else
            {
                // ถ้าไม่มีออเดอร์เหลืออยู่เลย ถึงค่อยให้ตารางล่างว่างเปล่า
                dgvOrderDetails.DataSource = null;
            }

            // ลบ MessageBox ออกหากไม่ต้องการให้รบกวนเวลาทำงานครับ
        }
    }
}