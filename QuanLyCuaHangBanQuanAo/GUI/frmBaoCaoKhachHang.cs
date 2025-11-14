using Microsoft.Reporting.WinForms;
using QuanLyCuaHangBanQuanAo.BLL;
using System;
using System.Windows.Forms;

namespace QuanLyCuaHangBanQuanAo.GUI
{
    public partial class FrmBaoCaoKhachHang : Form
    {
        private BaoCaoBLL bll = new BaoCaoBLL();

        public FrmBaoCaoKhachHang()
        {
            InitializeComponent();
        }

        private void FrmBaoCaoKhachHang_Load(object sender, EventArgs e)
        {
            int year = DateTime.Now.Year;
            var data = bll.GetTopKhachHang(year);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "Reports\\rptTopKhachHang.rdlc";
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TopKhachHangDS", data));
            reportViewer1.RefreshReport();
        }
    }
}
