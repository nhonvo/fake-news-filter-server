/*=========================================================================================
  File Name: topic.js
  Description: Topic Manage JS
  ----------------------------------------------------------------------------------------
  Item Name: Fake News Filter - Admin
  Author: Bui Phu Khuyen
  Author URL: fb.com/buiphukhuyen
==========================================================================================*/

//Load Datatable List Topic
$(document).ready(function () {
    $('#loading').hide();

    var 
        statusObj = {
            0: { title: 'Pending', class: 'badge-light-warning' },
            1: { title: 'Active', class: 'badge-light-success' },
            2: { title: 'Inactive', class: 'badge-light-secondary' }
        };

    $("#list_topic").DataTable({
        ajax: {
            url: "/Topic/GetTopics",
            type: "get",
            contentType: "application/json",
            dataType: "json",
            data: function (data) {
                return JSON.stringify(data);
            }
        },

        columns: [
            { "data": 'topicId' },
            { "data": 'label' },
            { "data": 'description' },
            { "data": 'noNews' },
            { "data": 'realTime' },
            { "data": 'status' },
            { "data": ''}
        ],
        columnDefs: [
            {
                // For Responsive
                className: 'control',
                orderable: false,
                visible: false,
                responsivePriority: 2,
                targets: 0
            },
            {
                // User full name and username
                targets: 1,
                responsivePriority: 4,
                render: function (data, type, full, meta) {
                    var $name = full['tag'],
                        $uname = full['label'],
                        $image = full['thumbImage'],
                        $assetPath = "http://localhost:5001/";
                    if ($image) {
                        // For Avatar image
                        var $output =
                            '<img src="' + $assetPath + 'images/topics/' + $image + '" alt="Thumb Topic" height="32" width="32">';
                    } else {
                        // For Avatar badge
                        var stateNum = Math.floor(Math.random() * 6) + 1;
                        var states = ['success', 'danger', 'warning', 'info', 'dark', 'primary', 'secondary'];
                        var $state = states[stateNum],
                            $name = full['tag'],
                            $initials = $name.match(/\b\w/g) || [];
                        $initials = (($initials.shift() || '') + ($initials.pop() || '')).toUpperCase();
                        $output = '<span class="avatar-content">' + $initials + '</span>';
                    }
                    var colorClass = $image === '' ? ' bg-light-' + $state + ' ' : '';
                    // Creates full output for row
                    var $row_output =
                        '<div class="d-flex justify-content-left align-items-center">' +
                        '<div class="avatar-wrapper">' +
                        '<div class="avatar ' +
                        colorClass +
                        ' me-1">' +
                        $output +
                        '</div>' +
                        '</div>' +
                        '<div class="d-flex flex-column">' +
                        '<a href="' +
                        '" class="user_name text-truncate"><span class="fw-bold">' +
                        $name +
                        '</span></a>' +
                        '<small class="emp_post text-muted">#' +
                        $uname +
                        '</small>' +
                        '</div>' +
                        '</div>';
                    return $row_output;
                }
            },
            {
                targets: 2,
                orderable: false
            },
            {
                targets: 4,
                render: function (data, type, full, meta) {
                    var $time = full['realTime'];
                    return moment($time).fromNow();
                }
            },
            {
                // User Status
                targets: 5,
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
                    var $topicId = full['topicId'];
                    return (
                        '<div class="d-flex align-items-center col-actions">' +
                        '<a class="me-1" href="/Topic/Edit/' + $topicId + ' "data-bs-toggle="tooltip" data-bs-placement="top" title = "Edit" > ' +
                        feather.icons['edit'].toSvg({ class: 'font-medium-1 text-warning' }) +
                        '</a>' +
                        '<a class="me-1" onclick=DeleteData("' + $topicId + '"); data-bs-toggle="tooltip" data-bs-placement="top" title="Delete">' +
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

        // Buttons with Dropdown
        buttons: [
            {
                text: 'Add New Topic',
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
                .columns(1)
                .every(function () {
                    var that = this;

                    // Create the `select` element
                    var select = $('<select id="FilterLabelTopic" class="form-select text-capitalize mb-md-0 mb-2"><option value=""> Select Label </option></select>')
                        .appendTo('.topic_label')
                        .on('change', function () {
                            that
                                .search($(this).val())
                                .draw();
                        });

                    // Add data
                    var newArray =
                        this
                            .data()
                            .sort()
                            .map(function (d) {
                                return d.toString().toLowerCase().replace(/\b\w/g, function (l) { return l.toUpperCase() })
                            })
                            .unique()
                            .each(function (d) {
                                select.append('<option value="' + d + '" class="text-capitalize">' + d + '</option>');
                            });

                    // Restore state saved values
                    var state = this.state.loaded();
                    if (state) {
                        var val = state.columns[this.index()];
                        select.val(val.search.search);
                    }
                });
            // Adding status filter once table initialized
            this.api()
                .columns(5)
                .every(function () {

                    var column = this;
                    var select = $(
                        '<select id="FilterTopicStatus" class="form-select text-capitalize mb-md-0 mb-2xx"><option value=""> Select Status </option></select>'
                    )
                        .appendTo('.topic_status')
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
        }
      
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
