﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using System.IO;
using System.Data;

namespace HRM.Reports
{
    public partial class reportHopDongThoiVu : DevExpress.XtraReports.UI.XtraReport
    {
        public reportHopDongThoiVu()
        {
            InitializeComponent();
        }

        private void reportHopDongThoiVu_PrintProgress(object sender, DevExpress.XtraPrinting.PrintProgressEventArgs e)
        {
            try
            {
                string txtfile = Application.StartupPath + "\\temp.png";
                if (File.Exists(txtfile))
                {
                    File.Delete(txtfile);
                }
                
                this.ExportOptions.Image.Resolution = 200;
                this.ExportToImage(txtfile);

                if (File.Exists(txtfile))
                {
                    // luu vao csdl HD da in
                    Class.NhanVien_HopDong_In cls = new Class.NhanVien_HopDong_In();
                    DataTable dt = (DataTable)this.DataSource;
                    cls.ContractCode = dt.Rows[0]["ContractCode"].ToString();
                    cls.ContractCode = cls.ContractCode.Replace("Đ", "D").Replace("ầ", "a").Replace("ụ","u");
                    cls.EmployeeCode = dt.Rows[0]["EmployeeCode"].ToString();
                    cls.DateTime = DateTime.Now;
                    cls.Images= System.IO.File.ReadAllBytes(txtfile);
                    cls.UserPrint = Class.App.client_User;
                    cls.Insert();

                    File.Delete(txtfile);
                }
            }
            catch { }
        }

       
               
    }
}
