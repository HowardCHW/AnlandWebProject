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