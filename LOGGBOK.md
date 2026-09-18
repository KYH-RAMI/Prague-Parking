# Loggbok – Prague Parking

## 2026-09-14

Idag började jag med första inlämningsuppgiften Prague Parking.

Jag läste igenom uppgiften och tyckte först att den såg ganska
komplicerad ut, framför allt eftersom både G- och VG-delarna står
i samma dokument.

Efter att ha gått igenom kraven mer noggrant blev det tydligare
vad som först behöver göras i version 1.0.

Jag skapade projektet PragueParking1.0 i Visual Studio och lade
också till README.md och LOGGBOK.md.

Jag har även börjat förstå hur parkeringen ska lagras i en
string-array med 100 platser. En sak jag måste tänka på är att
arrayen använder index 0–99 medan användaren ska se platserna
som 1–100.

Jag gick också igenom hur en bil och en MC ska lagras i arrayen, t.ex.:

CAR#ABC123

och två MC på samma plats:

MC#ABC123|MC#XYZ789

### Problem/funderingar

Det som varit svårast hittills är att förstå hur hela programmet
ska delas upp och hur två MC ska hanteras i samma arrayplats.

### Nästa steg

Nästa gång ska jag börja med arrayen och huvudmenyn.


## 2026-09-15

### Arrayen för parkeringen

Jag har nu skapat arrayen med 100 parkeringsplatser.
Jag har också skapat en metod som skriver ut arrayen.

Jag har fortfarande lite problem med att få utskriften av
parkeringen i det format jag vill ha, men det tänker jag
återkomma till senare.


### Huvudmenyn

Jag har nu skapat huvudmenyn och lagt den i en egen metod.

Jag har också skapat en metod som läser in användarens val
och returnerar det val användaren har gjort.

Jag har börjat förstå bättre varför alla metoder inte behöver
vara av typen void och hur en metod kan returnera ett värde.

### Fortsatt arbete med ParkeraFordon()

Jag fortsatte sedan med metoden `ParkeraFordon()`.

Jag lade till kontroll av fordonstyp så att programmet bara
accepterar CAR eller MC. Om användaren skriver något annat
fortsätter programmet att fråga tills ett giltigt värde anges.

Jag började även validera registreringsnumret. Programmet
kontrollerar nu att registreringsnumret:

- inte är tomt,
- inte är längre än 10 tecken,
- endast innehåller bokstäver och siffror.

För att kontrollera varje tecken använder jag en `foreach`-loop
tillsammans med `char.IsLetterOrDigit()`.

Jag gör också om registreringsnumret till stora bokstäver med
`ToUpper()` för att få ett mer enhetligt format.

Jag ändrade även `ParkeraFordon()` så att parkeringsarrayen
skickas in som en parameter:

`ParkeraFordon(string[] parkingGarage)`

### Problem/funderingar

Det som fortfarande är lite svårt är att förstå hur hela
programmet ska delas upp i olika metoder och hur två MC ska
hanteras i samma plats i arrayen.

Jag har därför tittat på några Youtube-klipp och läst C#-
dokumentation för att repetera metoder, while-loopar och
for-loopar.

Det var ungefär 16 år sedan jag programmerade senast, så jag
behöver uppdatera en del kunskaper även om mycket känns bekant
från Java.

Jag hade spontant velat använda List för att enklare kunna lägga till och ta bort fordon. 
Samtidigt börjar jag förstå varför en array passar ganska bra för själva parkeringen eftersom den alltid består av exakt 100 fasta platser. 
Det som känns mer omständligt är att all information om fordonen ska lagras i strängar i arrayen.

Jag gick även igenom .NET-gruppens serie med nybörjarfilmer om
C#, vilket hjälpte mig att repetera grunderna.

Länk:
https://aka.ms/dotnet/beginnervideos/youtube/csharp
C# Dokumentation:
https://learn.microsoft.com/sv-se/dotnet/csharp/tour-of-csharp/

Jag blandade först ihop hur `return`, `bool`-variabler och
while-loopar skulle användas vid valideringen.

Jag försökte också först kontrollera registreringsnumret som
ett fast format med tre bokstäver och tre siffror, men såg sedan
att uppgiften anger max 10 tecken och att registreringsnumret
ska kunna innehålla bokstäver och siffror.

Jag hade även ett fel med måsvingarna där jag råkade avsluta
metoden för tidigt. Det gjorde att kod hamnade utanför metoden.

### Nästa steg

Nästa steg är att kontrollera om registreringsnumret redan finns
i parkeringen och sedan börja skapa logiken för att hitta en
ledig parkeringsplats.

Jag behöver då ta hänsyn till att en bil kräver en tom plats,
medan en MC kan stå ensam eller tillsammans med en annan MC.

## 2026-09-16

### Fortsatt uppdelning av programmet

Idag fortsatte jag att dela upp programmet i mindre metoder.

Jag tyckte att `ParkeraFordon()` började bli svår att läsa eftersom
den innehöll flera loopar och kontroller. Jag började därför flytta
ut delar av logiken till egna metoder som returnerar ett resultat.

Jag har nu bland annat följande metoder:

`LäsFordonstyp()`
- Läser in fordonstyp.
- Accepterar endast CAR eller MC.
- Returnerar den godkända fordonstypen som en string.

`LäsRegistreringsnummer()`
- Läser in och validerar registreringsnummer.
- Kontrollerar tom input, max 10 tecken och att endast bokstäver
  och siffror används.
- Returnerar det godkända registreringsnumret.

`HittaTomPlats()`
- Söker igenom parkeringen efter första tomma plats.
- Returnerar platsnumret eller -1 om ingen plats hittas.

`HittaPlatsMedEnsamMC()`
- Söker efter en parkeringsplats där det står exakt en MC.
- Kontrollerar att platsen börjar med `MC#` och inte innehåller `|`.

`HittaPlatsFörFordon()`
- Bestämmer vilken plats ett fordon ska få.
- En MC försöker först dela plats med en ensam MC.
- Om ingen sådan plats finns används en tom plats.
- En CAR behöver alltid en tom plats.

### Två MC på samma parkeringsplats

Jag fick nu funktionen med två MC på samma parkeringsplats att fungera.

Exempel på hur en plats lagras:

`MC#ABC123|MC#KHF123`

Tecknet `|` används alltså för att skilja två MC som står på
samma parkeringsplats.

Jag testade även med fler fordon och såg att en tredje MC placeras
på nästa tillgängliga plats när den första MC-platsen redan innehåller
två motorcyklar.

### Sökning efter fordon

Jag implementerade också en första fungerande sökfunktion.

Jag skapade metoden:

`HittaFordon(string[] parkingGarage, string registreringsnummer)`

Metoden går igenom parkeringsplatserna och använder `Split('|')`
för att dela upp två MC som står på samma plats.

Varje fordon delas sedan med `Split('#')` så att jag kan jämföra
registreringsnumret exakt.

Metoden returnerar parkeringsplatsen om fordonet hittas och `-1`
om registreringsnumret inte finns.

Jag kan därför återanvända samma sökmetod senare när jag bygger
funktionerna för att flytta och hämta ut fordon.

### Parkeringsplatser 1–100

Efter diskussion med läraren ändrade jag arrayen så att den har
101 element och reserverade index 0 som personalparkering.

`parkingGarage[0] = "PERSONALPARKERING";`

Det gör att parkeringsplats 1 motsvarar index 1 och plats 100
motsvarar index 100.

Det gör koden enklare att läsa eftersom jag slipper använda
`plats + 1` vid in- och utmatning.

### GitHub och versionshantering

Jag publicerade även projektet på GitHub idag.

Läraren vill kunna följa utvecklingen genom commits, så jag valde
att börja använda GitHub redan under arbetet med version 1.0 istället
för att bara ladda upp det färdiga projektet i slutet.

Jag skapade repositoryt direkt från Visual Studio och använder
Visual Studios Git-funktioner för commits och push.

Jag lade även till Visual Studios standard `.gitignore` så att filer
och mappar som t.ex. `bin`, `obj` och `.vs` inte ska följa med till
GitHub.

Eftersom projektet redan hade kommit en bit när jag publicerade det
blev den första uppladdningen en större version av det jag hade gjort
hittills. Framöver tänker jag försöka göra mindre och tydligare
commits när en funktion eller förändring är färdig.

Jag har också börjat förstå bättre skillnaden mellan att göra en
commit lokalt och att sedan pusha ändringarna till GitHub.

Jag upptäckte även en gammal `TextFile1.txt` i solution-mappen som
råkat följa med till repositoryt. Den användes inte av programmet
och togs därför bort i en senare ändring.

### Problem/funderingar

Jag råkade vid ett tillfälle skapa `SökFordon()` inuti en annan
`SökFordon()` vilket gjorde att sökfunktionen inte kördes som jag
tänkt.

Jag har också märkt att det blir mycket lättare att felsöka programmet
när varje metod har ett mindre och tydligare ansvar.

Jag börjar förstå bättre varför metoder inte alltid ska vara `void`
och varför det är användbart att returnera exempelvis en string,
ett platsnummer eller `-1`.

### Nästa steg

Nästa steg är att:

1. kontrollera så att samma registreringsnummer inte kan parkeras två gånger,
2. testa sökfunktionen mer,
3. implementera `HämtaUtFordon()`,
4. implementera `FlyttaFordon()`,
5. förbättra utskriften av parkeringen.

Jag vill försöka återanvända `HittaFordon()` och de andra sökmetoderna
så att samma loopar inte behöver skrivas flera gånger.

## 2026-09-17

### Förbättrad visning av parkeringen

Idag fortsatte jag arbeta med `VisaParkering()`.

Tidigare skrevs alla 100 parkeringsplatser ut som en lång lista.
Det fungerade, men det var svårt att få en snabb överblick över
hela parkeringen.

Jag byggde därför om visningen med `Table` från Spectre.Console
och skapade en tabell med 10 kolumner och 10 rader.

För att räkna ut vilket parkeringsnummer varje ruta motsvarar
använder jag:

`int plats = (rad - 1) * 10 + kolumn + 1;`

På så sätt kan tabellen visa parkeringsplatserna 1–100.

Jag lade också till platsnumret direkt i varje ruta, t.ex.
`P-1`, `P-11` och `P-21`, eftersom det annars var svårt att se
exakt vilken parkeringsplats ett fordon stod på.

Lediga platser visas i grått, bilar i grönt och MC i mörkorange färg.

### Dubblettkontroll och färgkodning

Jag lade även till kontroll så att samma registreringsnummer inte
kan parkeras flera gånger.

Jag använder nu metoden:

`LäsUniktRegistreringsnummer(string[] parkingGarage)`

Metoden återanvänder `LäsRegistreringsnummer()` för valideringen
och `HittaFordon()` för att kontrollera om registreringsnumret
redan finns i parkeringen.

Om fordonet redan finns visas ett felmeddelande och vilken
parkeringsplats fordonet står på. Användaren får sedan ange ett
nytt registreringsnummer.

Jag började även använda Spectre.Console mer konsekvent för
meddelanden i programmet.

Felmeddelanden visas i rött och lyckade åtgärder i grönt.

I parkeringsöversikten använder jag även olika färger för att
göra den lättare att läsa:

- grått för lediga platser,
- grönt för CAR,
- DarkOrange för MC,
- vitt för parkeringsnummer.

Jag testade först färgen `orange`, men Spectre.Console gav felet:

`Could not find color or style 'orange'.`

Efter felsökning ändrade jag till `DarkOrange`, vilket fungerar
och dessutom syns bättre i terminalen.

### Problem/felsökning

Jag fick först ett fel från Spectre.Console:

`Value cannot be null`

Det gjorde att jag tittade närmare på hur `string[] row` fylls innan
den skickas till `table.AddRow(row)`.

Jag fick senare även felet:

`Could not find color or style 'orange'.`

Visual Studio markerade området kring min `else`-sats, så först trodde
jag att problemet låg där.

Efter att ha läst hela exception-meddelandet och stack trace såg jag
att det egentligen var färgnamnet `orange` som Spectre.Console inte
kunde tolka.

Det var bra träning i att läsa själva felmeddelandet och inte bara
titta på vilken rad Visual Studio markerar.

### Hämta ut fordon

Jag implementerade även funktionen `HämtaUtFordon()`.

Funktionen börjar med att läsa in registreringsnumret och använder
sedan den befintliga metoden `HittaFordon()` för att hitta vilken
parkeringsplats fordonet står på.

Om registreringsnumret inte finns i parkeringen får användaren
ett felmeddelande och kan välja mellan att försöka igen eller
gå tillbaka till huvudmenyn.

Jag använde en `while`-loop tillsammans med `continue` och `return`
för att styra detta flöde.

Jag börjar förstå skillnaden bättre:

- `continue` gör att loopen börjar om och användaren får försöka igen.
- `return` avslutar metoden och användaren kommer tillbaka till huvudmenyn.

### Ta bort CAR och MC

Om en parkeringsplats bara innehåller ett fordon, alltså en CAR
eller en ensam MC, kan hela arraypositionen tömmas.

Exempel:

`CAR#ABC123`

eller:

`MC#ABC123`

Då kan platsen göras ledig igen.

Det svårare fallet var när två MC delar samma parkeringsplats:

`MC#ABC123|MC#DEF456`

Jag får då inte tömma hela parkeringsplatsen eftersom den andra
motorcykeln ska stå kvar.

Jag använder därför `Split('|')` för att dela upp de två
motorcyklarna och kontrollera vilken som ska tas bort.

Jag gick även igenom `String.Join()` eftersom läraren tidigare
tipsat om att använda `Split()` och `Join()`.

Jag förstår nu principen bättre:

`Split()` används för att gå från en string till flera delar.

`Join()` används för att sätta ihop flera strings till en string
igen med en vald separator mellan dem.

I det här fallet används `|` som separator mellan två MC.

### Reflektion

Jag märkte under arbetet med `HämtaUtFordon()` att koden snabbt
blir ganska omfattande när all information om fordonen lagras
som strings i arrayen.

Samtidigt börjar jag förstå mer av varför metoder som
`HittaFordon()` är bra att återanvända istället för att skriva
samma söklogik på flera ställen.

Jag har fått en del hjälp av Visual Studio och även hjälp med att
förstå och felsöka koden, så jag vill fortsätta gå igenom delarna
så att jag själv förstår varför de fungerar och inte bara att de
fungerar.

### Funderingar

Jag funderade även på hur tabellen skulle vara tydligast att läsa.

Jag vill behålla kolumnnumreringen 1–10 högst upp, men även visa det
exakta parkeringsnumret i varje ruta. Det gör att man snabbare kan
orientera sig i tabellen samtidigt som man direkt ser vilken plats
ett fordon står på.

### Nästa steg

Nästa steg är att:

1. implementera `FlyttaFordon()`,
2. testa `HämtaUtFordon()` med CAR, ensam MC och två MC på samma plats,
3. fortsätta testa programmet med olika kombinationer av fordon,
4. gå igenom koden metod för metod för att säkerställa att jag själv förstår logiken,
5. kontrollera att alla funktioner fungerar tillsammans innan version 1.0 är klar,
6. se om det finns fler förbättringar som kan göras innan inlämning, t.ex. bättre läsbarhet och mer återanvändning av metoder.

## 2026-09-18

### Flytta fordon

Idag implementerade jag funktionen `FlyttaFordon()`.

Funktionen börjar med att läsa in registreringsnumret och använder
`HittaFordon()` för att hitta vilken parkeringsplats fordonet står på.

Om fordonet inte hittas kan användaren försöka igen eller gå tillbaka
till huvudmenyn.

När fordonet hittats visas den nuvarande parkeringsplatsen och
användaren får ange vilken plats fordonet ska flyttas till.

### Validering av parkeringsplats

Jag skapade en separat metod:

`LäsParkeringsplats(string[] parkingGarage)`

Metoden använder `int.TryParse()` för att kontrollera att användaren
skriver in ett heltal.

Den kontrollerar även att parkeringsplatsen ligger mellan 1 och 100.

Jag lade också till kontroll så att ett fordon inte kan flyttas till
samma parkeringsplats som det redan står på.

### Flytt av CAR och MC

Det enklaste fallet är när målplatsen är tom.

Då kan fordonet läggas på den nya platsen och tas bort från den gamla.

Det blev mer komplicerat när två MC står på samma parkeringsplats,
eftersom bara den MC som användaren söker efter ska flyttas.

Exempel:

`MC#ABC123|MC#DEF456`

Om `ABC123` ska flyttas får inte `DEF456` försvinna eller följa med
till den nya platsen.

Jag använder därför `Split('|')` för att dela upp fordonen på platsen
och `Split('#')` för att hitta exakt vilket fordon som ska flyttas.

Om två MC står på den gamla platsen kontrollerar programmet vilken av
dem som flyttas och sparar den andra på den gamla parkeringsplatsen.

Jag lade också till så att en MC får flyttas till en parkeringsplats
där det redan står exakt en MC.

För att det ska vara tillåtet kontrolleras att:

- fordonet som flyttas är en MC,
- fordonet på målplatsen är en MC,
- målplatsen inte redan innehåller två MC.

### Problem med programflödet

När jag fortsatte testa funktionen upptäckte jag att programmet
frågade efter registreringsnumret igen om användaren valde en
upptagen målplats.

Det berodde på att jag bara hade en `while`-loop runt hela
`FlyttaFordon()`.

När `continue` kördes hoppade programmet därför tillbaka till början
av den loopen och frågade efter registreringsnumret igen.

Jag löste detta genom att lägga till en andra `while`-loop för valet
av den nya parkeringsplatsen.

Den yttre loopen hanterar registreringsnumret och den inre loopen
hanterar valet av målplats.

Nu behöver användaren bara välja en ny parkeringsplats om den första
är upptagen.

Det gjorde att jag fick en bättre förståelse för hur loopar och
`continue` fungerar.

`continue` börjar om den närmaste loopen som koden ligger i.

Jag har också blivit tydligare med skillnaden mellan:

- `continue` - börjar nästa varv i närmaste loop,
- `break` - avslutar närmaste loop,
- `return` - avslutar hela metoden.

### Tester

Jag har testat bland annat:

- CAR till en tom plats,
- ensam MC till en tom plats,
- flytt från en plats med två MC,
- MC till en plats där en MC redan står,
- upptagen målplats,
- flytt till samma parkeringsplats,
- ogiltiga parkeringsnummer som 0 och 101,
- text istället för ett nummer,
- registreringsnummer som inte finns i parkeringen.

Jag upptäckte flera fel först när jag fortsatte testa specialfallen,
så jag har fått ändra flyttfunktionen flera gånger under dagen.

### Reflektion

`FlyttaFordon()` blev mer komplicerad än jag först trodde.

Det var framför allt hanteringen av två MC och programflödet med flera
loopar som gjorde den svårare.

När jag delar upp problemet i mindre steg blir det lättare att förstå:

1. hitta fordonet,
2. hitta exakt vilket fordon som ska flyttas,
3. läsa och validera den nya parkeringsplatsen,
4. kontrollera om fordonet får stå där,
5. lägga fordonet på den nya platsen,
6. uppdatera den gamla platsen.

Jag behöver fortsätta träna på `while`, `if`, `continue`, `break`
och `return` så att jag blir bättre på att själv följa programflödet.

### Nästa steg

Nästa steg är att:

1. göra de sista testerna av flyttfunktionen med olika kombinationer av MC,
2. gå igenom hela version 1.0 och kontrollera att alla krav är uppfyllda,
3. testa funktionerna tillsammans,
4. gå igenom koden metod för metod så att jag själv kan förklara hur den fungerar,
5. se om det finns upprepad kod som senare kan förenklas eller återanvändas bättre,
6. förbättra läsbarheten innan version 1.0 är helt klar.