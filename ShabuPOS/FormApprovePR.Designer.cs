namespace ShabuPOS
{
    partial class FormApprovePR
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
            btnApprove = new Button();
            dgvPendingPR = new DataGridView();
            txtApprover = new TextBox();
            label1 = new Label();
            btnReject = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPendingPR).BeginInit();
            SuspendLayout();
            // 
            // btnApprove
            // 
            btnApprove.Image = Properties.Resources.plus__1_;
            btnApprove.Location = new Point(433, 474);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(244, 68);
            btnApprove.TabIndex = 9;
            btnApprove.Text = "ยืนยันการอนุมัติ";
            btnApprove.TextAlign = ContentAlignment.MiddleRight;
            btnApprove.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnApprove.UseVisualStyleBackColor = true;
            btnApprove.Click += btnApprove_Click;
            // 
            // dgvPendingPR
            // 
            dgvPendingPR.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPendingPR.Location = new Point(38, 29);
            dgvPendingPR.Name = "dgvPendingPR";
            dgvPendingPR.RowHeadersWidth = 51;
            dgvPendingPR.Size = new Size(1355, 344);
            dgvPendingPR.TabIndex = 8;
            // 
            // txtApprover
            // 
            txtApprover.Location = new Point(603, 411);
            txtApprover.Name = "txtApprover";
            txtApprover.Size = new Size(178, 38);
            txtApprover.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(500, 411);
            label1.Name = "label1";
            label1.Size = new Size(85, 31);
            label1.TabIndex = 5;
            label1.Text = "ผู้อนุมัติ:";
            // 
            // btnReject
            // 
            btnReject.Image = Properties.Resources.bin__1_;
            btnReject.Location = new Point(695, 474);
            btnReject.Name = "btnReject";
            btnReject.Size = new Size(244, 68);
            btnReject.TabIndex = 10;
            btnReject.Text = "ยกเลิกใบขอซื้อ";
            btnReject.TextAlign = ContentAlignment.MiddleRight;
            btnReject.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReject.UseVisualStyleBackColor = true;
            btnReject.Click += btnReject_Click;
            // 
            // FormApprovePR
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1767, 958);
            Controls.Add(btnReject);
            Controls.Add(btnApprove);
            Controls.Add(dgvPendingPR);
            Controls.Add(txtApprover);
            Controls.Add(label1);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "FormApprovePR";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormApprovePR";
            Load += FormApprovePR_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPendingPR).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnApprove;
        private DataGridView dgvPendingPR;
        private TextBox txtApprover;
        private Label label1;
        private Button btnReject;
    }
}