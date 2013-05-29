function BindEvents() {
    $(document).ready(function () {
        $(".opis").each(function () {
            $(this).hide();
        });
    });
    //tutaj skrypt do nadawania obiektom z klasa 'opis' i 'id' status visible (lub tez dopisywanie nowej klasy
    //powodującej ze 'opis' bedzie jako chmurka
    $("img").mouseover(function (e) {
        var href = $(this).attr('id');
        var id = href.substring(0, href.length - 1);
        var text = $("#" + id).text();
        if (text == "") {
            text = "Brak opisu...";
        }
        else {
            if (text.length > 10) {
                var ii = 10;
                while (text.charAt(ii) != " " && ii != text.length)
                    ii++;
                if (ii != text.length)
                    text = text.substring(0, ii) + "...";
            }


        }
        $(this).after('<div id="tooltip"><div class="tipBody">' + text + '</div></div>');
        $('#tooltip').css('top', e.pageY + 10);
        $('#tooltip').css('left', e.pageX + 20);

        $('#tooltip').fadeIn('500');
        $('#tooltip').fadeTo('10', 0.8);

    }).mousemove(function (e) {

        $('#tooltip').css('top', e.pageY + 10);
        $('#tooltip').css('left', e.pageX + 20);

    }).mouseout(function () {

        $('div#tooltip').remove();

    });

}