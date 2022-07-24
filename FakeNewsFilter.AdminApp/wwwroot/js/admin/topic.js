/*=========================================================================================
  File Name: topic.js
  Description: Topic Manage JS
  ----------------------------------------------------------------------------------------
  Item Name: Fake News Filter - Admin
  Author: Bui Phu Khuyen
  Author URL: fb.com/buiphukhuyen
==========================================================================================*/

var select = $('.select2');

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

    var changePicture_create = $('.createThumbTopic'),
        userAvatar_create = $('.topic-thumb'),
        changePicture_edit = $('.editThumbTopic'),
        userAvatar_edit = $('.topic-thumb-edit')

    // Thay đổi hình ảnh
   
    if (changePicture_edit.length) {
        $(changePicture_edit).on('change', function (e) {
            var reader = new FileReader(),
                files = e.target.files;
            reader.onload = function () {
                if (userAvatar_edit.length) {
                    userAvatar_edit.attr('src', reader.result);
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
            data: { topicId: topicId },
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
    console.log(frm);
    caller.preventDefault();
    var fdata = new FormData();

    var topicId = $(frm).find('#TopicId')[0].value;
    var tag = $(frm).find('#Tag')[0].value;
    var description = $(frm).find('#Description')[0].value;
    var languageId = $(frm).find('#LanguageId')[0].value;
    var label = $(frm).find('#Label')[0].value;
    var thumbImage = $(frm).find('input:file[name="ThumbImage"]')[0].files[0];

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
                setTimeout(function () {
                    toastr['success'](
                        'Update Topic Successfully', 'Success', {
                        closeButton: true,
                        tapToDismiss: false,
                        positionClass: "toast-bottom-left",
                        rtl: $('html').attr('data-textdirection') === 'rtl'
                    }
                    );
                }, 2000);
            },
            error: function (data) {
                setTimeout(function () {
                    toastr['error'](
                        'Update Topic Unsuccessfully', 'Error'
                        , {
                            closeButton: true,
                            tapToDismiss: false,
                            positionClass: "toast-bottom-left",
                            rtl: $('html').attr('data-textdirection') === 'rtl'
                        }
                    );
                }, 2000);
            }
        })
}

function DeleteData(topicId) {
    if (confirm("Are you sure want to delele this topic?")) {
        Delete(topicId);
    } else {
        return false;
    }
}

function Delete(topicId) {
    var url = "/Topic/Delete";
    $.post(url, {topicId: topicId}, function (data) {
        location.reload();
    });
}