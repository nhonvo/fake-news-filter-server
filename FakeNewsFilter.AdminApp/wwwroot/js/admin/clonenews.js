/*=========================================================================================
  File Name: news.js
  Description: News Manage JS
  ----------------------------------------------------------------------------------------
  Item Name: Fake News Filter - Admin
  Author: Bui Phu Khuyen
  Author URL: fb.com/buiphukhuyen
==========================================================================================*/

let source;

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
        console.log(cardBody);
        $('#Title').val(`${$(cardBody).find('#news-title').text()}`);
        $('#Description').val(`${$(cardBody).find('#news-description p').text()}`);
        $('#ImageLink').val(`${$(cardBody).find('#news-imageLink img').attr('src')}`);
        $('#Publisher').val(`${$(cardBody).find('#news-publisher h5').text()}`);
        $('#Url').attr("href", `${$(cardBody).find('#news-url').attr('href')}`);
        $('#UrlNews').val(`${$(cardBody).find('#news-url').attr('href')}`);
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

        } else if (actionControllerNameInBrowser === 'OigetitIndex') {
            const pageNum = $('.pageNum').last().val();
            const page = parseInt(pageNum) + 1;

            $.ajax({
                type: "GET",
                url: `OigetitSearch/?query=${queryLoadMore}&page=${page}`,
                success: function (data) {
                    $('#loading').hide();
                    parentDiv.insertAdjacentHTML('beforeend', `${data} <input type="hidden" class="pageNum" value="${page}">`);
                }
            });
        }
    }
});

//search news
$('#factCheckSearchBtn').click(function () {
    $('#loading').show();
    source = "GoogleApi";
    //convert normal search input to without whitespace and lowercase
    const query = $('#factCheckSearchInput').val().toLowerCase().replace(/\s/g, "");

    //assign id querySeach's input equals id searchInput's input (temporary variable)
    queryLoadMore = $('#factCheckQuerySearch').val(`${$('#factCheckSearchInput').val()}`).val();

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
    source = "NewsApi";
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

$('#oigetitSearchBtn').click(function () {
    $('#loading').show();
    source = "Oigetit";
    //convert normal search input to without whitespace and lowercase
    const query = $('#oigetitSearchInput').val().toLowerCase().replace(/\s/g, "");

    //assign id querySeach's input equals id searchInput's input (temporary variable)
    queryLoadMore = $('#oigetitQuerySearch').val(`${$('#oigetitSearchInput').val()}`).val();

    //clear all previous data
    parentDiv.innerHTML = "";
    $.ajax({
        type: "GET",
        url: `OigetitSearch/?query=${query}`,
        success: function (data) {
            $('#loading').hide();
            parentDiv.insertAdjacentHTML('beforeend', `${data} <input type="hidden" class="pageNum" value="1">`);
        }
    });
})

function GetOigetitCategoryNews(categoryId){
    //clear all previous data
    source = "Oigetit";
    parentDiv.innerHTML = "";
    $.ajax({
        type: "GET",
        url: `GetOigetitCategory/?categoryId=${categoryId}`,
        success: function (data) {
            $('#loading').hide();
            parentDiv.insertAdjacentHTML('beforeend', data);
        }
    });
}

function CreateNews(frm, caller) {
    $('#loading').show();

    console.log("Source ne" + source);
    
    caller.preventDefault();
    var fdata = new FormData();

    var ImageLink = $(frm).find('input#ImageLink')[0].value;
    fdata.append("ImageLink", ImageLink);

    fdata.append("SourceCreate", source);
    var UrlNews = $(frm).find('input#UrlNews')[0].value;
    fdata.append("UrlNews", UrlNews);
    var Title = $(frm).find('input#Title')[0].value;
    var Publisher = $(frm).find('#Publisher')[0].value;
    // var DatePublished = $(frm).find('#DatePublished')[0].value;
    var officialRating = $(frm).find('#OfficialRating')[0].value;
    var languageId = $(frm).find('#LanguageId')[0].value;
    var topicIdList = $(frm).find('#TopicId').select2("val");

    fdata.append("Title", Title);
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
                frm.reset();
                $('#TopicId').val(null).trigger('change');
                $('#loading').hide();
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