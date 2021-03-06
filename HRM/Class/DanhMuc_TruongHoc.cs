﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HRM.Class
{
   public class DanhMuc_TruongHoc
   {
       #region KhaiBao_Bien
       private string _SchoolCode;

       public string SchoolCode
       {
           get { return _SchoolCode; }
           set { _SchoolCode = value; }
       }
       private string _SchoolName;

       public string SchoolName
       {
           get { return _SchoolName; }
           set { _SchoolName = value; }
       }
       private string _Description;

       public string Description
       {
           get { return _Description; }
           set { _Description = value; }
       }
       private bool _Active;

       public bool Active
       {
           get { return _Active; }
           set { _Active = value; }
       }
       #endregion

       public DataTable GetAllList_SCHOOL()
        {
            string procname = "DIC_SCHOOL_GetList";
            DbAccess db = new DbAccess();
            db.CreateNewSqlCommand();
            return db.ExecuteDataTable(procname);
        }

        public string GetNewCode()
        {
            string procname = "DIC_SCHOOL_GetList";
            DbAccess db = new DbAccess();
            db.CreateNewSqlCommand();
            DataTable dt = db.ExecuteDataTable(procname);
            if (dt.Rows.Count > 0)
            {
                string _strCode = dt.Rows[dt.Rows.Count - 1][0].ToString();
                _strCode = _strCode.Substring(2, _strCode.Length - 2);
                int next_id = int.Parse(_strCode) + 1;
                switch (next_id.ToString().Length)
                {
                    case 1:
                        return "TH00000" + next_id.ToString();
                    case 2:
                        return "TH0000" + next_id.ToString();
                    case 3:
                        return "TH000" + next_id.ToString();
                    case 4:
                        return "TH00" + next_id.ToString();
                    case 5:
                        return "TH0" + next_id.ToString();
                    case 6:
                        return "TH" + next_id.ToString();
                }
            }
            return "TH000001";

        }

        public DataTable GetSchoolByCode(string strCode)
        {
            string procname = "DIC_SCHOOL_Get";
            DbAccess db = new DbAccess();
            db.CreateNewSqlCommand();
            db.AddParameter("@SchoolCode", strCode);
            return db.ExecuteDataTable(procname);
        }

        public bool Insert()
        {
            DbAccess db = new DbAccess();
            db.BeginTransaction();
            try
            {
                db.CreateNewSqlCommand();
                db.AddParameter("@SchoolCode", SchoolCode);
                db.AddParameter("@SchoolName", SchoolName);
                db.AddParameter("@Description", Description);
                db.AddParameter("@Active", Active);
                db.ExecuteNonQueryWithTransaction("DIC_SCHOOL_Insert");
                db.CommitTransaction();
                Class.S_Log.Insert("Trường học", "Thêm Trường học: " + SchoolName);
                return true;
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
                Class.App.Log_Write(ex.Message);
                return false;
            }
        }

        public bool Update()
        {
            DbAccess db = new DbAccess();
            db.BeginTransaction();
            try
            {
                db.CreateNewSqlCommand();
                db.AddParameter("@SchoolCode", SchoolCode);
                db.AddParameter("@SchoolName", SchoolName);
                db.AddParameter("@Description", Description);
                db.AddParameter("@Active", Active);
                db.ExecuteNonQueryWithTransaction("DIC_SCHOOL_Update");
                db.CommitTransaction();
                Class.S_Log.Insert("Trường học", "Cập nhật Trường học: " + SchoolName);
                return true;
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
                Class.App.Log_Write(ex.Message);
                return false;
            }
        }

        public bool Delete()
        {
            DbAccess db = new DbAccess();
            db.BeginTransaction();
            try
            {
                db.CreateNewSqlCommand();
                db.AddParameter("@SchoolCode", SchoolCode);
                db.ExecuteNonQueryWithTransaction("DIC_SCHOOL_Delete");
                db.CommitTransaction();
                Class.S_Log.Insert("Trường học", "Xóa Trường học: " + SchoolCode);
                return true;
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
                Class.App.Log_Write(ex.Message);
                return false;
            }
        }
    }
}
