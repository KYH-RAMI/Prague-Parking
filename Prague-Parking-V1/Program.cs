
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
            HämtaUtFordon();
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

        Console.WriteLine("Ogiltig fordonstyp. Ange CAR eller MC.\n");
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
            Console.WriteLine("Registreringsnummer får inte vara tomt.\n");
            continue;
        }
        if (registreringsnummer.Length > 10)
        {
            Console.WriteLine("Registreringsnummer får inte vara längre än 10 tecken.\n");
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
            Console.WriteLine(
                "Registreringsnumret får bara innehålla bokstäver och siffror.\n");
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
static void ParkeraFordon(string[] parkingGarage)
{
    AnsiConsole.Clear();

    Console.WriteLine("=== Parkera fordon ===");

    // STEG 1: Fordonstyp
    string fordonstyp = LäsFordonstyp();
    // STEG 2: Registreringsnummer
    string registreringsnummer = LäsRegistreringsnummer();

    // STEG 3: Hitta en tom plats
    int plats = HittaPlatsFörFordon(parkingGarage, fordonstyp);
    if (plats == -1)
    {
        Console.WriteLine("Ingen tom plats kvar. Parkeringen är full.");
        Console.WriteLine("\nTryck på en tangent för att gå tillbaka...");
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
    Console.WriteLine($"Fordonet har parkerats på plats {plats}.");
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

static void HämtaUtFordon()
{
    AnsiConsole.Clear();
    Console.WriteLine("=== Hämta ut fordon ===");
    Console.WriteLine("\nTryck på en tangent för att gå tillbaka...");
    Console.ReadKey();
}

static void SökFordon(string[] parkingGarage)
{
    AnsiConsole.Clear();

    Console.WriteLine("=== Sök fordon ===");

    string registreringsnummer = LäsRegistreringsnummer();

    int plats = HittaFordon(parkingGarage, registreringsnummer);

    if (plats == -1)
    {
        Console.WriteLine("\nFordonet hittades inte.");
    }
    else
    {
        Console.WriteLine($"\nFordonet finns på plats {plats}.");
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
                innehåll = $"[bold white]P-{plats}[/]\n[yellow]{parkingGarage[plats]}[/]";
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
