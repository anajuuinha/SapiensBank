using System.Text.Json;

public class Banco
{
    public List<Conta> Contas { get; set; } = new List<Conta>();

    private readonly string fullPath;
    private readonly string directoryPath;

    public Banco()
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        directoryPath = Path.Combine(path, "SapiensBank");
        fullPath = Path.Combine(directoryPath, "banco.json");

        CarregarContas();
    }

    private void CarregarContas()
    {
        try
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            if (!File.Exists(fullPath))
            {
                File.WriteAllText(fullPath, "[]");
                Contas = new List<Conta>();
                return;
            }

            var json = File.ReadAllText(fullPath);
            if (string.IsNullOrWhiteSpace(json))
            {
                Contas = new List<Conta>();
                return;
            }

            var contas = JsonSerializer.Deserialize<List<Conta>>(json);
            Contas = contas ?? new List<Conta>();
        }
        catch
        {
            Contas = new List<Conta>();
        }
    }

    public void SaveContas()
    {
        try
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(Contas, options);

            File.WriteAllText(fullPath, json);
        }
        catch
        {
            Console.WriteLine("Erro ao salvar contas!");
        }
    }
}
