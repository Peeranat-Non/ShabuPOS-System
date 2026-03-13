using System.Drawing;
using System.Drawing.Printing;
using System.Net.Http.Json; // ⚠️ ต้องมีอันนี้
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using QRCoder;
using BuffetAPI.Models; // ✅ เพิ่มบรรทัดนี้เพื่อให้โปรแกรมรู้จัก BillResult ที่คุณเพิ่งแก้ไป

namespace ShabuPOS
{
    public partial class Form1 : Form

    {
        private MemberItem _currentMember = null;
        // IP ของ API (เปลี่ยนตามเครื่องคุณ)
        private const string ApiBaseUrl = "http://localhost:5000";

        // ตัวแปรเก็บข้อมูลชั่วคราว
        private Bitmap _qrCodeBitmap;
        private BillResult _currentBill;

        // ✅ เพิ่ม: ตัวแปรสำหรับระบบผังร้าน
        private string _selectedTableNo = ""; // เลขโต๊ะที่ถูกจิ้มเลือกอยู่
        private List<int> _busyTables = new List<int>(); // รายชื่อโต๊ะที่ไม่ว่าง
        private List<PromotionItem> _allPromotions = new List<PromotionItem>();

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            // 1. หยุดการวาดหน้าจอชั่วคราวเพื่อลดอาการกระพริบ
            this.SuspendLayout();

            // 2. จัดการเรื่องฟอนต์และ UI พื้นฐานทันที (ไม่ต้องรอ API)
            ThemeConfig.ApplyGlobalFont(this);
            SetupInitialUI();

            // 3. ปล่อยให้หน้าจอแสดงผลออกมา
            this.ResumeLayout();


            ThemeConfig.ApplyGlobalFont(this);

            // 1. ใส่แพ็กเกจ (Value ต้องตรงกับ ID ใน Database)
            cboPackage.Items.Clear();
            cboPackage.Items.Add(new PackageItem { Name = "Silver (299)", Id = "PACK001" });
            cboPackage.Items.Add(new PackageItem { Name = "Gold (399)", Id = "PACK002" });
            cboPackage.Items.Add(new PackageItem { Name = "Platinum (499)", Id = "PACK003" });
            cboPackage.SelectedIndex = 0;
            cboPackage.DisplayMember = "Name"; // โชว์ชื่อแต่เก็บ ID

            await PreloadAllPromotions();
            // 2. ✅ โหลดผังร้าน (สร้างปุ่มโต๊ะ)
            await RefreshTableLayout();
        }

        // 2. เพิ่มฟังก์ชันนี้เข้าไปในคลาส Form1
        private async Task PreloadAllPromotions()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // ยิง API ไปที่ Endpoint ที่ดึงโปรโมชั่นทั้งหมด (เช็ค URL กับฝั่ง API ของคุณด้วยนะครับ)
                    var response = await client.GetAsync($"{ApiBaseUrl}/api/Promotion");

                    if (response.IsSuccessStatusCode)
                    {
                        _allPromotions = await response.Content.ReadFromJsonAsync<List<PromotionItem>>();
                    }
                }
                catch (Exception ex)
                {
                    // ถ้าโหลดไม่ได้ ให้ลิสต์ว่างไว้เพื่อป้องกันโปรแกรมค้าง
                    _allPromotions = new List<PromotionItem>();
                    Console.WriteLine("โหลดโปรโมชั่นล่วงหน้าไม่สำเร็จ: " + ex.Message);
                }
            }
        }

        private void SetupInitialUI()
        {
            cboTableStatusFilter.Items.Clear();
            cboTableStatusFilter.Items.Add("แสดงทั้งหมด");
            cboTableStatusFilter.Items.Add("เฉพาะโต๊ะว่าง (สีเขียว)");
            cboTableStatusFilter.Items.Add("เฉพาะโต๊ะไม่ว่าง (สีแดง)");
            cboTableStatusFilter.SelectedIndex = 0; // เลือก "ทั้งหมด" เป็นค่าเริ่มต้น

            // ย้ายโค้ดพวกนี้มาไว้ที่นี่ หน้า Load จะได้ดูสะอาดครับ
            cboPackage.Items.Clear();
            cboPackage.Items.Add(new PackageItem { Name = "Silver (299)", Id = "PACK001" });
            cboPackage.Items.Add(new PackageItem { Name = "Gold (399)", Id = "PACK002" });
            cboPackage.Items.Add(new PackageItem { Name = "Platinum (499)", Id = "PACK003" });
            cboPackage.SelectedIndex = 0;
            cboPackage.DisplayMember = "Name";
        }

        private void ApplyTableFilter()
        {
            string selectedFilter = cboTableStatusFilter.Text;

            // ระงับการวาดหน้าจอชั่วคราวเพื่อให้ลื่นไหล
            flowTablePanel.SuspendLayout();

            foreach (Control ctrl in flowTablePanel.Controls)
            {
                if (ctrl is Button btn)
                {
                    // ตรวจสอบสีของปุ่มเพื่อแยกสถานะ [cite: 2026-02-26]
                    bool isBusy = (btn.BackColor == Color.Salmon); // โต๊ะไม่ว่าง
                    bool isVacant = (btn.BackColor == Color.LightGreen); // โต๊ะว่าง

                    if (selectedFilter == "แสดงทั้งหมด")
                    {
                        btn.Visible = true;
                    }
                    else if (selectedFilter == "เฉพาะโต๊ะว่าง (สีเขียว)")
                    {
                        btn.Visible = isVacant; // ถ้าว่างให้โชว์ ถ้าไม่ว่างให้ซ่อน
                    }
                    else if (selectedFilter == "เฉพาะโต๊ะไม่ว่าง (สีแดง)")
                    {
                        btn.Visible = isBusy; // ถ้าไม่ว่างให้โชว์ ถ้าว่างให้ซ่อน
                    }
                }
            }

            flowTablePanel.ResumeLayout();
        }

        // ========================================================
        // 🟢 ระบบจัดการผังร้าน (Table Dashboard)
        // ========================================================
        private async Task LoadPromotionsByPackage(string packageId)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{ApiBaseUrl}/api/Promotion/package/{packageId}");
                    cbmPromotions.Items.Clear();
                    cbmPromotions.Items.Add(new PromotionItem { Promo_ID = "NONE", Promo_Name = "--- ไม่ใช้โปรโมชั่น ---", Promo_Discount = 0 });

                    if (response.IsSuccessStatusCode)
                    {
                        var promotions = await response.Content.ReadFromJsonAsync<List<PromotionItem>>();
                        if (promotions != null)
                        {
                            foreach (var promo in promotions)
                            {
                                cbmPromotions.Items.Add(promo);
                            }
                        }
                    }

                    // ✅ จุดที่ต้องแก้: เพิ่ม 2 บรรทัดนี้เพื่อให้โชว์ชื่อโปรโมชั่น
                    cbmPromotions.DisplayMember = "Promo_Name";
                    cbmPromotions.ValueMember = "Promo_ID";

                    cbmPromotions.SelectedIndex = 0;
                }
                catch { /* จัดการ Error */ }
            }
        }
        public async Task RefreshTableLayout()
        {
            // 1. ระงับการวาดชั่วคราวเพื่อให้สร้างปุ่มได้เร็วขึ้น
            flowTablePanel.SuspendLayout();

            try
            {
                // 2. สร้างปุ่มโต๊ะ 25 ปุ่ม (ถ้ายังไม่มีใน Panel) [cite: 2026-02-26]
                if (flowTablePanel.Controls.Count == 0)
                {
                    for (int i = 1; i <= 25; i++)
                    {
                        Button btn = new Button();
                        btn.Text = $"โต๊ะ {i}";
                        btn.Tag = i.ToString();
                        btn.BackColor = Color.LightGray; // สีเทารอโหลด [cite: 2026-02-26]
                        btn.Font = new Font("Kanit", 18, FontStyle.Bold); // ดึงจากฟอนต์ที่เราตั้งไว้
                        btn.Click += TableButton_Click;

                        // ใช้เทคนิคช่วยให้ปุ่มมนและสวยตามธีม (ถ้ามี ThemeConfig)
                        // ThemeConfig.ApplyButtonStyle(btn, Color.LightGray, Color.Gray);

                        flowTablePanel.Controls.Add(btn);
                    }
                    ResizeTableButtons(); // จัดขนาดปุ่มให้พอดี [cite: 2026-02-26]
                }

                // 3. ปล่อยให้ปุ่มปรากฏตัวออกมาก่อน (User จะเห็นโต๊ะสีเทาทันที)
                flowTablePanel.ResumeLayout();
                flowTablePanel.Visible = true;

                // 4. ไปดึงข้อมูลสถานะโต๊ะจาก API (แบบไม่ขัดจังหวะหน้าจอ) [cite: 2026-02-26]
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"{ApiBaseUrl}/api/Service/active");
                    if (response.IsSuccessStatusCode)
                    {
                        _busyTables = await response.Content.ReadFromJsonAsync<List<int>>();

                        // 5. ✅ เมื่อได้ข้อมูลมาแล้ว วนลูป "ระบายสี" ให้ปุ่มแต่ละใบ
                        foreach (Control ctrl in flowTablePanel.Controls)
                        {
                            if (ctrl is Button btn)
                            {
                                int tableIdx = int.Parse(btn.Tag.ToString());

                                // ถ้าเลขโต๊ะอยู่ในรายการ "ไม่ว่าง" ให้เป็นสี Salmon [cite: 2026-02-26]
                                if (_busyTables.Contains(tableIdx))
                                {
                                    btn.BackColor = Color.Salmon;
                                    btn.ForeColor = Color.White;
                                }
                                else
                                {
                                    btn.BackColor = Color.LightGreen;
                                    btn.ForeColor = Color.Black;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // ถ้าต่อ API ไม่ติด ให้ปุ่มเป็นสีเทาเหมือนเดิม หรือแจ้งเตือน
                Console.WriteLine("API Error: " + ex.Message);
            }
        }

        // เมื่อกดปุ่มโต๊ะ
        // เปลี่ยน void เป็น async void เพื่อให้รอโหลดข้อมูลได้
        private async void TableButton_Click(object sender, EventArgs e)
        {
            Button clickedBtn = (Button)sender;
            _selectedTableNo = clickedBtn.Tag.ToString();
            lblSelectedTable.Text = $"กำลังเลือก: โต๊ะ {_selectedTableNo}";

            if (clickedBtn.BackColor == Color.Salmon) // 🔴 โต๊ะไม่ว่าง
            {
                btnCheckBill.Enabled = true;
                btnGenQR.Enabled = false;
                cboPackage.Enabled = false;
                txtPeople.Enabled = false;

                // ล้างค่าชั่วคราวระหว่างโหลด (ตาม DFD 7.3 ดึงข้อมูล)
                cboPackage.SelectedIndex = -1;
                cbmPromotions.SelectedIndex = -1; // ล้างช่องโปรโมชั่นด้วย
                txtPeople.Text = "...";

                string serviceId = "SRV" + _selectedTableNo.PadLeft(3, '0');

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        var response = await client.GetAsync($"{ApiBaseUrl}/api/Billing/check/{serviceId}");
                        if (response.IsSuccessStatusCode)
                        {
                            string json = await response.Content.ReadAsStringAsync();
                            var bill = JsonConvert.DeserializeObject<BillResult>(json);

                            txtPeople.Text = bill.TotalPeople.ToString();

                            // 1. เลือก Package เดิมที่เปิดไว้
                            foreach (PackageItem item in cboPackage.Items)
                            {
                                if (item.Id == bill.PackageId) { cboPackage.SelectedItem = item; break; }
                            }

                            await LoadPromotionsByPackage(bill.PackageId);
                        }
                    }
                    catch { txtPeople.Text = "Error"; }
                }
            }
            else // 🟢 โต๊ะว่าง
            {
                btnGenQR.Enabled = true;
                btnCheckBill.Enabled = false;
                cboPackage.Enabled = true;
                txtPeople.Enabled = true;

                txtPeople.Text = "1";
                if (cboPackage.Items.Count > 0) cboPackage.SelectedIndex = 0;

                // ✅ เพิ่มตรงนี้: รีเซ็ต ComboBox โปรโมชั่นให้เป็น "ไม่ใช้โปรโมชั่น"
                if (cbmPromotions.Items.Count > 0) cbmPromotions.SelectedIndex = 0;
            }
        }
        // ========================================================
        // 🟢 ปุ่มที่ 1: เปิดโต๊ะ (Open Table)
        // ========================================================
        private async void btnGenQR_Click(object sender, EventArgs e)
        {
            // ใช้ _selectedTableNo แทน cboTable.Text
            if (string.IsNullOrEmpty(_selectedTableNo))
            {
                MessageBox.Show("กรุณาจิ้มเลือกโต๊ะสีเขียวก่อนครับ!");
                return;
            }

            var selectedPackage = (PackageItem)cboPackage.SelectedItem;
            string peopleStr = txtPeople.Text;

            if (selectedPackage == null) return;

            // 1. เตรียมข้อมูล
            var requestData = new
            {
                TableNo = _selectedTableNo,
                PackageId = selectedPackage.Id,
                People = int.Parse(peopleStr)
            };

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // 2. ยิง API เปิดโต๊ะ
                    string url = $"{ApiBaseUrl}/api/Service/open";
                    var response = await client.PostAsJsonAsync(url, requestData);

                    if (response.IsSuccessStatusCode)
                    {
                        // 3. สร้าง QR Code
                        GenerateAndPrintQR(_selectedTableNo);

                        MessageBox.Show($"เปิดโต๊ะ {_selectedTableNo} สำเร็จ!");

                        // ✅ รีเฟรชหน้าจอ (ให้ปุ่มกลายเป็นสีแดง)
                        await RefreshTableLayout();

                        // เคลียร์ค่า
                        _selectedTableNo = "";
                        lblSelectedTable.Text = "กรุณาเลือกโต๊ะ";
                        btnGenQR.Enabled = false;
                    }
                    else
                    {
                        string errorMsg = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Server แจ้งว่า: {errorMsg}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เชื่อมต่อ API ไม่ได้: " + ex.Message);
                }
            }
        }

        private void GenerateAndPrintQR(string tableNo)
        {
            string baseUrl = "https://ashely-anthracoid-trappedly.ngrok-free.dev";
            string finalUrl = $"{baseUrl}/?table={tableNo}";

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(finalUrl, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    _qrCodeBitmap = qrCode.GetGraphic(50);
                    picQR.Image = _qrCodeBitmap;
                    picQR.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += Pd_PrintQRPage;

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = pd;
            preview.ShowDialog();
        }

        private void Pd_PrintQRPage(object sender, PrintPageEventArgs e)
        {
            if (_qrCodeBitmap == null) return;
            Graphics g = e.Graphics;

            float centerX = e.PageBounds.Width / 2;
            float y = 100;

            Font fontBig = new Font("Kanit", 20, FontStyle.Bold);
            // เปลี่ยนหัวข้อเป็นเลขโต๊ะที่เลือก
            string title = $"Table {_selectedTableNo}";
            SizeF s = g.MeasureString(title, fontBig);
            g.DrawString(title, fontBig, Brushes.Black, centerX - (s.Width / 2), y);
            y += 50;

            int qrSize = 400; // ขนาดตามที่คุณตั้งไว้
            float qrX = centerX - (qrSize / 2);
            g.DrawImage(_qrCodeBitmap, qrX, y, qrSize, qrSize);

            y += qrSize + 20;

            Font fontSmall = new Font("Kanit", 12);
            string footer = "Scan to Order";
            SizeF sf = g.MeasureString(footer, fontSmall);
            g.DrawString(footer, fontSmall, Brushes.Black, centerX - (sf.Width / 2), y);
        }

        // ========================================================
        // 🔴 ปุ่มที่ 2: เช็คบิล + เคลียร์โต๊ะ
        // ========================================================
        // ใน Form1.cs
        private async void btnCheckBill_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedTableNo)) return;

            string serviceId = "SRV" + _selectedTableNo.PadLeft(3, '0');

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{ApiBaseUrl}/api/Billing/check/{serviceId}");

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        _currentBill = JsonConvert.DeserializeObject<BillResult>(json);

                        var selectedPromo = (PromotionItem)cbmPromotions.SelectedItem;

                        decimal promoPct = selectedPromo?.Promo_Discount ?? 0;
                        decimal memberPct = _currentMember != null ? 5 : 0;

                        decimal price = _currentBill.Package_Price;

                        // ⭐ เก็บข้อมูลเดิมจาก API
                        string packageName = _currentBill.Package_Name;
                        string packageId = _currentBill.PackageId;
                        int people = _currentBill.TotalPeople;

                        // คำนวณบิลใหม่
                        _currentBill = BillResult.Calculate(
                            price,
                            people,
                            promoPct,
                            memberPct
                        );

                        // ⭐ ใส่ข้อมูลเดิมกลับเข้าไป
                        _currentBill.ServiceId = serviceId;
                        _currentBill.Package_Name = packageName;
                        _currentBill.PackageId = packageId;

                        PrintBillReceipt();

                        FromCashier.SharedTableNo = _selectedTableNo;
                        FromCashier.SharedBillData = _currentBill;

                        if (Application.OpenForms["FromCashier"] is FromCashier cashierForm)
                        {
                            cashierForm.DisplayBillData();
                        }

                        MessageBox.Show("ปริ้นใบแจ้งหนี้เรียบร้อย กรุณาให้ลูกค้าไปชำระเงินที่เคาน์เตอร์");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // แยกฟังก์ชันปิดโต๊ะออกมา เพื่อเรียกใช้ซ้ำได้ง่ายๆ
        //private async Task CloseTable(HttpClient client, string serviceId)
        //{
        //    // เรียก API ลบข้อมูล
        //    var response = await client.DeleteAsync($"{ApiBaseUrl}/api/Service/close/{serviceId}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        MessageBox.Show("เคลียร์โต๊ะเรียบร้อยครับ!");
        //        await RefreshTableLayout(); // รีเฟรชปุ่มให้กลับเป็นสีเขียว

        //        _selectedTableNo = "";
        //        lblSelectedTable.Text = "กรุณาเลือกโต๊ะ";
        //        btnCheckBill.Enabled = false;

        //        // ล้างหน้าจอขวา
        //        txtPeople.Text = "";
        //        cboPackage.SelectedIndex = -1;
        //        cboPackage.Text = "";
        //    }
        //    else
        //    {
        //        MessageBox.Show("ลบไม่สำเร็จ: " + await response.Content.ReadAsStringAsync());
        //    }

        //}
        //private async Task<bool> SavePaymentAsync(string serviceId, decimal grandTotal)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        try
        //        {
        //            // 1. ดึงรหัสโปรโมชั่นที่เลือก (ถ้าเลือก "ไม่ใช้โปรโมชั่น" ให้ส่งเป็น null)
        //            var selectedPromo = (PromotionItem)cbmPromotions.SelectedItem;
        //            string promoId = (selectedPromo != null && selectedPromo.Promo_ID != "NONE")
        //                             ? selectedPromo.Promo_ID : null;

        //            // 2. ดึงรหัสสมาชิก (ถ้ามีการค้นหาสมาชิกเจอ _currentMember จะไม่เป็น null)
        //            int? memberId = _currentMember != null ? _currentMember.MemberId : (int?)null;

        //            // 3. แพ็กข้อมูลเพื่อส่งไป API (ชื่อฟีลด์ต้องตรงกับ Model ใน API)
        //            var paymentData = new
        //            {
        //                ServiceId = serviceId,
        //                TotalAmount = grandTotal,
        //                Promo_ID = promoId,
        //                MemberId = memberId,
        //                PaymentMethod = "Cash" // สมมติว่าจ่ายเงินสด ถ้ามี Dropdown เลือกวิธีจ่ายก็ดึงมาใส่ได้ครับ
        //            };

        //            // 4. ยิง API ไปบันทึก (ต้องไปสร้าง Endpoint นี้ในฝั่ง API ด้วยนะครับ)
        //            var response = await client.PostAsJsonAsync($"{ApiBaseUrl}/api/Billing/pay", paymentData);
        //            if (!response.IsSuccessStatusCode)
        //            {
        //                // ✅ เพิ่มตรงนี้: ดึงข้อความ Error จาก Server มาโชว์ จะได้รู้ว่าพังที่ตรงไหน
        //                string errorDetails = await response.Content.ReadAsStringAsync();
        //                MessageBox.Show($"เซิร์ฟเวอร์แจ้งสาเหตุว่า:\n{errorDetails}", "พบข้อผิดพลาดจาก API", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //            return response.IsSuccessStatusCode;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("เกิดข้อผิดพลาดตอนบันทึกบิล: " + ex.Message);
        //            return false;
        //        }
        //    }
        //}

        private void PrintBillReceipt()
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += Pd_PrintBillPage;

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = pd;
            preview.Width = 400;
            preview.Height = 700;
            preview.ShowDialog();
        }

        private void Pd_PrintBillPage(object sender, PrintPageEventArgs e)
        {
            if (_currentBill == null) return;

            Graphics g = e.Graphics;

            Font fontHeader = new Font("Kanit", 22, FontStyle.Bold);
            Font fontSubHeader = new Font("Kanit", 18, FontStyle.Bold);
            Font fontBody = new Font("Kanit", 16);
            Font fontBold = new Font("Kanit", 16, FontStyle.Bold);
            Font fontSmall = new Font("Kanit", 12);

            Brush brush = Brushes.Black;
            Pen pen = new Pen(Color.Black, 2);

            float contentWidth = 600;
            float centerX = e.PageBounds.Width / 2;
            float left = centerX - (contentWidth / 2);
            float right = centerX + (contentWidth / 2);
            float y = 50;

            // Helper Functions
            void DrawRight(string text, Font f, float yPos)
            {
                SizeF s = g.MeasureString(text, f);
                g.DrawString(text, f, brush, right - s.Width, yPos);
            }
            void DrawCenter(string text, Font f, float yPos)
            {
                SizeF s = g.MeasureString(text, f);
                g.DrawString(text, f, brush, centerX - (s.Width / 2), yPos);
            }
            void DrawLine(float yPos)
            {
                g.DrawLine(pen, left, yPos, right, yPos);
            }

            // --- เริ่มวาดใบเสร็จ ---
            DrawCenter("SHABU NOONY", fontHeader, y); y += 40;
            DrawCenter("Tax Invoice (ABB)", fontBody, y); y += 30;
            DrawLine(y); y += 20;

            g.DrawString($"Date: {DateTime.Now:dd/MM/yyyy HH:mm}", fontBody, brush, left, y); y += 25;
            g.DrawString($"Table: {_selectedTableNo}", fontBody, brush, left, y);
            DrawRight($"Bill ID: {_currentBill.ServiceId}", fontBody, y); y += 30;

            DrawLine(y); y += 20;

            g.DrawString("Item", fontBold, brush, left, y);
            DrawRight("Amount", fontBold, y); y += 30;

            string itemName = $"{_currentBill.Package_Name} (x{_currentBill.TotalPeople})";
            string itemPrice = _currentBill.Subtotal.ToString("N2");

            g.DrawString(itemName, fontBody, brush, left, y);
            DrawRight(itemPrice, fontBody, y); y += 30;

            y += 10;
            DrawLine(y); y += 20;

            g.DrawString("Subtotal", fontBody, brush, left, y);
            DrawRight(_currentBill.Subtotal.ToString("N2"), fontBody, y); y += 30;

            // ==========================================
            // ✅ ส่วนที่เพิ่ม: จัดการเรื่องการแสดงส่วนลดในใบเสร็จ
            // ==========================================
            decimal netAfterPromo = _currentBill.Subtotal; // ยอดตั้งต้น

            // 1. โชว์ส่วนลดโปรโมชั่น (ถ้ามี)
            var selectedPromo = (PromotionItem)cbmPromotions.SelectedItem;
            if (selectedPromo != null && (selectedPromo.Promo_Discount ?? 0) > 0)
            {
                decimal discountVal = _currentBill.Subtotal * ((selectedPromo.Promo_Discount ?? 0) / 100m);
                netAfterPromo -= discountVal; // หักยอดโปรโมชั่นออก

                g.DrawString($"Promo Disc ({selectedPromo.Promo_Discount}%)", fontBody, brush, left, y);
                DrawRight("-" + discountVal.ToString("N2"), fontBody, y); y += 30;
            }

            // 2. โชว์ส่วนลดสมาชิก 5% (ถ้ามีการล็อกอินสมาชิก)
            if (_currentMember != null)
            {
                decimal memberDiscount = netAfterPromo * 0.05m; // คิด 5% จากยอดที่หักโปรโมชั่นแล้ว

                g.DrawString($"Member Disc (5%)", fontBody, brush, left, y);
                DrawRight("-" + memberDiscount.ToString("N2"), fontBody, y); y += 30;
            }
            // ==========================================

            g.DrawString("VAT 7%", fontBody, brush, left, y);
            DrawRight(_currentBill.VatAmount.ToString("N2"), fontBody, y); y += 40;

            g.DrawString("TOTAL", fontHeader, brush, left, y);
            DrawRight(_currentBill.GrandTotal.ToString("N2"), fontHeader, y); y += 50;

            DrawLine(y); y += 30;

            DrawCenter("Thank you for your visit!", fontBody, y); y += 25;
            DrawCenter("Powered by ShabuPOS", fontSmall, y);
        }

        private void buttonManageProducts_Click(object sender, EventArgs e)
        {
            //formProductList fProd = new formProductList();
            //fProd.ShowDialog(this);
            FormEmployee fEmp = new FormEmployee();
            fEmp.ShowDialog(this);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void picQR_Click(object sender, EventArgs e)
        {

        }
        private async Task SearchMember(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) return;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // เรียก API: GET /api/Member/search/{phone}
                    var response = await client.GetAsync($"{ApiBaseUrl}/api/Member/search/{phoneNumber}");

                    if (response.IsSuccessStatusCode)
                    {
                        _currentMember = await response.Content.ReadFromJsonAsync<MemberItem>();
                        if (_currentMember != null)
                        {
                            // แสดงชื่อสมาชิกใน UI 
                            lblMemberName.Text = $"สมาชิก: คุณ {_currentMember.FirstName} {_currentMember.LastName}";
                            lblMemberName.ForeColor = Color.DarkGreen;
                        }
                    }
                    else
                    {
                        _currentMember = null;
                        lblMemberName.Text = "ไม่พบสมาชิก หรือเบอร์โทรไม่ถูกต้อง";
                        lblMemberName.ForeColor = Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"เชื่อมต่อระบบสมาชิกไม่ได้: {ex.Message}");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void btnSearchMember_Click(object sender, EventArgs e)
        {
            // ✅ ใส่กับดักบรรทัดนี้ไว้บนสุดเลยครับ
            MessageBox.Show("เย้! ปุ่มค้นหาถูกกดแล้ว โค้ดทำงาน!");

            // (สมมติว่าช่องชื่อ txtSearchMember)
            if (string.IsNullOrWhiteSpace(txtSearchMember.Text))
            {
                MessageBox.Show("กรุณากรอกเบอร์โทรศัพท์ด้วยครับ");
                return;
            }

            await SearchMember(txtSearchMember.Text.Trim());
        }

        private void cbmPromotions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void ResizeTableButtons()
        {
            if (flowTablePanel.Controls.Count == 0) return;

            int columns = 5; // ต้องการให้โชว์ 5 แถว
            int margin = 6;

            // ✅ หักลบพื้นที่สำหรับ Scrollbar (ประมาณ 25 px) เพื่อไม่ให้ปุ่มเบียดจนตกบรรทัด
            int scrollBarWidth = SystemInformation.VerticalScrollBarWidth + 5;
            int availableWidth = flowTablePanel.ClientSize.Width - scrollBarWidth;

            int targetWidth = (availableWidth / columns) - (margin * 2);
            int targetHeight = 100; // ปรับความสูงตามความสวยงาม

            foreach (Control ctrl in flowTablePanel.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Width = targetWidth;
                    btn.Height = targetHeight;
                    btn.Margin = new Padding(margin);
                }
            }
        }

        private void flowTablePanel_Resize(object sender, EventArgs e)
        {
            ResizeTableButtons();
        }

        private void btnFilterTable_Click(object sender, EventArgs e)
        {
            ApplyTableFilter();
        }
    } // ✅ ปิดวงเล็บของคลาส Form1 ตรงนี้ครับ!    


    // Class Helper
    public class PackageItem
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }

    //public class BillResult
    //{
    //    public string ServiceId { get; set; }
    //    public string Package_Name { get; set; }
    //    public int TotalPeople { get; set; }
    //    public decimal PackagePrice { get; set; }
    //    public decimal TotalAmount { get; set; }
    //    public decimal VatAmount { get; set; }
    //    public decimal GrandTotal { get; set; }
    //    public string PackageId { get; set; }
    //}
    public class MemberItem
    {
        public int MemberId { get; set; } // เปลี่ยนจาก string เป็น int ตาม API
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string Phone { get; set; } = null!;
        // ข้อมูลอื่นๆ เช่น Gender, BirthDate สามารถเพิ่มได้ถ้าต้องการโชว์หน้าจอครับ
    }
    public class PromotionItem
    {
        public string Promo_ID { get; set; }
        public string Package_ID { get; set; }
        public string Promo_Name { get; set; }
        public int? Promo_Discount { get; set; } // เก็บค่า % ส่วนลด
    }
}