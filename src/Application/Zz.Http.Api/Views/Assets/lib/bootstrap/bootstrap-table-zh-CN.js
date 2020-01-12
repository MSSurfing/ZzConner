/**
 * Bootstrap Table Chinese translation
 * Author: Zhixin Wen<wenzhixin2010@gmail.com>
 */
(function ($) {
    'use strict';

    $.fn.bootstrapTable.locales['zh-CN'] = {
        formatLoadingMessage: function () {
            return '正在加载...';
        },
        formatRecordsPerPage: function (pageNumber) {
            return pageNumber;
        },
        formatShowingRows: function (pageFrom, pageTo, totalRows) {
            return '共:' + totalRows + '条记录';// pageFrom + '-' + pageTo + '(共' + totalRows + '条记录)';
        },
        formatSearch: function () {
            return '搜索';
        },
        formatNoMatches: function () {
            return '没有找到匹配的记录';
        },
        formatPaginationSwitch: function () {
            return '隐藏/显示分页';
        },
        formatRefresh: function () {
            return '刷新';
        },
        formatToggle: function () {
            return '切换';
        },
        formatColumns: function () {
            return '列';
        },
        formatExport: function () {
            return '导出数据';
        },
        formatClearFilters: function () {
            return '清空条件';
        }
    };

    $.extend($.fn.bootstrapTable.defaults, $.fn.bootstrapTable.locales['zh-CN']);

})(jQuery);
