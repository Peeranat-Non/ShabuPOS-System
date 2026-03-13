using Microsoft.Data.SqlClient;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BuffetAPI.Models;

namespace ShabuPOS
{
    public partial class FromCashier : Form
    {
        // ✅ ข้อมูลชุดนี้ต้องใช้ร่วมกันทุกหน้า
        public static string SharedTableNo = null;
        public static BillResult SharedBillData = null;

        private readonly string _connectionString = DbConfig.ConnectionString;
        private BillResult _currentBill;
        private decimal _memberDiscountPercent = 5; // ✅ ปรับเป็น 5% ตามรูปหน้าเช็คบิล

        public FromCashier()
        {
            InitializeComponent();
        }

        // ✅ ฟังก์ชันคำนวณมาตรฐาน (แก้ชื่อ Package_Price เป็น PackagePrice)
        //private BillResult CalculateFinalBill(decimal price, int qty, decimal promoPct, decimal memPct)
        //{
        //    if (price <= 0) price = 299.00m; // ✅ ดักค่าเผื่อราคามาเป็น 0

        //    var res = new BillResult();
        //    res.Package_Price = price; // 👈 แก้จาก Package_Price เป็น PackagePrice
        //    res.TotalPeople = qty;
        //    res.Subtotal = price * qty;

        //    res.PromoDiscAmount = res.Subtotal * (promoPct / 100m);
        //    res.MemberDiscAmount = res.Subtotal * (memPct / 100m);

        //    decimal net = res.Subtotal - res.PromoDiscAmount - res.MemberDiscAmount;
        //    res.VatAmount = Math.Round(net * 0.07m, 2, MidpointRounding.AwayFromZero);
        //    res.GrandTotal = net + res.VatAmount;
        //    return res;
        //}

        private void SetupDataGridView()
        {
            dgvReceipt.Columns.Clear();
            dgvReceipt.Columns.Add("ColTable", "โต๊ะ");
            dgvReceipt.Columns.Add("ColPkg", "แพ็กเกจ");
            dgvReceipt.Columns.Add("ColPricePerHead", "ราคา/คน");
            dgvReceipt.Columns.Add("ColQty", "จำนวนคน");
            dgvReceipt.Columns.Add("ColGrandTotal", "ราคารวมสุทธิ");

            dgvReceipt.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReceipt.AllowUserToAddRows = false;
            dgvReceipt.ReadOnly = true;
        }

        private void FromCashier_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            if (SharedBillData != null)
            {
                DisplayBillData();
            }

            ThemeConfig.FormatMinimalistDataGridView(dgvReceipt);
        }

        // ✅ เปลี่ยนเป็น public เพื่อให้หน้า Form1 เรียกใช้ได้
        public void DisplayBillData()
        {
            try
            {
                if (SharedBillData == null) return;

                dgvReceipt.Rows.Clear();

                decimal price = SharedBillData.Package_Price;
                int qty = SharedBillData.TotalPeople;

                _currentBill = BillResult.Calculate(price, qty, 20, _memberDiscountPercent);

                // ⭐ ใส่ชื่อแพ็คเกจกลับเข้าไป
                _currentBill.Package_Name = SharedBillData.Package_Name;
                _currentBill.OrderHeaderId = SharedBillData.OrderHeaderId;

                dgvReceipt.Rows.Add(
                    SharedTableNo,
                    SharedBillData.Package_Name,
                    price.ToString("N2"),
                    qty,
                    _currentBill.GrandTotal.ToString("N2")
                );

                txtPrice.Text = _currentBill.GrandTotal.ToString("N2");
                txtCash.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying data: " + ex.Message);
            }
        }

        // ... (ส่วน btnPrint_Click และ SavePaymentAndClearTable เหมือนเดิมที่คุณมี) ...

        private void SavePayment(decimal cash)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string payId = "PAY" + DateTime.Now.ToString("HHmmss");

                    string sql = @"INSERT INTO Payment
                           (Pay_id, Pay_forkey, Pay_mem, Pay_date, Pay_time, Pay_amont, Pay_channel)
                           VALUES
                           (@id, @promo, @mem, @date, @time, @amount, @channel)";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@id", payId);
                    cmd.Parameters.AddWithValue("@promo", DBNull.Value);
                    cmd.Parameters.AddWithValue("@mem", DBNull.Value);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@time", DateTime.Now.TimeOfDay);
                    cmd.Parameters.AddWithValue("@amount", (int)_currentBill.GrandTotal);
                    cmd.Parameters.AddWithValue("@channel", "CASH");

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("บันทึก Payment ไม่สำเร็จ: " + ex.Message);
            }
        }

        private void ShowPrintPreview(decimal cash)
        {
            PrintDocument pd = new PrintDocument();

            pd.PrintPage += (s, ev) =>
            {
                decimal change = cash - _currentBill.GrandTotal;
                DrawReceiptPage(ev, cash, change);
            };

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = pd;
            preview.WindowState = FormWindowState.Maximized;

            preview.ShowDialog();

            // บันทึกการจ่ายเงิน
            SavePayment(cash);

            // ⭐ ปิดโต๊ะ
            CloseTable();

            if (Application.OpenForms["Form1"] is Form1 mainForm)
            {
                _ = mainForm.RefreshTableLayout();
            }

            MessageBox.Show("จ่ายเงินเรียบร้อยแล้ว โต๊ะถูกปิดแล้ว", "Payment Success");

            ClearBill();
        }

        private void ClearBill()
        {
            dgvReceipt.Rows.Clear();

            txtPrice.Text = "";
            txtCash.Text = "";

            _currentBill = null;
        }
        public class Bill
        {
            public int OrderHeaderId { get; set; }
            public decimal GrandTotal { get; set; }
            public string PackageId { get; set; }
        }

        private void CloseTable()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // ปิด Order
                    string sql1 = @"UPDATE OrderHeader
                            SET TotalStatus = 'Completed'
                            WHERE OrderHeaderId = @id";

                    SqlCommand cmd1 = new SqlCommand(sql1, conn);
                    cmd1.Parameters.AddWithValue("@id", _currentBill.OrderHeaderId);
                    cmd1.ExecuteNonQuery();

                    // ⭐ ลบ Service เพื่อเคลียร์โต๊ะ
                    string serviceId = "SRV" + SharedTableNo.PadLeft(3, '0');

                    string sql2 = @"DELETE FROM Service
                            WHERE ServiceId = @serviceId";

                    SqlCommand cmd2 = new SqlCommand(sql2, conn);
                    cmd2.Parameters.AddWithValue("@serviceId", serviceId);
                    cmd2.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ปิดโต๊ะไม่สำเร็จ : " + ex.Message);
            }
        }

        private void DrawReceiptPage(PrintPageEventArgs e, decimal cash, decimal change)
        {
            Graphics g = e.Graphics;

            Font header = new Font("Tahoma", 16, FontStyle.Bold);
            Font body = new Font("Tahoma", 11);
            Font bold = new Font("Tahoma", 11, FontStyle.Bold);

            float centerX = e.PageBounds.Width / 2;
            float left = 100;
            float right = e.PageBounds.Width - 100;

            float y = 50;

            StringFormat rightAlign = new StringFormat();
            rightAlign.Alignment = StringAlignment.Far;

            StringFormat center = new StringFormat();
            center.Alignment = StringAlignment.Center;

            // HEADER
            g.DrawString("SHABU NOONY", header, Brushes.Black, centerX, y, center);
            y += 40;

            g.DrawString("Tax Invoice (ABB)", body, Brushes.Black, centerX, y, center);
            y += 40;

            g.DrawLine(Pens.Black, left, y, right, y);
            y += 20;

            g.DrawString($"Date: {DateTime.Now:dd/MM/yyyy HH:mm}", body, Brushes.Black, left, y);
            y += 25;

            g.DrawString($"Table: {SharedTableNo}", body, Brushes.Black, left, y);
            y += 30;

            g.DrawLine(Pens.Black, left, y, right, y);
            y += 20;

            // ITEM
            g.DrawString("Item", bold, Brushes.Black, left, y);
            g.DrawString("Amount", bold, Brushes.Black, right, y, rightAlign);
            y += 30;

            string item = $"{_currentBill.Package_Name} (x{_currentBill.TotalPeople})";

            g.DrawString(item, body, Brushes.Black, left, y);
            g.DrawString(_currentBill.Subtotal.ToString("N2"), body, Brushes.Black, right, y, rightAlign);

            y += 30;

            g.DrawLine(Pens.Black, left, y, right, y);
            y += 20;

            // SUMMARY

            g.DrawString("Subtotal", body, Brushes.Black, left, y);
            g.DrawString(_currentBill.Subtotal.ToString("N2"), body, Brushes.Black, right, y, rightAlign);
            y += 25;

            g.DrawString("Promo Disc", body, Brushes.Black, left, y);
            g.DrawString("-" + _currentBill.PromoDiscAmount.ToString("N2"), body, Brushes.Black, right, y, rightAlign);
            y += 25;

            g.DrawString("Member Disc", body, Brushes.Black, left, y);
            g.DrawString("-" + _currentBill.MemberDiscAmount.ToString("N2"), body, Brushes.Black, right, y, rightAlign);
            y += 25;

            g.DrawString("VAT 7%", body, Brushes.Black, left, y);
            g.DrawString(_currentBill.VatAmount.ToString("N2"), body, Brushes.Black, right, y, rightAlign);
            y += 30;

            g.DrawLine(Pens.Black, left, y, right, y);
            y += 25;

            g.DrawString("TOTAL", bold, Brushes.Black, left, y);
            g.DrawString(_currentBill.GrandTotal.ToString("N2"), bold, Brushes.Black, right, y, rightAlign);
            y += 40;

            // PAYMENT
            g.DrawString("Cash", body, Brushes.Black, left, y);
            g.DrawString(cash.ToString("N2"), body, Brushes.Black, right, y, rightAlign);
            y += 25;

            g.DrawString("Change", bold, Brushes.Black, left, y);
            g.DrawString(change.ToString("N2"), bold, Brushes.Black, right, y, rightAlign);
            y += 40;

            g.DrawLine(Pens.Black, left, y, right, y);
            y += 30;

            g.DrawString("Thank you for your visit!", body, Brushes.Black, centerX, y, center);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_currentBill == null)
            {
                MessageBox.Show("ยังไม่มีข้อมูลบิล");
                return;
            }

            if (!decimal.TryParse(txtCash.Text, out decimal cash))
            {
                MessageBox.Show("กรุณากรอกจำนวนเงินที่ถูกต้อง");
                return;
            }

            if (cash < _currentBill.GrandTotal)
            {
                MessageBox.Show("เงินไม่พอ");
                return;
            }

            ShowPrintPreview(cash);
        }
    }
}