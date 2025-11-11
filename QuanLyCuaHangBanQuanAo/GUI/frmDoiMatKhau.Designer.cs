namespace QuanLyCuaHangBanQuanAo.GUI
{
    partial class frmDoiMatKhau
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
            this.panelCard = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.txtCurrent = new System.Windows.Forms.TextBox();
            this.lblNew = new System.Windows.Forms.Label();
            this.txtNew = new System.Windows.Forms.TextBox();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.btnEyeNew = new System.Windows.Forms.Button();
            this.btnEyeCurrent = new System.Windows.Forms.Button();
            this.btnEyeConfirm = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.panelCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCard
            // 
            this.panelCard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelCard.BackColor = System.Drawing.Color.White;
            this.panelCard.Controls.Add(this.btnChange);
            this.panelCard.Controls.Add(this.btnEyeConfirm);
            this.panelCard.Controls.Add(this.btnEyeCurrent);
            this.panelCard.Controls.Add(this.btnEyeNew);
            this.panelCard.Controls.Add(this.txtConfirm);
            this.panelCard.Controls.Add(this.lblConfirm);
            this.panelCard.Controls.Add(this.txtNew);
            this.panelCard.Controls.Add(this.lblNew);
            this.panelCard.Controls.Add(this.txtCurrent);
            this.panelCard.Controls.Add(this.lblCurrent);
            this.panelCard.Controls.Add(this.lblTitle);
            this.panelCard.Location = new System.Drawing.Point(50, 30);
            this.panelCard.Name = "panelCard";
            this.panelCard.Padding = new System.Windows.Forms.Padding(20);
            this.panelCard.Size = new System.Drawing.Size(420, 300);
            this.panelCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(36)))));
            this.lblTitle.Location = new System.Drawing.Point(10, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(234, 33);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🔒 Đổi mật khẩu";
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(96)))), ((int)(((byte)(120)))));
            this.lblCurrent.Location = new System.Drawing.Point(10, 52);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(116, 17);
            this.lblCurrent.TabIndex = 0;
            this.lblCurrent.Text = "Mật khẩu hiện tại";
            // 
            // txtCurrent
            // 
            this.txtCurrent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrent.Location = new System.Drawing.Point(160, 49);
            this.txtCurrent.Name = "txtCurrent";
            this.txtCurrent.Size = new System.Drawing.Size(220, 22);
            this.txtCurrent.TabIndex = 1;
            this.txtCurrent.UseSystemPasswordChar = true;
            // 
            // lblNew
            // 
            this.lblNew.AutoSize = true;
            this.lblNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(96)))), ((int)(((byte)(120)))));
            this.lblNew.Location = new System.Drawing.Point(10, 90);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(88, 16);
            this.lblNew.TabIndex = 0;
            this.lblNew.Text = "Mật khẩu mới";
            // 
            // txtNew
            // 
            this.txtNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNew.Location = new System.Drawing.Point(160, 87);
            this.txtNew.Name = "txtNew";
            this.txtNew.Size = new System.Drawing.Size(220, 22);
            this.txtNew.TabIndex = 2;
            this.txtNew.UseSystemPasswordChar = true;
            // 
            // lblConfirm
            // 
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(96)))), ((int)(((byte)(120)))));
            this.lblConfirm.Location = new System.Drawing.Point(10, 128);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(118, 16);
            this.lblConfirm.TabIndex = 0;
            this.lblConfirm.Text = "Xác nhận mật khẩu";
            // 
            // txtConfirm
            // 
            this.txtConfirm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirm.Location = new System.Drawing.Point(160, 125);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(220, 22);
            this.txtConfirm.TabIndex = 3;
            this.txtConfirm.UseSystemPasswordChar = true;
            // 
            // btnEyeNew
            // 
            this.btnEyeNew.BackColor = System.Drawing.Color.Transparent;
            this.btnEyeNew.FlatAppearance.BorderSize = 0;
            this.btnEyeNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEyeNew.Location = new System.Drawing.Point(363, 84);
            this.btnEyeNew.Name = "btnEyeNew";
            this.btnEyeNew.Size = new System.Drawing.Size(37, 27);
            this.btnEyeNew.TabIndex = 0;
            this.btnEyeNew.TabStop = false;
            this.btnEyeNew.Text = "👁";
            this.btnEyeNew.UseVisualStyleBackColor = false;
            this.btnEyeNew.Click += new System.EventHandler(this.btnEyeNew_Click);
            // 
            // btnEyeCurrent
            // 
            this.btnEyeCurrent.BackColor = System.Drawing.Color.Transparent;
            this.btnEyeCurrent.FlatAppearance.BorderSize = 0;
            this.btnEyeCurrent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEyeCurrent.Location = new System.Drawing.Point(363, 46);
            this.btnEyeCurrent.Name = "btnEyeCurrent";
            this.btnEyeCurrent.Size = new System.Drawing.Size(37, 27);
            this.btnEyeCurrent.TabIndex = 0;
            this.btnEyeCurrent.TabStop = false;
            this.btnEyeCurrent.Text = "👁";
            this.btnEyeCurrent.UseVisualStyleBackColor = false;
            this.btnEyeCurrent.Click += new System.EventHandler(this.btnEyeCurrent_Click);
            // 
            // btnEyeConfirm
            // 
            this.btnEyeConfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnEyeConfirm.FlatAppearance.BorderSize = 0;
            this.btnEyeConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEyeConfirm.Location = new System.Drawing.Point(363, 123);
            this.btnEyeConfirm.Name = "btnEyeConfirm";
            this.btnEyeConfirm.Size = new System.Drawing.Size(37, 27);
            this.btnEyeConfirm.TabIndex = 0;
            this.btnEyeConfirm.TabStop = false;
            this.btnEyeConfirm.Text = "👁";
            this.btnEyeConfirm.UseVisualStyleBackColor = false;
            this.btnEyeConfirm.Click += new System.EventHandler(this.btnEyeConfirm_Click);
            // 
            // btnChange
            // 
            this.btnChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.btnChange.Enabled = false;
            this.btnChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChange.ForeColor = System.Drawing.Color.White;
            this.btnChange.Location = new System.Drawing.Point(260, 240);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(140, 36);
            this.btnChange.TabIndex = 4;
            this.btnChange.Text = "Đổi mật khẩu";
            this.btnChange.UseVisualStyleBackColor = false;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // frmDoiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(502, 313);
            this.Controls.Add(this.panelCard);
            this.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDoiMatKhau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đổi Mật Khẩu";
            this.panelCard.ResumeLayout(false);
            this.panelCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtCurrent;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Label lblConfirm;
        private System.Windows.Forms.TextBox txtNew;
        private System.Windows.Forms.Label lblNew;
        private System.Windows.Forms.Button btnEyeNew;
        private System.Windows.Forms.Button btnEyeConfirm;
        private System.Windows.Forms.Button btnEyeCurrent;
        private System.Windows.Forms.Button btnChange;
    }
}