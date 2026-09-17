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

Lediga platser visas i grått, bilar i grönt och MC i gult färg.

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

### Funderingar

Jag funderade även på hur tabellen skulle vara tydligast att läsa.

Jag vill behålla kolumnnumreringen 1–10 högst upp, men även visa det
exakta parkeringsnumret i varje ruta. Det gör att man snabbare kan
orientera sig i tabellen samtidigt som man direkt ser vilken plats
ett fordon står på.

### Nästa steg

Nästa steg är att:

1. fortsätta justera parkeringsöversikten,
2. kontrollera dubbla registreringsnummer,
3. implementera `HämtaUtFordon()`,
4. implementera `FlyttaFordon()`,
5. fortsätta testa programmet med olika typer av fordon.