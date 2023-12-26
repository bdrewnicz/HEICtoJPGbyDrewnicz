using ImageMagick;

string directoryPath;
int converted = 0;

Title();
if (args.Length > 0 && Directory.Exists(args[0]))
{
    directoryPath = args[0];
}
else
{
    Console.WriteLine("Enter the folder path:");
    directoryPath = Console.ReadLine();

    if (!Directory.Exists(directoryPath))
    {
        Console.WriteLine("Path does not exist.");
        return;
    }
}

var files = Directory.GetFiles(directoryPath, "*.heic");
int toConvert = files.Length;
if (files.Length > 0)
{
    foreach (var file in files)
    {
        try
        {
            using var image = new MagickImage(file);
            string jpegPath = Path.ChangeExtension(file, ".jpeg");
            image.Write(jpegPath);
            converted++;
            Title();
            DisplayProgressBar(converted, toConvert);
            File.Delete(file);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in {file}: {ex.Message}");
        }
    }
}
else
{
    Console.WriteLine($"No pictures to convert.");
}


Console.ReadKey();


static void Title()
{
    Console.Clear();
    Console.WriteLine("HEICtoJPG by Drewnicz");
    Console.WriteLine("HEIC to JPG Image Converter.");
    Console.WriteLine("-------------------------------------");
}

static void DisplayProgressBar(int converted, int toConvert)
{
    if (toConvert <= 0) return;

    int barWidth = 20;
    double percentageDone = (double)converted / toConvert;
    int filledLength = (int)(barWidth * percentageDone);

    Console.Write("\rProgress: [");
    Console.BackgroundColor = ConsoleColor.Green;

    for (int i = 0; i < filledLength; i++)
    {
        Console.Write(" ");
    }

    Console.BackgroundColor = ConsoleColor.Gray;

    for (int i = filledLength; i < barWidth; i++)
    {
        Console.Write(" ");
    }

    Console.BackgroundColor = ConsoleColor.Black;
    Console.Write($"] {percentageDone:P0}\r");

    if (converted == toConvert)
    {
        Console.WriteLine($"Complete!");
    }
}
