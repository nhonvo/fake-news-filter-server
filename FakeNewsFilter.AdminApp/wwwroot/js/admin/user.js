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
    
    $("#list_users").DataTable({        
        "pageLength": 25,
        columns: [
            { "data": 'userId' },
            { "data": 'fullName' },
            { "data": 'email' },
            { "data": 'roles' },
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
                        '<select id="FilterStatus" class="form-select text-capitalize mb-md-0 mb-2xx"><option value=""> Select Status </option></select>'
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

function DeleteData(UserId) {
    if (confirm("Are you sure want to delele this user?")) {
        Delete(UserId);
    }
    else {
        return false;
    }
}

function Delete(UserId) {
    var url = "/User/Delete";
    $.post(url, { UserId: UserId }, function (data) {
        location.reload();
    });
}

