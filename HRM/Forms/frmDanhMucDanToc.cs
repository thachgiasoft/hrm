﻿using System;
using System.Data;
using System.Windows.Forms;

namespace HRM.Forms
{
    public partial class frmDanhMucDanToc : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhMucDanToc()
        {
            InitializeComponent();
        }

        private void frmDanhMucDanToc_Load(object sender, EventArgs e)
        {
            GetAllList_ETHNIC();
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
        }
        public void GetAllList_ETHNIC()
        {
            Class.DanhMuc_DanToc dm = new Class.DanhMuc_DanToc();
            gridItem.DataSource = dm.GetAllList_ETHNIC();
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDanhMuc_Update frm = new frmDanhMuc_Update(true, "Thêm Dân tộc", "DT", null,"frmDanhMucDanToc");
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!btnEdit.Enabled)
                return;
            int SelectedRow = gridItemDetail.FocusedRowHandle;
            if (SelectedRow >= 0)
            {
                DataRow drow = gridItemDetail.GetDataRow(SelectedRow);
                string _value = drow["EthnicCode"].ToString();

                frmDanhMuc_Update frm = new frmDanhMuc_Update(false, "Sửa Dân tộc", "DT", _value,"frmDanhMucDanToc");
                frm.Owner = this;
                frm.ShowDialog();
            }
        }

        private void gridItemDetail_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_ItemClick(null, null);
        }

        private void btnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int SelectedRow = gridItemDetail.FocusedRowHandle;
            if (SelectedRow >= 0)
            {
                DataRow drow = gridItemDetail.GetDataRow(SelectedRow);
                string _value = drow["EthnicCode"].ToString();
                if (Class.App.ConfirmDeletion() == DialogResult.No)
                    return;

                Class.DanhMuc_DanToc dmbc = new Class.DanhMuc_DanToc();
                dmbc.EthnicCode = _value;
                if (dmbc.Delete())
                {
                    Class.App.DeleteSuccessfully();
                    GetAllList_ETHNIC();
                }
                else
                {
                    Class.App.DeleteNotSuccessfully();
                }
            }
        }
    }
}