using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace eBook.Models
{
    public class BookService
    {
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }
        /// <summary>
        /// 依查詢條件取得圖書
        /// </summary>
        /// <param name="arg">查詢條件</param>
        /// <returns></returns>
        public List<BookSearchResult> GetBookByCondition(BookSearchArg arg)
        {
                DataTable dt = new DataTable();
                string sql = @"SELECT BOOK_CLASS_NAME AS 圖書類別,BOOK_NAME AS 書名,CONVERT(VARCHAR, BOOK_BOUGHT_DATE,111) AS 購書日期,  CODE_NAME AS 借閱狀態,USER_ENAME AS 借閱人
                            FROM MEMBER_M M INNER JOIN 	BOOK_DATA BD 
		                        ON BD.BOOK_KEEPER=M.[USER_ID]
	                        INNER JOIN BOOK_CLASS BC1 
		                        ON BD.BOOK_CLASS_ID = BC1.BOOK_CLASS_ID
	                        INNER JOIN BOOK_CODE BC2 
		                    ON BD.BOOK_STATUS = BC2.CODE_ID AND BC2.CODE_TYPE = 'BOOK_STATUS'
                            Where (BOOK_NAME LIKE('%' + @bookName + '%') OR @bookName= '') AND
                                  (BC1 .BOOK_CLASS_ID=@bookClassID) AND
                                  (M.USER_ID = @userId) AND
                                  (BC2.CODE_ID = @bookStatusId)
                            ORDER BY BD.BOOK_PUBLISHER DESC ";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@bookName", arg.bookName == null ? string.Empty : arg.bookName));
                    cmd.Parameters.Add(new SqlParameter("@bookClassID", arg.bookClassId));    //要再新增是否可以空值
                    cmd.Parameters.Add(new SqlParameter("@userId", arg.userId));              //要再新增是否可以空值
                cmd.Parameters.Add(new SqlParameter("@bookStatusId", arg.bookStatusId));      //要再新增是否可以空值
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                    sqlAdapter.Fill(dt);
                    conn.Close();
                }
                return this.MapBookDataToList(dt);
            }

            private List<Models.BookSearchResult> MapBookDataToList(DataTable bd)
            {
                    List<Models.BookSearchResult> result = new List<BookSearchResult>();
                    foreach (DataRow row in bd.Rows)
                    {
                        result.Add(new BookSearchResult()
                        {   //row[]裡面的值盡量還是使用database的欄位名，不要用as後的名字來放
                            bookClassName = row["圖書類別"].ToString(),
                            bookName = row["書名"].ToString(),
                            bookBoughtDate = row["購書日期"].ToString(),
                            codeName = row["借閱狀態"].ToString(),
                            userEName = row["借閱人"].ToString(),
                        });
                     }
                    return result;
            }
            
        }
        
    }
    
