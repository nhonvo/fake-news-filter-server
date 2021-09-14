
/*=========================================================================================
  File Name: edittopic.js
  Description: Topic Manage JS
  ----------------------------------------------------------------------------------------
  Item Name: Fake News Filter - Admin
  Author: Bui Phu Khuyen
  Author URL: fb.com/buiphukhuyen
==========================================================================================*/

//Change Picture
$(function () {

    var changePicture = $('#ThumbImage'),
        userAvatar = $('.thumb-image'),
        form = $('#form-validate');

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
                    Label: {
                        required: true
                    },
                    Tag: {
                        required: true
                    },
                    Description: {
                        required: true,
                    }
                }
            });
        });

        $(this).on('submit', function (event) {
            event.preventDefault();
        });
    }
});

