# Kunskapskontroll 2: Robust inköpslista
## Del 1 - laga ett trasigt programm
## Del 2 - bygg ut det
### De sex felen: Vad hände, varför, hur du löste det. 


# Del 1 - Felrapport (De sex felen i startkoden)

- Efter en noggrann felsökning med hjälp av VS Codes debugger har jag identifierat och åtgärdat de sex felen i startkoden:

# Fel 1: Krasch vid uppstart om filen saknas (FileNotFoundException)

- Vad som hände: När jag startade programmet för första gången kraschade det omedelbart.
- Varför: Koden försökte läsa in filen aldos_items.txt med File.ReadAllText() innan filen över huvud taget var skapad.
- Lösning: Jag kapslade in filinläsningen i en try-catch som fångar FileNotFoundException. Nu startar programmet utan problem med en tom lista om filen saknas.

# Fel 2: Krasch vid bokstäver i huvudmenyn (FormatException)

- Vad som hände: Om jag skrev bokstäver istället för siffror i huvudmenyn kraschade programmet.
- Varför: Debuggern avslöjade direkt att koden använde en osäker int.Parse(Console.ReadLine()) som inte kan hantera text.
- Lösning: Jag bytte ut int.Parse mot en säker int.TryParse(). Om användaren anger ett ogiltigt val visas nu ett felmeddelande istället för att programmet kraschar.

# Fel 3: Krasch vid bokstäver i pris-inmatningen (FormatException)

- Vad som hände: Samma krasch uppstod inuti menyval 1 (lägga till vara) om man angav priset med bokstäver.
- Varför: Koden använde även här en osäker int.Parse. Det intressanta var att min debugger inte kunde komma ner till min breakpoint här först. Det berodde på att programmet hann krascha redan i huvudmenyn (Fel 2) innan koden ens nådde hit. När jag väl lagat menyn kunde jag felsöka nästa steg.
- Lösning: Jag ändrade även denna till int.TryParse() och lade till en if-sats för att säkerställa att programmet hanterar inmatningen robust.

# Fel 4: Krasch vid borttagning av en vara som inte finns (ArgumentOutOfRangeException)

- Vad som hände: När jag valde menyval 2 för att ta bort en vara och angav ett nummer som inte fanns i listan (t.ex. rad 5 i en lista med 2 varor), så kraschade programmet.
- Varför: Koden försökte skicka in ett ogiltigt index till items.RemoveAt(), vilket ligger utanför listans faktiska storlek.
- Lösning: Jag lade till en try-catch runt list.RemoveAt(number) som fångar ArgumentOutOfRangeException. Nu kraschar inte programmet, utan ger istället användaren ett tydligt felmeddelande.

# Fel 5: Dolt fel – Felaktig totalsumma och osynliga produktnamn

- Vad som hände: Totalsumman räknade fel (den hoppade över den allra första varan) och produktlistan på skärmen var helt tom på namn och pris – bara siffrorna (1., 2...) syntes.
- Varför: Detta var det fel som "dolde att något gick fel". Varorna fanns i minnet, men loopen i Total() startade felaktigt på index 1 (int i = 1;) istället för 0. Dessutom saknades variablerna {items[i].Name} och {items[i].Price} helt inuti stränginterpolationen i Print().
- Lösning: Jag ändrade loopen till int i = 0; och skrev in namnet och priset i Print()-metodens Console.WriteLine. Nu syns varorna och summan stämmer.

# Fel 6: Fel resultat vid sökning (Skiftlägeskänslighet / NullReferenceException)

- Vad som hände: När jag sökte efter en vara (menyval 4) som jag tydligt såg i listan, påstod programmet att den inte fanns, eller så uppstod problem med att koden försökte läsa ett objekt som var null.
- Varför: Detta var felet som "gav fel resultat utan att krascha". C# är skiftlägeskänsligt. Sökte jag på "kaffe" hittades inte "Kaffe", vilket gjorde att sökmetoden returnerade null. När programmet sedan försökte läsa egenskaper från det tomma resultatet uppstod ett referensfel.
- Lösning: Jag lade till en try-catch för NullReferenceException i menyn, och i själva sökfunktionen (Find) lade jag till .ToLower() på både varans namn och söksträngen så att sökningen helt ignorerar stora och små bokstäver.

# Del 2 - Robusthetssäkring av filinläsning (Följdfel)

- När jag i uppgiftens andra del uppdaterade Item-klassen till att kasta undantag om ett pris är negativt, upptäckte jag att programmet kraschade vid uppstart om jag manuellt skrev en rad med ett negativt pris (t.ex. -15;Mjölk) direkt i textfilen.
Filinläsningen var inte förberedd på att Item-konstruktorn nu skyddar sig själv. Jag löste detta genom att lägga en try-catch inuti själva foreach-loopen i Load(). Nu hoppar programmet smidigt över skadade rader och startar ändå.


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



[Klicka här för att öppna klassdiagrammet (PDF)](./Klassdiagram.pdf)