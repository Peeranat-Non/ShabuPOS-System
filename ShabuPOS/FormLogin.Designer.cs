namespace ShabuPOS
{
    partial class FormLogin
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
            buttonExit = new Button();
            buttonLogin = new Button();
            textPassword = new TextBox();
            textUsername = new TextBox();
            label2 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // buttonExit
            // 
            buttonExit.Location = new Point(239, 175);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(127, 45);
            buttonExit.TabIndex = 17;
            buttonExit.Text = "ออกจากระบบ";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += btnExit_Click;
            // 
            // buttonLogin
            // 
            buttonLogin.Location = new Point(92, 175);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(127, 45);
            buttonLogin.TabIndex = 16;
            buttonLogin.Text = "เข้าใช้งาน";
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // textPassword
            // 
            textPassword.Location = new Point(216, 114);
            textPassword.Name = "textPassword";
            textPassword.PasswordChar = '*';
            textPassword.Size = new Size(187, 38);
            textPassword.TabIndex = 15;
            textPassword.Text = "1234";
            // 
            // textUsername
            // 
            textUsername.Location = new Point(216, 58);
            textUsername.Name = "textUsername";
            textUsername.Size = new Size(187, 38);
            textUsername.TabIndex = 14;
            textUsername.Text = "admin";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(92, 110);
            label2.Name = "label2";
            label2.Size = new Size(116, 31);
            label2.TabIndex = 13;
            label2.Text = "Password:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(88, 58);
            label1.Name = "label1";
            label1.Size = new Size(122, 31);
            label1.TabIndex = 12;
            label1.Text = "Username:";
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(485, 300);
            Controls.Add(buttonExit);
            Controls.Add(buttonLogin);
            Controls.Add(textPassword);
            Controls.Add(textUsername);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormLogin";
            Load += FormLogin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonExit;
        private Button buttonLogin;
        private TextBox textPassword;
        private TextBox textUsername;
        private Label label2;
        private Label label1;
    }
}