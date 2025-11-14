using Microsoft.Reporting.WinForms;
using QuanLyCuaHangBanQuanAo.BLL;
using System;
using System.Windows.Forms;

namespace QuanLyCuaHangBanQuanAo.GUI
{
    public partial class FrmBaoCaoHangTon : Form
    {
        private BaoCaoBLL bll = new BaoCaoBLL();

        public FrmBaoCaoHangTon()
        {
            InitializeComponent();
        }

        private void FrmBaoCaoHangTon_Load(object sender, EventArgs e)
        {
            var data = bll.GetTonKho();

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "Reports\\rptTonKho.rdlc";
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TonKhoDS", data));
            reportViewer1.RefreshReport();
        }
    }
}
