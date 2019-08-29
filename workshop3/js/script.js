
var bookDataFromLocalStorage = [];

$(document).ready(function(){
    loadBookData();
  /*  var data = [
        {text:"資料庫",value:"image/database.jpg"},
        {text:"網際網路",value:"image/internet.jpg"},
        {text:"應用系統整合",value:"image/system.jpg"},
        {text:"家庭保健",value:"image/home.jpg"},
        {text:"語言",value:"image/language.jpg"}
    ]
    $("#book_category").kendoDropDownList({
        dataTextField: "text", 
        dataValueField: "value",
        dataSource: data,
        index: 0,
        change: onChange
    });*/

    //建立一個window
    $("#wnd").kendoWindow();          //window的命名不好，可以換成insert_window之類的
    $("#bought_datepicker").kendoDatePicker();
    $("#book_grid").kendoGrid({
        dataSource: {
            data: bookDataFromLocalStorage,
            schema: {
                model: {
                    fields: {
                        BookId: {type:"int"},
                        BookName: { type: "string" },
                        BookCategory: { type: "string" },
                        BookAuthor: { type: "string" },
                        BookBoughtDate: { type: "string" }
                    }
                    
                }
            },
            
            pageSize: 20,
        },
        toolbar: kendo.template("<div class='book-grid-toolbar'><input class='book-grid-search' placeholder='我想要找......' type='text'></input></div>"),
        height: 550,
        sortable: true,
        pageable: {
            input: true,
            numeric: false
        },
        columns: [
            { field: "BookId", title: "書籍編號",width:"10%"},
            { field: "BookName", title: "書籍名稱", width: "50%" },
            { field: "BookCategory", title: "書籍種類", width: "10%" },
            { field: "BookAuthor", title: "作者", width: "15%" },
            { field: "BookBoughtDate", title: "購買日期", width: "15%" },
            { command: { text: "刪除", click: deleteBook }, title: " ", width: "120px" }
        ] 
        
        
    }); 

    //查詢資料
    $(".book-grid-search").on("input",function () {
        var val = $(".book-grid-search").val();
        //console.log(val);
        $("#book_grid").data("kendoGrid").dataSource.filter({
            
            filters: [
                {   //field後放欄位名，operator後放contains或是equal的功能，value後則放上面定義的變數
                    field: "BookName",
                    operator: "contains",
                    value: val
                },   
            ]
        });
    });
})

function loadBookData(){
    bookDataFromLocalStorage = JSON.parse(localStorage.getItem("bookData"));
    if(bookDataFromLocalStorage == null){
        bookDataFromLocalStorage = bookData;
        localStorage.setItem("bookData",JSON.stringify(bookDataFromLocalStorage));
    }
    //console.log(bookDataFromLocalStorage);
    //local storage是將儲存資料在Client端的瀏覽器，有點類似Cookie，不過Web Storage寫程式存取都是寫在Client端Javascript裡，Server端用不到
}

function onChange(){
    var BOOK_PICTURE = $("#book_category").val() //連到第116行的dropdownlist
    $(".book-image").attr("src",BOOK_PICTURE) //用book-image範圍太大，可以直接選用id
    //選取下拉選單後可以變換不同圖片，先取下拉選單的值給一個變數(可以判斷是哪一個選項)，再用attr("src",變數名)的方式來抓取資料
}


function deleteBook(e){   
        e.preventDefault();                //阻止元素發生默認的行為（例如，當點擊提交按鈕時阻止對表單的提交）
        var tr = (e.target).closest("tr"); //當e(event)事件觸發時，會找到最靠近此td的tr
        var DATA =this.dataItem(tr);      
        var grid = $("#book_grid").data("kendoGrid").dataSource;
        grid.remove(DATA);
        localStorage.setItem('bookData', JSON.stringify(grid.data())); 
        //把新的表格(有被刪除過的表格)傳到原本的表格，覆蓋掉原本的表格
        alert('Are you sure to delete?'); //這是強制刪除，可以改成用confirm來詢問是否刪除
    } 
    
    //新增書籍時可以用modal (可以把背景變成灰色，不能使用)
    var wnd = $("#wnd").kendoWindow({
        title : "新增書籍",
        height: 600,
        width: 400,
        visible: false
      }).data("kendoWindow");
    
    $(".k-button").click(function(e) {   //k-button和k1-button的命名不好，要更改
              wnd.open().center();
              var data = [
                {text:"資料庫",value:"image/database.jpg"},
                {text:"網際網路",value:"image/internet.jpg"},
                {text:"應用系統整合",value:"image/system.jpg"},
                {text:"家庭保健",value:"image/home.jpg"},
                {text:"語言",value:"image/language.jpg"}
            ]
              $("#book_category").kendoDropDownList({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: data,
                index: 0,
                change: onChange
            });
    
            $(".k1-button").click(function(e) {  //可以試著看看可不可以放外面
                    
                    var Bcategory= $("#book_category").data("kendoDropDownList").text();
                    var Bname= $("#book_name").val();
                    var BAuthor= $("#book_author").val();
                    var BBoughtDate=$("#bought_datepicker").val();
                       
                        //alert(fname);
                        var grid = $("#book_grid").data("kendoGrid");
                        var dataSource = grid.dataSource;
                        var total = (dataSource.data().length)+1; //可以用找最大陣列再加一的方式
                        dataSource.insert({BookId:total, BookName:Bname, BookCategory:Bcategory,BookAuthor:BAuthor,BookBoughtDate:BBoughtDate});
                        //alert('New row added !!');
                        $("#book_name").val('');
                        $("#book_author").val('');
                        localStorage.setItem('bookData', JSON.stringify(dataSource.data()));
                        wnd.close();
                
            });
        });
  



 
