using System.Net.Http.Json;

public class Tarefas
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public bool Concluida { get; set; }
}

class programa
{
    static async Task Main(string[] args)
    {
        string apiUrl = "https://localhost:7066/api/tarefas";

        // Solicita informações do pedido ao usuário
        Console.Write("Digite a tarefa: ");
        string titulo = Console.ReadLine();

        bool concluida = false;

        Console.Write("A tarefa esta concluida?  ");
        string resposta = Console.ReadLine().ToLower();
        if (resposta == "sim")
        {
            concluida=true;
        }

        Tarefas novaTarefa = new Tarefas
        {
            Titulo = titulo,
            Concluida = concluida
        };

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, novaTarefa);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Tarefa adicionada com sucesso!");
                var tarefaCriada = await response.Content.ReadFromJsonAsync<Tarefas>();
            }
            else
            {
                Console.WriteLine("Erro ao adicionar a tarefa.");
                Console.WriteLine($"Status Code: {response.StatusCode}");
            }
        }
    }
}