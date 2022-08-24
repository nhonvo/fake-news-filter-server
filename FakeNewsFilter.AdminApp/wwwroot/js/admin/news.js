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

function loadSelect2() {
    var select = $('.select2');
    select.select2();
    select.each(function () {
        var $this = $(this);
        $this.wrap('<div class="position-relative"></div>');
        $this.select2({
            dropdownAutoWidth: true,
            width: '100%',
            dropdownParent: $this.parent()
        });
    });
}


$(document).ready(function () {
    loadSelect2();
    table = $('#list_news').dataTable({
        "pageLength": 100,
        columnDefs: [
            {
                orderable: true, targets: '_all'
            }
        ],
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

function CreateNews(frm, caller, source) {

    $('#loading').show();
    caller.preventDefault();
    var fdata = new FormData();

    if (source == "system") {
        var content = CKEDITOR.instances.ckeditor1.getData();
        fdata.append("Content", content);
        var thumbNews = $(frm).find('input:file[name="ThumbNews"]')[0]?.files[0];
        fdata.append("ThumbNews", thumbNews);
    } else {
        var ImageLink = $(frm).find('input#ImageLink')[0].value;
        fdata.append("ImageLink", ImageLink);
        var SourceCreate = $(frm).find('#SourceCreate')[0].value;
        fdata.append("SourceCreate", SourceCreate);
        var UrlNews = $(frm).find('input#UrlNews')[0].value;
        fdata.append("UrlNews", UrlNews);

    }
    var Title = $(frm).find('input#Title')[0].value;
    var Description = $(frm).find('textarea#Description')[0].value;
    var Publisher = $(frm).find('#Publisher')[0].value;
    // var DatePublished = $(frm).find('#DatePublished')[0].value;
    var officialRating = $(frm).find('#OfficialRating')[0].value;
    var languageId = $(frm).find('#LanguageId')[0].value;
    var topicIdList = $(frm).find('#TopicId').select2("val");

    fdata.append("Title", Title);
    fdata.append("Description", Description);
    fdata.append("Publisher", Publisher);
    // fdata.append("DatePublished", DatePublished);
    fdata.append("OfficialRating", officialRating);
    fdata.append("LanguageId", languageId);
    topicIdList.forEach((topicId) => fdata.append("TopicId", topicId));
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
                dataTable.row.add(
                    [
                        data.resultObj.newsId,
                        data.resultObj.title,
                        `<img alt="" src="/img/flags-icons/${data.resultObj.languageId}.png" width="30px">`,
                        moment(`${data.resultObj.timestamp.toString("yyyy-MM-dd HH:mm")}`).fromNow(),
                        '<div class="' + statusObj[data.resultObj.status].class + '" data-title="' + statusObj[data.resultObj.status].title + '" data-bs-toggle="tooltip" title="' + statusObj[data.resultObj.status].title + '"> <span hidden>' + statusObj[data.resultObj.status].title + '</span></div>',
                        `<div>
                            <a class="btn btn-link" onclick=Detail(${data.resultObj.newsId}) data-bs-toggle="tooltip" data-bs-placement="top" title="Edit">
                                <i class="os-icon os-icon-ui-49"></i>
                            </a>
                            <a class="danger" onclick=DeleteData(${data.resultObj.newsId}) data-bs-toggle="tooltip" data-bs-placement="top" title="Delete">
                                <i class="os-icon os-icon-ui-15"></i>
                            </a>
                        </div>`
                    ]
                ).draw(false);

                frm.reset();
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

//Xem chi tiết 1 chủ đề (phục vụ cho chỉnh sửa dữ liệu)
function Detail(newsId, source) {
    $.ajax({
        type: "GET",
        url: "News/GetNewsById",
        data: {Id: newsId, source: source},
        success: function (msg) {
            $('#PartialViewNews').html(msg);
            if (source == "system") {
                $('#updateNewsBySystem').modal('show');
            } else {
                $('#updateNewsByOutsource').modal('show');
            }
            loadSelect2();
            //populate list topic in news to modal select2
            var listInput = $('.news-in-topic-list-id');
            var listTopicId = listInput.map(function () {
                return $(this).val();
            }).get();
                var select = listInput.parent().find('select');
                select.val(listTopicId).trigger('change');
        },
        error: function (req, status, error) {
            toastr['error'](
                'Watch Detail News Unsuccessfully ' + error, 'Error'
                , {
                    closeButton: true,
                    tapToDismiss: false,
                    positionClass: "toast-bottom-left",
                    rtl: $('html').attr('data-textdirection') === 'rtl'
                }
            );
        }
    });
}

function DeleteData(newsId) {
    if (confirm("Are you sure want to archive this news?")) {
        Archive(newsId);
    } else {
        return false;
    }
}

function Archive(newsId){
    var url = "/News/Archive";
    $.post(url, {newsId: newsId}, function (data) {
        console.log(data);
        location.reload();
    });
}

function Delete(newsId) {
    var url = "/News/Delete";
    $.post(url, {newsId: newsId}, function (data) {
        location.reload();
    });
}