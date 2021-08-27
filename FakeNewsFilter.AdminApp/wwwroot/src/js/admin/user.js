/*=========================================================================================
  File Name: user.js
  Description: User Manage JS
  ----------------------------------------------------------------------------------------
  Item Name: Fake News Filter - Admin
  Author: Bui Phu Khuyen
  Author URL: fb.com/buiphukhuyen
==========================================================================================*/

//Load Datatable List Users
$(document).ready(function () {
    var dtUserTable = $('.user-list-table'),
        newUserSidebar = $('.new-user-modal'),
        newUserForm = $('.add-new-user'),
        statusObj = {
            0: { title: 'Pending', class: 'badge-light-warning' },
            1: { title: 'Active', class: 'badge-light-success' },
            2: { title: 'Inactive', class: 'badge-light-secondary' }
        };

    $("#list_users").DataTable({        
        ajax: {
            url: "/User/GetUsers",
            type: "get",
            contentType: "application/json",
            dataType: "json",
            data: function (data) {
                return JSON.stringify(data);
            }
        },
       
        columns: [
            { "data": 'userId' },
            { "data": 'fullName' },
            { "data": 'email' },
            { "data": 'roles' },
            {
                "data": 'status',
            },
            {
                "data": ''
            }
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
                    var $name = full['fullName'],
                        $uname = full['userName'],
                        $image = full['avatar'],
                        $assetPath = "http://localhost:5001/";
                    if ($image) {
                        // For Avatar image
                        var $output =
                            '<img src="' + $assetPath + 'images/avatars/' + $image + '" alt="Avatar" height="32" width="32">';
                    } else {
                        // For Avatar badge
                        var stateNum = Math.floor(Math.random() * 6) + 1;
                        var states = ['success', 'danger', 'warning', 'info', 'dark', 'primary', 'secondary'];
                        var $state = states[stateNum],
                            $name = full['fullName'],
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
                        '<small class="emp_post text-muted">&#x00040;' +
                        $uname +
                        '</small>' +
                        '</div>' +
                        '</div>';
                    return $row_output;
                }
            },
            {
                // User Role
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    var $role = full['roles'];
                    var roleBadgeObj = {
                        Subscriber: feather.icons['user'].toSvg({ class: 'font-medium-3 text-primary me-50' }),
                        Author: feather.icons['settings'].toSvg({ class: 'font-medium-3 text-warning me-50' }),
                        Maintainer: feather.icons['database'].toSvg({ class: 'font-medium-3 text-success me-50' }),
                        Editor: feather.icons['edit-2'].toSvg({ class: 'font-medium-3 text-info me-50' }),
                        Admin: feather.icons['slack'].toSvg({ class: 'font-medium-3 text-danger me-50' })
                    };
                    var list = '';
                    $.each($role, function (key, value) {
                        list += "<div> <span class='text-truncate align-middle'>" + roleBadgeObj[value] + value + '</span></div>';
                    });
                    return list;
                }
            },
            {
                // User Status
                targets: 4,
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
                    var $userid = full['userId'];
                    return (
                    '<div class="d-flex align-items-center col-actions">' +
                        '<a class="me-1" href="/User/Edit/' + $userid+ ' "data - bs - toggle="tooltip" data - bs - placement="top" title = "Edit" > ' +
                        feather.icons['edit'].toSvg({ class: 'font-medium-1 text-warning' }) +
                        '</a>' +
                        '<a class="me-1" href="#" data-bs-toggle="tooltip" data-bs-placement="top" title="Delete">' +
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
                text: 'Add New User',
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
        // For responsive popup
        responsive: {
            details: {
                display: $.fn.dataTable.Responsive.display.modal({
                    header: function (row) {
                        var data = row.data();
                        return 'Details of ' + data['full_name'];
                    }
                }),
                type: 'column',
                renderer: $.fn.dataTable.Responsive.renderer.tableAll({
                    tableClass: 'table',
                    columnDefs: [
                        {
                            targets: 2,
                            visible: false
                        },
                        {
                            targets: 3,
                            visible: false
                        }
                    ]
                })
            }
        },
        //Tooltip
        drawCallback: function () {
            $(document).find('[data-bs-toggle="tooltip"]').tooltip();
        },
        
        initComplete: function () {
            // Adding role filter once table initialized

            this.api()
                .columns(3)
                .every(function () {
                    var that = this;

                    // Create the `select` element
                    var select = $('<select id="UserRole" class="form-select text-capitalize mb-md-0 mb-2"><option value=""> Select Role </option></select>')
                        .appendTo('.user_role')
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
                .columns(4)
                .every(function () {
                   
                    var column = this;
                    var select = $(
                        '<select id="FilterTransaction" class="form-select text-capitalize mb-md-0 mb-2xx"><option value=""> Select Status </option></select>'
                    )
                        .appendTo('.user_status')
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

