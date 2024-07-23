using busca_cep;
using System;
using System.Threading.Tasks;

public class Program
{
    public static void Main(string[] args)
    {
        // Chama o método assíncrono
        RunAsync().GetAwaiter().GetResult();
    }

    private static async Task RunAsync()
    {
        Console.Write("Digite seu CEP: ");
        string meuCep = Console.ReadLine();

        ConsumoAPI consumoAPI = new ConsumoAPI();
        Endereco endereco = await consumoAPI.pegaCep(meuCep); // Exemplo de CEP

        Console.WriteLine($"\nLogradouro: {endereco.logradouro}");
        Console.WriteLine($"CEP: {endereco.cep}");
        Console.WriteLine($"Localidade: {endereco.localidade}");
        Console.WriteLine($"UF: {endereco.uf}");
    }
}
