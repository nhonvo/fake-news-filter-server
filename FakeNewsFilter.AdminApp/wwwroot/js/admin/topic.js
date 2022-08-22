/*=========================================================================================
  File Name: topic.js
  Description: Topic Manage JS
  ----------------------------------------------------------------------------------------
  Item Name: Fake News Filter - Admin
  Author: Bui Phu Khuyen
  Author URL: fb.com/buiphukhuyen
==========================================================================================*/

var select = $('.select2');

var statusObj = {
    0: {title: 'Archive', class: 'status-pill red'},
    1: {title: 'Active', class: 'status-pill green'},
    2: {title: 'Inactive', class: 'status-pill yellow'}
};

$(document).ready(function () {
    //Hiển thị database table js
    table = $('#list_topic').dataTable({
        columnDefs: [
            {
                orderable: true, targets: '_all'
            }
        ],
    });

});

$(function () {

    // var changePicture_create = $('#ThumbTopic'),
    //     userAvatar_create = $('.topic-thumb'),
    //     changePicture_edit = $('.editThumbTopic'),
    //     userAvatar_edit = $('.topic-thumb-edit')


    var changePicture = $('#ThumbTopic'),
        userAvatar = $('.topic-thumb');

    // Thay đổi hình ảnh

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
});


//Xem chi tiết 1 chủ đề (phục vụ cho chỉnh sửa dữ liệu)
function Detail(topicId) {
    $.ajax({
        type: "GET",
        url: "Topic/DetailTopic",
        data: {topicId: topicId},
        success: function (msg) {
            $('#PartialViewTopic').html(msg);
            $('#updateTopic').modal('show');
        },
        error: function (req, status, error) {
            toastr['error'](
                'Watch Detail Topic Unsuccessfully ' + error, 'Error'
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

//AJAX tạo mới chủ đề
function CreateTopic(frm, caller) {
    $('#loading').show();

    caller.preventDefault();
    var fdata = new FormData();

    var tag = $(frm).find('#Tag')[0].value;
    var description = $(frm).find('#Description')[0].value;
    var languageId = $(frm).find('#LanguageId')[0].value;
    var label = $(frm).find('#Label')[0].value;
    var thumbTopic = $(frm).find('input:file[name="ThumbTopic"]')[0].files[0];

    fdata.append("Tag", tag);
    fdata.append("Description", description);
    fdata.append("LanguageId", languageId);
    fdata.append("Label", label);
    fdata.append("ThumbTopic", thumbTopic);
    $.ajax(
        {
            type: frm.method,
            url: frm.action,
            data: fdata,
            processData: false,
            contentType: false,
            success: function (data) {

                $('#loading').hide();
                $('.bd-example-modal-lg').modal('hide');

                var dataTable = $('#list_topic').DataTable();
                dataTable.row.add(
                    [
                        data.resultObj.topicId,
                        data.resultObj.tag,
                        data.resultObj.label,
                        `<img alt="" src="/img/flags-icons/${data.resultObj.languageId}.png" width="30px">`,
                        data.resultObj.noNews,
                        moment(`${data.resultObj.realTime.toString("yyyy-MM-dd HH:mm")}`).fromNow(),
                        '<div class="' + statusObj[data.resultObj.status].class + '" data-title="' + statusObj[data.resultObj.status].title + '" data-bs-toggle="tooltip" title="' + statusObj[data.resultObj.status].title + '"> <span hidden>' + statusObj[data.resultObj.status].title + '</span></div>',
                        `<div>
                             <a class="btn btn-link" onclick=Detail(${data.resultObj.topicId}) data-bs-toggle="tooltip" data-bs-placement="top" title="Edit">
                                    <i class="os-icon os-icon-ui-49"></i>
                                </a>
                            <a class="danger" onclick=DeleteData(${data.resultObj.topicId}) data-bs-toggle="tooltip" data-bs-placement="top" title="Delete">
                                <i class="os-icon os-icon-ui-15"></i>
                            </a>
                        </div>`
                    ]
                ).draw(false);

                toastr['success'](
                    'Create Topic Successfully', 'Success', {
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
                    'Create Topic Unsuccessfully', 'Error'
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


//AJAX chỉnh sửa chủ đề
function EditTopic(frm, caller) {
    caller.preventDefault();
    var fdata = new FormData();

    var topicId = $(frm).find('#TopicId').val();
    var tag = $(frm).find('#Tag')[0].value;
    var description = $(frm).find('#Description')[0].value;
    var languageId = $(frm).find('#LanguageId')[0].value;
    var label = $(frm).find('#Label')[0].value;
    
    var thumbImage = $(frm).find('input:file[name="ThumbImage"]')[0].files[0];
    
    
    console.log(thumbImage);
    
    fdata.append("TopicId", topicId);
    fdata.append("Tag", tag);
    fdata.append("Description", description);
    fdata.append("LanguageId", languageId);
    fdata.append("Label", label);
    fdata.append("ThumbImage", thumbImage);

    $.ajax(
        {
            type: frm.method,
            url: frm.action,
            data: fdata,
            processData: false,
            contentType: false,
            success: function (data) {
                $('.bd-example-modal-lg').modal('hide');
                // location.reload();
                toastr['success'](
                    'Update Topic Successfully', 'Success', {
                        closeButton: true,
                        tapToDismiss: false,
                        positionClass: "toast-bottom-left",
                        rtl: $('html').attr('data-textdirection') === 'rtl'
                    }
                );
            },
            error: function (data) {
                toastr['error'](
                    'Update Topic Unsuccessfully', 'Error'
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

function DeleteData(topicId) {
    if (confirm("Are you sure want to archive this topic?")) {
        Archive(topicId);
    } else {
        return false;
    }
}

function Archive(topicId){
    var url = "/Topic/Archive";
    $.post(url, {topicId: topicId}, function (data) {
        location.reload();
    });
}

function Delete(topicId) {
    var url = "/Topic/Delete";
    $.post(url, {topicId: topicId}, function (data) {
        location.reload();
    });
}