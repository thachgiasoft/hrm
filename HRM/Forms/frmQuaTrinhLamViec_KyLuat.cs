﻿using System;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace HRM.Forms
{
    public partial class frmQuaTrinhLamViec_KyLuat : DevExpress.XtraEditors.XtraForm
    {
        public frmQuaTrinhLamViec_KyLuat()
        {
            InitializeComponent();
        }
        string template_grid = Application.StartupPath + @"\Templates\Templates_QTKL.xml";
        private void frmQuaTrinhLamViec_KyLuat_Load(object sender, EventArgs e)
        {
            if (File.Exists(template_grid))
            {
                // gridItemDetail.SaveLayoutToXml(template_grid);
                gridItemDetail.RestoreLayoutFromXml(template_grid);
            }
            HRM_PROCESS_DISCIPLINE_GetListByEmployee();
        
            #region Khoa Phan quyen
            // thuc hien khoa phan quyen phan quyen
            for (int i = 0; i < barManager1.Items.Count; i++)
            {
                if (barManager1.Items[i].Tag != null)
                {
                    string txt = barManager1.Items[i].Tag.ToString();
                    if (txt.Length > 0)
                    {
                        barManager1.Items[i].Enabled = false;
                    }
                }

            }
            #endregion
            //-----------
            #region mo phan quyen
            for (int i = 0; i < barManager1.Items.Count; i++)
            {
                if (barManager1.Items[i].Tag != null)
                {
                    string txt = barManager1.Items[i].Tag.ToString();
                    if (txt.Length > 0)
                    {
                        for (int l = 0; l < Class.App.dtPermision.Rows.Count; l++)
                        {
                            string _Object_ID = Class.App.dtPermision.Rows[l]["Object_ID"].ToString();
                            if (_Object_ID == txt)
                            {
                                barManager1.Items[i].Enabled = true;
                            }
                        }

                    }
                }

            }
            #endregion
            EnableControl();
        }
        public void HRM_PROCESS_DISCIPLINE_GetListByEmployee()
        {
            Class.QuaTrinhLamViec_KyLuat kl = new Class.QuaTrinhLamViec_KyLuat();
            gridItem.DataSource = kl.HRM_PROCESS_DISCIPLINE_GetListByEmployee();
            gridItemDetail.OptionsView.ColumnAutoWidth = false;
        }
        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Class.App._manv == "")
            {
                MessageBox.Show("Vui lòng chọn nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmQuaTrinhLamViec_KyLuat_Update frm = new frmQuaTrinhLamViec_KyLuat_Update(true, "Thêm Thông tin kỷ luật", "KL", null, null, "frmQuaTrinhLamViec_KyLuat");
            frm.Owner = this;
            frm.ShowDialog();
          
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int SelectedRow = gridItemDetail.FocusedRowHandle;
            if (SelectedRow >= 0)
            {
                DataRow drow = gridItemDetail.GetDataRow(SelectedRow);
                string _value = drow["DisciplineID"].ToString();
                frmQuaTrinhLamViec_KyLuat_Update frm = new frmQuaTrinhLamViec_KyLuat_Update(false, "Cập nhật Thông tin kỷ luật", "KL", _value, Class.App._hotennv, "frmQuaTrinhLamViec_KyLuat");
                frm.Owner = this;
                frm.ShowDialog();
            }
        }

        private void gridItemDetail_DoubleClick(object sender, EventArgs e)
        {
            if( btnEdit.Enabled)
             btnEdit_ItemClick(null, null);
        }

        private void btnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int SelectedRow = gridItemDetail.FocusedRowHandle;
            if (SelectedRow >= 0)
            {
                DataRow drow = gridItemDetail.GetDataRow(SelectedRow);
                string _value = drow["DisciplineID"].ToString();
                if (Class.App.ConfirmDeletion() == DialogResult.No)
                    return;

                Class.QuaTrinhLamViec_KyLuat kl = new Class.QuaTrinhLamViec_KyLuat();
                kl.DisciplineID = _value;
                if (kl.Delete())
                {
                    Class.App.DeleteSuccessfully();
                    HRM_PROCESS_DISCIPLINE_GetListByEmployee();
                }
                else
                {
                    Class.App.DeleteNotSuccessfully();
                }
            }
           
        }

        private void EnableControl()
        {
            if (gridItemDetail.RowCount < 1)
            {
                btnEdit.Enabled = false;
                btnDel.Enabled = false;
            }
            else
            {
              //  btnEdit.Enabled = true;
               // btnDel.Enabled = true;
                #region mo phan quyen
                for (int i = 0; i < barManager1.Items.Count; i++)
                {
                    if (barManager1.Items[i].Tag != null)
                    {
                        string txt = barManager1.Items[i].Tag.ToString();
                        if (txt.Length > 0)
                        {
                            for (int l = 0; l < Class.App.dtPermision.Rows.Count; l++)
                            {
                                string _Object_ID = Class.App.dtPermision.Rows[l]["Object_ID"].ToString();
                                if (_Object_ID == txt)
                                {
                                    barManager1.Items[i].Enabled = true;
                                }
                            }

                        }
                    }

                }
                #endregion
            }

        }

     

        private void gridItemDetail_RowCountChanged(object sender, EventArgs e)
        {
           EnableControl();
        }

        private void frmQuaTrinhLamViec_KyLuat_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                gridItemDetail.SaveLayoutToXml(template_grid);
            }
            catch { }
        }
    }
}