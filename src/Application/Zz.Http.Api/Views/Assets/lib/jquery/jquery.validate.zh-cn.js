(function (factory) {
    if (typeof define === "function" && define.amd) {
        define(["jquery", "../jquery.validate"], factory);
    } else {
        factory(jQuery);
    }
}(function ($) {

    /*
     * Translated default messages for the jQuery validation plugin.
     * Locale: ZH (Chinese, 中文 (Zhōngwén), 汉语, 漢語)
     */
    $.extend($.validator.messages, {
        required: "必填",
        remote: "错误",
        email: "电子邮件格式错误",
        url: "网址格式错误",
        date: "日期无效",
        dateISO: "日期无效（YYYY-MM-DD）",
        number: "无效数字",
        digits: "只能输入数字",
        creditcard: "信用卡号码无效",
        equalTo: "你的输入不相同",
        extension: "扩展名无效",
        maxlength: $.validator.format("最多{0}个字符"),
        minlength: $.validator.format("最少{0}个字符"),
        rangelength: $.validator.format("长度在{0}到{1}之间"),
        range: $.validator.format("数值在{0}到{1}之间"),
        max: $.validator.format("数值不能大于{0}"),
        min: $.validator.format("数值不能小于{0}")
    });

}));