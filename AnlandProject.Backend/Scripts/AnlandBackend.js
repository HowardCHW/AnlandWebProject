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