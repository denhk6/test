using Microsoft.Reporting.WinForms;
using QuanLyCuaHangBanQuanAo.BLL;
using System;
using System.Windows.Forms;

namespace QuanLyCuaHangBanQuanAo.GUI
{
    public partial class FrmBaoCaoDoanhThu : Form
    {
        private BaoCaoBLL bll = new BaoCaoBLL();

        public FrmBaoCaoDoanhThu()
        {
            InitializeComponent();
        }

        private void FrmBaoCaoDoanhThu_Load(object sender, EventArgs e)
        {
            int currentYear = DateTime.Now.Year;
            cboYear.Items.Clear();
            for (int i = currentYear - 5; i <= currentYear; i++)
                cboYear.Items.Add(i);
            cboYear.SelectedItem = currentYear;

            LoadReport(currentYear);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (cboYear.SelectedItem != null)
                LoadReport((int)cboYear.SelectedItem);
        }

        private void LoadReport(int year)
        {
            var data = bll.GetDoanhThu(year);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "Reports\\rptDoanhThu.rdlc";
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DoanhThuDS", data));
            reportViewer1.RefreshReport();
        }
    
    }
}
