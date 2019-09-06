using System.ComponentModel;


namespace BookManagement.Models
{
    public class BookSearchArg
    {
        [DisplayName("書名")]
        public string BookName { get; set; }
        [DisplayName("借閱人")]
        public string UserName { get; set; }
        [DisplayName("圖書類別")]
        public string BookClassName { get; set; }
        [DisplayName("借閱狀態")]
        public string CodeName{ get; set; }
        [DisplayName("借閱狀態ID")]
        public string CodeID { get; set; }
    }
}