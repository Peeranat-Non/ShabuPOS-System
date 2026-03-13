namespace ShabuPOS
{
    partial class FormManageExpense
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
            txtAmount = new TextBox();
            dtpDate = new DateTimePicker();
            btnEdit = new Button();
            btnClear = new Button();
            btnDelete = new Button();
            btnSave = new Button();
            SuspendLayout();
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(327, 36);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(250, 38);
            txtAmount.TabIndex = 0;
            // 
            // dtpDate
            // 
            dtpDate.Location = new Point(327, 103);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(250, 38);
            dtpDate.TabIndex = 1;
            // 
            // btnEdit
            // 
            btnEdit.Image = Properties.Resources.editอันนี้แหละ;
            btnEdit.ImageAlign = ContentAlignment.MiddleLeft;
            btnEdit.Location = new Point(366, 202);
            btnEdit.Name = "btnEdit";
            btnEdit.Padding = new Padding(30, 0, 0, 0);
            btnEdit.Size = new Size(288, 62);
            btnEdit.TabIndex = 69;
            btnEdit.Text = "แก้ไขข้อมูล";
            btnEdit.TextAlign = ContentAlignment.MiddleRight;
            btnEdit.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Image = Properties.Resources.bin__1_;
            btnClear.Location = new Point(960, 205);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(261, 62);
            btnClear.TabIndex = 68;
            btnClear.Text = "ล้างข้อมูล";
            btnClear.TextAlign = ContentAlignment.MiddleRight;
            btnClear.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Image = Properties.Resources.bin__1_;
            btnDelete.Location = new Point(676, 205);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(261, 62);
            btnDelete.TabIndex = 67;
            btnDelete.Text = "ลบข้อมูล";
            btnDelete.TextAlign = ContentAlignment.MiddleRight;
            btnDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Image = Properties.Resources.save;
            btnSave.Location = new Point(4, 205);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(344, 56);
            btnSave.TabIndex = 66;
            btnSave.Text = "บันทึกรายจ่าย";
            btnSave.TextAlign = ContentAlignment.MiddleRight;
            btnSave.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSave.UseVisualStyleBackColor = true;
            // 
            // FormManageExpense
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1767, 958);
            Controls.Add(btnEdit);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Controls.Add(dtpDate);
            Controls.Add(txtAmount);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5, 5, 5, 5);
            Name = "FormManageExpense";
            Text = "FormManageExpense";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtAmount;
        private DateTimePicker dtpDate;
        private Button btnEdit;
        private Button btnClear;
        private Button btnDelete;
        private Button btnSave;
    }
}