// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
/*
 * jQuery autoResize (textarea auto-resizer)
 * @copyright James Padolsey http://james.padolsey.com
 * @version 1.04.1 (kama fix)
 */
(function (b) { b.fn.autoResize = function (f) { var a = b.extend({ onResize: function () { }, animate: !0, animateDuration: 150, animateCallback: function () { }, extraSpace: 20, limit: 1E3 }, f); this.filter("textarea").each(function () { var d = b(this).css({ "overflow-y": "hidden", display: "block" }), f = d.height(), g = function () { var c = {}; b.each(["height", "width", "lineHeight", "textDecoration", "letterSpacing"], function (b, a) { c[a] = d.css(a) }); return d.clone().removeAttr("id").removeAttr("name").css({ position: "absolute", top: 0, left: -9999 }).css(c).attr("tabIndex", "-1").insertBefore(d) }(), h = null, e = function () { g.height(0).val(b(this).val()).scrollTop(1E4); var c = Math.max(g.scrollTop(), f) + a.extraSpace, e = b(this).add(g); h !== c && (h = c, c >= a.limit ? b(this).css("overflow-y", "") : (a.onResize.call(this), a.animate && "block" === d.css("display") ? e.stop().animate({ height: c }, a.animateDuration, a.animateCallback) : e.height(c))) }; d.unbind(".dynSiz").bind("keyup.dynSiz", e).bind("keydown.dynSiz", e).bind("change.dynSiz", e) }); return this } })(jQuery);

// инициализация
jQuery(function () {
	jQuery('textarea').autoResize();
});

$(function () {
    $(".button").click(function () {
        // validate and process form here
    });
});

$(function () {
    $('.error').hide();
    $(".button").click(function () {
        // validate and process form here

        $('.error').hide();
        var name = $("input#name").val();
        if (name == "") {
            $("label#name_error").show();
            $("input#name").focus();
            return false;
        }
        var email = $("input#email").val();
        if (email == "") {
            $("label#email_error").show();
            $("input#email").focus();
            return false;
        }
        var phone = $("input#phone").val();
        if (phone == "") {
            $("label#phone_error").show();
            $("input#phone").focus();
            return false;
        }

        var dataString = 'name=' + name + '&email=' + email + '&phone=' + phone;
        //alert (dataString);return false;
        $.ajax({
            type: "POST",
            url: "bin/process.php",
            data: dataString,
            success: function () {
                $('#contact_form').html("<div id='message'></div>");
                $('#message').html("<h2>Contact Form Submitted!</h2>")
                    .append("<p>We will be in touch soon.</p>")
                    .hide()
                    .fadeIn(1500, function () {
                        $('#message').append("<img id='checkmark' src='images/check.png' />");
                    });
            }
        });
        return false;
    });
});
