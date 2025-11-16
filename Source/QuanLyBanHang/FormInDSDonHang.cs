using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public partial class FormInDSDonHang : Form
    {
        public FormInDSDonHang()
        {
            InitializeComponent();
        }

        private void FormInDSDonHang_Load(object sender, EventArgs e)
        {
            ReportDataSource reportDataSource = new ReportDataSource();
            // Must match the DataSource in the RDLC
            reportDataSource.Name = "DSDonHang";
            reportDataSource.Value = GetDSDonHang();
            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = "Reports/RptDSDonHang.rdlc";
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;
            this.reportViewer1.RefreshReport();
        }

        private DataTable GetDSDonHang()
        {
            var conn = Connection.connect;
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            string sql = "SELECT MaHD, NgayBan, TongTien, KhachHang.HoTen AS TenKH, NhanVien.HoTen AS TenNV FROM HoaDon INNER JOIN KhachHang ON HoaDon.MaKH = KhachHang.MaKH\r\nINNER JOIN NhanVien ON HoaDon.MaNV = NhanVien.MaNV";
            var da = new System.Data.SqlClient.SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
