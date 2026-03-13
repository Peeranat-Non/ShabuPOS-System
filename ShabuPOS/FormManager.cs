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

namespace ShabuPOS
{
    public partial class FormManager : Form
    {
        int itemIndex = 0;
        public FormManager()
        {
            InitializeComponent();
        }

        private void FormManager_Load(object sender, EventArgs e)
        {
            // 1. ใส่รายการประเภทรายงาน [cite: 2026-03-08]
            cboReportType.Items.Clear();
            cboReportType.Items.AddRange(new string[] {
                    "รายงานใบสั่งซื้อ (PO)",
                    "รายงานค่าใช้จ่าย (Expense)",
                    "รายงานใบขอซื้อ (PR)"
                });
            cboReportType.SelectedIndex = 0; // เลือกอันแรกเป็นค่าเริ่มต้น

            // 2. ตั้งค่าวันที่เริ่มต้น-สิ้นสุด [cite: 2026-03-08]
            dtpStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEnd.Value = DateTime.Now;

            // 3. ตกแต่งตาราง [cite: 2026-03-08]
            ThemeConfig.FormatMinimalistDataGridView(dgvReport);
            ThemeConfig.FormatMinimalistDataGridView(dgvMaster);
            ThemeConfig.FormatMinimalistDataGridView(dgvRevenue);
            ThemeConfig.FormatMinimalistDataGridView(dgvExpense);
            ThemeConfig.ApplyGlobalFont(this);
        }

        private void LoadManagerReport()
        {
            using (var db = DbConfig.GetDbContext())
            {
                DateTime start = dtpStart.Value.Date;
                DateTime end = dtpEnd.Value.Date.AddDays(1).AddTicks(-1);
                string currentTab = tabControl1.SelectedTab.Text;

                // 💰 1. แท็บรายรับของผู้จัดการ
                if (currentTab == "รายรับ")
                {
                    var data = db.Payments
                        .Where(p => p.Pay_date >= start && p.Pay_date <= end)
                        .OrderByDescending(p => p.Pay_date)
                        .Select(p => new {
                            เลขที่อ้างอิง = p.Pay_id,
                            วันที่ = p.Pay_date,
                            เวลา = p.Pay_time,
                            ช่องทาง = p.Pay_channel,
                            จำนวนเงิน = p.Pay_amont, // ชื่อตาม DB (ไม่มี u)
                                                     // ✅ ตรวจสอบสถานะโดยอ้างอิงฟิลด์ Income_payment จาก Model ของคุณ
                            สถานะ = db.Incomes.Any(i => i.Income_payment == p.Pay_id) ? "✅ ลงบัญชีแล้ว" : "⏳ รอดำเนินการ"
                        }).ToList();

                    dgvRevenue.DataSource = data;

                    // ตกแต่งตารางเบื้องต้น
                    if (dgvRevenue.Columns.Contains("จำนวนเงิน"))
                        dgvRevenue.Columns["จำนวนเงิน"].DefaultCellStyle.Format = "N2";
                }

                // 💸 2. แท็บรายจ่าย (ดึงจากตาราง Fee ตามที่คุณต้องการก่อนหน้านี้)
                else if (currentTab == "รายจ่าย")
                {
                    var data = db.Fees
                        .Where(f => f.fee_date >= start && f.fee_date <= end)
                        .OrderByDescending(f => f.fee_date)
                        .Select(f => new {
                            รหัสรายการ = f.fee_id,
                            วันที่ = f.fee_date,
                            อ้างอิงรายจ่าย = f.Exp_id,
                            จำนวนเงิน = f.fee_total
                        }).ToList();

                    dgvExpense.DataSource = data;
                }

                // 👥 3. แท็บอื่นๆ (พนักงาน/ร้านค้า)
                else if (currentTab.Contains("พนักงาน"))
                {
                    dgvMaster.DataSource = db.Employees.Select(e => new {
                        รหัส = e.EmployId,
                        ชื่อ = e.EmployName,
                        ตำแหน่ง = e.EmployPosition
                    }).ToList();
                }

                CalculateGrandTotal();
            }
        }

        private void CalculateGrandTotal()
        {
            DataGridView dgv = GetActiveDGV();   // ⭐ ใช้ตารางที่เปิดอยู่

            string targetCol = "จำนวนเงิน";

            if (!dgv.Columns.Contains(targetCol))
            {
                lblVatAmount.Visible = false;
                lblBeforeVat.Visible = false;
                lblGrandTotal.Visible = false;
                return;
            }

            lblVatAmount.Visible = true;
            lblBeforeVat.Visible = true;
            lblGrandTotal.Visible = true;

            decimal netTotal = dgv.Rows.Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow && r.Cells[targetCol].Value != null)
                .Sum(r => Convert.ToDecimal(r.Cells[targetCol].Value));

            decimal vat = netTotal * 7 / 107;
            decimal beforeVat = netTotal - vat;

            lblVatAmount.Text = $"ภาษีมูลค่าเพิ่ม 7%: {vat:N2} บาท";
            lblBeforeVat.Text = $"ราคาไม่รวมภาษีมูลค่าเพิ่ม: {beforeVat:N2} บาท";
            lblGrandTotal.Text = $"จำนวนเงินรวมทั้งสิ้น: {netTotal:N2} บาท";
        }

        private DataGridView GetActiveDGV()
        {
            string tab = tabControl1.SelectedTab.Text;

            if (tab == "รายรับ")
                return dgvRevenue;

            if (tab == "รายจ่าย")
                return dgvExpense;

            if (tab.Contains("พนักงาน"))
                return dgvMaster;

            return dgvReport;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataGridView currentDGV = GetActiveDGV();

            if (currentDGV.Rows.Count == 0)
            {
                MessageBox.Show("ไม่มีข้อมูลสำหรับการส่งออก", "แจ้งเตือน");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV files (*.csv)|*.csv";
            sfd.FileName = $"Report_{DateTime.Now:yyyyMMdd_HHmm}.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<string> lines = new List<string>();

                    // 1. ดึงหัวคอลัมน์จากตารางที่เลือกอยู่ [cite: 2026-03-08]
                    string header = string.Join(",", currentDGV.Columns.Cast<DataGridViewColumn>()
                                                  .Select(c => $"\"{c.HeaderText}\""));
                    lines.Add(header);

                    // 2. ดึงข้อมูลแต่ละแถว [cite: 2026-03-08]
                    foreach (DataGridViewRow row in currentDGV.Rows)
                    {
                        if (row.IsNewRow) continue;

                        var cells = row.Cells.Cast<DataGridViewCell>().Select(c =>
                        {
                            string val = c.Value?.ToString() ?? "";

                            // ✅ ป้องกัน Excel ตีความผิดเป็นสูตร (กรณีรายการเมนูขึ้นต้นด้วย -) [cite: 2026-03-08]
                            if (val.StartsWith("-")) val = "'" + val;

                            // ✅ จัดการเครื่องหมาย " และการขึ้นบรรทัดใหม่ [cite: 2026-03-08]
                            val = val.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"");
                            return $"\"{val}\"";
                        });

                        lines.Add(string.Join(",", cells));
                    }

                    // 3. บันทึกไฟล์พร้อม BOM สำหรับภาษาไทย [cite: 2026-03-08]
                    byte[] bom = { 0xEF, 0xBB, 0xBF };
                    using (var fs = new System.IO.FileStream(sfd.FileName, System.IO.FileMode.Create))
                    {
                        fs.Write(bom, 0, bom.Length);
                        byte[] content = System.Text.Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, lines));
                        fs.Write(content, 0, content.Length);
                    }

                    MessageBox.Show("ส่งออกข้อมูลสำเร็จ!", "สำเร็จ");
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadManagerReport();    // ✅ เรียกใช้งานเพื่อให้หายสีเทา [cite: 2026-03-08]
            CalculateGrandTotal(); // ✅ เรียกใช้งานเพื่อให้คำนวณยอดรวมโชว์ [cite: 2026-03-08]

            using (var db = DbConfig.GetDbContext())
            {
                DateTime start = dtpStart.Value.Date;
                DateTime end = dtpEnd.Value.Date.AddDays(1).AddTicks(-1);

                if (cboReportType.Text == "รายงานใบสั่งซื้อ (PO)")
                {
                    var pos = db.PurchaseOrders
                        .Where(p => p.Po_date >= start && p.Po_date <= end)
                        .Select(p => new
                        {
                            เลขที่PO = p.Po_id,      // ✅ พิมพ์ติดกัน ห้ามเว้นวรรค
                            วันที่ = p.Po_date,
                            สถานะ = p.Po_status,
                            ผู้อนุมัติ = p.Po_approver,
                            จำนวนเงิน = db.PurchaseOrderDetails.Where(d => d.Po_id == p.Po_id).Sum(d => d.Order_Qty * d.Unit_Price)
                        }).ToList();
                    dgvReport.DataSource = pos;
                    CalculateGrandTotal();
                }
                else if (cboReportType.Text == "รายงานค่าใช้จ่าย (Expense)")
                {
                    var exps = db.Expenses
                        .Where(ex => ex.Exp_date >= start && ex.Exp_date <= end)
                        .Select(ex => new
                        {
                            เลขที่อ้างอิง = ex.Exp_id,
                            วันที่ = ex.Exp_date,
                            จำนวนเงิน = ex.Exp_amount
                        }).ToList();
                    dgvReport.DataSource = exps;
                }
                else if (cboReportType.Text == "รายงานใบขอซื้อ (PR)")
                {
                    var prs = db.PurchaseRequisitions
                        .Where(p => p.PR_Date >= start && p.PR_Date <= end)
                        .Select(p => new
                        {
                            เลขที่PR = p.PR_ID,
                            // ✅ แก้ Error CS1061: ลบ .HasValue ออก
                            วันที่ = p.PR_Date.ToString("dd/MM/yyyy HH:mm"),
                            รายการเมนู = string.Join(Environment.NewLine, db.PurchaseRequisitionDetails
                                                        .Where(d => d.PR_ID == p.PR_ID)
                                                        .Select(d => "- " + d.Product.Pro_name)),
                            สถานะ = p.PR_Status,
                            // ✅ ต้องมั่นใจว่าชื่อคอลัมน์คือ "จำนวนเงิน" (ไม่มีเว้นวรรค) [cite: 2026-03-08]
                            จำนวนเงิน = db.PurchaseRequisitionDetails
                                         .Where(d => d.PR_ID == p.PR_ID)
                                         .Sum(d => (decimal?)(d.Request_Qty * d.Product.Pro_price)) ?? 0
                        }).ToList();

                    dgvReport.DataSource = prs;
                    CalculateGrandTotal(); // ✅ ต้องเรียกบรรทัดนี้เพื่อให้ค่าบนหน้าจออัปเดต [cite: 2026-03-08]
                }

                // ✅ เพิ่มเงื่อนไขสำหรับรายงานข้อมูลพนักงาน [cite: 2026-03-08]
                else if (cboReportType.Text == "รายงานข้อมูลพนักงาน")
                {
                    var emps = db.Employees.Select(emp => new
                    {
                        รหัสพนักงาน = emp.EmployId,       // ✅ แก้จาก Emp_id เป็น EmployId
                        ชื่อนามสกุล = emp.EmployName,     // ✅ แก้จาก Emp_name เป็น EmployName
                        ตำแหน่ง = emp.EmployPosition,     // ✅ แก้จาก Position เป็น EmployPosition
                        วันที่เริ่มงาน = emp.EmploySdate,   // ✅ ใช้ฟิลด์ที่มีใน Model จริง
                        ระดับผู้ใช้งาน = emp.Role           // ✅ ใช้ Role แทนสถานะ
                    }).ToList();
                    dgvMaster.DataSource = emps;
                }
                else if (cboReportType.Text == "รายงานข้อมูลร้านค้า")
                {
                    var shops = db.Shops.Select(s => new
                    {
                        รหัสร้าน = s.ShopId,             // ✅ แก้ให้ตรงตาม Model
                        ชื่อร้าน = s.ShopName,           // ✅ แก้จาก Shop_name เป็น ShopName
                        ที่อยู่ = s.ShopAddress,         // ✅ แก้จาก Shop_address เป็น ShopAddress
                        เบอร์โทร = s.ShopPhone           // ✅ แก้จาก Shop_phone เป็น ShopPhone
                    }).ToList();
                    dgvMaster.DataSource = shops;
                    CalculateGrandTotal();
                }
                //CalculateGrandTotal();
                else if (cboReportType.Text == "รายเมนูอาหารในระบบ")
                {
                    var menus = db.Products.Select(p => new
                    {
                        รหัสเมนู = p.Pro_id,
                        ชื่อเมนู = p.Pro_name,
                        ราคา = p.Pro_price,
                        หมวดหมู่ = p.Pro_category,
                        หน่วย = p.Pro_unit
                    }).ToList();

                    dgvMaster.DataSource = menus;
                }

                else if (cboReportType.Text == "รายงานโปรโมชั่น")
                {
                    var promos = db.Promotion.Select(pr => new
                    {
                        รหัสโปรโมชั่น = pr.Promo_ID,
                        ชื่อโปรโมชั่น = pr.Promo_Name,
                        ส่วนลดเปอร์เซ็นต์ = pr.Promo_Discount
                    }).ToList();

                    dgvMaster.DataSource = promos;
                }

                else if (cboReportType.Text == "รายงานแพ็คเกจ")
                {
                    var packages = db.Packages.Select(pk => new
                    {
                        รหัสแพ็คเกจ = pk.Package_ID,
                        ชื่อแพ็คเกจ = pk.Package_Name,
                        ราคา = pk.Package_Price,
                    }).ToList();

                    dgvMaster.DataSource = packages;
                }

            }
        }

        private void printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            DataGridView currentDGV = GetActiveDGV();

            // 1. กำหนดฟอนต์และเครื่องมือวาด
            Font fontInfo = new Font("Tahoma", 9);
            Font fontTitle = new Font("Tahoma", 18, FontStyle.Bold);
            Font fontHeader = new Font("Tahoma", 10, FontStyle.Bold);
            Font fontBody = new Font("Tahoma", 10);
            Font fontBold = new Font("Tahoma", 10, FontStyle.Bold);
            Pen tablePen = new Pen(Color.Gray, 0.5f);

            float xStart = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float rowHeight = 30;

            // --- ส่วนหัวรายงานและวันที่ (วาดทุกหน้า) ---
            string printDate = "วันที่พิมพ์: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            e.Graphics.DrawString(printDate, fontInfo, Brushes.Gray, e.PageBounds.Width - 250, y);
            y += 40;
            string reportTitle = "รายงานสรุป " + cboReportType.Text;
            e.Graphics.DrawString(reportTitle, fontTitle, Brushes.Black, xStart, y);
            y += 60;

            // --- เตรียมความกว้างคอลัมน์ ---
            int colCount = currentDGV.Columns.Count; // ✅ เปลี่ยน dgvReport เป็น currentDGV
            float[] colWidths = new float[colCount];
            for (int i = 0; i < colCount; i++)
            {
                string h = currentDGV.Columns[i].HeaderText; // ✅ เปลี่ยน dgvReport เป็น currentDGV
                if (h.Contains("เมนู") || h.Contains("ชื่อ") || h.Contains("ที่อยู่")) colWidths[i] = 230f;
                else if (h.Contains("เงิน")) colWidths[i] = 110f;
                else colWidths[i] = 100f;
            }

            // วาด Header ตาราง
            float x = xStart;
            e.Graphics.DrawLine(tablePen, xStart, y, xStart + colWidths.Sum(), y);
            for (int i = 0; i < colCount; i++)
            {
                // ✅ เปลี่ยน dgvReport เป็น currentDGV
                e.Graphics.DrawString(currentDGV.Columns[i].HeaderText, fontHeader, Brushes.DarkBlue, x + 5, y + 5);
                x += colWidths[i];
            }
            y += rowHeight;
            e.Graphics.DrawLine(tablePen, xStart, y, xStart + colWidths.Sum(), y);

            // --- 5. วาดข้อมูล (รองรับหลายหน้า) ---
            while (itemIndex < currentDGV.Rows.Count) // ✅ เปลี่ยน dgvReport เป็น currentDGV
            {
                DataGridViewRow row = currentDGV.Rows[itemIndex]; // ✅ เปลี่ยน dgvReport เป็น currentDGV
                if (row.IsNewRow) { itemIndex++; continue; }

                // ✅ เปลี่ยน dgvReport เป็น currentDGV
                string menuList = currentDGV.Columns.Contains("รายการเมนู") ? row.Cells["รายการเมนู"].Value?.ToString() ?? "" : "";
                int lineCount = string.IsNullOrEmpty(menuList) ? 1 : menuList.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Length;
                float dynamicRowHeight = Math.Max(30, lineCount * 22);

                // ตรวจสอบขอบล่างกระดาษ
                if (y + dynamicRowHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                // เช็คหน้าเต็ม
                if (y + 40 > e.MarginBounds.Bottom) { e.HasMorePages = true; return; }

                x = xStart;
                for (int i = 0; i < colCount; i++)
                {
                    string val = row.Cells[i].Value?.ToString() ?? "";
                    RectangleF rect = new RectangleF(x + 5, y + 5, colWidths[i] - 10, dynamicRowHeight - 10);

                    // ✅ เปลี่ยน dgvReport เป็น currentDGV
                    if (currentDGV.Columns[i].HeaderText.Contains("เงิน"))
                    {
                        if (decimal.TryParse(val, out decimal n)) val = n.ToString("N2");
                        e.Graphics.DrawString(val, fontBody, Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Far });
                    }
                    else
                    {
                        e.Graphics.DrawString(val, fontBody, Brushes.Black, rect);
                    }
                    x += colWidths[i];
                }
                y += dynamicRowHeight;
                e.Graphics.DrawLine(tablePen, xStart, y, xStart + colWidths.Sum(), y);
                itemIndex++;
            }

            // --- 6. ส่วนท้ายรายงาน (Summary Section) ---

            if (currentDGV.Columns.Contains("จำนวนเงิน"))
            {
                if (y + 180 > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                string cleanAmount = lblGrandTotal.Text.Replace("จำนวนเงินรวมทั้งสิ้น: ", "").Replace(" บาท", "").Replace(",", "");
                decimal total = decimal.TryParse(cleanAmount, out decimal res) ? res : 0;
                decimal vat = total * 7 / 107;
                decimal beforeVat = total - vat;

                y += 25;
                float summaryX = e.PageBounds.Width - 350;
                float valueX = e.PageBounds.Width - 150;

                e.Graphics.DrawString("รวมเป็นเงิน:", fontBody, Brushes.Black, summaryX, y);
                e.Graphics.DrawString($"{total:N2} บาท", fontBody, Brushes.Black, valueX, y);
                y += 22;
                e.Graphics.DrawString("ภาษีมูลค่าเพิ่ม 7%:", fontBody, Brushes.Black, summaryX, y);
                e.Graphics.DrawString($"{vat:N2} บาท", fontBody, Brushes.Black, valueX, y);
                y += 22;
                e.Graphics.DrawString("ราคาไม่รวมภาษีมูลค่าเพิ่ม:", fontBody, Brushes.Black, summaryX, y);
                e.Graphics.DrawString($"{beforeVat:N2} บาท", fontBody, Brushes.Black, valueX, y);
                y += 30;
                e.Graphics.DrawString("จำนวนเงินรวมทั้งสิ้น:", fontBold, Brushes.Red, summaryX, y);
                e.Graphics.DrawString($"{total:N2} บาท", fontBold, Brushes.Red, valueX, y);
            }
            else
            {
                // สำหรับรายงานพนักงาน/ร้านค้า
                y += 20;
                e.Graphics.DrawString($"--- จบรายงาน: จำนวนทั้งหมด {currentDGV.Rows.Count - (currentDGV.AllowUserToAddRows ? 1 : 0)} รายการ ---", fontInfo, Brushes.Gray, xStart, y); // ✅ เปลี่ยน dgvReport เป็น currentDGV
            }

            e.HasMorePages = false;
            itemIndex = 0;
        }

        private void btnPrintPDF_Click(object sender, EventArgs e)
        {
            DataGridView currentDGV = GetActiveDGV();

            if (currentDGV.Rows.Count == 0 ||
               (currentDGV.Rows.Count == 1 && currentDGV.Rows[0].IsNewRow))
            {
                MessageBox.Show("ไม่มีข้อมูลสำหรับการพิมพ์", "แจ้งเตือน");
                return;
            }

            itemIndex = 0;

            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = printDoc;
            ppd.WindowState = FormWindowState.Maximized;
            ppd.ShowDialog();
        }

        private void printDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            itemIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        // ฟังก์ชันช่วยซ่อน/แสดง ส่วนสรุปยอดเงิน
        private void SetFinanceUI(bool isFinance)
        {
            lblVatAmount.Visible = lblBeforeVat.Visible = lblGrandTotal.Visible = isFinance;
            dtpStart.Visible = dtpEnd.Visible = isFinance;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboReportType.Items.Clear();

            // ตรวจสอบตามลำดับแท็บในรูปภาพของคุณ
            // สมมติ: Index 4 คือ รายรับ, Index 5 คือ รายจ่าย (นับจากซ้ายไปขวา)

            if (tabControl1.SelectedTab.Text == "รายรับ")
            {
                cboReportType.Items.Add("รายงานสรุปรายรับ (Payment)");
                cboReportType.SelectedIndex = 0;
                SetFinanceUI(true); // โชว์วันที่และยอดสรุป
            }
            else if (tabControl1.SelectedTab.Text == "รายจ่าย")
            {
                cboReportType.Items.Add("รายงานค่าใช้จ่าย (Expense)");
                cboReportType.SelectedIndex = 0;
                SetFinanceUI(true);
            }
            else if (tabControl1.SelectedTab.Text.Contains("พนักงาน"))
            {
                cboReportType.Items.Add("รายงานข้อมูลพนักงาน");
                cboReportType.Items.Add("รายงานข้อมูลร้านค้า");
                cboReportType.Items.Add("รายเมนูอาหารในระบบ");
                cboReportType.Items.Add("รายงานโปรโมชั่น");
                cboReportType.Items.Add("รายงานแพ็คเกจ");
                cboReportType.SelectedIndex = 0;
                SetFinanceUI(false); // ซ่อนยอดสรุปเงิน
            }
        }
    }
}
