/*=========================================================================================
  File Name: news.js
  Description: News Manage JS
  ----------------------------------------------------------------------------------------
  Item Name: Fake News Filter - Admin
  Author: Bui Phu Khuyen
  Author URL: fb.com/buiphukhuyen
==========================================================================================*/

//Load Datatable List Topic
$(document).ready(function () {
    var select = $('.select2');

    var
        language = {
            "EN": 'flag-icon flag-icon-us',
            "VN": 'flag-icon flag-icon-vn'
        };

    var
        statusObj = {
            0: { title: 'Pending', class: 'badge-light-warning' },
            1: { title: 'Active', class: 'badge-light-success' },
            2: { title: 'Inactive', class: 'badge-light-secondary' }
        };

    select.each(function () {
        var $this = $(this);
        $this.wrap('<div class="position-relative"></div>');
        $this.select2({
            // the following code is used to disable x-scrollbar when click in select input and
            // take 100% width in responsive also
            dropdownAutoWidth: true,
            width: '100%',
            dropdownParent: $this.parent()
        });
    });

    $("#list_news").DataTable({
        responsive: true,
        ajax: {
            url: "/News/GetNews",
            type: "get",
            contentType: "application/json",
            dataType: "json",
            data: function (data) {
                return JSON.stringify(data);
            }
        },
        columns: [
            { "data": 'newsId' },
            { "data": 'newsId' },
            { "data": 'newsId' },
            { "data": 'topicInfo' },
            { "data": 'officialRating' },
            { "data": 'languageCode' },
            { "data": 'timestamp' },
            { "data": 'status' },
            { "data": '' }
        ],
        columnDefs: [
            {
                // For Responsive
                className: 'control',
                orderable: false,
                responsivePriority: 2,
                targets: 0
            },
            {
                // For Checkboxes
                targets: 1,
                orderable: false,
                responsivePriority: 3,
                render: function (data, type, full, meta) {
                    return (
                        '<div class="form-check"> <input class="form-check-input dt-checkboxes" type="checkbox" value="" id="checkbox' +
                        data +
                        '" /><label class="form-check-label" for="checkbox' +
                        data +
                        '"></label></div>'
                    );
                },
                checkboxes: {
                    selectAllRender:
                        '<div class="form-check"> <input class="form-check-input" type="checkbox" value="" id="checkboxSelectAll" /><label class="form-check-label" for="checkboxSelectAll"></label></div>'
                }
            },
            {
                targets: 2,
                visible: false
            },
            {
                // User full name and username
                targets: 3,
                responsivePriority: 4,
                render: function (data, type, full, meta) {
                    var $name = full['name'],
                        $uname = full['topicInfo'].map(function (item) {
                            return ' ' + item['topicName'];
                        }),
                        $description = full['description'],
                        $link = full['postURL'];
                    // Creates full output for row
                    var $row_output =
                        '<div class="align-items-center"> </div>' +
                        '<div class="d-flex flex-column">' +
                        '<a target="_blank" href="' + $link +
                        '" class="user_name text-truncate"><span class="fw-bold">' +
                        $name +
                        '</span></a> <span class="fw-bold"> <i data-feather="copy"></i>' +
                        $uname +
                        '</span></a> ' +
                        '<small class="emp_post text-muted">' +
                        $description +
                        '</small>' +
                        '</div>' +
                        '</div>';
                    return $row_output;
                }
            },
            {
                // User Status
                targets: 5,
                render: function (data, type, full, meta) {
                    var $status = full['languageCode'];
                    return (
                        '<div><i class="' + language[$status] + '"></i><span hidden>'+ $status + '</span></div>'
                    );
                }
            },
            {
                targets: 6,
                render: function (data, type, full, meta) {
                    var $time = full['timestamp'];
                    return moment($time).fromNow();
                }
            },
            {
                // User Status
                targets: 7,
                orderable: false,
                render: function (data, type, full, meta) {
                    var $status = full['status'];
                    return (
                        '<span class="badge rounded-pill ' +
                        statusObj[$status].class +
                        '" text-capitalized>' +
                        statusObj[$status].title +
                        '</span>'
                    );
                }
            },
            {
                // Actions
                targets: -1,
                orderable: false,
                render: function (data, type, full, meta) {
                    var $newsId = full['newsId'];
                    return (
                        '<div class="d-flex align-items-center col-actions">' +
                        '<a class="me-1" href="/News/Edit/' + $newsId + ' "data-bs-toggle="tooltip" data-bs-placement="top" title = "Edit" > ' +
                        feather.icons['edit'].toSvg({ class: 'font-medium-1 text-warning' }) +
                        '</a>' +
                        '<a class="me-1" onclick=DeleteData("' + $newsId + '"); data-bs-toggle="tooltip" data-bs-placement="top" title="Delete">' +
                        feather.icons['trash'].toSvg({ class: 'font-medium-1 text-danger' }) +
                        '</a>' +
                        '<a class="me-1" href="#" data-bs-toggle="tooltip" data-bs-placement="top" title="View">' +
                        feather.icons['eye'].toSvg({ class: 'font-medium-1' }) +
                        '</a>'
                        +
                        '</div>'
                    );
                }
            }

        ],
        dom:
            '<"d-flex justify-content-between align-items-center header-actions mx-1 row mt-75"' +
            '<"col-sm-12 col-md-4 col-lg-6" l>' +
            '<"col-sm-12 col-md-8 col-lg-6 ps-xl-75 ps-0"<"dt-action-buttons text-xl-end text-lg-start text-md-end text-start d-flex align-items-center justify-content-md-end align-items-center flex-sm-nowrap flex-wrap me-1"<"me-1"f>B>>' +
            '>t' +
            '<"d-flex justify-content-between mx-2 row mb-1"' +
            '<"col-sm-12 col-md-6"i>' +
            '<"col-sm-12 col-md-6"p>' +
            '>',
        language: {
            sLengthMenu: 'Show _MENU_',
            search: 'Search',
            searchPlaceholder: 'Search...'
        },
        responsive: {
            details: {
                display: $.fn.dataTable.Responsive.display.modal({
                    
                }),
                type: 'column',
                renderer: function (api, rowIdx, columns) {
                    var data = $.map(columns, function (col, i) {
                        return col.title !== '' // ? Do not show row in modal popup if title is blank (for check box)
                            ? '<tr data-dt-row="' +
                            col.rowIdx +
                            '" data-dt-column="' +
                            col.columnIndex +
                            '">' +
                            '<td>' +
                            col.title +
                            ':' +
                            '</td> ' +
                            '<td>' +
                            col.data +
                            '</td>' +
                            '</tr>'
                            : '';
                    }).join('');

                    return data ? $('<table class="table"/>').append('<tbody>' + data + '</tbody>') : false;
                }
            }
        },
        // Buttons with Dropdown
        buttons: [
            {
                text: 'Add News',
                className: 'add-new btn btn-primary mt-50',
                attr: {
                    'data-bs-toggle': 'modal',
                    'data-bs-target': '#modals-slide-in'
                },
                init: function (api, node, config) {
                    $(node).removeClass('btn-secondary');
                }
            }
        ],
        language: {
            paginate: {
                // remove previous & next text from pagination
                previous: '&nbsp;',
                next: '&nbsp;'
            }
        },
        //Tooltip

        drawCallback: function () {
            $(document).find('[data-bs-toggle="tooltip"]').tooltip();
        },

        initComplete: function () {
            // Adding role filter once table initialized
            this.api()
                .columns(5)
                .every(function () {
                    var column = this;
                    var select = $(
                        '<select id="FilterNewsLanguage" class="form-select text-capitalize mb-md-0 mb-2xx"><option value=""> Select Language </option></select>'
                    )
                        .appendTo('.news_language')
                        .on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex($(this).val());
                            column.search(val ? '^' + val + '$' : '', true, false).draw();
                        });

                    column
                        .data()
                        .unique()
                        .sort()
                        .each(function (d, j) {
                            select.append(
                                '<option value="' +
                                    d +
                                    '" class="text-capitalize">' +
                                    d +
                                '</option>'
                            );
                        });

                });
            // Adding status filter once table initialized
            this.api()
                .columns(7)
                .every(function () {

                    var column = this;
                    var select = $(
                        '<select id="FilterNewsStatus" class="form-select text-capitalize mb-md-0 mb-2xx"><option value=""> Select Status </option></select>'
                    )
                        .appendTo('.news_status')
                        .on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex($(this).val());
                            column.search(val ? '^' + val + '$' : '', true, false).draw();
                        });

                    column
                        .data()
                        .unique()
                        .sort()
                        .each(function (d, j) {
                            select.append(
                                '<option value="' +
                                statusObj[d].title +
                                '" class="text-capitalize">' +
                                statusObj[d].title +
                                '</option>'
                            );
                        });

                });
        },

    });

});

$(function () {
    //Select
    // Loading array data
    var data = [
        { id: 0, text: 'breaking' },
        { id: 1, text: 'featured' },
        { id: 2, text: 'normal' }
    ];

    selectArray = $('.select2-data-array'),

        selectArray.wrap('<div class="position-relative"></div>').select2({
            dropdownAutoWidth: true,
            dropdownParent: selectArray.parent(),
            width: '100%',
            data: data
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

function DeleteData(topicId) {
    if (confirm("Are you sure want to delele this topic?")) {
        Delete(topicId);
    }
    else {
        return false;
    }
}

function Delete(topicId) {
    var url = "/Topic/Delete";
    $.post(url, { topicId: topicId }, function (data) {
        location.reload();
    });
}




