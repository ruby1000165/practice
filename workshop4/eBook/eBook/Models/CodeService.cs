using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace eBook.Models
{
    public class CodeService
    {
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }
        /// <summary>
        /// 取得圖書類別
        /// </summary>
        /// <returns></returns>

        public List<SelectListItem> GetBookClass1() //要改GetBookClass1()的名字，此處代表圖書類別
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT BOOK_CLASS_ID,BOOK_CLASS_NAME
                            FROM BOOK_CLASS";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData1(dt);
            
        }

        public List<SelectListItem> GetBookClass2() //要改GetBookClass2()的名字，此處代表借閱人
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT USER_ID,USER_ENAME
                            FROM  MEMBER_M ";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData2(dt);

        }

        public List<SelectListItem> GetBookClass3()  //要改GetBookClass3()的名字，此處代表借書狀態
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT  CODE_NAME,CODE_ID
                            FROM   BOOK_CODE 
                            WHERE CODE_TYPE = 'BOOK_STATUS'";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData3(dt);

        }

        //MapCodeData1、MapCodeData2、MapCodeData3要更改變數名稱
        private List<SelectListItem> MapCodeData1(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["BOOK_CLASS_NAME"].ToString(),
                    Value = row["BOOK_CLASS_ID"].ToString()
                });
            }
            return result;
        }

        private List<SelectListItem> MapCodeData2(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["USER_ENAME"].ToString(),
                    Value = row["USER_ID"].ToString()
                });
            }
            return result;
        }

        private List<SelectListItem> MapCodeData3(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CODE_NAME"].ToString(),
                    Value = row["CODE_ID"].ToString()
                });
            }
            return result;
        }
    }

}

