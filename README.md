# Kunskapskontroll 2: Robust inköpslista
## Del 1 - laga ett trasigt programm
## Del 2 - bygg ut det
### De sex felen: Vad hände, varför, hur du löste det. 


- När jag startade programet så kraschade den direkt så jag läste felmeddelandet och bestämde mig efter stund att gära en try-catch 
Men jag satte den innanför foreach vilket var fel. Jag öppnade och läste
Felhantering i C# och det gick bättre. Nu fick jag igång programmet.

- Nu när jag fick igång programmet så försökte jag läsa igenom programet 
för att hitta fel men jag lyckades bara hitta eller rättare sagt misstänka ett fel och detta efter en lång stund och det var i ShoppingList.cs metoden public int total, så var (int i = 1;) . Jag ville nu ha bekräftelse på min misstanke så jag bäjade använda debug. Så började jag upptäcka andra fel.

- Debug avslöjade direkt att (int choice = int.parse.Console.ReadLine())
debug visade då en stor ruta. Där man kunde läsa felmeddelande och jag uppmärksammade om jag minns rätt (FormatException). Ja detta vissa sig att det var den andra program fel som orsakar krasch. Sedan kom jag att tänka på (TryParse används för inmatning som förväntas kunna bli fel) Så jag gjorde om den till en string input och int,parse byta jag till int.TryParse.

- Sedan upptckta jag att i (else if (choice == 1)) så hade vi samma fel. Det konstiga var att debug inte kunde komma ner hit . Fast jag hade satt ut brakpoint. Jag rådfrågade AI, som jag kör med Benjamins prompt och den bekräftade felet. Så jag gjorde om den också. Men det räckte int jag fick även lägga in en som kallad innästlad if-sats fär att säkerhetställa att  att programmet inte rasade om man skrev flera siffror som t ex 87, 897, 7898 osv, Nu hade jag hittat tredje felet som orsakar krasch.

- Nu när jag kände mig mer säkert så tog jag tag i tatal summan som blev fel. Jag tittade i metoden public int total, så var (int i = 1;) igen. Men denna gången ändrade jag felet. Nu blev total summan rätt. Men jag upptäckte att produktlistan alltås namnet på varan var borta. Detta var ett nytt fel.

- När det gäller produktnamnet så tog det ganska lång tid att lösa. Jag försökta med allt undersökte flera metoder utan frmgång, så jag råd frågade AI och den kom på olika förslag som jag redan hade använt men även nya. Tills slut så hittade vi felet. Detta fel finns under metoden public void print. Det saknades (item Name och Price) mellan måsvingarna under for loopen. Jag skrev in dessa och listan visade nu produkt namnet.

- När jag testade karav listan upptäckte jag dem två sistta felen den ena var att jag skulle tag bort varan och när jag vade denna så rasade programmet. Färst trodde jag att det var en enkel sak som att ändra int.parse till en TryParse vilket jag gjorde, men nej. Även här fick jag lägga ine en innästlad if-sats men det hjälpte inte Så jag gjorde det väldigt enkel och la i en try-catch som fångade ArgumentOutOfRangeException och detta gjorde det möjligt för att ta bort varan.

-







