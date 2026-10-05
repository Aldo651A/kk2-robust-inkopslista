# Kunskapskontroll 2: Robust inköpslista
## Del 1 - laga ett trasigt programm
## Del 2 - bygg ut det
### De sex felen: Vad hände, varför, hur du löste det. 


- När jag startade programmet så kraschade den direkt så jag läste felmeddelandet och bestämde mig efter en stund att göra en try-catch 
Men jag satte den innanför foreach vilket var fel. Jag öppnade och läste
Felhantering i C# och det gick bättre. Nu fick jag igång programmet.

- Nu när jag fick igång programmet så försökte jag läsa igenom programet 
för att hitta fel men jag lyckades bara hitta eller rättare sagt misstänka ett fel och detta efter en lång stund och det var i ShoppingList.cs metoden public int total, så var (int i = 1;) fel tyckte jag. Jag ville nu ha bekräftelse på min misstanke, så jag började använda debug. På denna viss började jag upptäcka andra fel.

- Debug avslöjade direkt att (int choice = int.parse.Console.ReadLine())
eftersom den visade en stor ruta. Där man kunde läsa felmeddelande och jag uppmärksammade, om jag minns rätt (FormatException). Detta visade sig vara den andra felet som orsakar krasch. Sedan kom jag att tänka på (TryParse används för inmatning som förväntas kunna bli fel i int. TryParse) Så jag lade till en (string input) och (int, parse) tog jag bort och lade till (int.TryParse.).

- Sedan upptckta jag att i (else if (choice == 1)) så hade vi samma fel. Det konstiga var att debug inte kunde komma ner hit. Fast jag hade satt ut breakpoint. Jag rådfrågade AI, som jag kör med Benjamins prompt och den bekräftade felet. Så jag gjorde om den också. Men det räckte inte så jag fick även lägga till en som kallad innästlad if-sats för att säkerhetställa att programmet inte rasade om man skrev flera siffror som t ex 87, 897, 7898 osv, Nu hade jag hittat tredje felet som orsakar krasch.

- Nu när jag kände mig mer säkert så tog jag tag i tatal summan som blev fel. Jag tittade i metoden public int total, så var (int i = 1;) igen. Men denna gången ändrade jag felet. Nu blev total summan rätt. Men jag upptäckte att produktlistan, alltås namnet på varan var borta. Detta var ett nytt fel.

- När det gäller produktnamnet så tog det ganska lång tid att lösa. Jag försökta med allt undersökte flera metoder utan framgång, så jag råd frågade AI och den kom på olika förslag som jag redan hade använt men även nya. Tills slut så hittade vi felet. Detta fel finns under ShoppingList.cs metoden public void print. Det saknades (item Name och Price) mellan måsvingarna under for loopen. Jag skrev in dessa och listan visade nu produktnamnet.

- När jag kontrollerade kravlistan för godkänd. Upptäckte jag dem två sista felen den ena var att jag skulle "Ta bort varan" och när jag valde denna så rasade programmet. Först trodde jag att det var en enkel sak som att ändra int. parse till en TryParse vilket jag gjorde, men nej. Även här fick jag lägga in en innästlad if-sats men det hjälpte inte. Så jag gjorde det väldigt enkel och la i en try-catch som fångade ArgumentOutOfRangeException och detta gjorde det möjligt för att ta bort varan. Om nu detta stämmer, så är felet att listan har ett visst antal element och vi väljer att ta bort ett index som inte finns. Då rasar programmet.

- Den sista felet upptäckte jag, också när jag gick igenom kravlistan. När jag valde nummer 4 sök varan. Då fick jag svaret "Varan finns inte i listan" inte finns" men jag hade ju varan med i listan. denna kund man se klart och tydlig. Jag fångade felet med en try-catch nu kunde programmet söka varan. Men programmet kraschade inte av detta fel. Felet här är att koden försöker att använda en referens/variabel som inte finns alltså null.


## Del 2-Designval:Hur Add säger nej när taket överskrids, och varför du valde så.

I Shopping.cs så valde jag signaturen public bool Add(Item item). Anledningen till att jag gjorde detta är helt enkelt att vi har använt denna booleska metod ganska ofta. Därför kändes det rätt att välja just bool.

Det kan liknas vid att någon skulle be mig att välja en loop som snurrar hela tiden; som nybörjare i programmering skulle jag lätt säga en while-loop, förutsatt att den passar in i metoden. Dessutom tycker jag att man hanterar if-satser ganska bra vid det här laget – kanske inte perfekt, men tillräckligt bra för att kunna jobba med dem, och dessa ingår ju naturligt när man arbetar med bool.

Om vi dessutom tittar på utbildningen fram tills nu, så använder ni själva ofta if-satser med true eller false. Men den största anledningen till att jag valde bool var nog att jag tittade på uppgiften ”Kodexempel C# Bankkonto” och mitt eget bankkonto-projekt. Det var det som gjorde att jag bestämde mig för det booleska uttrycket.

Det kanske var lite fegt av mig att inte använda throw, men det kändes helt enkelt bättre med bool. Jag har dock en känsla av att jag kommer att stöta på throw längre fram och då även lära mig den metoden ordentligt.


## Vad behöver Program.cs göra med svaret?

När vi lägger till en vara hamnar den i metoden public bool Add(Item item). Här kontrollerar metoden om den nya varan ryms inom budgeten. Om varan överskrider gränsen returneras false, och om den är inom gränsen returneras true.

Nu tar Program.cs över i metoden för menyvalet if (choice == 1). Här skriver användaren in varans namn och pris.

Sedan kontrollerar TryParse om man har angett priset med siffror. Om man inte har skrivit siffror får användaren ett felmeddelande.

Därefter tar ett try-block över för att kontrollera om namnet är tomt eller om priset är negativt. Om så är fallet aktiveras throw i Item.cs, vilket avbryter try-blocket och hoppar direkt över till catch. Om man har skrivit ett negativt pris eller utelämnat namnet, får användaren ett meddelande om detta.

Om allt är ok, kontrolleras detta i nästa steg av if (list.Add(new Item(name, price))).

Om varan är inom budgetgränsen läggs den till i listan. Om varan däremot är för dyr, får du ett meddelande om att du sprängde budgeten och varan läggs inte till.

