using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace busca_cep
{
    internal class ConsumoAPI
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<Endereco> pegaCep(string cep)
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Endereco endereco = JsonSerializer.Deserialize<Endereco>(responseBody);

                string caminhoArquivo = @"C:\ProjetosDev\ProjetosCsharp\busca-cep\dados";
                await salvaArquivoJson(endereco, caminhoArquivo);

                return endereco;

            } 
            catch (HttpRequestException ex) 
            {
                throw new Exception($"Erro ao buscar o cep: {cep}. " +
                    $"Detalhes: {ex.Message}", ex);
            }
            catch (JsonException ex) 
            {
                throw new Exception($"Erro ao desserializar a resposta JSON para o " +
                    $"CEP: {cep}. Detalhes: {ex.Message}", ex);
            }

        }

        private async Task salvaArquivoJson(Endereco endereco, string caminhoArquivo)
        {
            try
            {
                string arquivoJson = JsonSerializer.Serialize(endereco, new JsonSerializerOptions { WriteIndented = true });
                string combinaArquivoCaminho = Path.Combine(caminhoArquivo, endereco.cep+".json");
                await File.WriteAllTextAsync(combinaArquivoCaminho, arquivoJson);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException($"Erro ao salvar o arquivo JSON: {ex.Message}", ex);
            }
        }
    }
}
