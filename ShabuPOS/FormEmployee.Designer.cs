namespace ShabuPOS
{
    partial class FormEmployee
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
            labelMode = new Label();
            buttonDelete = new Button();
            buttonEdit = new Button();
            buttonAdd = new Button();
            textEmployPosition = new TextBox();
            textEmployName = new TextBox();
            textEmployId = new TextBox();
            comboShop = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            dgvEmployees = new DataGridView();
            dtpEmploySdate = new DateTimePicker();
            label5 = new Label();
            cbRoleFilter = new ComboBox();
            txtSearchName = new TextBox();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            txtSearchId = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvEmployees).BeginInit();
            SuspendLayout();
            // 
            // labelMode
            // 
            labelMode.BackColor = Color.Green;
            labelMode.BorderStyle = BorderStyle.Fixed3D;
            labelMode.ForeColor = Color.White;
            labelMode.Location = new Point(177, 566);
            labelMode.Name = "labelMode";
            labelMode.Size = new Size(231, 56);
            labelMode.TabIndex = 37;
            // 
            // buttonDelete
            // 
            buttonDelete.Image = Properties.Resources.bin__1_;
            buttonDelete.Location = new Point(902, 856);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(225, 56);
            buttonDelete.TabIndex = 36;
            buttonDelete.Text = "ลบข้อมูล";
            buttonDelete.TextAlign = ContentAlignment.MiddleRight;
            buttonDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Image = Properties.Resources.editอันนี้แหละ;
            buttonEdit.Location = new Point(671, 856);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(225, 56);
            buttonEdit.TabIndex = 35;
            buttonEdit.Text = "แก้ไขข้อมูล";
            buttonEdit.TextAlign = ContentAlignment.MiddleRight;
            buttonEdit.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.Image = Properties.Resources.plus__1_;
            buttonAdd.Location = new Point(440, 856);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(225, 56);
            buttonAdd.TabIndex = 34;
            buttonAdd.Text = "เพิ่มพนักงานใหม่";
            buttonAdd.TextAlign = ContentAlignment.MiddleRight;
            buttonAdd.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // textEmployPosition
            // 
            textEmployPosition.Location = new Point(671, 713);
            textEmployPosition.Name = "textEmployPosition";
            textEmployPosition.Size = new Size(239, 38);
            textEmployPosition.TabIndex = 33;
            // 
            // textEmployName
            // 
            textEmployName.Location = new Point(671, 662);
            textEmployName.Name = "textEmployName";
            textEmployName.Size = new Size(238, 38);
            textEmployName.TabIndex = 32;
            // 
            // textEmployId
            // 
            textEmployId.Location = new Point(671, 614);
            textEmployId.Name = "textEmployId";
            textEmployId.Size = new Size(238, 38);
            textEmployId.TabIndex = 31;
            // 
            // comboShop
            // 
            comboShop.FormattingEnabled = true;
            comboShop.Location = new Point(671, 566);
            comboShop.Name = "comboShop";
            comboShop.Size = new Size(238, 39);
            comboShop.TabIndex = 30;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(557, 713);
            label4.Name = "label4";
            label4.Size = new Size(90, 31);
            label4.TabIndex = 29;
            label4.Text = "ตำแหน่ง:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(525, 662);
            label3.Name = "label3";
            label3.Size = new Size(120, 31);
            label3.TabIndex = 28;
            label3.Text = "ชื่อพนักงาน:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(517, 617);
            label2.Name = "label2";
            label2.Size = new Size(130, 31);
            label2.TabIndex = 27;
            label2.Text = "รหัสพนักงาน:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(551, 569);
            label1.Name = "label1";
            label1.Size = new Size(96, 31);
            label1.TabIndex = 26;
            label1.Text = "หมวดหมู่:";
            // 
            // dgvEmployees
            // 
            dgvEmployees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEmployees.Location = new Point(165, 182);
            dgvEmployees.Margin = new Padding(5);
            dgvEmployees.Name = "dgvEmployees";
            dgvEmployees.RowHeadersWidth = 51;
            dgvEmployees.Size = new Size(1183, 344);
            dgvEmployees.TabIndex = 25;
            dgvEmployees.CellContentClick += dgvEmployees_CellClick;
            // 
            // dtpEmploySdate
            // 
            dtpEmploySdate.Location = new Point(671, 774);
            dtpEmploySdate.Name = "dtpEmploySdate";
            dtpEmploySdate.Size = new Size(250, 38);
            dtpEmploySdate.TabIndex = 38;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(588, 774);
            label5.Name = "label5";
            label5.Size = new Size(57, 31);
            label5.TabIndex = 39;
            label5.Text = "วันที่:";
            // 
            // cbRoleFilter
            // 
            cbRoleFilter.FormattingEnabled = true;
            cbRoleFilter.Location = new Point(272, 109);
            cbRoleFilter.Name = "cbRoleFilter";
            cbRoleFilter.Size = new Size(236, 39);
            cbRoleFilter.TabIndex = 40;
            cbRoleFilter.SelectedIndexChanged += cbRoleFilter_SelectedIndexChanged;
            // 
            // txtSearchName
            // 
            txtSearchName.Location = new Point(1058, 109);
            txtSearchName.Name = "txtSearchName";
            txtSearchName.Size = new Size(238, 38);
            txtSearchName.TabIndex = 41;
            txtSearchName.TextChanged += txtSearchName_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(166, 107);
            label6.Name = "label6";
            label6.Size = new Size(90, 31);
            label6.TabIndex = 42;
            label6.Text = "ตำแหน่ง:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(922, 109);
            label7.Name = "label7";
            label7.Size = new Size(120, 31);
            label7.TabIndex = 43;
            label7.Text = "ชื่อพนักงาน:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(536, 110);
            label8.Name = "label8";
            label8.Size = new Size(130, 31);
            label8.TabIndex = 45;
            label8.Text = "รหัสพนักงาน:";
            // 
            // txtSearchId
            // 
            txtSearchId.Location = new Point(672, 110);
            txtSearchId.Name = "txtSearchId";
            txtSearchId.Size = new Size(238, 38);
            txtSearchId.TabIndex = 44;
            txtSearchId.TextChanged += txtSearchId_TextChanged;
            // 
            // FormEmployee
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1767, 958);
            Controls.Add(label8);
            Controls.Add(txtSearchId);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(txtSearchName);
            Controls.Add(cbRoleFilter);
            Controls.Add(label5);
            Controls.Add(dtpEmploySdate);
            Controls.Add(labelMode);
            Controls.Add(buttonDelete);
            Controls.Add(buttonEdit);
            Controls.Add(buttonAdd);
            Controls.Add(textEmployPosition);
            Controls.Add(textEmployName);
            Controls.Add(textEmployId);
            Controls.Add(comboShop);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvEmployees);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "FormEmployee";
            StartPosition = FormStartPosition.CenterScreen;
            Load += FormEmployee_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEmployees).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelMode;
        private Button buttonDelete;
        private Button buttonEdit;
        private Button buttonAdd;
        private TextBox textEmployPosition;
        private TextBox textEmployName;
        private TextBox textEmployId;
        private ComboBox comboShop;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private DataGridView dgvEmployees;
        private DateTimePicker dtpEmploySdate;
        private Label label5;
        private ComboBox cbRoleFilter;
        private TextBox txtSearchName;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox txtSearchId;
    }
}