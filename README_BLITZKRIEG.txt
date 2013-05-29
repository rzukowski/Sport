// 18:57 2013-03-16
darek: Hej Rafa�, doda�em ten plik do cel�w testowych, Gita.
My�l� �e mo�e zosta�, mo�emy tu wpisywa� r��ne informacje dla nas.
Proponuj�, najaktualniejsze u g�ry, w formacie np. tak jak zacz��em.
Acha, Rafa� jeszcze jedno, nie widz� pliku *.sln ...

Co do pliku .sln --> stwórz u siebie prosze nowy website, i z menu wybierz --> add existing website (i tutaj szukasz katalogu z plikami zrodlowymi)(
// 
20:33 2013-03-23 Dodałem checkboxlist ze spisem sportów (źródłem jest tabela Sport z naszej bazy). checkbox obsługiwany jest przez code behind. Możecie zauważyć, że jest tam button 'zmień' - w domyśle ma updajtować tabelę User_Sport, nie zdążyłem jeszcze dodać metody, prawdopodobnie zrobię to jutro.
CheckBox wyglada na razie okropnie, Krystian - liczę na Ciebie w tej kwestii.
Krystian: jeszcze jedno - na stronie masterPage możesz zauważyć kontrolkę:<asp:Label ID="NowaWiadomosc" runat="server"></asp:Label>.
W momencie gdy user dostaje wiadomość do swojej skrzynki, kontrolka ta zmienia się w liczbę prezentującą liczbę nieprzeczytanych wiadomości. Spróbuj ją jakoś sensownie ustawić (możesz to zrobić używając css'a ustawiając atrybut CssClass czyli: <asp:Label ID="NowaWiadomosc" runat="server" CssClass="">
