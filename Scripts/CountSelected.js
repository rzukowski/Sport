//funckja do obliczania zaznaczonych kategorii sport
function CountSelected() {
    var hdnCount = 0;
    getSelectionCount();
    ChangeDiv();

    //zmiana tekstu diva
    function ChangeDiv() {
        if (hdnCount > 0) {
            var div = document.getElementById('<%=divDDL.ClientID%>');
            if (hdnCount == 1) { div.innerHTML = "1 element"; }
            else if (hdnCount < 5) { div.innerHTML = hdnCount + " elementy"; }
            else { div.innerHTML = hdnCount + " elementów"; }

        }
    }
    //zliczanie zaznaczonych elementów
    function getSelectionCount() {
        var cbl = document.getElementById('<%=pnlCustomers.FindControl("cblCustomerList").ClientID%>');
        var browser = navigator.appName;
        var pos = 0;
        if (browser.indexOf("Microsoft") >= 0) {
            pos = 0;
        }
        else {
            pos = 1;
        }
        var tbody = cbl.childNodes[pos];
        var length = (tbody.childNodes.length - pos);
        hdnCount = 0;
        for (i = 0; i < length; i++) {
            var td = tbody.childNodes[i].childNodes[pos];
            var chk = td.childNodes[0];
            if (chk.checked) {
                hdnCount++;
            }
        }

    }
}