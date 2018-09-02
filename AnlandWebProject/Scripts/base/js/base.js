$(document).ready(function () {
    ////引入各元件
    //$('#headerTxt').load('header.html');
    //$('#mainMenuTxt').load('mainMenu.html');
    //$('#fatFooterTxt').load('fatFooter.html');
    //$('#footerTxt').load('footer.html');

    // 字體放大
    $('.font-large').on('click', function (event) {
        /* Act on the event */
        event.preventDefault();
        if ($('.subContent').hasClass('smallFont')) {
            $('.subContent').removeClass('smallFont').addClass('largeFont');
        } else {
            $('.subContent').addClass('largeFont');
        }

        //  $('.subContent').css('font-size', '1.176rem');
    });
    // 字體放中
    $('.font-default').click(function (event) {
        /* Act on the event */
        event.preventDefault();
        if ($('.subContent').hasClass('smallFont')) {
            $('.subContent').removeClass('smallFont');
        }
        if ($('.subContent').hasClass('largeFont')) {
            $('.subContent').removeClass('largeFont');
        }
    });
    // 字體放小
    $('.font-small').click(function (event) {
        /* Act on the event */
        event.preventDefault();
        if ($('.subContent').hasClass('largeFont')) {
            $('.subContent').removeClass('largeFont').addClass('smallFont');
        } else {
            $('.subContent').addClass('smallFont');
        }
    });
    // Data Table
    $('.myTable').DataTable({
        "language": {
            "processing":   "處理中...",
            "loadingRecords": "載入中...",
            "lengthMenu":   "顯示 _MENU_ 筆結果",
            "zeroRecords":  "沒有符合的結果",
            "info":         "顯示第 _START_ 至 _END_ 筆結果，共 _TOTAL_ 筆",
            "infoEmpty":    "顯示第 0 至 0 筆結果，共 0 筆",
            "infoFiltered": "(從 _MAX_ 筆結果中過濾)",
            "infoPostFix":  "",
            "search":       "搜尋:",
            "searchPlaceholder": "請輸入關鍵字",
            "paginate": {
                "first":    "第一頁",
                "previous": "上一頁",
                "next":     "下一頁",
                "last":     "最後一頁"
            },
            "aria": {
                "sortAscending":  ": 升冪排列",
                "sortDescending": ": 降冪排列"
            }
        },
        "pagingType": "full_numbers",
        "ordering": false
        // ,
        // "searching": false
    });

    // $('#myTable_filter label').on('focus', function () {
    //     this.setAttribute('id', 'tablefilter');
    // });
    // $('#myTable_filter label input').on('focus', function () {
    //     this.setAttribute('for', 'myTable_filter');
    // });
});

function validateSuggestForm() {
    var subject = document.suggestForm.title.value;
    var name = document.suggestForm.name.value;
    var email = document.suggestForm.email.value;
    var contents = document.suggestForm.content.value;
    /*檢查是email格式是否正確*/
    var regularExpression = /^[^\s]+@[^\s]+\.[^\s]{2,3}$/;
    if (subject.trim() == "") {
        document.getElementById('errorTitle').classList.add("active");
        document.suggestForm.title.focus();
        return false;
    } else if (name.trim() == "") {
        document.getElementById('errorName').classList.add("active");
        document.suggestForm.name.focus();
        return false;
    } else if (email.trim() == "") {
        document.getElementById('errorEmail').classList.add("active");
        document.suggestForm.email.focus();
        return false;
    } else if (regularExpression.test(email) == false) {
        document.getElementById('errorEmail').classList.add("active");
        document.suggestForm.email.focus();
        return false;
    } else if (contents.trim() == "") {
        document.getElementById('errorContent').classList.add("active");
        document.suggestForm.content.focus();
        return false;
    } else {
        /*注意：表單中不要有input name是submit的會出問題*/
        document.suggestForm.submit();
    }
}
/* 去掉字串兩端的空白字元 */
String.prototype.trim = function () {
    return this.replace(/(^\s+)|(\s+$)/g, "");
}

function hideError(formItem) {
    document.getElementById(formItem).classList.remove("active");
}