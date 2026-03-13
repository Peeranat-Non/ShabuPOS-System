namespace ShabuPOS
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnGenQR = new Button();
            picQR = new PictureBox();
            btnCheckBill = new Button();
            cboPackage = new ComboBox();
            txtPeople = new TextBox();
            flowTablePanel = new FlowLayoutPanel();
            lblSelectedTable = new Label();
            btnSearchMember = new Button();
            txtSearchMember = new TextBox();
            lblMemberName = new Label();
            cbmPromotions = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            cboTableStatusFilter = new ComboBox();
            label6 = new Label();
            btnFilterTable = new Button();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)picQR).BeginInit();
            SuspendLayout();
            // 
            // btnGenQR
            // 
            btnGenQR.Location = new Point(1292, 396);
            btnGenQR.Margin = new Padding(5);
            btnGenQR.Name = "btnGenQR";
            btnGenQR.Size = new Size(156, 76);
            btnGenQR.TabIndex = 0;
            btnGenQR.Text = "เปิดโต๊ะ";
            btnGenQR.UseVisualStyleBackColor = true;
            btnGenQR.Click += btnGenQR_Click;
            // 
            // picQR
            // 
            picQR.Location = new Point(1292, 689);
            picQR.Margin = new Padding(5);
            picQR.Name = "picQR";
            picQR.Size = new Size(321, 284);
            picQR.SizeMode = PictureBoxSizeMode.Zoom;
            picQR.TabIndex = 2;
            picQR.TabStop = false;
            // 
            // btnCheckBill
            // 
            btnCheckBill.Location = new Point(1457, 396);
            btnCheckBill.Margin = new Padding(5);
            btnCheckBill.Name = "btnCheckBill";
            btnCheckBill.Size = new Size(156, 76);
            btnCheckBill.TabIndex = 3;
            btnCheckBill.Text = "เช็คบิล";
            btnCheckBill.UseVisualStyleBackColor = true;
            btnCheckBill.Click += btnCheckBill_Click;
            // 
            // cboPackage
            // 
            cboPackage.FormattingEnabled = true;
            cboPackage.Items.AddRange(new object[] { "Silver (299)", "", "", "Gold (399)", "", "", "Platinum (499)" });
            cboPackage.Location = new Point(1292, 269);
            cboPackage.Margin = new Padding(5);
            cboPackage.Name = "cboPackage";
            cboPackage.Size = new Size(243, 39);
            cboPackage.TabIndex = 4;
            // 
            // txtPeople
            // 
            txtPeople.Location = new Point(1292, 338);
            txtPeople.Margin = new Padding(5);
            txtPeople.Name = "txtPeople";
            txtPeople.Size = new Size(68, 38);
            txtPeople.TabIndex = 5;
            txtPeople.Text = "1";
            // 
            // flowTablePanel
            // 
            flowTablePanel.AutoScroll = true;
            flowTablePanel.BackColor = SystemColors.ControlLight;
            flowTablePanel.Location = new Point(35, 38);
            flowTablePanel.Margin = new Padding(5);
            flowTablePanel.Name = "flowTablePanel";
            flowTablePanel.Size = new Size(1032, 823);
            flowTablePanel.TabIndex = 6;
            flowTablePanel.Visible = false;
            flowTablePanel.Resize += flowTablePanel_Resize;
            // 
            // lblSelectedTable
            // 
            lblSelectedTable.AutoSize = true;
            lblSelectedTable.Font = new Font("Leelawadee UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSelectedTable.Location = new Point(1129, 211);
            lblSelectedTable.Margin = new Padding(5, 0, 5, 0);
            lblSelectedTable.Name = "lblSelectedTable";
            lblSelectedTable.Size = new Size(181, 41);
            lblSelectedTable.TabIndex = 7;
            lblSelectedTable.Text = "กรุณาเลือกโต๊ะ";
            // 
            // btnSearchMember
            // 
            btnSearchMember.Image = Properties.Resources.search_10023603;
            btnSearchMember.Location = new Point(1503, 550);
            btnSearchMember.Margin = new Padding(5, 6, 5, 6);
            btnSearchMember.Name = "btnSearchMember";
            btnSearchMember.Size = new Size(140, 53);
            btnSearchMember.TabIndex = 15;
            btnSearchMember.Text = "ค้นหา";
            btnSearchMember.TextAlign = ContentAlignment.MiddleRight;
            btnSearchMember.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSearchMember.UseVisualStyleBackColor = true;
            btnSearchMember.Click += btnSearchMember_Click;
            // 
            // txtSearchMember
            // 
            txtSearchMember.Location = new Point(1292, 556);
            txtSearchMember.Margin = new Padding(5, 6, 5, 6);
            txtSearchMember.Name = "txtSearchMember";
            txtSearchMember.Size = new Size(201, 38);
            txtSearchMember.TabIndex = 14;
            // 
            // lblMemberName
            // 
            lblMemberName.AutoSize = true;
            lblMemberName.Location = new Point(1292, 620);
            lblMemberName.Margin = new Padding(5, 0, 5, 0);
            lblMemberName.Name = "lblMemberName";
            lblMemberName.Size = new Size(102, 31);
            lblMemberName.TabIndex = 13;
            lblMemberName.Text = "ชื่อสมาชิก";
            // 
            // cbmPromotions
            // 
            cbmPromotions.DropDownStyle = ComboBoxStyle.DropDownList;
            cbmPromotions.FormattingEnabled = true;
            cbmPromotions.Location = new Point(1290, 496);
            cbmPromotions.Margin = new Padding(5, 6, 5, 6);
            cbmPromotions.Name = "cbmPromotions";
            cbmPromotions.Size = new Size(245, 39);
            cbmPromotions.TabIndex = 16;
            cbmPromotions.SelectedIndexChanged += cbmPromotions_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Leelawadee UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(1141, 335);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(104, 41);
            label1.TabIndex = 17;
            label1.Text = "จำนวน:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Leelawadee UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(1393, 335);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(68, 41);
            label2.TabIndex = 18;
            label2.Text = "ท่าน";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Leelawadee UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(1129, 264);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(116, 41);
            label3.TabIndex = 19;
            label3.Text = "แพ็คเกจ:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Leelawadee UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(1129, 491);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(119, 41);
            label4.TabIndex = 20;
            label4.Text = "โปโมชั่น:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Leelawadee UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(1087, 553);
            label5.Margin = new Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(161, 41);
            label5.TabIndex = 21;
            label5.Text = "เบอร์สมาชิก:";
            // 
            // cboTableStatusFilter
            // 
            cboTableStatusFilter.FormattingEnabled = true;
            cboTableStatusFilter.Location = new Point(1290, 52);
            cboTableStatusFilter.Name = "cboTableStatusFilter";
            cboTableStatusFilter.Size = new Size(210, 39);
            cboTableStatusFilter.TabIndex = 22;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Leelawadee UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(1129, 52);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(139, 41);
            label6.TabIndex = 23;
            label6.Text = "สถานะโต๊ะ:";
            // 
            // btnFilterTable
            // 
            btnFilterTable.Location = new Point(1530, 43);
            btnFilterTable.Name = "btnFilterTable";
            btnFilterTable.Size = new Size(140, 55);
            btnFilterTable.TabIndex = 24;
            btnFilterTable.Text = "ค้นหา";
            btnFilterTable.UseVisualStyleBackColor = true;
            btnFilterTable.Click += btnFilterTable_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Leelawadee UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(1129, 110);
            label7.Name = "label7";
            label7.Size = new Size(155, 41);
            label7.TabIndex = 25;
            label7.Text = "แสดงเฉพาะ:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1767, 958);
            Controls.Add(label7);
            Controls.Add(btnFilterTable);
            Controls.Add(label6);
            Controls.Add(cboTableStatusFilter);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cbmPromotions);
            Controls.Add(btnSearchMember);
            Controls.Add(txtSearchMember);
            Controls.Add(lblMemberName);
            Controls.Add(lblSelectedTable);
            Controls.Add(flowTablePanel);
            Controls.Add(txtPeople);
            Controls.Add(cboPackage);
            Controls.Add(btnCheckBill);
            Controls.Add(picQR);
            Controls.Add(btnGenQR);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)picQR).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnGenQR;
        private PictureBox picQR;
        private Button btnCheckBill;
        private ComboBox cboPackage;
        private TextBox txtPeople;
        private FlowLayoutPanel flowTablePanel;
        private Label lblSelectedTable;
        private Button btnSearchMember;
        private TextBox txtSearchMember;
        private Label lblMemberName;
        private ComboBox cbmPromotions;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox cboTableStatusFilter;
        private Label label6;
        private Button btnFilterTable;
        private Label label7;
    }
}
