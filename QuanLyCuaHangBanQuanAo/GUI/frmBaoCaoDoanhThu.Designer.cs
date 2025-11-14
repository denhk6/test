using System;
using System.Windows.Forms;
namespace QuanLyCuaHangBanQuanAo.GUI
{
    partial class FrmBaoCaoDoanhThu
    {
        private ComboBox cboYear;
        private Button btnLoad;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;

        private void InitializeComponent()
        {
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // cboYear
            // 
            this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYear.Location = new System.Drawing.Point(12, 12);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(100, 28);
            this.cboYear.TabIndex = 0;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(120, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(80, 28);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(12, 138);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(760, 400);
            this.reportViewer1.TabIndex = 2;
            // 
            // FrmBaoCaoDoanhThu
            // 
            this.ClientSize = new System.Drawing.Size(1059, 601);
            this.Controls.Add(this.cboYear);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmBaoCaoDoanhThu";
            this.Text = "Báo cáo Doanh Thu";
            this.Load += new System.EventHandler(this.FrmBaoCaoDoanhThu_Load);
            this.ResumeLayout(false);

        }
    }

}