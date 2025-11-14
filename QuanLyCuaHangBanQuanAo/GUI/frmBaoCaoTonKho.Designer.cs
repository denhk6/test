using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace QuanLyCuaHangBanQuanAo.GUI
{
    partial class FrmBaoCaoHangTon
    {
        private ReportViewer reportViewer1;

        private void InitializeComponent()
        {
            this.reportViewer1 = new ReportViewer();
            this.SuspendLayout();

            // reportViewer1
            this.reportViewer1.Dock = DockStyle.Fill;
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(784, 461);

            // FrmBaoCaoHangTon
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.reportViewer1);
            this.Text = "Báo cáo Hàng Tồn";
            this.Load += new EventHandler(this.FrmBaoCaoHangTon_Load);
            this.ResumeLayout(false);
        }
    }

}