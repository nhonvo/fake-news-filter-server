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
    console.log(newsId);
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
        url: "/News/GetNewsById",
        dataType: 'json',
    }).then(function (data) {
        console.log(data);
        var result = data.data.find(x => x.newsId == _newsId).topicInfo
            .map(x => x.topicId);

        select.val(result).trigger('change');
    });
});


function EditNews(frm, caller, source) {
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
                toastr['success'](
                    'Update News Successfully', 'Success', {
                        closeButton: true,
                        tapToDismiss: false,
                        positionClass: "toast-bottom-left",
                        rtl: $('html').attr('data-textdirection') === 'rtl'
                    }
                );
            },
            error: function (data) {
                toastr['error'](
                    'Update News Unsuccessfully', 'Error'
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