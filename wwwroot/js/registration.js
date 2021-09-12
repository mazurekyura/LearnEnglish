$(document).ready(function () {
    $('.icon-registration').hide();

    $('.icon-ok-cancel').keyup(function (e) {
        var self = $(this);
        var currentName = self.val();

        var url = '/user/IsUniq?name=' + currentName;
        var promise = $.get(url);
        promise.done(function (respone) {
            $('.icon-registration').hide();
            if (respone) {
                $('.icon-registration.ok').show();
            }
            else {
                $('.icon-registration.cancel').show();
            }
        });
    });
});