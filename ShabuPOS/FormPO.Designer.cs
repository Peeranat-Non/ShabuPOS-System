namespace ShabuPOS
{
    partial class FormPO
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
            btnNewPO = new Button();
            dgvPODetails = new DataGridView();
            cboStatus = new ComboBox();
            label4 = new Label();
            dtpPO_Date = new DateTimePicker();
            label3 = new Label();
            txtEmploy_ID = new TextBox();
            label2 = new Label();
            txtPO_ID = new TextBox();
            label1 = new Label();
            label5 = new Label();
            btnSavePO = new Button();
            lblTotalAmount = new Label();
            label6 = new Label();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            txtSearchPR = new TextBox();
            dgvPRList = new DataGridView();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            dtpPOEnd = new DateTimePicker();
            dtpPOStart = new DateTimePicker();
            cbPOStatusFilter = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvPODetails).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPRList).BeginInit();
            SuspendLayout();
            // 
            // btnNewPO
            // 
            btnNewPO.Location = new Point(1407, 267);
            btnNewPO.Name = "btnNewPO";
            btnNewPO.Size = new Size(148, 48);
            btnNewPO.TabIndex = 29;
            btnNewPO.Text = "ทำรายการใหม่";
            btnNewPO.UseVisualStyleBackColor = true;
            btnNewPO.Click += btnNewPO_Click;
            // 
            // dgvPODetails
            // 
            dgvPODetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPODetails.Location = new Point(36, 379);
            dgvPODetails.Name = "dgvPODetails";
            dgvPODetails.RowHeadersWidth = 51;
            dgvPODetails.Size = new Size(967, 519);
            dgvPODetails.TabIndex = 25;
            // 
            // cboStatus
            // 
            cboStatus.FormattingEnabled = true;
            cboStatus.Location = new Point(1317, 555);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(163, 39);
            cboStatus.TabIndex = 24;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1192, 552);
            label4.Name = "label4";
            label4.Size = new Size(76, 31);
            label4.TabIndex = 23;
            label4.Text = "สถานะ:";
            // 
            // dtpPO_Date
            // 
            dtpPO_Date.Location = new Point(1317, 507);
            dtpPO_Date.Name = "dtpPO_Date";
            dtpPO_Date.Size = new Size(250, 38);
            dtpPO_Date.TabIndex = 22;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1159, 504);
            label3.Name = "label3";
            label3.Size = new Size(104, 31);
            label3.TabIndex = 21;
            label3.Text = "วันที่สั่งซื้อ:";
            // 
            // txtEmploy_ID
            // 
            txtEmploy_ID.Location = new Point(1317, 463);
            txtEmploy_ID.Name = "txtEmploy_ID";
            txtEmploy_ID.Size = new Size(163, 38);
            txtEmploy_ID.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1082, 460);
            label2.Name = "label2";
            label2.Size = new Size(181, 31);
            label2.TabIndex = 19;
            label2.Text = "รหัสพนักงานจัดซื้อ:";
            // 
            // txtPO_ID
            // 
            txtPO_ID.Location = new Point(1317, 419);
            txtPO_ID.Name = "txtPO_ID";
            txtPO_ID.Size = new Size(163, 38);
            txtPO_ID.TabIndex = 18;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1143, 416);
            label1.Name = "label1";
            label1.Size = new Size(120, 31);
            label1.TabIndex = 17;
            label1.Text = "รหัสใบสั่งซื้อ:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1187, 97);
            label5.Name = "label5";
            label5.Size = new Size(125, 31);
            label5.TabIndex = 33;
            label5.Text = "รหัสใบขอซื้อ:";
            // 
            // btnSavePO
            // 
            btnSavePO.Image = Properties.Resources.plus__1_;
            btnSavePO.Location = new Point(1223, 785);
            btnSavePO.Name = "btnSavePO";
            btnSavePO.Size = new Size(344, 69);
            btnSavePO.TabIndex = 26;
            btnSavePO.Text = "บันทึกใบสั่งซื้อ";
            btnSavePO.TextAlign = ContentAlignment.MiddleRight;
            btnSavePO.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSavePO.UseVisualStyleBackColor = true;
            btnSavePO.Click += btnSavePO_Click;
            // 
            // lblTotalAmount
            // 
            lblTotalAmount.BackColor = SystemColors.MenuHighlight;
            lblTotalAmount.BorderStyle = BorderStyle.Fixed3D;
            lblTotalAmount.ForeColor = Color.White;
            lblTotalAmount.Location = new Point(1317, 706);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(192, 42);
            lblTotalAmount.TabIndex = 35;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1142, 706);
            label6.Name = "label6";
            label6.Size = new Size(127, 31);
            label6.TabIndex = 34;
            label6.Text = "ยอดรวมสุทธิ:";
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // txtSearchPR
            // 
            txtSearchPR.Location = new Point(1318, 94);
            txtSearchPR.Name = "txtSearchPR";
            txtSearchPR.Size = new Size(203, 38);
            txtSearchPR.TabIndex = 37;
            txtSearchPR.TextChanged += txtSearchPR_TextChanged;
            // 
            // dgvPRList
            // 
            dgvPRList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPRList.Location = new Point(36, 33);
            dgvPRList.Name = "dgvPRList";
            dgvPRList.RowHeadersWidth = 51;
            dgvPRList.Size = new Size(967, 322);
            dgvPRList.TabIndex = 38;
            dgvPRList.CellClick += dgvPRList_CellClick;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(1203, 196);
            label10.Name = "label10";
            label10.Size = new Size(108, 31);
            label10.TabIndex = 46;
            label10.Text = "วันที่สิ้นสุด:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(1198, 151);
            label9.Name = "label9";
            label9.Size = new Size(113, 31);
            label9.TabIndex = 45;
            label9.Text = "วันที่เริ่มต้น:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(1161, 51);
            label8.Name = "label8";
            label8.Size = new Size(150, 31);
            label8.TabIndex = 44;
            label8.Text = "สถานะใบขอซื้อ:";
            // 
            // dtpPOEnd
            // 
            dtpPOEnd.Location = new Point(1317, 195);
            dtpPOEnd.Name = "dtpPOEnd";
            dtpPOEnd.Size = new Size(250, 38);
            dtpPOEnd.TabIndex = 42;
            dtpPOEnd.ValueChanged += dtpPOEnd_ValueChanged;
            // 
            // dtpPOStart
            // 
            dtpPOStart.Location = new Point(1317, 151);
            dtpPOStart.Name = "dtpPOStart";
            dtpPOStart.Size = new Size(250, 38);
            dtpPOStart.TabIndex = 41;
            dtpPOStart.ValueChanged += dtpPOStart_ValueChanged;
            // 
            // cbPOStatusFilter
            // 
            cbPOStatusFilter.FormattingEnabled = true;
            cbPOStatusFilter.Location = new Point(1317, 48);
            cbPOStatusFilter.Name = "cbPOStatusFilter";
            cbPOStatusFilter.Size = new Size(204, 39);
            cbPOStatusFilter.TabIndex = 39;
            cbPOStatusFilter.SelectedIndexChanged += cbPOStatusFilter_SelectedIndexChanged;
            // 
            // FormPO
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1767, 958);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(dtpPOEnd);
            Controls.Add(dtpPOStart);
            Controls.Add(cbPOStatusFilter);
            Controls.Add(dgvPRList);
            Controls.Add(txtSearchPR);
            Controls.Add(lblTotalAmount);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(btnNewPO);
            Controls.Add(btnSavePO);
            Controls.Add(dgvPODetails);
            Controls.Add(cboStatus);
            Controls.Add(label4);
            Controls.Add(dtpPO_Date);
            Controls.Add(label3);
            Controls.Add(txtEmploy_ID);
            Controls.Add(label2);
            Controls.Add(txtPO_ID);
            Controls.Add(label1);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = SystemColors.ControlText;
            Margin = new Padding(5);
            Name = "FormPO";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormPO";
            Load += FormPO_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPODetails).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPRList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnNewPO;
        private DataGridView dgvPODetails;
        private ComboBox cboStatus;
        private Label label4;
        private DateTimePicker dtpPO_Date;
        private Label label3;
        private TextBox txtEmploy_ID;
        private Label label2;
        private TextBox txtPO_ID;
        private Label label1;
        private Label label5;
        private Button btnSavePO;
        private Label lblTotalAmount;
        private Label label6;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private TextBox txtSearchPR;
        private DataGridView dgvPRList;
        private Label label10;
        private Label label9;
        private Label label8;
        private DateTimePicker dtpPOEnd;
        private DateTimePicker dtpPOStart;
        private ComboBox cbPOStatusFilter;
    }
}