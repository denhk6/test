using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangBanQuanAo.GUI
{
    public partial class frmDoiMatKhau : Form
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();

            txtCurrent.TextChanged += ValidateForm;
            txtNew.TextChanged += ValidateForm;
            txtConfirm.TextChanged += ValidateForm;
        }

        private void ValidateForm(object sender, EventArgs e)
        {
            // Kiểm tra tất cả các trường đã được điền
            bool isValid = !string.IsNullOrWhiteSpace(txtCurrent.Text) &&
                          !string.IsNullOrWhiteSpace(txtNew.Text) &&
                          !string.IsNullOrWhiteSpace(txtConfirm.Text);

            // Chỉ bật nút khi tất cả trường đã điền
            if (isValid && !btnChange.Enabled)
            {
                btnChange.Enabled = true;
            }
        }

        private void btnEyeCurrent_Click(object sender, EventArgs e)
        {
            txtCurrent.UseSystemPasswordChar = !txtCurrent.UseSystemPasswordChar;
        }

        private void btnEyeNew_Click(object sender, EventArgs e)
        {
            txtNew.UseSystemPasswordChar = !txtNew.UseSystemPasswordChar;
        }

        private void btnEyeConfirm_Click(object sender, EventArgs e)
        {
            txtConfirm.UseSystemPasswordChar = !txtConfirm.UseSystemPasswordChar;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrent.Text.Trim();
            string newPassword = txtNew.Text.Trim();
            string confirmPassword = txtConfirm.Text.Trim();

            if (string.IsNullOrWhiteSpace(currentPassword))
            {
                ShowError("Vui lòng nhập mật khẩu hiện tại!");
                txtCurrent.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                ShowError("Vui lòng nhập mật khẩu mới!");
                txtNew.Focus();
                return;
            }

            if (currentPassword == newPassword)
            {
                ShowError("Mật khẩu mới không được trùng với mật khẩu hiện tại!");
                txtNew.Focus();
                txtNew.SelectAll();
                return;
            }

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                ShowError("Vui lòng xác nhận mật khẩu mới!");
                txtConfirm.Focus();
                return;
            }

            if (newPassword != confirmPassword)
            {
                ShowError("Mật khẩu xác nhận không khớp!");
                txtConfirm.Focus();
                txtConfirm.SelectAll();
                return;
            }

            ShowSuccess("Đổi mật khẩu thành công!");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
