namespace QuanLyCuaHangBanQuanAo.GUI
{
    partial class frmGioiThieu
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
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblTenPhanMem = new System.Windows.Forms.Label();
            this.lblPhienBan = new System.Windows.Forms.Label();
            this.lblTacGia = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // picLogo
            // 
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.picLogo.Image = global::QuanLyCuaHangBanQuanAo.Properties.Resources.logo;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(582, 80);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // lblTenPhanMem
            // 
            this.lblTenPhanMem.AutoSize = true;
            this.lblTenPhanMem.Font = new System.Drawing.Font("Arial", 14F);
            this.lblTenPhanMem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.lblTenPhanMem.Location = new System.Drawing.Point(81, 83);
            this.lblTenPhanMem.Name = "lblTenPhanMem";
            this.lblTenPhanMem.Size = new System.Drawing.Size(420, 27);
            this.lblTenPhanMem.TabIndex = 1;
            this.lblTenPhanMem.Text = "PHẦN MỀM QUẢN LÝ BÁN QUẦN ÁO";
            this.lblTenPhanMem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPhienBan
            // 
            this.lblPhienBan.AutoSize = true;
            this.lblPhienBan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhienBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblPhienBan.Location = new System.Drawing.Point(40, 155);
            this.lblPhienBan.Name = "lblPhienBan";
            this.lblPhienBan.Size = new System.Drawing.Size(113, 17);
            this.lblPhienBan.TabIndex = 2;
            this.lblPhienBan.Text = "Phiên bản: 1.0.0";
            // 
            // lblTacGia
            // 
            this.lblTacGia.AutoSize = true;
            this.lblTacGia.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTacGia.Location = new System.Drawing.Point(40, 175);
            this.lblTacGia.Name = "lblTacGia";
            this.lblTacGia.Size = new System.Drawing.Size(155, 17);
            this.lblTacGia.TabIndex = 3;
            this.lblTacGia.Text = "Tác giả: Nhóm Xyanua";
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnDong.FlatAppearance.BorderSize = 0;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(241, 254);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 34);
            this.btnDong.TabIndex = 4;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(413, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Thành viên: Lương Phúc, Thuận, Hoàng, Duy Đăng, Nhật Duy ";
            // 
            // frmGioiThieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(582, 313);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.lblTacGia);
            this.Controls.Add(this.lblPhienBan);
            this.Controls.Add(this.lblTenPhanMem);
            this.Controls.Add(this.picLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGioiThieu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Giới Thiệu";
            this.Load += new System.EventHandler(this.frmGioiThieu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTenPhanMem;
        private System.Windows.Forms.Label lblPhienBan;
        private System.Windows.Forms.Label lblTacGia;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Label label1;
    }
}