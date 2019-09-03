using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBook.Models
{
    public class BookSearchResult
    {
       // [DisplayName("圖書類別")]
        public string bookClassName { get; set; }
        //[DisplayName("書名")]
        public string bookName { get; set; }
       // [DisplayName("購書日期")]
        public string bookBoughtDate { get; set; }
       // [DisplayName("借閱狀態")]
        public string codeName { get; set; }
       // [DisplayName("借閱人")]
        public string userEName { get; set; }
    }
}