
/*=========================================================================================
  File Name: edittopic.js
  Description: Topic Manage JS
  ----------------------------------------------------------------------------------------
  Item Name: Fake News Filter - Admin
  Author: Bui Phu Khuyen
  Author URL: fb.com/buiphukhuyen
==========================================================================================*/

$(document).ready(function () {
    $('#loading').hide();

    var select = $('.select2');

    const _newsId = document.getElementById('newsId').value;

    select.each(function () {
        var $this = $(this);
        $this.wrap('<div class="position-relative"></div>');
        $this.select2({
            dropdownAutoWidth: true,
            width: '100%',
            dropdownParent: $this.parent()
        });
    });

    //populate topic to edit news view
    $.ajax({
        type: 'GET',
        url: "/News/GetNews",
        dataType: 'json',
    }).then(function (data) {

        var result = data.data.find(x => x.newsId == _newsId).topicInfo
            .map(x => x.topicId);

        select.val(result).trigger('change');
    });
});


function EditNews(frm, caller) {
    $('#loading').show();

    caller.preventDefault();
    var fdata = new FormData();

    var title = $(frm).find('input#Title')[0].value;
    var officialRating = $(frm).find('#OfficialRating')[0].value;
    var Source = $(frm).find('#Source')[0].value;
    var ImageLink = $(frm).find('#ImageLink')[0].value;
    var content = $(frm).find('#content')[0].value;
    var languageId = $(frm).find('#LanguageId')[0].value;
    var topicIdList = $(frm).find('#TopicId').select2("val");
    var Publisher = $(frm).find('#Publisher')[0].value;

    var thumbNews = $(frm).find('input:file[name="ThumbNews"]')[0].files[0];

    fdata.append("Title", title);
    fdata.append("OfficialRating", officialRating);
    fdata.append("Source", Source);
    fdata.append("ImageLink", ImageLink);
    fdata.append("Content", content);
    fdata.append("LanguageId", languageId);
    topicIdList.forEach((topicId) => fdata.append("TopicId", topicId));
    fdata.append("ThumbNews", thumbNews);
    fdata.append("Publisher", Publisher);

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
                        'Update News Successfully', 'Success', {
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
                    $('#loading').hide();

                    toastr['error'](
                        'Update News Unsuccessfully', 'Error'
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