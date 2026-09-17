
using Spectre.Console;

bool programmetKörs = true;
string[] parkingGarage = new string[101];
parkingGarage[0] = "PERSONALPARKERING";

while (programmetKörs)
{
    AnsiConsole.Clear();

    AnsiConsole.Write(
        new FigletText("Prague Parking")
            .Centered()
            .Color(Color.White));

    AnsiConsole.MarkupLine("[grey]Version 1.0[/]\n");

    string val = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[red]Vad vill du göra?[/]")
            .PageSize(12)
            .HighlightStyle(new Style(Color.LightGoldenrod1))
            .AddChoices(
                "Parkera fordon",
                "Flytta fordon",
                "Hämta ut fordon",
                "Sök fordon",
                "Visa parkering",
                "Avsluta"
            )
    );

    switch (val)
    {
        case "Parkera fordon":
            ParkeraFordon(parkingGarage);
            break;

        case "Flytta fordon":
            FlyttaFordon();
            break;

        case "Hämta ut fordon":
            HämtaUtFordon(parkingGarage);
            break;

        case "Sök fordon":
            SökFordon(parkingGarage);
            break;

        case "Visa parkering":
            VisaParkering(parkingGarage);
            break;

        case "Avsluta":
            programmetKörs = false;
            break;
    }
}

static string LäsFordonstyp()
{
    while (true)
    {
        Console.Write("Ange fordonstyp (CAR/MC): ");
        string fordonstyp = (Console.ReadLine() ?? "").ToUpper();

        if (fordonstyp == "CAR" || fordonstyp == "MC")
        {
            return fordonstyp;
        }

        AnsiConsole.MarkupLine($"[red]\nOgiltig fordonstyp. Ange CAR eller MC.[/]\n");
    }
}


static int HittaPlatsMedEnsamMC(string[] parkingGarage)
{
    for (int plats = 1; plats < parkingGarage.Length; plats++)
    {
        string? innehåll = parkingGarage[plats];

        if (!string.IsNullOrEmpty(innehåll) &&
            innehåll.StartsWith("MC#") &&
            !innehåll.Contains("|"))
        {
            return plats;
        }
    }

    return -1;
}

static int HittaPlatsFörFordon(string[] parkingGarage, string fordonstyp)
{
    if (fordonstyp == "MC")
    {
        int mcPlats = HittaPlatsMedEnsamMC(parkingGarage);

        if (mcPlats != -1)
        {
            return mcPlats;
        }
    }

    return HittaTomPlats(parkingGarage);
}
static int HittaTomPlats(string[] parkingGarage)
{
    for (int plats = 1; plats < parkingGarage.Length; plats++)
    {
        if (string.IsNullOrEmpty(parkingGarage[plats]))
        {
            return plats;
        }
    }

    return -1;
}

static string LäsRegistreringsnummer()
{
    while (true)
    {
        Console.Write("Ange registreringsnummer: ");
        string registreringsnummer = (Console.ReadLine() ?? "").ToUpper();
        if (string.IsNullOrWhiteSpace(registreringsnummer))
        {
            AnsiConsole.MarkupLine($"[red]\nRegistreringsnummer får inte vara tomt.[/]\n");
            continue;
        }
        if (registreringsnummer.Length > 10)
        {
            AnsiConsole.MarkupLine($"[red]\nRegistreringsnummer får inte vara längre än 10 tecken.[/]\n");
            continue;
        }
        bool giltigaTecken = true;
        foreach (char tecken in registreringsnummer)
        {
            if (!char.IsLetterOrDigit(tecken))
            {
                giltigaTecken = false;
                break;
            }
        }
        if (!giltigaTecken)
        {
            AnsiConsole.MarkupLine($"[red]" +
                $"Registreringsnumret får bara innehålla bokstäver och siffror.[/]\n");
            continue;
        }
        return registreringsnummer;
    }
}
static int HittaFordon(string[] parkingGarage, string registreringsnummer)
{
    for (int plats = 1; plats < parkingGarage.Length; plats++)
    {
        if (string.IsNullOrEmpty(parkingGarage[plats]))
        {
            continue;
        }

        string[] fordonPåPlatsen = parkingGarage[plats].Split('|');

        foreach (string fordon in fordonPåPlatsen)
        {
            string[] delar = fordon.Split('#');

            if (delar.Length >= 2 && delar[1] == registreringsnummer)
            {
                return plats;
            }
        }
    }

    return -1;
}
// Kontrollera registreringsnummer för undvika dubbletter
static string LäsUniktRegistreringsnummer(string[] parkingGarage)
{
    while (true)
    {
        string registreringsnummer = LäsRegistreringsnummer();
        int plats = HittaFordon(parkingGarage, registreringsnummer);
        if (plats != -1)
        {
            AnsiConsole.MarkupLine($"[red]\nFordonet med registreringsnummer {registreringsnummer} finns redan på plats {plats}.[/]\n\n");
            continue;
        }
        return registreringsnummer;
    }
}
static void ParkeraFordon(string[] parkingGarage)
{
    AnsiConsole.Clear();

    Console.WriteLine("=== Parkera fordon ===");

    // STEG 1: Fordonstyp
    string fordonstyp = LäsFordonstyp();
    // STEG 2: Registreringsnummer
    string registreringsnummer = LäsUniktRegistreringsnummer(parkingGarage);

    // STEG 3: Hitta en lämplig plats för fordonet
    int plats = HittaPlatsFörFordon(parkingGarage, fordonstyp);
    if (plats == -1)
    {
        AnsiConsole.MarkupLine($"[red]\nIngen tom plats kvar. Parkeringen är full.[/]");
        AnsiConsole.MarkupLine($"[white]\n\nTryck på en tangent för att gå tillbaka...[/]");
        Console.ReadKey();
        return;
    }
    // STEG 4: Parkera fordonet
    if (fordonstyp == "MC" && !string.IsNullOrEmpty(parkingGarage[plats]))
    {
        parkingGarage[plats] += $"|MC#{registreringsnummer}";
    }
    else
    {
        parkingGarage[plats] = $"{fordonstyp}#{registreringsnummer}";
    }
    AnsiConsole.MarkupLine($"[green]\nFordonet har parkerats på plats {plats}.[/]");
    Console.WriteLine("\nTryck på en tangent för att gå tillbaka...");
    Console.ReadKey();
}


static void FlyttaFordon()
{
    AnsiConsole.Clear();
    Console.WriteLine("=== Flytta fordon ===");
    Console.WriteLine("\nTryck på en tangent för att gå tillbaka...");
    Console.ReadKey();
}

static void HämtaUtFordon(string[] parkingGarage)
{
    AnsiConsole.Clear();
    Console.WriteLine("=== Hämta ut fordon ===");
    while (true)
    {
        string registreringsnummer = LäsRegistreringsnummer();
        int plats = HittaFordon(parkingGarage, registreringsnummer);

        if (plats == -1)
        {
            AnsiConsole.MarkupLine($"[red]\n\nFordonet hittades inte.[/]");
            string val = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Vad vill du göra? ")
                .HighlightStyle(new Style(Color.LightGoldenrod1))
                .AddChoices(
                    "Försök igen!",
                    "Tillbaka till huvudmenyn"
                    ));
            if (val == "Tillbaka till huvudmenyn")
            {
                return;
            }
            Console.WriteLine();
            continue;
        }
        else if (parkingGarage[plats].Contains("|"))
        {
            string[] mcFordon = parkingGarage[plats].Split('|');
            string[] kvarVarandeFordon = new string[mcFordon.Length - 1];
            int index = 0;
            foreach (string fordon in mcFordon)
            {
                if (!fordon.EndsWith($"#{registreringsnummer}"))
                {
                    kvarVarandeFordon[index] = fordon;
                    index++;
                }
            }
            parkingGarage[plats] = string.Join("|", kvarVarandeFordon);

            AnsiConsole.MarkupLine($"[green]\nFordonet har hämtats ut från plats {plats}.[/]");
        }
        else
        {
            parkingGarage[plats] = string.Empty;
            AnsiConsole.MarkupLine($"[green]\nFordonet har hämtats ut från plats {plats}.[/]");
        }

        Console.WriteLine("\nTryck på en tangent för att gå tillbaka...");
        Console.ReadKey();
    }
}

static void SökFordon(string[] parkingGarage)
{
    AnsiConsole.Clear();

    Console.WriteLine("=== Sök fordon ===");

    string registreringsnummer = LäsRegistreringsnummer();

    int plats = HittaFordon(parkingGarage, registreringsnummer);

    if (plats == -1)
    {
        AnsiConsole.MarkupLine($"[red]\n\nFordonet hittades inte.[/]");
    }
    else
    {
        AnsiConsole.MarkupLine($"[green]\nFordonet finns på plats {plats}.[/]");
    }

    Console.WriteLine("\nTryck på en tangent för att gå tillbaka...");
    Console.ReadKey();

}

static void VisaParkering(string[] parkingGarage)
{

    AnsiConsole.Clear();

    AnsiConsole.MarkupLine("[bold green]=== Visa parkering ===[/]\n");

    var table = new Table()
        .Border(TableBorder.Rounded)
        .BorderColor(Color.Blue)
        .Title("[bold red]=== Parkeringen ===[/]")
        .ShowRowSeparators();
   
    //Kolumnrubriker 1-10
    for (int i = 1; i <= 10; i++)
    {
        table.AddColumn($"[grey]{i}[/]");

    }
    
    //10 rader med 10 P-platser
    for (int rad = 1; rad <= 10; rad++)
    {
        string[] row = new string[10];
        for (int kolumn = 0; kolumn < 10; kolumn++)
        {

            int plats = (rad - 1) * 10 + kolumn + 1;
            string innehåll;

            if (string.IsNullOrEmpty(parkingGarage[plats]))
            {
                innehåll = $"[bold white]P-{plats}[/]\n[grey]LEDIG[/]";
            }
            else if (parkingGarage[plats].StartsWith("CAR#"))
            {
                innehåll = $"[bold white]P-{plats}[/]\n[green]{parkingGarage[plats]}[/]";
            }
            else if (parkingGarage[plats].StartsWith("MC#"))
            {
                string mcVisning = parkingGarage[plats].Replace("|", "\n");
                innehåll = $"[bold white]P-{plats}[/]\n[DarkOrange]{mcVisning}[/]";
            }
            else
            {
                innehåll = $"[bold white]P-{plats}[/]\n{parkingGarage[plats]}";
            }
            row[kolumn] = innehåll;

        }
     

        table.AddRow(row);
    }
    AnsiConsole.Write(table);
    Console.WriteLine();
    Console.WriteLine("Tryck på en tangent för att gå tillbaka...");
    Console.ReadKey();
}
