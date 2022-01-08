
/*=========================================================================================
  File Name: editnews.js
  Description: Topic Manage JS
  ----------------------------------------------------------------------------------------
  Item Name: Fake News Filter - Admin
  Author: Bui Phu Khuyen
  Author URL: fb.com/buiphukhuyen
==========================================================================================*/

$(window).on('load', function () {
    $('#loading').hide();
}) 

function EditTopic(frm, caller) {
    $('#loading').show();
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
                $('#loading').hide();

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
                $('#loading').hide();

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