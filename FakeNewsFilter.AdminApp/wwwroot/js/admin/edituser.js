
/*=========================================================================================
  File Name: edituser.js
  Description: User Manage JS
  ----------------------------------------------------------------------------------------
  Item Name: Fake News Filter - Admin
  Author: Bui Phu Khuyen
  Author URL: fb.com/buiphukhuyen
==========================================================================================*/

//Change Picture
$(function () {

    var changePicture = $('#MediaFile'),
        userAvatar = $('.user-avatar'),
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
                    UserName: {
                        required: true
                    },
                    Name: {
                        required: true
                    },
                    Email: {
                        required: true,
                        email: true
                    }
                }
            });
        });

        $(this).on('submit', function (event) {
            event.preventDefault();
        });
    }
});

