using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using BuffetAPI.Data;
using BuffetAPI.Models;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using ClosedXML.Excel;

namespace ShabuPOS
{
    public partial class FormAccountFee : Form
    {
        PrintDocument printDocument = new PrintDocument();
        PrintPreviewDialog previewDialog = new PrintPreviewDialog();

        public FormAccountFee()
        {
            InitializeComponent();
        }

        string selectedExpId = "";

        private void FormAccountFee_Load(object sender, EventArgs e)
        {
            ThemeConfig.FormatMinimalistDataGridView(dgvExpense);
            ThemeConfig.FormatMinimalistDataGridView(dgvPayments);
            ThemeConfig.FormatMinimalistDataGridView(dgvFees);
            ThemeConfig.FormatMinimalistDataGridView(dgvExpensePicker);

            ThemeConfig.ApplyGlobalFont(this);

            dgvExpense.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExpense.ReadOnly = true;
            dgvFees.ReadOnly = true;

            RefreshAll();
            LoadExpensePicker();
            SetupStatusFilter(); // ✅ ต้องเรียกบรรทัดนี้ ข้อมูลถึงจะขึ้นใน ComboBox [cite: 2026-03-09]
        }

        private void RefreshAll()
        {
            LoadExpenses();
            LoadFees();
            LoadExpenseCombo();
            ClearExpense();
            ClearFee();
            LoadExpensePicker(); // ✅ ต้องมีบรรทัดนี้ เพื่อโหลดตารางเลือกฝั่งซ้าย
            LoadPayments();      // ✅ แท็บ 3: รายรับ (เพิ่มอันนี้เข้าไป)

            // ✅ เพิ่มการตั้งค่าวันที่เริ่มต้นให้เป็นต้นเดือน เพื่อให้เห็นข้อมูลทันที
            dtpPayStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpPayEnd.Value = DateTime.Now;
        }

        // =============================================
        // 💰 ส่วนที่ 1: รายจ่ายหลัก (EXPENSE)
        // =============================================

        void LoadExpenses()
        {
            using (var db = DbConfig.GetDbContext())
            {
                dgvExpense.DataSource = db.Expenses
                    .OrderByDescending(x => x.Exp_date)
                    .Select(x => new
                    {
                        รหัสรายจ่าย = x.Exp_id,
                        วันที่ = x.Exp_date.HasValue ? x.Exp_date.Value.ToString("dd/MM/yyyy") : "",
                        เวลา = x.Exp_time.HasValue ? x.Exp_time.Value.ToString(@"hh\:mm") : "",
                        จำนวนเงิน_บาท = x.Exp_amount
                    }).ToList();
            }
            dgvExpense.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // ✅ ฟังก์ชันโหลดรายการที่ "ยังไม่ได้จ่าย" หรือ "ยอดเป็น 0" มาให้เลือก
        // ✅ โหลดรายการ Expense พร้อมสถานะและรองรับการกรอง
        // ✅ ฟังก์ชันโหลดตารางเลือก พร้อมรองรับ Search และ Status Filter
        // ✅ ฟังก์ชันโหลดข้อมูลพร้อมตัวกรอง (Search, Status, Date Range)
        // ✅ ฟังก์ชันโหลดข้อมูลพร้อมตัวกรอง (Search, Status, Date Range)
        private void LoadExpensePicker()
        {
            string keyword = txtSearchExp.Text.Trim();
            int statusIndex = cboStatusFilter.SelectedIndex; // ✅ ใช้ Index แทนข้อความ
            DateTime startDate = dtpStart.Value.Date;
            DateTime endDate = dtpEnd.Value.Date;

            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    var query = db.Expenses.Select(x => new
                    {
                        รหัสอ้างอิง = x.Exp_id,
                        วันที่ = x.Exp_date,
                        ยอดเงินรวม = x.Exp_amount,
                        // ตรวจสอบสถานะว่าบัญชีบันทึกหรือยัง
                        สถานะ = db.Fees.Any(f => f.Exp_id == x.Exp_id) ? "✅ ตรวจสอบแล้ว" : "⏳ การเงินบันทึก"
                    });

                    // 1. กรองวันที่
                    query = query.Where(x => x.วันที่ >= startDate && x.วันที่ <= endDate);

                    // 2. กรองคำค้นหา
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query = query.Where(x => x.รหัสอ้างอิง.Contains(keyword));
                    }

                    // 3. กรองสถานะตาม Index
                    if (statusIndex == 1) // "⏳ การเงินบันทึก"
                    {
                        query = query.Where(x => !db.Fees.Any(f => f.Exp_id == x.รหัสอ้างอิง));
                    }
                    else if (statusIndex == 2) // "✅ ตรวจสอบแล้ว"
                    {
                        query = query.Where(x => db.Fees.Any(f => f.Exp_id == x.รหัสอ้างอิง));
                    }

                    dgvExpensePicker.DataSource = query.OrderByDescending(x => x.วันที่).ToList();
                    //ApplyRowStyles();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        // ✅ ฟังก์ชันโหลดข้อมูลรายรับ (Payment)
        private void LoadPayments()
        {
            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    DateTime start = dtpPayStart.Value.Date;
                    DateTime end = dtpPayEnd.Value.Date;
                    string keyword = txtSearchPay.Text.Trim();

                    // ดึงข้อมูลจากตาราง Payment
                    var query = db.Payments.Where(p => p.Pay_date >= start && p.Pay_date <= end);

                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query = query.Where(p => p.Pay_id.Contains(keyword) || p.Pay_channel.Contains(keyword));
                    }

                    var result = query.OrderByDescending(p => p.Pay_date)
                                      .Select(p => new {
                                          รหัสการชำระ = p.Pay_id,
                                          วันที่ = p.Pay_date,
                                          จำนวนเงิน = p.Pay_amont, // ✅ ใช้ชื่อตาม DB ของคุณ
                                          ช่องทาง = p.Pay_channel,
                                          สถานะ = db.Incomes.Any(i => i.Income_payment == p.Pay_id) ? "✅ ลงบัญชีแล้ว" : "⏳ รอดำเนินการ"
                                      }).ToList();

                    dgvPayments.DataSource = result;

                    // 💰 คำนวณยอดรวมรายรับ
                    decimal total = result.Sum(x => (decimal?)x.จำนวนเงิน) ?? 0;
                    lblTotalRevenue.Text = $"ยอดรวมรายรับ: {total:N2} บาท";
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void SetupStatusFilter()
        {
            // ล้างข้อมูลเก่าก่อนเพิ่มใหม่ [cite: 2026-03-09]
            cboStatusFilter.Items.Clear();

            // เพิ่มตัวเลือกสถานะตามที่คุณต้องการ
            cboStatusFilter.Items.Add("ทั้งหมด");
            cboStatusFilter.Items.Add("⏳ การเงินบันทึก");
            cboStatusFilter.Items.Add("✅ ตรวจสอบแล้ว");

            // ตั้งค่าเริ่มต้นให้เลือก "ทั้งหมด" [cite: 2026-03-09]
            cboStatusFilter.SelectedIndex = 0;
        }

        private void cboStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadExpensePicker(); // โหลดข้อมูลใหม่ตามสถานะที่เลือก
        }

        private void txtSearchExp_TextChanged(object sender, EventArgs e)
        {
            LoadExpensePicker(); // โหลดข้อมูลใหม่ตามคำค้นหา
        }

        // ✅ เมื่อคลิกเลือกในตาราง ยอดเงินจะขึ้นอัตโนมัติ [cite: 2026-03-09]
        private void dgvExpensePicker_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // ดึงค่าจากคอลัมน์ "รหัสอ้างอิง" (สมมติว่าเป็นคอลัมน์ที่ 0) [cite: 2026-03-09]
                selectedExpId = dgvExpensePicker.Rows[e.RowIndex].Cells[0].Value.ToString();

                // แสดงรหัสที่เลือกใน Label หรือช่องอ้างอิงเพื่อให้ผู้ใช้เห็น
                lblSelectedExp.Text = selectedExpId;

                using (var db = DbConfig.GetDbContext())
                {
                    var exp = db.Expenses.Find(selectedExpId);
                    if (exp != null)
                    {
                        // ✅ ยอดเงินขึ้นอัตโนมัติในช่องกรอก (ดึงจากยอดใน Expense) [cite: 2026-03-09]
                        txtFeeTotal.Text = exp.Exp_amount?.ToString("0.00");
                    }
                }
            }
        }

        void GenerateExpenseId()
        {
            using (var db = DbConfig.GetDbContext())
            {
                var last = db.Expenses.OrderByDescending(x => x.Exp_id).Select(x => x.Exp_id).FirstOrDefault();
                int num = 1;
                if (!string.IsNullOrEmpty(last) && last.StartsWith("EXP-"))
                {
                    if (int.TryParse(last.Replace("EXP-", ""), out int n)) num = n + 1;
                }
                txtExpId.Text = $"EXP-{num:D5}";
            }
        }

        void ClearExpense()
        {
            txtExpAmount.Clear();
            dtpExpDate.Value = DateTime.Now;
            GenerateExpenseId();
            btnSaveExp.Enabled = true;
            dgvExpense.ClearSelection();
        }

        private void btnSaveExp_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    string id = txtExpId.Text.Trim();
                    // ค้นหาว่ารหัสนี้มีในระบบหรือยัง
                    var existingExp = db.Expenses.Find(id);

                    if (existingExp == null)
                    {
                        // ✅ กรณี "เพิ่มข้อมูลใหม่"
                        db.Expenses.Add(new Expense
                        {
                            Exp_id = id,
                            Exp_date = dtpExpDate.Value.Date,
                            Exp_time = DateTime.Now.TimeOfDay,
                            Exp_amount = 0 // เริ่มต้นที่ 0 รอการบวกจากรายจ่ายย่อย
                        });
                        MessageBox.Show("เพิ่มรายจ่ายหลักใหม่สำเร็จ");
                    }
                    else
                    {
                        // ✅ กรณี "แก้ไขข้อมูลเดิม" (อัปเดตสถานะ/วันที่)
                        existingExp.Exp_date = dtpExpDate.Value.Date;
                        // คุณสามารถอัปเดตสถานะอื่นๆ ของ Expense ได้ที่นี่ [cite: 2026-03-09]
                        MessageBox.Show("อัปเดตข้อมูลรายจ่ายเดิมเรียบร้อย");
                    }
                    db.SaveChanges();

                    if (db.SaveChanges() > 0)
                    {
                        MessageBox.Show("บันทึกข้อมูลเรียบร้อย");

                        // ✅ อัปเดตยอดรวมในตารางหลัก
                        UpdateExpenseTotal(selectedExpId);

                        // ✅ สั่งโหลดตารางเลือกใหม่เพื่อให้สถานะเปลี่ยนจาก "รอดำเนินการ" เป็น "บันทึกแล้ว"
                        LoadExpensePicker();

                        // ล้างค่าเพื่อให้พร้อมบันทึกรายการถัดไป
                        selectedExpId = "";
                        txtFeeTotal.Clear();
                    }

                    RefreshAll();
                }
            }
            catch (Exception ex) { MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message); }
        }

        // =============================================
        // 🧾 ส่วนที่ 2: รายจ่ายย่อย (FEE)
        // =============================================

        void LoadFees()
        {
            using (var db = DbConfig.GetDbContext())
            {
                dgvFees.DataSource = db.Fees
                    .OrderByDescending(x => x.fee_date)
                    .Select(x => new
                    {
                        รหัสรายการ = x.fee_id,
                        วันที่ = x.fee_date.HasValue ? x.fee_date.Value.ToString("dd/MM/yyyy") : "",
                        เวลา = x.fee_time.HasValue ? x.fee_time.Value.ToString(@"hh\:mm") : "",
                        อ้างอิงรายจ่าย = x.Exp_id,
                        จำนวนเงิน = x.fee_total
                    }).ToList();
            }
            dgvFees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        void LoadExpenseCombo()
        {
            using (var db = DbConfig.GetDbContext())
            {
                var list = db.Expenses.Select(x => new
                {
                    x.Exp_id,
                    DisplayText = x.Exp_id + " (ยอดรวม: " + (x.Exp_amount ?? 0) + ")"
                }).ToList();
                cboExpenseRef.DataSource = list;
                cboExpenseRef.DisplayMember = "DisplayText";
                cboExpenseRef.ValueMember = "Exp_id";
                cboExpenseRef.SelectedIndex = -1;
            }
        }

        void GenerateFeeId()
        {
            using (var db = DbConfig.GetDbContext())
            {
                var last = db.Fees.OrderByDescending(x => x.fee_id).Select(x => x.fee_id).FirstOrDefault();
                int num = 1;
                if (!string.IsNullOrEmpty(last) && last.Length > 3)
                {
                    if (int.TryParse(last.Substring(3), out int n)) num = n + 1;
                }
                txtFeeId.Text = "FEE" + num.ToString("D4");
            }
        }

        void ClearFee()
        {
            txtFeeTotal.Clear();
            cboExpenseRef.SelectedIndex = -1;
            dtpFeeDate.Value = DateTime.Now;
            GenerateFeeId();
            //btnSaveFee.Enabled = true;
            dgvFees.ClearSelection();
        }

        private void btnSaveFee_Click(object sender, EventArgs e)
        {
            if (cboExpenseRef.SelectedValue == null) { MessageBox.Show("กรุณาเลือกรายจ่ายหลักที่ต้องการอ้างอิง"); return; }
            if (!decimal.TryParse(txtFeeTotal.Text, out decimal total)) { MessageBox.Show("ยอดเงินไม่ถูกต้อง"); return; }

            try
            {
                string expId = cboExpenseRef.SelectedValue.ToString();
                using (var db = DbConfig.GetDbContext())
                {
                    db.Fees.Add(new Fee
                    {
                        fee_id = txtFeeId.Text.Trim(),
                        Exp_id = expId,
                        fee_date = dtpFeeDate.Value.Date,
                        fee_time = DateTime.Now.TimeOfDay,
                        fee_total = (int)total // แปลงเป็น int ตาม Model ของคุณ
                    });
                    db.SaveChanges();
                    UpdateExpenseTotal(expId); // ✅ อัปเดตยอดรวมในตารางหลักทันที
                }
                MessageBox.Show("บันทึกรายจ่ายย่อยสำเร็จ");
                RefreshAll();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        // =========================================================
        // 🚀 ปุ่มบันทึกอัจฉริยะ (จัดการทั้ง EXP และ FEE ในปุ่มเดียว)
        // =========================================================
        private void btnSave_Click(object sender, EventArgs e)
        {
            // ✅ ประกาศ db เพียงครั้งเดียวที่นี่ ครอบคลุมทั้งฟังก์ชัน
            using (var db = DbConfig.GetDbContext())
            {
                // -----------------------------------------
                // แท็บที่ 1: รายจ่ายหลัก
                // -----------------------------------------
                if (tabControl1.SelectedIndex == 0)
                {
                    string expId = txtExpId.Text.Trim();
                    if (!decimal.TryParse(txtExpAmount.Text, out decimal amount))
                    {
                        MessageBox.Show("กรุณากรอกจำนวนเงินให้ถูกต้อง"); return;
                    }

                    try
                    {
                        // ตรวจสอบรหัสซ้ำ
                        if (db.Expenses.Any(x => x.Exp_id == expId))
                        {
                            MessageBox.Show("รหัสรายจ่ายนี้มีอยู่แล้ว"); return;
                        }

                        db.Expenses.Add(new Expense
                        {
                            Exp_id = expId,
                            Exp_date = dtpExpDate.Value.Date,
                            Exp_time = DateTime.Now.TimeOfDay,
                            Exp_amount = amount
                        });

                        // ✅ ไม่ต้องประกาศ var db ใหม่ ใช้ db จากด้านบนได้เลย
                        string feeId = GenerateFeeIdInternal();
                        db.Fees.Add(new Fee
                        {
                            fee_id = feeId,
                            Exp_id = expId,
                            fee_date = dtpExpDate.Value.Date,
                            fee_time = DateTime.Now.TimeOfDay,
                            fee_total = amount
                        });

                        db.SaveChanges();
                        MessageBox.Show("บันทึกรายจ่ายหลักสำเร็จ!");
                        RefreshAll();
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                }
                // -----------------------------------------
                // แท็บที่ 2: รายจ่ายย่อย (ส่วนที่คุณกำลังแก้)
                // -----------------------------------------
                else if (tabControl1.SelectedIndex == 1)
                {
                    if (string.IsNullOrEmpty(selectedExpId))
                    {
                        MessageBox.Show("กรุณาคลิกเลือกรายการรายจ่ายอ้างอิงจากตารางด้านซ้ายก่อน"); return;
                    }

                    if (!decimal.TryParse(txtFeeTotal.Text, out decimal total))
                    {
                        MessageBox.Show("ระบุยอดเงินให้ถูกต้อง"); return;
                    }

                    try
                    {
                        // ✅ ตรงนี้ห้ามใส่ using (var db = ...) ซ้ำ เพราะมีอยู่ด้านบนแล้ว
                        db.Fees.Add(new Fee
                        {
                            fee_id = txtFeeId.Text.Trim(),
                            Exp_id = selectedExpId,
                            fee_date = dtpFeeDate.Value.Date,
                            fee_time = DateTime.Now.TimeOfDay,
                            fee_total = total
                        });

                        db.SaveChanges();

                        // อัปเดตยอดรวมในตารางหลัก
                        var exp = db.Expenses.Find(selectedExpId);
                        if (exp != null)
                        {
                            exp.Exp_amount = db.Fees.Where(x => x.Exp_id == selectedExpId).Sum(x => x.fee_total) ?? 0;
                            db.SaveChanges();
                        }

                        MessageBox.Show("เพิ่มรายการรายจ่ายย่อยสำเร็จ!");
                        RefreshAll();
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                }

                // --- 🔵 แท็บที่ 3: รายรับ (SelectedIndex == 2) ---
                else if (tabControl1.SelectedIndex == 2)
                {
                    if (dgvPayments.CurrentRow == null)
                    {
                        MessageBox.Show("กรุณาเลือกรายการรายรับจากตารางก่อนบันทึก"); return;
                    }

                    try
                    {
                        // ดึงข้อมูลจากแถวที่เลือก
                        string payId = dgvPayments.CurrentRow.Cells["รหัสการชำระ"].Value.ToString();
                        decimal amount = Convert.ToDecimal(dgvPayments.CurrentRow.Cells["จำนวนเงิน"].Value);
                        DateTime payDate = Convert.ToDateTime(dgvPayments.CurrentRow.Cells["วันที่"].Value);

                        // ตรวจสอบข้อมูลซ้ำในตาราง Income (อ้างอิง Income_payment)
                        if (db.Incomes.Any(i => i.Income_payment == payId))
                        {
                            MessageBox.Show("รายการนี้ถูกบันทึกลงบัญชีเรียบร้อยแล้ว"); return;
                        }

                        // สร้าง Object ใหม่ตาม Model Income ของคุณเป๊ะๆ
                        var newIncome = new Income
                        {
                            Income_id = GenerateIncomeId(),
                            Income_payment = payId,              // เก็บ FK
                            Income_date = payDate,
                            Income_time = DateTime.Now.TimeOfDay,
                            Income_amount = (int)amount,         // ⚠️ Model ของคุณเป็น int? จึงต้อง Cast ครับ
                            Income_description = "บันทึกบัญชีรายรับ"
                        };

                        db.Incomes.Add(newIncome);

                        if (db.SaveChanges() > 0)
                        {
                            MessageBox.Show($"บันทึกรหัส {newIncome.Income_id} ลงตาราง Income สำเร็จ!");
                            LoadPayments(); // รีเฟรชหน้าจอ
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Error SaveIncome: " + ex.Message); }
                }
            }
        }

        // ✅ ฟังก์ชันสร้างรหัส Income อัตโนมัติ (อ้างอิงชื่อ Income_id)
        private string GenerateIncomeId()
        {
            using (var db = DbConfig.GetDbContext())
            {
                var last = db.Incomes.OrderByDescending(x => x.Income_id).FirstOrDefault();
                int num = 1;
                if (last != null && last.Income_id.StartsWith("INC-"))
                {
                    if (int.TryParse(last.Income_id.Replace("INC-", ""), out int n)) num = n + 1;
                }
                return $"INC-{num:D5}";
            }
        }

        // ✅ ฟังก์ชันสำหรับสร้างรหัส FEE อัตโนมัติ (Internal Helper)
        private string GenerateFeeIdInternal()
        {
            using (var db = DbConfig.GetDbContext())
            {
                // ค้นหารหัส FEE ล่าสุดจากตาราง Fees [cite: 2026-03-09]
                var lastId = db.Fees
                    .OrderByDescending(f => f.fee_id)
                    .Select(f => f.fee_id)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(lastId))
                {
                    return "FEE0001"; // เริ่มต้นถ้ายังไม่มีข้อมูล [cite: 2026-03-09]
                }
                else
                {
                    // ดึงเฉพาะตัวเลขออกมา เช่น "FEE0001" -> "0001" [cite: 2026-03-09]
                    string numericPart = new string(lastId.Where(char.IsDigit).ToArray());
                    int nextNum = int.TryParse(numericPart, out int n) ? n + 1 : 1;
                    return "FEE" + nextNum.ToString("D4"); // ส่งกลับเป็น FEE0002 เป็นต้น [cite: 2026-03-09]
                }
            }
        }


        // =============================================
        // 🔄 ระบบรวมยอดอัตโนมัติ (Update Sync)
        // =============================================

        void UpdateExpenseTotal(string expId)
        {
            using (var db = DbConfig.GetDbContext())
            {
                // คำนวณผลรวมแบบ decimal [cite: 2026-03-09]
                var total = db.Fees
                    .Where(x => x.Exp_id == expId)
                    .Sum(x => x.fee_total) ?? 0m; // ✅ ใช้ 0m เพื่อระบุว่าเป็น decimal

                var exp = db.Expenses.Find(expId);
                if (exp != null)
                {
                    exp.Exp_amount = total;
                    db.SaveChanges();
                }
            }
        }

        //private void txtSearchExp_TextChanged(object sender, EventArgs e)
        //{
        //    LoadExpensePicker(txtSearchExp.Text.Trim());
        //}

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearchExp.Clear();       // ล้างช่องค้นหา [cite: 2026-03-09]
            cboStatusFilter.SelectedIndex = 0; // กลับไปที่ "ทั้งหมด" [cite: 2026-03-09]

            RefreshAll(); // โหลดข้อมูลใหม่ทั้งหมด [cite: 2026-03-09]

            MessageBox.Show("อัปเดตข้อมูลล่าสุดเรียบร้อยแล้ว");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadExpensePicker();
        }

        private void btnPrintPDF_Click(object sender, EventArgs e)
        {
            printDocument.PrintPage -= PrintDocument_PrintPage;
            printDocument.PrintPage -= PrintRevenue_PrintPage;

            if (tabControl1.SelectedIndex == 1) // แท็บรายจ่ายย่อย
                printDocument.PrintPage += PrintDocument_PrintPage;

            else if (tabControl1.SelectedIndex == 2) // แท็บรายรับ
                printDocument.PrintPage += PrintRevenue_PrintPage;

            previewDialog.Document = printDocument;
            previewDialog.Width = 1000;
            previewDialog.Height = 800;

            previewDialog.ShowDialog();
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int startX = 50;
            int startY = 150;
            int offsetY = 25;

            Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
            Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Font textFont = new Font("Segoe UI", 10);
            Font totalFont = new Font("Segoe UI", 11, FontStyle.Bold);

            string title = "รายงาน สรุปรายจ่าย";

            // จัด Title กลางหน้า
            SizeF titleSize = e.Graphics.MeasureString(title, titleFont);
            float titleX = (e.PageBounds.Width - titleSize.Width) / 2;

            e.Graphics.DrawString(title, titleFont, Brushes.Black, titleX, 40);

            // ✅ วันที่พิมพ์
            string printDate = "วันที่พิมพ์ : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            e.Graphics.DrawString(printDate, textFont, Brushes.Black, startX, 90);

            DataGridView dgv = dgvFees;

            decimal sum = 0;

            int x = startX;
            int y = startY;

            // Header
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                e.Graphics.DrawString(col.HeaderText, headerFont, Brushes.Black, x, y);
                x += 150;
            }

            y += offsetY;

            // Rows
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                x = startX;

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    string value = row.Cells[i].Value?.ToString();

                    if (!string.IsNullOrEmpty(value))
                    {
                        e.Graphics.DrawString(value, textFont, Brushes.Black, x, y);

                        // รวมยอดเงินคอลัมน์สุดท้าย
                        if (i == dgv.Columns.Count - 1)
                        {
                            if (decimal.TryParse(value, out decimal num))
                                sum += num;
                        }
                    }

                    x += 150;
                }

                y += offsetY;
            }

            // เส้นแบ่ง
            y += 10;
            e.Graphics.DrawLine(Pens.Black, startX, y, startX + 700, y);

            y += 20;

            // แสดงยอดรวม
            string totalText = "ยอดรวมทั้งหมด : " + sum.ToString("N2") + " บาท";
            e.Graphics.DrawString(totalText, totalFont, Brushes.Black, 550, y);
        }

        private void PrintRevenue_PrintPage(object sender, PrintPageEventArgs e)
        {
            int startX = 50;
            int startY = 150;
            int offsetY = 25;

            Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
            Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Font textFont = new Font("Segoe UI", 10);
            Font totalFont = new Font("Segoe UI", 11, FontStyle.Bold);

            string title = "รายงาน สรุปรายรับ";

            SizeF titleSize = e.Graphics.MeasureString(title, titleFont);
            float titleX = (e.PageBounds.Width - titleSize.Width) / 2;

            e.Graphics.DrawString(title, titleFont, Brushes.Black, titleX, 40);

            string printDate = "วันที่พิมพ์ : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            e.Graphics.DrawString(printDate, textFont, Brushes.Black, startX, 90);

            DataGridView dgv = dgvPayments;

            decimal sum = 0;

            int x = startX;
            int y = startY;

            // Header
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                e.Graphics.DrawString(col.HeaderText, headerFont, Brushes.Black, x, y);
                x += 150;
            }

            y += offsetY;

            // Rows
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                x = startX;

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    string value = row.Cells[i].Value?.ToString();

                    if (!string.IsNullOrEmpty(value))
                    {
                        e.Graphics.DrawString(value, textFont, Brushes.Black, x, y);

                        if (i == dgv.Columns.Count - 2) // คอลัมน์จำนวนเงิน
                        {
                            if (decimal.TryParse(value, out decimal num))
                                sum += num;
                        }
                    }

                    x += 150;
                }

                y += offsetY;
            }

            y += 10;
            e.Graphics.DrawLine(Pens.Black, startX, y, startX + 700, y);

            y += 20;

            string totalText = "ยอดรวมรายรับ : " + sum.ToString("N2") + " บาท";
            e.Graphics.DrawString(totalText, totalFont, Brushes.Black, 550, y);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files|*.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        DataGridView dgv;

                        if (tabControl1.SelectedIndex == 1)
                            dgv = dgvFees;       // รายจ่าย
                        else
                            dgv = dgvPayments;   // รายรับ

                        var worksheet = workbook.Worksheets.Add("Report");

                        // Header
                        for (int i = 0; i < dgv.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = dgv.Columns[i].HeaderText;
                        }

                        // Data
                        for (int i = 0; i < dgv.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgv.Columns.Count; j++)
                            {
                                worksheet.Cell(i + 2, j + 1).Value =
                                dgv.Rows[i].Cells[j].Value?.ToString();
                            }
                        }

                        worksheet.Columns().AdjustToContents();

                        workbook.SaveAs(sfd.FileName);
                    }

                    MessageBox.Show("Export Excel สำเร็จ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnSearchPay_Click(object sender, EventArgs e)
        {
            LoadPayments();
        }

        // (ปุ่มแก้ไข/ลบ ให้ใช้ Logic คล้ายกัน โดยต้องเรียก UpdateExpenseTotal ทุกครั้งที่มีการเปลี่ยนแปลง Fee) [cite: 2026-03-09]
    }
}