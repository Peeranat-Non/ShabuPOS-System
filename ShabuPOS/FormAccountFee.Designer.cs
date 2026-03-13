namespace ShabuPOS
{
    partial class FormAccountFee
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
            label1 = new Label();
            txtExpAmount = new TextBox();
            dtpExpDate = new DateTimePicker();
            tabControl1 = new TabControl();
            tabFinancial = new TabPage();
            label3 = new Label();
            txtExpId = new TextBox();
            dgvExpense = new DataGridView();
            tabMasterData = new TabPage();
            btnSearch = new Button();
            dtpEnd = new DateTimePicker();
            dtpStart = new DateTimePicker();
            btnRefresh = new Button();
            cboStatusFilter = new ComboBox();
            txtSearchExp = new TextBox();
            lblSelectedExp = new Label();
            dgvExpensePicker = new DataGridView();
            txtFeeId = new TextBox();
            dtpFeeDate = new DateTimePicker();
            cboExpenseRef = new ComboBox();
            txtFeeTotal = new TextBox();
            label2 = new Label();
            button1 = new Button();
            btnClear = new Button();
            btnEditExp = new Button();
            btnDeleteExp = new Button();
            button4 = new Button();
            btnSaveExp = new Button();
            dgvFees = new DataGridView();
            tabL = new TabPage();
            label5 = new Label();
            label4 = new Label();
            lblTotalRevenue = new Label();
            btnSearchPay = new Button();
            dtpPayEnd = new DateTimePicker();
            dtpPayStart = new DateTimePicker();
            button3 = new Button();
            txtSearchPay = new TextBox();
            dgvPayments = new DataGridView();
            btnSave = new Button();
            btnPrintPDF = new Button();
            btnExportExcel = new Button();
            tabControl1.SuspendLayout();
            tabFinancial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExpense).BeginInit();
            tabMasterData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExpensePicker).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvFees).BeginInit();
            tabL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayments).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(992, 45);
            label1.Name = "label1";
            label1.Size = new Size(121, 31);
            label1.TabIndex = 1;
            label1.Text = "รหัสรายจ่าย:";
            // 
            // txtExpAmount
            // 
            txtExpAmount.Location = new Point(1129, 92);
            txtExpAmount.Name = "txtExpAmount";
            txtExpAmount.ReadOnly = true;
            txtExpAmount.Size = new Size(304, 38);
            txtExpAmount.TabIndex = 2;
            // 
            // dtpExpDate
            // 
            dtpExpDate.Location = new Point(1129, 162);
            dtpExpDate.Name = "dtpExpDate";
            dtpExpDate.Size = new Size(304, 38);
            dtpExpDate.TabIndex = 4;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabFinancial);
            tabControl1.Controls.Add(tabMasterData);
            tabControl1.Controls.Add(tabL);
            tabControl1.Location = new Point(25, 34);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1716, 764);
            tabControl1.TabIndex = 66;
            // 
            // tabFinancial
            // 
            tabFinancial.Controls.Add(label3);
            tabFinancial.Controls.Add(txtExpId);
            tabFinancial.Controls.Add(dtpExpDate);
            tabFinancial.Controls.Add(txtExpAmount);
            tabFinancial.Controls.Add(label1);
            tabFinancial.Controls.Add(dgvExpense);
            tabFinancial.Location = new Point(4, 40);
            tabFinancial.Name = "tabFinancial";
            tabFinancial.Padding = new Padding(3);
            tabFinancial.Size = new Size(1708, 720);
            tabFinancial.TabIndex = 0;
            tabFinancial.Text = "รายจ่ายหลัก";
            tabFinancial.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(958, 99);
            label3.Name = "label3";
            label3.Size = new Size(155, 31);
            label3.TabIndex = 8;
            label3.Text = "ยอดรวมรายจ่าย:";
            // 
            // txtExpId
            // 
            txtExpId.Location = new Point(1129, 38);
            txtExpId.Name = "txtExpId";
            txtExpId.ReadOnly = true;
            txtExpId.Size = new Size(304, 38);
            txtExpId.TabIndex = 7;
            // 
            // dgvExpense
            // 
            dgvExpense.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExpense.Location = new Point(30, 368);
            dgvExpense.Name = "dgvExpense";
            dgvExpense.RowHeadersWidth = 51;
            dgvExpense.Size = new Size(1662, 325);
            dgvExpense.TabIndex = 6;
            // 
            // tabMasterData
            // 
            tabMasterData.Controls.Add(btnSearch);
            tabMasterData.Controls.Add(dtpEnd);
            tabMasterData.Controls.Add(dtpStart);
            tabMasterData.Controls.Add(btnRefresh);
            tabMasterData.Controls.Add(cboStatusFilter);
            tabMasterData.Controls.Add(txtSearchExp);
            tabMasterData.Controls.Add(lblSelectedExp);
            tabMasterData.Controls.Add(dgvExpensePicker);
            tabMasterData.Controls.Add(txtFeeId);
            tabMasterData.Controls.Add(dtpFeeDate);
            tabMasterData.Controls.Add(cboExpenseRef);
            tabMasterData.Controls.Add(txtFeeTotal);
            tabMasterData.Controls.Add(label2);
            tabMasterData.Controls.Add(button1);
            tabMasterData.Controls.Add(btnClear);
            tabMasterData.Controls.Add(btnEditExp);
            tabMasterData.Controls.Add(btnDeleteExp);
            tabMasterData.Controls.Add(button4);
            tabMasterData.Controls.Add(btnSaveExp);
            tabMasterData.Controls.Add(dgvFees);
            tabMasterData.Location = new Point(4, 40);
            tabMasterData.Name = "tabMasterData";
            tabMasterData.Padding = new Padding(3);
            tabMasterData.Size = new Size(1708, 720);
            tabMasterData.TabIndex = 1;
            tabMasterData.Text = "รายจ่ายย่อย/ค่าธรรมเนียม";
            tabMasterData.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Image = Properties.Resources.save;
            btnSearch.Location = new Point(813, 277);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(189, 56);
            btnSearch.TabIndex = 79;
            btnSearch.Text = "ค้นหา";
            btnSearch.TextAlign = ContentAlignment.MiddleRight;
            btnSearch.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(792, 191);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(304, 38);
            dtpEnd.TabIndex = 78;
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(792, 147);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(304, 38);
            dtpStart.TabIndex = 77;
            // 
            // btnRefresh
            // 
            btnRefresh.Image = Properties.Resources.save;
            btnRefresh.Location = new Point(1057, 277);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(189, 56);
            btnRefresh.TabIndex = 76;
            btnRefresh.Text = "ปุ่มรีเฟรช";
            btnRefresh.TextAlign = ContentAlignment.MiddleRight;
            btnRefresh.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // cboStatusFilter
            // 
            cboStatusFilter.FormattingEnabled = true;
            cboStatusFilter.Location = new Point(792, 97);
            cboStatusFilter.Name = "cboStatusFilter";
            cboStatusFilter.Size = new Size(239, 39);
            cboStatusFilter.TabIndex = 75;
            // 
            // txtSearchExp
            // 
            txtSearchExp.Location = new Point(792, 41);
            txtSearchExp.Name = "txtSearchExp";
            txtSearchExp.Size = new Size(239, 38);
            txtSearchExp.TabIndex = 74;
            // 
            // lblSelectedExp
            // 
            lblSelectedExp.BorderStyle = BorderStyle.Fixed3D;
            lblSelectedExp.Location = new Point(1116, 98);
            lblSelectedExp.Name = "lblSelectedExp";
            lblSelectedExp.Size = new Size(150, 53);
            lblSelectedExp.TabIndex = 73;
            // 
            // dgvExpensePicker
            // 
            dgvExpensePicker.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExpensePicker.Location = new Point(39, 41);
            dgvExpensePicker.Name = "dgvExpensePicker";
            dgvExpensePicker.RowHeadersWidth = 51;
            dgvExpensePicker.Size = new Size(747, 322);
            dgvExpensePicker.TabIndex = 72;
            dgvExpensePicker.CellClick += dgvExpensePicker_CellClick;
            // 
            // txtFeeId
            // 
            txtFeeId.Location = new Point(1337, 41);
            txtFeeId.Name = "txtFeeId";
            txtFeeId.ReadOnly = true;
            txtFeeId.Size = new Size(304, 38);
            txtFeeId.TabIndex = 71;
            // 
            // dtpFeeDate
            // 
            dtpFeeDate.Location = new Point(1337, 217);
            dtpFeeDate.Name = "dtpFeeDate";
            dtpFeeDate.Size = new Size(304, 38);
            dtpFeeDate.TabIndex = 70;
            // 
            // cboExpenseRef
            // 
            cboExpenseRef.FormattingEnabled = true;
            cboExpenseRef.Location = new Point(1337, 96);
            cboExpenseRef.Name = "cboExpenseRef";
            cboExpenseRef.Size = new Size(304, 39);
            cboExpenseRef.TabIndex = 69;
            // 
            // txtFeeTotal
            // 
            txtFeeTotal.Location = new Point(1337, 157);
            txtFeeTotal.Name = "txtFeeTotal";
            txtFeeTotal.Size = new Size(304, 38);
            txtFeeTotal.TabIndex = 68;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1200, 41);
            label2.Name = "label2";
            label2.Size = new Size(121, 31);
            label2.TabIndex = 67;
            label2.Text = "รหัสรายจ่าย:";
            // 
            // button1
            // 
            button1.Location = new Point(273, 766);
            button1.Name = "button1";
            button1.Size = new Size(215, 59);
            button1.TabIndex = 52;
            button1.Text = "พิมพ์ PDF";
            button1.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Image = Properties.Resources.bin__1_;
            btnClear.Location = new Point(1427, 763);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(261, 62);
            btnClear.TabIndex = 18;
            btnClear.Text = "ล้างข้อมูล";
            btnClear.TextAlign = ContentAlignment.MiddleRight;
            btnClear.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnEditExp
            // 
            btnEditExp.Image = Properties.Resources.editอันนี้แหละ;
            btnEditExp.ImageAlign = ContentAlignment.MiddleLeft;
            btnEditExp.Location = new Point(853, 763);
            btnEditExp.Name = "btnEditExp";
            btnEditExp.Padding = new Padding(30, 0, 0, 0);
            btnEditExp.Size = new Size(288, 62);
            btnEditExp.TabIndex = 65;
            btnEditExp.Text = "แก้ไขข้อมูล";
            btnEditExp.TextAlign = ContentAlignment.MiddleRight;
            btnEditExp.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEditExp.UseVisualStyleBackColor = true;
            // 
            // btnDeleteExp
            // 
            btnDeleteExp.Image = Properties.Resources.bin__1_;
            btnDeleteExp.Location = new Point(1156, 766);
            btnDeleteExp.Name = "btnDeleteExp";
            btnDeleteExp.Size = new Size(261, 62);
            btnDeleteExp.TabIndex = 17;
            btnDeleteExp.Text = "ลบข้อมูล";
            btnDeleteExp.TextAlign = ContentAlignment.MiddleRight;
            btnDeleteExp.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDeleteExp.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(39, 766);
            button4.Name = "button4";
            button4.Size = new Size(215, 59);
            button4.TabIndex = 51;
            button4.Text = "ส่งออก Excel";
            button4.UseVisualStyleBackColor = true;
            // 
            // btnSaveExp
            // 
            btnSaveExp.Image = Properties.Resources.save;
            btnSaveExp.Location = new Point(503, 766);
            btnSaveExp.Name = "btnSaveExp";
            btnSaveExp.Size = new Size(344, 56);
            btnSaveExp.TabIndex = 15;
            btnSaveExp.Text = "บันทึกรายจ่าย";
            btnSaveExp.TextAlign = ContentAlignment.MiddleRight;
            btnSaveExp.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSaveExp.UseVisualStyleBackColor = true;
            // 
            // dgvFees
            // 
            dgvFees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFees.Location = new Point(39, 407);
            dgvFees.Name = "dgvFees";
            dgvFees.RowHeadersWidth = 51;
            dgvFees.Size = new Size(1602, 329);
            dgvFees.TabIndex = 44;
            // 
            // tabL
            // 
            tabL.Controls.Add(label5);
            tabL.Controls.Add(label4);
            tabL.Controls.Add(lblTotalRevenue);
            tabL.Controls.Add(btnSearchPay);
            tabL.Controls.Add(dtpPayEnd);
            tabL.Controls.Add(dtpPayStart);
            tabL.Controls.Add(button3);
            tabL.Controls.Add(txtSearchPay);
            tabL.Controls.Add(dgvPayments);
            tabL.Location = new Point(4, 40);
            tabL.Name = "tabL";
            tabL.Size = new Size(1708, 720);
            tabL.TabIndex = 2;
            tabL.Text = "รายรับ";
            tabL.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(314, 57);
            label5.Name = "label5";
            label5.Size = new Size(108, 31);
            label5.TabIndex = 86;
            label5.Text = "พิมพ์ค้นหา";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(798, 72);
            label4.Name = "label4";
            label4.Size = new Size(86, 31);
            label4.TabIndex = 85;
            label4.Text = "สรุปยอด";
            // 
            // lblTotalRevenue
            // 
            lblTotalRevenue.BorderStyle = BorderStyle.Fixed3D;
            lblTotalRevenue.Location = new Point(890, 71);
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Size = new Size(509, 53);
            lblTotalRevenue.TabIndex = 84;
            // 
            // btnSearchPay
            // 
            btnSearchPay.Image = Properties.Resources.save;
            btnSearchPay.Location = new Point(428, 203);
            btnSearchPay.Name = "btnSearchPay";
            btnSearchPay.Size = new Size(189, 56);
            btnSearchPay.TabIndex = 83;
            btnSearchPay.Text = "ค้นหา";
            btnSearchPay.TextAlign = ContentAlignment.MiddleRight;
            btnSearchPay.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSearchPay.UseVisualStyleBackColor = true;
            btnSearchPay.Click += btnSearchPay_Click;
            // 
            // dtpPayEnd
            // 
            dtpPayEnd.Location = new Point(428, 142);
            dtpPayEnd.Name = "dtpPayEnd";
            dtpPayEnd.Size = new Size(304, 38);
            dtpPayEnd.TabIndex = 82;
            // 
            // dtpPayStart
            // 
            dtpPayStart.Location = new Point(428, 98);
            dtpPayStart.Name = "dtpPayStart";
            dtpPayStart.Size = new Size(304, 38);
            dtpPayStart.TabIndex = 81;
            // 
            // button3
            // 
            button3.Image = Properties.Resources.save;
            button3.Location = new Point(652, 203);
            button3.Name = "button3";
            button3.Size = new Size(189, 56);
            button3.TabIndex = 80;
            button3.Text = "ปุ่มรีเฟรช";
            button3.TextAlign = ContentAlignment.MiddleRight;
            button3.TextImageRelation = TextImageRelation.ImageBeforeText;
            button3.UseVisualStyleBackColor = true;
            // 
            // txtSearchPay
            // 
            txtSearchPay.Location = new Point(428, 54);
            txtSearchPay.Name = "txtSearchPay";
            txtSearchPay.Size = new Size(239, 38);
            txtSearchPay.TabIndex = 75;
            // 
            // dgvPayments
            // 
            dgvPayments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPayments.Location = new Point(64, 305);
            dgvPayments.Name = "dgvPayments";
            dgvPayments.RowHeadersWidth = 51;
            dgvPayments.Size = new Size(1602, 329);
            dgvPayments.TabIndex = 45;
            // 
            // btnSave
            // 
            btnSave.Image = Properties.Resources.save;
            btnSave.Location = new Point(808, 845);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(344, 56);
            btnSave.TabIndex = 66;
            btnSave.Text = "บันทึกรายจ่าย";
            btnSave.TextAlign = ContentAlignment.MiddleRight;
            btnSave.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnPrintPDF
            // 
            btnPrintPDF.Location = new Point(573, 844);
            btnPrintPDF.Name = "btnPrintPDF";
            btnPrintPDF.Size = new Size(215, 59);
            btnPrintPDF.TabIndex = 38;
            btnPrintPDF.Text = "พิมพ์ PDF";
            btnPrintPDF.UseVisualStyleBackColor = true;
            btnPrintPDF.Click += btnPrintPDF_Click;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Location = new Point(343, 844);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(215, 59);
            btnExportExcel.TabIndex = 7;
            btnExportExcel.Text = "ส่งออก Excel";
            btnExportExcel.UseVisualStyleBackColor = true;
            btnExportExcel.Click += btnExportExcel_Click;
            // 
            // FormAccountFee
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1767, 958);
            Controls.Add(tabControl1);
            Controls.Add(btnSave);
            Controls.Add(btnExportExcel);
            Controls.Add(btnPrintPDF);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "FormAccountFee";
            Text = "FormAccountFee";
            Load += FormAccountFee_Load;
            Click += btnSaveExp_Click;
            tabControl1.ResumeLayout(false);
            tabFinancial.ResumeLayout(false);
            tabFinancial.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExpense).EndInit();
            tabMasterData.ResumeLayout(false);
            tabMasterData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExpensePicker).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvFees).EndInit();
            tabL.ResumeLayout(false);
            tabL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayments).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label label1;
        private TextBox txtExpAmount;
        private DateTimePicker dtpExpDate;
        private TabControl tabControl1;
        private TabPage tabFinancial;
        private Button btnPrintPDF;
        private Button btnExportExcel;
        private DataGridView dgvExpense;
        private Button btnSave;
        private TextBox txtExpId;
        private TabPage tabMasterData;
        private DateTimePicker dtpFeeDate;
        private ComboBox cboExpenseRef;
        private TextBox txtFeeTotal;
        private Label label2;
        private Button button1;
        private Button btnClear;
        private Button btnEditExp;
        private Button btnDeleteExp;
        private Button button4;
        private Button btnSaveExp;
        private DataGridView dgvFees;
        private TextBox txtFeeId;
        private Label label3;
        private Label lblSelectedExp;
        private DataGridView dgvExpensePicker;
        private TextBox txtSearchExp;
        private Button btnRefresh;
        private ComboBox cboStatusFilter;
        private DateTimePicker dtpEnd;
        private DateTimePicker dtpStart;
        private Button btnSearch;
        private TabPage tabL;
        private Button btnSearchPay;
        private DateTimePicker dtpPayEnd;
        private DateTimePicker dtpPayStart;
        private Button button3;
        private TextBox txtSearchPay;
        private DataGridView dgvPayments;
        private Label label4;
        private Label lblTotalRevenue;
        private Label label5;
    }
}