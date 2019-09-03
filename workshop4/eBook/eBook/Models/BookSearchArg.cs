using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBook.Models
{
    public class BookSearchArg
    {    
        public string bookId { get; set; }
        //[DisplayName("書名")]
        public string bookName { get; set; }
       // [DisplayName("圖書類別")]
        public string bookClassName { get; set; }
       // [DisplayName("借閱人")]
        public string userEName { get; set; }
       // [DisplayName("借閱狀態")]
        public string codeName { get; set; }
        // [DisplayName("圖書類別代碼")]
        public string bookClassId { get; set; }
        // [DisplayName("借閱人代碼")]
        public string userId { get; set; }
        // [DisplayName("借閱狀態代碼")]
        public string bookStatusId { get; set; }
        

    }
}