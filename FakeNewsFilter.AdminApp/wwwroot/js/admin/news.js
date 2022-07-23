/*=========================================================================================
  File Name: news.js
  Description: News Manage JS
  ----------------------------------------------------------------------------------------
  Item Name: Fake News Filter - Admin
  Author: Bui Phu Khuyen
  Author URL: fb.com/buiphukhuyen
==========================================================================================*/

//Load Datatable List Topic
var statusObj = {
    0: {title: 'Archive', class: 'status-pill red'},
    1: {title: 'Active', class: 'status-pill green'},
    2: {title: 'Inactive', class: 'status-pill yellow'}
};

$(document).ready(function () {

    $('#loading').hide();

    var select = $('.select2');

    select.each(function () {
        var $this = $(this);
        $this.wrap('<div class="position-relative"></div>');
        $this.select2({
            dropdownAutoWidth: true,
            width: '100%',
            dropdownParent: $this.parent()
        });
    });

    $("#list_news").DataTable({
        ajax: {
            url: "/News/GetNews",
            type: "get",
            contentType: "application/json",
            dataType: "json",
            data: function (data) {
                // return JSON.stringify(data);
            }
        },

        // <th>ID</th>
        // <th>News</th>
        // <th>Lang</th>
        // <th>SyncTime</th>
        // <th>Status</th>
        // <th>Actions</th>

        columns: [
            {"data": 'newsId'},
            {"data": 'title'},
            {"data": 'languageId'},
            {"data": 'timestamp'},
            {"data": 'status'},
            {"data": ''}
        ],
        columnDefs: [
            {
                //Lấy ảnh cờ của ngôn ngữ
                targets: 2,
                render: function (data, type, full, meta) {
                    var $lang = full['languageId'];
                    return (
                        '<div><img alt="" src="/img/flags-icons/' + $lang + '.png" width="25px"><span hidden>' + $lang + '</span></div>'
                    );
                }
            },
            {
                //Đồng bộ thời gian thực (Sử dụng MommentJS)
                targets: 3,
                render: function (data, type, full, meta) {
                    var $time = full['timestamp'];
                    return moment($time).fromNow();
                }
            },
            {
                //Trạng thái của tin tức (Active/InActive/Archive)
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    var $status = full['status'];
                    return (
                        '<div class="' + statusObj[$status].class + '" data-title="' + statusObj[$status].title + '" data-bs-toggle="tooltip" title="' + statusObj[$status].title + '"> <span hidden>' + statusObj[$status].title + '</span></div>'
                    );
                }
            },
            {
                //Các nút Actions
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    var $newsId = full['newsId'];
                    return (
                        '<div class="row-actions text-center">' +
                        '<a href="/News/Edit/' + $newsId + '" data-bs-toggle="tooltip" data-bs-placement="top" title = "Edit"> <i class="os-icon os-icon-ui-49"></i> </a>' +
                        '<a class="danger" onclick=DeleteData("' + $newsId + '"); data-bs-toggle="tooltip" data-bs-placement="top" title="Delete"> <i class="os-icon os-icon-ui-15"></i> </a>' +
                        '</div>'
                    );
                }
            }
        ],
        dom:
            '<"d-flex justify-content-between align-items-center header-actions mx-1 row mt-75"' +
            '<"col-sm-12 col-md-4 col-lg-6" l>' +
            '<"col-sm-12 col-md-8 col-lg-6 ps-xl-75 ps-0"<"dt-action-buttons text-xl-end text-lg-start text-md-end text-start d-flex align-items-center justify-content-md-end align-items-center flex-sm-nowrap flex-wrap me-1"<"me-1"f>B>>' +
            '>t' +
            '<"d-flex justify-content-between mx-2 row mb-1"' +
            '<"col-sm-12 col-md-6"i>' +
            '<"col-sm-12 col-md-6"p>' +
            '>',
        buttons: [
            {
                text: 'Add News',
                className: 'add-new btn btn-rounded btn-primary',
                attr: {
                    'data-toggle': 'modal',
                    'data-target': '.bd-example-modal-lg'
                },
                init: function (api, node, config) {
                    $(node).removeClass('btn-secondary');
                }
            }
        ],
        language: {
            paginate: {
                //Thay đổi nút Next/Previous
                previous: '<',
                next: '>'
            }
        },
        //Thiết lập Tooltip
        drawCallback: function () {
            $(document).find('[data-bs-toggle="tooltip"]').tooltip();
        },

        initComplete: function () {
            //API lọc ngôn ngữ tin tức
            this.api()
                .columns(2)
                .every(function () {
                    var column = this;
                    var select = $(
                        '<select id="FilterNewsLanguage" class="form-control form-control-sm rounded bright"><option value=""> Select Language </option></select>'
                    )
                        .appendTo('.news_language')
                        .on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex($(this).val());
                            column.search(val ? '^' + val + '$' : '', true, false).draw();
                        });

                    column
                        .data()
                        .unique()
                        .sort()
                        .each(function (d, j) {
                            select.append(
                                '<option value="' +
                                d +
                                '" class="text-capitalize">' +
                                d +
                                '</option>'
                            );
                        });

                });
            //API lọc trạng thái tin tức
            this.api()
                .columns(4)
                .every(function () {

                    var column = this;
                    var select = $(
                        '<select id="FilterNewsStatus" class="form-control form-control-sm rounded bright"><option value=""> Select Status </option></select>'
                    )
                        .appendTo('.news_status')
                        .on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex($(this).val());
                            column.search(val ? '^' + val + '$' : '', true, false).draw();
                        });

                    column
                        .data()
                        .unique()
                        .sort()
                        .each(function (d, j) {
                            select.append(
                                '<option value="' +
                                statusObj[d].title +
                                '" class="text-capitalize">' +
                                statusObj[d].title +
                                '</option>'
                            );
                        });

                });
        },

    });
});

$(function () {
    //Select

    selectArray = $('.select2-data-array'),

        selectArray.wrap('<div class="position-relative"></div>').select2({
            dropdownAutoWidth: true,
            dropdownParent: selectArray.parent(),
            width: '100%',
        });

    var changePicture = $('#ThumbNews'),
        userAvatar = $('.news-thumb'),
        form = $('.form-validate');

    // Change user profile picture
    if (changePicture.length) {
        $(changePicture).on('change', function (e) {
            var reader = new FileReader(),
                files = e.target.files;
            reader.onload = function () {
                if (userAvatar.length) {
                    userAvatar.attr('src', reader.result);
                }
            };
            reader.readAsDataURL(files[0]);
        });
    }

    // Validation
    if (form.length) {
        $(form).each(function () {
            var $this = $(this);
            $this.validate({
                submitHandler: function (form, event) {
                    event.preventDefault();
                },
                rules: {
                    username: {
                        required: true
                    },
                    name: {
                        required: true
                    },
                    email: {
                        required: true,
                        email: true
                    },
                    phone: {
                        required: true
                    },


                }
            });
        });

        $(this).on('submit', function (event) {
            event.preventDefault();
        });
    }
});

function CreateNews(frm, caller) {
    $('#loading').show();
    caller.preventDefault();
    var fdata = new FormData();
    var Title = $(frm).find('input#Title')[0].value;
    var Publisher = $(frm).find('#Publisher')[0].value;
    var DatePublished = $(frm).find('#DatePublished')[0].value;
    var officialRating = $(frm).find('#OfficialRating')[0].value;
    var content = CKEDITOR.instances.ckeditor1.getData();
    var languageId = $(frm).find('#LanguageId')[0].value;
    var topicIdList = $(frm).find('#TopicId').select2("val");

    var thumbNews = $(frm).find('input:file[name="ThumbNews"]')[0].files[0];

    fdata.append("Title", Title);
    fdata.append("Publisher", Publisher);
    fdata.append("DatePublished", DatePublished);
    fdata.append("OfficialRating", officialRating);
    fdata.append("Content", content);
    fdata.append("LanguageId", languageId);
    topicIdList.forEach((topicId) => fdata.append("TopicId", topicId));
    fdata.append("ThumbNews", thumbNews);

    $.ajax(
        {
            type: frm.method,
            url: frm.action,
            data: fdata,
            processData: false,
            contentType: false,
            success: function (data) {
                $('#loading').hide();
                var dataTable = $('#list_news').DataTable();

                dataTable.row.add({
                    "newsId": data.resultObj.newsId,
                    "title": data.resultObj.title,
                    "languageId": data.resultObj.languageId,
                    "timestamp": data.resultObj.timestamp,
                    "status": data.resultObj.status,
                    "newsId": data.resultObj.newsId
                }).draw();
                
                $('form[action="/News/Create"]')[0].reset();
                CKEDITOR.instances.ckeditor1.setData("");
                $('#TopicId').val(null).trigger('change');
                
                toastr['success'](
                    'Create News Successfully', 'Success', {
                        closeButton: true,
                        tapToDismiss: false,
                        positionClass: "toast-bottom-left",
                        rtl: $('html').attr('data-textdirection') === 'rtl'
                    }
                );
            },
            
            error: function (data) {
                $('#loading').hide();
                $('.bd-example-modal-lg').modal('hide');
                toastr['error'](
                    'Create News Unsuccessfully', 'Error'
                    , {
                        closeButton: true,
                        tapToDismiss: false,
                        positionClass: "toast-bottom-left",
                        rtl: $('html').attr('data-textdirection') === 'rtl'
                    }
                );
            }
        })
}

function DeleteData(newsId) {
    if (confirm("Are you sure want to delele this news?")) {
        Delete(newsId);
    } else {
        return false;
    }
}

function Delete(newsId) {
    var url = "/News/Delete";
    $.post(url, {newsId: newsId}, function (data) {
        location.reload();
    });
}