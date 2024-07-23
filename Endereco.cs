using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busca_cep
{
    public record Endereco(string logradouro, 
                           string cep,
                           string localidade, 
                           string uf)
    {

    }
}
