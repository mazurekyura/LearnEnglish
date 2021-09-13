$(document).ready(function () {

    const langCookieName = 'lang';

    var cookies = document.cookie.split('; ');
    var filterCookie = cookies.filter(x => x.split('=')[0] == langCookieName);

    if (filterCookie.length > 0) {
        var currentCookieLang = filterCookie[0].split('=')[1];
        $('.language').val(currentCookieLang);
    }

    $('.language').change(function () {
        var newlang = $('.language').val();
        document.cookie = langCookieName + '=' + newlang;
        location.reload();
    });
});