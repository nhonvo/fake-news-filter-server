/*=========================================================================================
  File Name: news.js
  Description: News Manage JS
  ----------------------------------------------------------------------------------------
  Item Name: Fake News Filter - Admin
  Author: Bui Phu Khuyen
  Author URL: fb.com/buiphukhuyen
==========================================================================================*/


var select = $('.select2');
$(document).ready(function () {
    select.each(function () {
        var $this = $(this);
        $this.wrap('<div class="position-relative"></div>');
        $this.select2({
            dropdownAutoWidth: true,
            width: '100%',
            dropdownParent: $this.parent()
        });
    });
})

//change photo when click on button "Choose Image"
var changePicture = $('#ThumbNews'),
    userAvatar = $('.news-thumb'),
    form = $('.form-validate');

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

$(window).on('load', function () {
    $('#loading').hide();
})


let nextPageToken = $('.nextPageToken').last().val();
let queryLoadMore;

const parentDiv = document.querySelector('.match-height');

//populate data to modal
document.addEventListener('click', function (e) {
    if (e.target && e.target.classList.contains("add-new")) {

        const cardBody = e.target.closest(`#card-body-${e.target.id}`);

        $('#Title').val(`${$(cardBody).children('p').first().text()}`);

        $('#Description').val(`${$(cardBody).children('a').first().text()}`);

        $('#OfficialRating').val(`${$(cardBody).children('h4').first().text()}`);
        $('#Publisher').val(`${$(cardBody).children('h4').first().text()}`);
        $('#Url').attr("href", `${$(cardBody).children('a').first().attr('href')}`);
        $('#Url').text(`${$(cardBody).children('a').first().attr('href')}`);

    }
});

//load more news
$(window).scroll(function () {
    if ($(window).scrollTop() + $(window).height() > $(document).height() - 100) {
        const actionControllerNameInBrowser = $(window.location.pathname.split('/')).get(-1);

        $('#loading').show();
        if (actionControllerNameInBrowser === 'GoogleFactCheckIndex') {
            //get the last value of nextPageToken inputsÎ
            nextPageToken = [...$('.nextPageToken')][[...$('.nextPageToken')].length - 1].value;
            $.ajax({
                type: "GET",
                url: `LoadMore/?nextPageToken=${nextPageToken}&query=${queryLoadMore}`,
                success: function (data) {
                    $('#loading').hide();
                    parentDiv.insertAdjacentHTML('beforeend', data);
                }
            });
        } else if (actionControllerNameInBrowser === 'NewsApiIndex') {
            const pageNum = $('.pageNum').last().val();
            const page = parseInt(pageNum) + 1;
            $.ajax({
                type: "GET",
                url: `NewsApiSearch/?query=${queryLoadMore}&page=${page}`,
                success: function (data) {
                    $('#loading').hide();
                    parentDiv.insertAdjacentHTML('beforeend', `${data} <input type="hidden" class="pageNum" value="${page}">`);
                }
            });
        }
    }
});

//search news
$('#searchBtn').click(function () {
    $('#loading').show();

    //convert normal search input to without whitespace and lowercase
    const query = $('#searchInput').val().toLowerCase().replace(/\s/g, "");

    //assign id querySeach's input equals id searchInput's input (temporary variable)
    queryLoadMore = $('#querySearch').val(`${$('#searchInput').val()}`).val();

    //clear all previous data
    parentDiv.innerHTML = "";
    $.ajax({
        type: "GET",
        url: `FactCheckSearch/?query=${query}`,
        success: function (data) {
            $('#loading').hide();
            parentDiv.insertAdjacentHTML('beforeend', data);
        }
    });
})

$('#newsApiSearchBtn').click(function () {
    $('#loading').show();

    //convert normal search input to without whitespace and lowercase
    const query = $('#newsApiSearchInput').val().toLowerCase().replace(/\s/g, "");

    //assign id querySeach's input equals id searchInput's input (temporary variable)
    queryLoadMore = $('#newsApiQuerySearch').val(`${$('#newsApiSearchInput').val()}`).val();

    //clear all previous data
    parentDiv.innerHTML = "";
    $.ajax({
        type: "GET",
        url: `NewsApiSearch/?query=${query}`,
        success: function (data) {
            $('#loading').hide();
            parentDiv.insertAdjacentHTML('beforeend', `${data} <input type="hidden" class="pageNum" value="1">`);
        }
    });
})

function CreateNews(frm, caller) {
    $('#loading').show();

    caller.preventDefault();
    var fdata = new FormData();

    var Title = $(frm).find('input#Title')[0].value;
    var Source = $(frm).find('#Url').attr('href');

    var officialRating = $(frm).find('#OfficialRating')[0].value;
    var languageId = $(frm).find('#LanguageId')[0].value;
    var topicIdList = $(frm).find('#TopicId').select2("val");
    var Publisher = $(frm).find('#Publisher')[0].value;
    var DatePublished = $(frm).find('#DatePublished')[0].value;

    fdata.append("Title", Title);
    fdata.append("OfficialRating", officialRating);
    fdata.append("Source", Source);
    fdata.append("LanguageId", languageId);
    topicIdList.forEach((topicId) => fdata.append("TopicId", topicId));
    fdata.append("Publisher", Publisher);
    fdata.append("DatePublished", DatePublished);

    $.ajax(
        {
            type: frm.method,
            url: frm.action,
            data: fdata,
            processData: false,
            contentType: false,
            success: function (data) {

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