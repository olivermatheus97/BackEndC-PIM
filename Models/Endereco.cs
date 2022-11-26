using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;


namespace ConsoleApp1.Models;

internal class Endereco
{
    public int IdEnd { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public int NumeroEnd { get; set; }
    public int Cep { get; set; }
    public string Estado { get; set; }


}
