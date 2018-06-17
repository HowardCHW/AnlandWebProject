///
///  model的DateTime 轉換 'YYYY-MM-DD
///  value 值
function DateTimeFormatYYYYMMDD(value) {
    return DateTimeFormat(value, 'YYYY-MM-DD');
}
///
///  model的DateTime 轉換 'YYYY-MM-DD HH:mm:ss
///  value 值
function DateTimeFormatYYYYMMDDHHmmss(value) {
    return DateTimeFormat(value, 'YYYY-MM-DD HH:mm:ss');
}
///
/// model的DateTime 轉換方法
/// value 值
/// format 格式
function DateTimeFormat(value, format) {
    if (value === null) {
        return "";
    }
    var dt = moment(value);
    return dt.format(format);
}

var pfnDataTable = {
    _timmer: {
        init: {},
        change: {}
    },
    defOpts: {
        //dom: '<"dataTables_header_page"p>lit',
        language: {
            aria: {
                sortAscending: ": activate to sort column ascending",
                sortDescending: ": activate to sort column descending"
            },
            emptyTable: "無資料",
            info: "顯示 _START_ 到 _END_ 總共 _TOTAL_ 筆",
            infoEmpty: "無資料",
            infoFiltered: "(已篩選 1 至 _MAX_ 筆紀錄)",
            lengthMenu: "每次顯示 _MENU_ 筆",
            search: "篩選:",
            zeroRecords: "沒有找到符合的資料",
            paginate: {
                previous: "上一頁",
                next: "下一頁",
                last: "最後一頁",
                first: "第一頁",
                page: "第",
                pageOf: "頁，總頁數： "
            }
        },
        bStateSave: !0,
        pagingType: "bootstrap_extended",
        paging: true,
        lengthMenu: [
            [5, 10, 15, 20, -1],
            [5, 10, 15, 20, "All"]
        ],
        pageLength: 5,
        columnDefs: [{
            orderable: !1,
            targets: 'nosort'
        }, {
            searchable: !1,
            targets: [0]
        }],
        order: [
            [1, "asc"]
        ]
    },
    listOptions: {
        dom: '<"dataTables_header_page"p>lit',
        sPaginationType: "full_numbers",
        autoWidth: false,
        oLanguage: {
            sUrl: "./json/dataTableLang.json"
        },
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [
            { className: 'control input-width--xs', orderable: false, targets: 0 },
            { orderable: false, targets: 'datatable__disorder' }
        ]
    },
    serversideOpts: {},
    highlightChecked: function (_tableId, _allCheckId, _checkName) {
        var allCheckId = _tableId + ' ' + _allCheckId;
        var checkName = _tableId + ' ' + _checkName;

        $(document).on('change', _allCheckId, function () {
            var _boolChecked = $(this).prop('checked');
            $(checkName).prop('checked', _boolChecked);
            _boolChecked ? $(checkName + ':not(' + _allCheckId + ')').closest('tr').addClass('bg-highlight') : $(checkName).closest('tr').removeClass('bg-highlight');
        });

        $(document).on('change', checkName, function () {
            var _boolChecked = $(this).prop('checked');
            _boolChecked ? $(this).not(_allCheckId).closest('tr').addClass('bg-highlight') : $(this).closest('tr').removeClass('bg-highlight');
        });
    },
    removeChecked: function (_tableId, _allCheckId, _checkName, callback) {
        var allCheckId = _tableId + ' ' + _allCheckId;

        $(allCheckId).prop('checked', false);

        $(_checkName + ":checked", $(_tableId).dataTable().fnGetNodes())
            .each(function (i, e) {
                $(this).prop('checked', false);
                $(this).closest('tr').removeClass('bg-highlight');
            });

        callback && callback();
    },
    ableChecked: function (_bool, _tableId, _allCheckId, _checkName) {
        var allCheckId = _tableId + ' ' + _allCheckId;

        $(allCheckId).prop('disabled', !_bool);

        $(_checkName, $(_tableId).dataTable().fnGetNodes())
            .each(function (i, e) {
                $(this).prop('disabled', !_bool);
            });
    },
    getCheckedData: function (_tableId, _checkName) {
        var _choosedData = [];

        $(_checkName + ":checked", $(_tableId).dataTable().fnGetNodes())
            .each(function (i, e) {

                var _pk = $(e).data('id');

                $($(_tableId).dataTable().fnGetData())
                    .each(function (i, e) {
                        this.primaryKey == _pk ? _choosedData.push(this) : null;
                    });
            });

        return _choosedData;
    },
    setIndex: function (_dt, className) {
        _dt
            .column('.' + className, { search: 'applied', order: 'applied' })
            .nodes().each(function (cell, i) {
                cell.innerHTML = i + 1;
            });
    },
    updateSizeShown: function () {
        $('.modal, .tab-pane').on('shown.bs.modal shown.bs.tab', function () {
            if ($(this).find('.dataTable').length > 0) {
                try {
                    $(this).find('.dataTable').each(function () {
                        $(this).DataTable().columns.adjust().draw();
                    });
                } catch (e) {
                    console.error(e, ' Not a datatable.')
                }
            }
        });
    },
    ajaxDataFilter: function (data) {
        var json = jQuery.parseJSON(data);
        json.recordsTotal = json.data.recordsTotal;
        json.recordsFiltered = json.data.recordsFiltered;
        json.data = json.data.items;

        return JSON.stringify(json); // return JSON string
    }
}

var pfnBlockUI = {
    growUIOpts: {
        message: function () {
            return pfnBlockUI.setMessage('success');
        },
        fadeIn: 700,
        fadeOut: 700,
        timeout: 2000,
        showOverlay: false,
        centerY: false,
        blockMsgClass: 'block--success'
    },
    blockUIOpts: {
        message: function () {
            return pfnBlockUI.setMessage('資料處理中，請稍候...', 'normal');
        },
        fadeIn: 700,
        fadeOut: 700,
        // timeout: 2000, 
        // draggable: false,
        overlayCSS: {
            backgroundColor: '#000',
            opacity: 0.6
        },
        showOverlay: true,
        centerY: false,
        blockMsgClass: 'block--loading'
    },
    setMessage: function (_message, type) {
        var __msgStr = '';
        switch (type) {
            case 'normal':
                __msgStr = '<div class="ajax-loading"><div class="item1"></div>&nbsp;<div class="item2"></div>&nbsp;<div class="item3"></div>&nbsp;<div class="item4"></div>&nbsp;<div class="item5"></div></div><br />' + _message + '</div>';
                break;
            default:
                __msgStr = '<div class="block__body"> <i class="ion-android-done"></i>' + _message + '</div>';
        }
        return __msgStr;
    },
    setGrowDom: function () {
        $(document).on('click', '[data-toggle="correctmsg"]', function () {
            var _tmpOption = $.extend({}, pfnBlockUI.growUIOpts);
            _tmpOption.message = pfnBlockUI.setMessage($(this).data('message'));

            $.blockUI(_tmpOption);
        });
    },
    setAjaxLoading: function (opts) {

        var _tmpOption = $.extend({}, pfnBlockUI.blockUIOpts);

        if (opts && opts.message) {
            _tmpOption.message = pfnBlockUI.setMessage(opts.message, 'normal');
        }

        var ajaxDurationObj = {
            start: function () { $.blockUI(_tmpOption) },
            complete: function () { $.unblockUI() },
            setMessage: function (msg) {
                _tmpOption.message = pfnBlockUI.setMessage(msg, 'normal');
            },
            destroy: function () {
                delete ajaxDurationObj.start;
                delete ajaxDurationObj.complete;
                delete ajaxDurationObj.destroy;
            }
        }
        $(document)
            .ajaxStart(function () {
                try {
                    ajaxDurationObj.start();
                } catch (e) { }
            })
            .ajaxComplete(function () {
                setTimeout(function () {
                    try {
                        ajaxDurationObj.complete();
                    } catch (e) { }
                }, 1000);
            });
        return ajaxDurationObj;
    }
}

$(function () {
    SetDialog();
});

///
/// 訊息畫面
///
/// functionType 右上角訊息：'success_msg'  ，dialog：'other_msg'
/// size 視窗大小 ：小 'small'、中 'medium'、大 'large'
/// msgTitle 標題
/// msgContent 內容
/// isDisplay 顯示左邊按鈕 ：true、false
function msgResoure(functionType, size, msgTitle, msgContent, isDisplay) {

    if (functionType === 'success_msg') {
        $.blockUI($.extend({}, pfnBlockUI.growUIOpts, { message: pfnBlockUI.setMessage(msgContent) }));
        return;
    }
    if (isDisplay === undefined) {
        isDisplay = false;
    }
    if (functionType === 'other_msg') {
        if ($("#dvMessageBox").length > 0) {
            $("#dvMessageBox").remove();
        }
        var modalWidthCalss = "modal-sm";
        //訊息框大小設定
        switch (size) {
            case "medium":
                modalWidthCalss = "modal-md";
                break;
            case "large":
                modalWidthCalss = "modal-lg";
                break;
            default:
        }
        $("<div/>", {
            "id": "dvMessageBox",
            "class": "modal",
            "html": $("<div />", {
                "class": "modal-dialog " + modalWidthCalss,
                "html": $("<div />", {
                    "class": "modal-content ",
                    "html": $("<div />", {
                        "class": "modal-header ",
                        "html": $("<button />", {
                            "class": "close",
                            "type": "button",
                            "data-dismiss": "modal",
                            "aria-label": "Close",
                            "html": "<span aria-hidden='true'>&times;</span>"
                        })
                    })
                })
            })
        }).appendTo("body");
        $("<h4 />", {
            "class": "modal-title msg-resource-title",
            "html": msgTitle
        }).appendTo("#dvMessageBox .modal-header");
        $("<div />", {
            "class": "modal-body",
            "html": $("<p />", {
                "class": "msg-resource-body",
                "html": msgContent
            })
        }).appendTo("#dvMessageBox .modal-content");
        $("<div />", {
            "class": "modal-footer",
            "html": $("<button />", {
                "class": "btn btn-default",
                "type": "buton",
                "data-dismiss": "modal",
                "data-result": "false",
                "id": "MsgCancel",
                "style": isDisplay ? "" : "display:none",
                "text": "取消"
            })
        }).appendTo("#dvMessageBox .modal-content");
        $("<button />", {
            "class": "btn btn-primary",
            "type": "buton",
            "data-dismiss": "modal",
            "data-result": "true",
            "text": "確認"
        }).appendTo("#dvMessageBox .modal-footer");
        $("#dvMessageBox").modal("show");
        return;

    }
}

///
/// SuccessMSGAfterAction 右上角的訊息
/// msgContent 內容
/// url 跳頁
/// sec 不傳 就是600豪秒
function SuccessMSGAfterAction(msgContent, url, sec) {
    msgResoure('success_msg', "", "", msgContent);
    if (sec === undefined) {
        sec = 600;
    }
    if (url !== undefined) {
        setTimeout(function () {
            location = url;
        }, sec);
    }

}

///
/// SuccessMSGAfterAction 右上角的訊息
/// msgContent 內容
/// fun 需執行方法
/// sec 不傳 就是600豪秒
function SuccessMSGAfterFun(msgContent, fun, sec) {
    msgResoure('success_msg', "", "", msgContent);
    if (sec === undefined) {
        sec = 600;
    }
    if (typeof fun === "function") {
        setTimeout(function () {
            fun();
        }, sec);
    }

}

///
/// ErrorMSG
/// msgContent 內容
/// size 視窗大小 ：小 'small'、中 'medium'、大 'large'
function ErrorMSG(msgContent, size) {
    if (size === undefined) {
        size = "small";
    }
    msgResoure('other_msg', size, '<h4 class="modal-title text-red"><i class="fa fa-exclamation-triangle" aria-hidden="true"></i>&nbsp;錯誤！</h4>', msgContent);
}

/* 將 Modal 額外屬性綁定到 modal 上 */
function SetDialog() {
    $(document).on('click', '[data-toggle=modal]', function () {
        var dialogID = $(this).attr("href");// >>> a
        if (dialogID === undefined) {
            dialogID = $(this).data("target");// >>> button
        }
        $(dialogID + " .modal-footer #id").val($(this).data("id"));

        var contentArgs = $(this).data("contentargs");

        if (contentArgs && contentArgs.length > 0) {
            var content = $(dialogID + " .modal-body").data("content");
            $(dialogID + " .modal-body").html(String.format(content, contentArgs));
        }
    })
}

//jQuery String Format Extension
String.format = function (src, args) {
    var result = src;

    for (var i = 0; i < args.length; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");

        result = result.replace(reg, args[i]);
    }

    return result;
}