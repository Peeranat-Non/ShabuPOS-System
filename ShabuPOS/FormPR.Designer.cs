namespace ShabuPOS
{
    partial class FormPR
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
            txtPR_ID = new TextBox();
            txtEmploy_ID = new TextBox();
            label2 = new Label();
            label3 = new Label();
            dtpPR_Date = new DateTimePicker();
            label4 = new Label();
            cboStatus = new ComboBox();
            dgvPRDetails = new DataGridView();
            btnSave = new Button();
            cboProducts = new ComboBox();
            numQty = new NumericUpDown();
            btnClear = new Button();
            btnDelete = new Button();
            btnAdd = new Button();
            label5 = new Label();
            label6 = new Label();
            cbStatusFilter = new ComboBox();
            cbDeptFilter = new ComboBox();
            dtpStart = new DateTimePicker();
            dtpEnd = new DateTimePicker();
            dgvPRMaster = new DataGridView();
            btnNewPR = new Button();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            cboPRDept = new ComboBox();
            label11 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPRDetails).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numQty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPRMaster).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(880, 57);
            label1.Name = "label1";
            label1.Size = new Size(125, 31);
            label1.TabIndex = 0;
            label1.Text = "รหัสใบขอซื้อ:";
            // 
            // txtPR_ID
            // 
            txtPR_ID.Location = new Point(1028, 57);
            txtPR_ID.Name = "txtPR_ID";
            txtPR_ID.Size = new Size(163, 38);
            txtPR_ID.TabIndex = 1;
            // 
            // txtEmploy_ID
            // 
            txtEmploy_ID.Location = new Point(1028, 101);
            txtEmploy_ID.Name = "txtEmploy_ID";
            txtEmploy_ID.Size = new Size(163, 38);
            txtEmploy_ID.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(875, 101);
            label2.Name = "label2";
            label2.Size = new Size(130, 31);
            label2.TabIndex = 2;
            label2.Text = "รหัสพนักงาน:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(896, 145);
            label3.Name = "label3";
            label3.Size = new Size(109, 31);
            label3.TabIndex = 4;
            label3.Text = "วันที่ขอซื้อ:";
            // 
            // dtpPR_Date
            // 
            dtpPR_Date.Location = new Point(1028, 145);
            dtpPR_Date.Name = "dtpPR_Date";
            dtpPR_Date.Size = new Size(250, 38);
            dtpPR_Date.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(929, 193);
            label4.Name = "label4";
            label4.Size = new Size(76, 31);
            label4.TabIndex = 6;
            label4.Text = "สถานะ:";
            // 
            // cboStatus
            // 
            cboStatus.FormattingEnabled = true;
            cboStatus.Location = new Point(1028, 193);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(163, 39);
            cboStatus.TabIndex = 7;
            // 
            // dgvPRDetails
            // 
            dgvPRDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPRDetails.Location = new Point(765, 441);
            dgvPRDetails.Name = "dgvPRDetails";
            dgvPRDetails.RowHeadersWidth = 51;
            dgvPRDetails.Size = new Size(966, 296);
            dgvPRDetails.TabIndex = 8;
            // 
            // btnSave
            // 
            btnSave.Image = Properties.Resources.save;
            btnSave.Location = new Point(1009, 855);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(344, 56);
            btnSave.TabIndex = 9;
            btnSave.Text = "บันทึกใบขอซื้อ";
            btnSave.TextAlign = ContentAlignment.MiddleRight;
            btnSave.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // cboProducts
            // 
            cboProducts.FormattingEnabled = true;
            cboProducts.Location = new Point(1028, 299);
            cboProducts.Name = "cboProducts";
            cboProducts.Size = new Size(270, 39);
            cboProducts.TabIndex = 10;
            // 
            // numQty
            // 
            numQty.Location = new Point(1128, 346);
            numQty.Name = "numQty";
            numQty.Size = new Size(150, 38);
            numQty.TabIndex = 11;
            // 
            // btnClear
            // 
            btnClear.Image = Properties.Resources.bin__1_;
            btnClear.Location = new Point(1317, 759);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(261, 62);
            btnClear.TabIndex = 14;
            btnClear.Text = "ล้างข้อมูล";
            btnClear.TextAlign = ContentAlignment.MiddleRight;
            btnClear.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnDelete
            // 
            btnDelete.Image = Properties.Resources.bin__1_;
            btnDelete.Location = new Point(1050, 759);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(261, 62);
            btnDelete.TabIndex = 13;
            btnDelete.Text = "ลบข้อมูล";
            btnDelete.TextAlign = ContentAlignment.MiddleRight;
            btnDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnAdd
            // 
            btnAdd.Image = Properties.Resources.plus__1_;
            btnAdd.Location = new Point(783, 759);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(261, 62);
            btnAdd.TabIndex = 12;
            btnAdd.Text = "เพิ่มใบขอซื้อ";
            btnAdd.TextAlign = ContentAlignment.MiddleRight;
            btnAdd.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(878, 302);
            label5.Name = "label5";
            label5.Size = new Size(127, 31);
            label5.TabIndex = 15;
            label5.Text = "เลือกรายการ:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(926, 346);
            label6.Name = "label6";
            label6.Size = new Size(79, 31);
            label6.TabIndex = 16;
            label6.Text = "จำนวน:";
            // 
            // cbStatusFilter
            // 
            cbStatusFilter.FormattingEnabled = true;
            cbStatusFilter.Location = new Point(349, 193);
            cbStatusFilter.Name = "cbStatusFilter";
            cbStatusFilter.Size = new Size(204, 39);
            cbStatusFilter.TabIndex = 17;
            cbStatusFilter.SelectedIndexChanged += cbStatusFilter_SelectedIndexChanged;
            // 
            // cbDeptFilter
            // 
            cbDeptFilter.FormattingEnabled = true;
            cbDeptFilter.Location = new Point(349, 251);
            cbDeptFilter.Name = "cbDeptFilter";
            cbDeptFilter.Size = new Size(204, 39);
            cbDeptFilter.TabIndex = 18;
            cbDeptFilter.SelectedIndexChanged += cbDeptFilter_SelectedIndexChanged;
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(349, 296);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(250, 38);
            dtpStart.TabIndex = 19;
            dtpStart.ValueChanged += dtpStart_ValueChanged;
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(349, 340);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(250, 38);
            dtpEnd.TabIndex = 20;
            dtpEnd.ValueChanged += dtpEnd_ValueChanged;
            // 
            // dgvPRMaster
            // 
            dgvPRMaster.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPRMaster.Location = new Point(29, 441);
            dgvPRMaster.Name = "dgvPRMaster";
            dgvPRMaster.RowHeadersWidth = 51;
            dgvPRMaster.Size = new Size(716, 296);
            dgvPRMaster.TabIndex = 21;
            dgvPRMaster.CellClick += dgvPRMaster_CellClick;
            // 
            // btnNewPR
            // 
            btnNewPR.Image = Properties.Resources.plus__1_;
            btnNewPR.Location = new Point(1450, 261);
            btnNewPR.Name = "btnNewPR";
            btnNewPR.Size = new Size(261, 62);
            btnNewPR.TabIndex = 22;
            btnNewPR.Text = "ทำรายการใหม่";
            btnNewPR.TextAlign = ContentAlignment.MiddleRight;
            btnNewPR.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNewPR.UseVisualStyleBackColor = true;
            btnNewPR.Click += btnNewPR_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(272, 251);
            label7.Name = "label7";
            label7.Size = new Size(71, 31);
            label7.TabIndex = 23;
            label7.Text = "แผนก:";
            //label7.Click += this.label7_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(193, 196);
            label8.Name = "label8";
            label8.Size = new Size(150, 31);
            label8.TabIndex = 24;
            label8.Text = "สถานะใบขอซื้อ:";
            //label8.Click += this.label8_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(230, 296);
            label9.Name = "label9";
            label9.Size = new Size(113, 31);
            label9.TabIndex = 25;
            label9.Text = "วันที่เริ่มต้น:";
            //label9.Click += this.label9_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(235, 341);
            label10.Name = "label10";
            label10.Size = new Size(108, 31);
            label10.TabIndex = 26;
            label10.Text = "วันที่สิ้นสุด:";
            label10.Click += label10_Click;
            // 
            // cboPRDept
            // 
            cboPRDept.FormattingEnabled = true;
            cboPRDept.Location = new Point(1028, 243);
            cboPRDept.Name = "cboPRDept";
            cboPRDept.Size = new Size(163, 39);
            cboPRDept.TabIndex = 27;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(868, 240);
            label11.Name = "label11";
            label11.Size = new Size(137, 31);
            label11.TabIndex = 28;
            label11.Text = "แผนกที่ขอซื้อ:";
            // 
            // FormPR
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1767, 958);
            Controls.Add(label11);
            Controls.Add(cboPRDept);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(btnNewPR);
            Controls.Add(dgvPRMaster);
            Controls.Add(dtpEnd);
            Controls.Add(dtpStart);
            Controls.Add(cbDeptFilter);
            Controls.Add(cbStatusFilter);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(numQty);
            Controls.Add(cboProducts);
            Controls.Add(btnSave);
            Controls.Add(dgvPRDetails);
            Controls.Add(cboStatus);
            Controls.Add(label4);
            Controls.Add(dtpPR_Date);
            Controls.Add(label3);
            Controls.Add(txtEmploy_ID);
            Controls.Add(label2);
            Controls.Add(txtPR_ID);
            Controls.Add(label1);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "FormPR";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormPR";
            Load += FormPR_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPRDetails).EndInit();
            ((System.ComponentModel.ISupportInitialize)numQty).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPRMaster).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtPR_ID;
        private TextBox txtEmploy_ID;
        private Label label2;
        private Label label3;
        private DateTimePicker dtpPR_Date;
        private Label label4;
        private ComboBox cboStatus;
        private DataGridView dgvPRDetails;
        private Button btnSave;
        private ComboBox cboProducts;
        private NumericUpDown numQty;
        private Button btnClear;
        private Button btnDelete;
        private Button btnAdd;
        private Label label5;
        private Label label6;
        private ComboBox cbStatusFilter;
        private ComboBox cbDeptFilter;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private DataGridView dgvPRMaster;
        private Button btnNewPR;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private ComboBox cboPRDept;
        private Label label11;
    }
}