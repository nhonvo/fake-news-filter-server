
/*=========================================================================================
  File Name: edittopic.js
  Description: Topic Manage JS
  ----------------------------------------------------------------------------------------
  Item Name: Fake News Filter - Admin
  Author: Bui Phu Khuyen
  Author URL: fb.com/buiphukhuyen
==========================================================================================*/

$(document).ready(function () {

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

    $.ajax({
        type: 'GET',
        url: "/News/GetNews",
        dataType: 'json',
    }).then(function (data) {

        var result = data.data.find(x => x.newsId == _newsId).topicInfo
            .map(x => x.topicId);

        select.val(result).trigger('change');
    });

    ClassicEditor
        .create(document.querySelector('#Content'))
        .then(editor => {
            editor.editing.view.change(writer => {
                writer.setStyle('min-with', '500px', editor.editing.view.document.getRoot());
                writer.setStyle('min-height', '300px', editor.editing.view.document.getRoot());
            });
            window.editor = editor;
        })
        .catch(error => {
            console.error(error);
        });
});