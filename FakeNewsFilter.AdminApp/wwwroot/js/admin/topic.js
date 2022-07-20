/*=========================================================================================
  File Name: topic.js
  Description: Topic Manage JS
  ----------------------------------------------------------------------------------------
  Item Name: Fake News Filter - Admin
  Author: Bui Phu Khuyen
  Author URL: fb.com/buiphukhuyen
==========================================================================================*/

//Load Datatable List Topic
var select = $('.select2');


$(function () {
    //Select
    // Loading array data
    var data = [
        {id: 0, text: 'breaking'},
        {id: 1, text: 'featured'},
        {id: 2, text: 'normal'}
    ];

    selectArray = $('.select2-data-array'),

        selectArray.wrap('<div class="position-relative"></div>').select2({
            dropdownAutoWidth: true,
            dropdownParent: selectArray.parent(),
            width: '100%',
            data: data
        });

    var changePicture = $('#ThumbTopic'),
        userAvatar = $('.topic-thumb'),
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

                setTimeout(function () {
                    toastr['success'](
                        'Create Topic Successfully', 'Success', {
                            closeButton: true,
                            tapToDismiss: false,
                            positionClass: "toast-bottom-left",
                            rtl: $('html').attr('data-textdirection') === 'rtl'
                        }
                    );
                }, 2000);
            },
            error: function (data) {
                $('#loading').hide();

                setTimeout(function () {
                    toastr['error'](
                        'Create Topic Unsuccessfully', 'Error'
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
