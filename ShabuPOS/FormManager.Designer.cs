namespace ShabuPOS
{
    partial class FormManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormManager));
            cboReportType = new ComboBox();
            label1 = new Label();
            dtpStart = new DateTimePicker();
            label2 = new Label();
            dtpEnd = new DateTimePicker();
            label3 = new Label();
            dgvReport = new DataGridView();
            btnExportExcel = new Button();
            lblGrandTotal = new Label();
            btnSearch = new Button();
            btnPrintPDF = new Button();
            printDoc = new System.Drawing.Printing.PrintDocument();
            printPreviewDialog1 = new PrintPreviewDialog();
            lblVatAmount = new Label();
            lblBeforeVat = new Label();
            tabControl1 = new TabControl();
            tabFinancial = new TabPage();
            tabMasterData = new TabPage();
            button1 = new Button();
            button4 = new Button();
            label7 = new Label();
            label8 = new Label();
            button2 = new Button();
            label9 = new Label();
            button3 = new Button();
            dgvMaster = new DataGridView();
            tabPage1 = new TabPage();
            lblRevenueTotal = new Label();
            btnPDFRevenue = new Button();
            btnExcelRevenue = new Button();
            dgvRevenue = new DataGridView();
            tabPage2 = new TabPage();
            label4 = new Label();
            btnPDFExpense = new Button();
            btnExcelExpense = new Button();
            dgvExpense = new DataGridView();
            pnlDateFilter = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvReport).BeginInit();
            tabControl1.SuspendLayout();
            tabFinancial.SuspendLayout();
            tabMasterData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMaster).BeginInit();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRevenue).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExpense).BeginInit();
            pnlDateFilter.SuspendLayout();
            SuspendLayout();
            // 
            // cboReportType
            // 
            cboReportType.FormattingEnabled = true;
            cboReportType.Location = new Point(219, 37);
            cboReportType.Name = "cboReportType";
            cboReportType.Size = new Size(250, 39);
            cboReportType.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 37);
            label1.Name = "label1";
            label1.Size = new Size(192, 31);
            label1.TabIndex = 1;
            label1.Text = "เลือกประเภทรายงาน:";
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(219, 103);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(250, 38);
            dtpStart.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(90, 110);
            label2.Name = "label2";
            label2.Size = new Size(113, 31);
            label2.TabIndex = 3;
            label2.Text = "วันที่เริ่มต้น:";
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(219, 164);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(250, 38);
            dtpEnd.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(95, 170);
            label3.Name = "label3";
            label3.Size = new Size(108, 31);
            label3.TabIndex = 5;
            label3.Text = "วันที่สิ้นสุด:";
            // 
            // dgvReport
            // 
            dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReport.Location = new Point(29, 26);
            dgvReport.Name = "dgvReport";
            dgvReport.RowHeadersWidth = 51;
            dgvReport.Size = new Size(1662, 432);
            dgvReport.TabIndex = 6;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Location = new Point(799, 520);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(215, 59);
            btnExportExcel.TabIndex = 7;
            btnExportExcel.Text = "ส่งออก Excel";
            btnExportExcel.UseVisualStyleBackColor = true;
            btnExportExcel.Click += btnExportExcel_Click;
            // 
            // lblGrandTotal
            // 
            lblGrandTotal.BackColor = SystemColors.MenuHighlight;
            lblGrandTotal.BorderStyle = BorderStyle.Fixed3D;
            lblGrandTotal.ForeColor = Color.White;
            lblGrandTotal.Location = new Point(28, 587);
            lblGrandTotal.Name = "lblGrandTotal";
            lblGrandTotal.Size = new Size(631, 42);
            lblGrandTotal.TabIndex = 36;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(500, 37);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(215, 59);
            btnSearch.TabIndex = 37;
            btnSearch.Text = "ค้นหา";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnPrintPDF
            // 
            btnPrintPDF.Location = new Point(1043, 520);
            btnPrintPDF.Name = "btnPrintPDF";
            btnPrintPDF.Size = new Size(215, 59);
            btnPrintPDF.TabIndex = 38;
            btnPrintPDF.Text = "พิมพ์ PDF";
            btnPrintPDF.UseVisualStyleBackColor = true;
            btnPrintPDF.Click += btnPrintPDF_Click;
            // 
            // printDoc
            // 
            printDoc.PrintPage += printDoc_PrintPage;
            // 
            // printPreviewDialog1
            // 
            printPreviewDialog1.AutoScrollMargin = new Size(0, 0);
            printPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
            printPreviewDialog1.ClientSize = new Size(400, 300);
            printPreviewDialog1.Document = printDoc;
            printPreviewDialog1.Enabled = true;
            printPreviewDialog1.Icon = (Icon)resources.GetObject("printPreviewDialog1.Icon");
            printPreviewDialog1.Name = "printPreviewDialog1";
            printPreviewDialog1.Visible = false;
            // 
            // lblVatAmount
            // 
            lblVatAmount.BackColor = SystemColors.MenuHighlight;
            lblVatAmount.BorderStyle = BorderStyle.Fixed3D;
            lblVatAmount.ForeColor = Color.White;
            lblVatAmount.Location = new Point(28, 479);
            lblVatAmount.Name = "lblVatAmount";
            lblVatAmount.Size = new Size(631, 42);
            lblVatAmount.TabIndex = 39;
            // 
            // lblBeforeVat
            // 
            lblBeforeVat.BackColor = SystemColors.MenuHighlight;
            lblBeforeVat.BorderStyle = BorderStyle.Fixed3D;
            lblBeforeVat.ForeColor = Color.White;
            lblBeforeVat.Location = new Point(28, 533);
            lblBeforeVat.Name = "lblBeforeVat";
            lblBeforeVat.Size = new Size(631, 42);
            lblBeforeVat.TabIndex = 40;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabFinancial);
            tabControl1.Controls.Add(tabMasterData);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(39, 241);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1716, 693);
            tabControl1.TabIndex = 41;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabFinancial
            // 
            tabFinancial.Controls.Add(lblBeforeVat);
            tabFinancial.Controls.Add(lblVatAmount);
            tabFinancial.Controls.Add(btnPrintPDF);
            tabFinancial.Controls.Add(lblGrandTotal);
            tabFinancial.Controls.Add(btnExportExcel);
            tabFinancial.Controls.Add(dgvReport);
            tabFinancial.Location = new Point(4, 40);
            tabFinancial.Name = "tabFinancial";
            tabFinancial.Padding = new Padding(3);
            tabFinancial.Size = new Size(1708, 649);
            tabFinancial.TabIndex = 0;
            tabFinancial.Text = "รายงานใบขอซื้อ, รายงานใบสั่งซื้อ, รายงานการจ่ายเงิน";
            tabFinancial.UseVisualStyleBackColor = true;
            // 
            // tabMasterData
            // 
            tabMasterData.Controls.Add(button1);
            tabMasterData.Controls.Add(button4);
            tabMasterData.Controls.Add(label7);
            tabMasterData.Controls.Add(label8);
            tabMasterData.Controls.Add(button2);
            tabMasterData.Controls.Add(label9);
            tabMasterData.Controls.Add(button3);
            tabMasterData.Controls.Add(dgvMaster);
            tabMasterData.Location = new Point(4, 40);
            tabMasterData.Name = "tabMasterData";
            tabMasterData.Padding = new Padding(3);
            tabMasterData.Size = new Size(1708, 649);
            tabMasterData.TabIndex = 1;
            tabMasterData.Text = "รายงานข้อมูลพนักงาน, รายงานข้อมูลร้านค้า";
            tabMasterData.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(778, 514);
            button1.Name = "button1";
            button1.Size = new Size(215, 59);
            button1.TabIndex = 52;
            button1.Text = "พิมพ์ PDF";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnPrintPDF_Click;
            // 
            // button4
            // 
            button4.Location = new Point(534, 514);
            button4.Name = "button4";
            button4.Size = new Size(215, 59);
            button4.TabIndex = 51;
            button4.Text = "ส่งออก Excel";
            button4.UseVisualStyleBackColor = true;
            button4.Click += btnExportExcel_Click;
            // 
            // label7
            // 
            label7.BackColor = SystemColors.MenuHighlight;
            label7.BorderStyle = BorderStyle.Fixed3D;
            label7.ForeColor = Color.White;
            label7.Location = new Point(26, 740);
            label7.Name = "label7";
            label7.Size = new Size(631, 42);
            label7.TabIndex = 50;
            // 
            // label8
            // 
            label8.BackColor = SystemColors.MenuHighlight;
            label8.BorderStyle = BorderStyle.Fixed3D;
            label8.ForeColor = Color.White;
            label8.Location = new Point(26, 686);
            label8.Name = "label8";
            label8.Size = new Size(631, 42);
            label8.TabIndex = 49;
            // 
            // button2
            // 
            button2.Location = new Point(1041, 727);
            button2.Name = "button2";
            button2.Size = new Size(215, 59);
            button2.TabIndex = 48;
            button2.Text = "พิมพ์ PDF";
            button2.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.BackColor = SystemColors.MenuHighlight;
            label9.BorderStyle = BorderStyle.Fixed3D;
            label9.ForeColor = Color.White;
            label9.Location = new Point(26, 794);
            label9.Name = "label9";
            label9.Size = new Size(631, 42);
            label9.TabIndex = 47;
            // 
            // button3
            // 
            button3.Location = new Point(797, 727);
            button3.Name = "button3";
            button3.Size = new Size(215, 59);
            button3.TabIndex = 46;
            button3.Text = "ส่งออก Excel";
            button3.UseVisualStyleBackColor = true;
            // 
            // dgvMaster
            // 
            dgvMaster.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMaster.Location = new Point(26, 19);
            dgvMaster.Name = "dgvMaster";
            dgvMaster.RowHeadersWidth = 51;
            dgvMaster.Size = new Size(1662, 432);
            dgvMaster.TabIndex = 44;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(lblRevenueTotal);
            tabPage1.Controls.Add(btnPDFRevenue);
            tabPage1.Controls.Add(btnExcelRevenue);
            tabPage1.Controls.Add(dgvRevenue);
            tabPage1.Location = new Point(4, 40);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(1708, 649);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "รายรับ";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblRevenueTotal
            // 
            lblRevenueTotal.BackColor = SystemColors.MenuHighlight;
            lblRevenueTotal.BorderStyle = BorderStyle.Fixed3D;
            lblRevenueTotal.ForeColor = Color.White;
            lblRevenueTotal.Location = new Point(23, 541);
            lblRevenueTotal.Name = "lblRevenueTotal";
            lblRevenueTotal.Size = new Size(631, 42);
            lblRevenueTotal.TabIndex = 56;
            // 
            // btnPDFRevenue
            // 
            btnPDFRevenue.Location = new Point(1008, 528);
            btnPDFRevenue.Name = "btnPDFRevenue";
            btnPDFRevenue.Size = new Size(215, 59);
            btnPDFRevenue.TabIndex = 55;
            btnPDFRevenue.Text = "พิมพ์ PDF";
            btnPDFRevenue.UseVisualStyleBackColor = true;
            btnPDFRevenue.Click += btnPrintPDF_Click;
            // 
            // btnExcelRevenue
            // 
            btnExcelRevenue.Location = new Point(764, 528);
            btnExcelRevenue.Name = "btnExcelRevenue";
            btnExcelRevenue.Size = new Size(215, 59);
            btnExcelRevenue.TabIndex = 54;
            btnExcelRevenue.Text = "ส่งออก Excel";
            btnExcelRevenue.UseVisualStyleBackColor = true;
            btnExcelRevenue.Click += btnExportExcel_Click;
            // 
            // dgvRevenue
            // 
            dgvRevenue.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRevenue.Location = new Point(23, 47);
            dgvRevenue.Name = "dgvRevenue";
            dgvRevenue.RowHeadersWidth = 51;
            dgvRevenue.Size = new Size(1662, 432);
            dgvRevenue.TabIndex = 53;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(btnPDFExpense);
            tabPage2.Controls.Add(btnExcelExpense);
            tabPage2.Controls.Add(dgvExpense);
            tabPage2.Location = new Point(4, 40);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(1708, 649);
            tabPage2.TabIndex = 3;
            tabPage2.Text = "รายจ่าย";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.BackColor = SystemColors.MenuHighlight;
            label4.BorderStyle = BorderStyle.Fixed3D;
            label4.ForeColor = Color.White;
            label4.Location = new Point(23, 548);
            label4.Name = "label4";
            label4.Size = new Size(631, 42);
            label4.TabIndex = 60;
            // 
            // btnPDFExpense
            // 
            btnPDFExpense.Location = new Point(1008, 535);
            btnPDFExpense.Name = "btnPDFExpense";
            btnPDFExpense.Size = new Size(215, 59);
            btnPDFExpense.TabIndex = 59;
            btnPDFExpense.Text = "พิมพ์ PDF";
            btnPDFExpense.UseVisualStyleBackColor = true;
            btnPDFExpense.Click += btnPrintPDF_Click;
            // 
            // btnExcelExpense
            // 
            btnExcelExpense.Location = new Point(764, 535);
            btnExcelExpense.Name = "btnExcelExpense";
            btnExcelExpense.Size = new Size(215, 59);
            btnExcelExpense.TabIndex = 58;
            btnExcelExpense.Text = "ส่งออก Excel";
            btnExcelExpense.UseVisualStyleBackColor = true;
            btnExcelExpense.Click += btnExportExcel_Click;
            // 
            // dgvExpense
            // 
            dgvExpense.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExpense.Location = new Point(23, 54);
            dgvExpense.Name = "dgvExpense";
            dgvExpense.RowHeadersWidth = 51;
            dgvExpense.Size = new Size(1662, 432);
            dgvExpense.TabIndex = 57;
            // 
            // pnlDateFilter
            // 
            pnlDateFilter.Controls.Add(cboReportType);
            pnlDateFilter.Controls.Add(label3);
            pnlDateFilter.Controls.Add(dtpEnd);
            pnlDateFilter.Controls.Add(label1);
            pnlDateFilter.Controls.Add(label2);
            pnlDateFilter.Controls.Add(btnSearch);
            pnlDateFilter.Controls.Add(dtpStart);
            pnlDateFilter.Location = new Point(323, 12);
            pnlDateFilter.Name = "pnlDateFilter";
            pnlDateFilter.Size = new Size(777, 213);
            pnlDateFilter.TabIndex = 41;
            pnlDateFilter.TabStop = false;
            // 
            // FormManager
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1767, 958);
            Controls.Add(pnlDateFilter);
            Controls.Add(tabControl1);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "FormManager";
            Text = "FormManager";
            Load += FormManager_Load;
            Click += FormManager_Load;
            ((System.ComponentModel.ISupportInitialize)dgvReport).EndInit();
            tabControl1.ResumeLayout(false);
            tabFinancial.ResumeLayout(false);
            tabMasterData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMaster).EndInit();
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRevenue).EndInit();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvExpense).EndInit();
            pnlDateFilter.ResumeLayout(false);
            pnlDateFilter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cboReportType;
        private Label label1;
        private DateTimePicker dtpStart;
        private Label label2;
        private DateTimePicker dtpEnd;
        private Label label3;
        private DataGridView dgvReport;
        private Button btnExportExcel;
        private Label lblGrandTotal;
        private Button btnSearch;
        private Button btnPrintPDF;
        private System.Drawing.Printing.PrintDocument printDoc;
        private PrintPreviewDialog printPreviewDialog1;
        private Label lblVatAmount;
        private Label lblBeforeVat;
        private TabControl tabControl1;
        private TabPage tabFinancial;
        private TabPage tabMasterData;
        private DataGridView dgvMaster;
        private Label label7;
        private Label label8;
        private Button button2;
        private Label label9;
        private Button button3;
        private GroupBox pnlDateFilter;
        private Button button1;
        private Button button4;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button btnPDFRevenue;
        private Button btnExcelRevenue;
        private DataGridView dgvRevenue;
        private Label lblRevenueTotal;
        private Label label4;
        private Button btnPDFExpense;
        private Button btnExcelExpense;
        private DataGridView dgvExpense;
    }
}