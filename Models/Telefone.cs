using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;


namespace ConsoleApp1.Models;

internal class Telefone
{
    public int IdTel { get; set; }
    public int NumeroTel { get; set; }
    public int Ddd { get; set; }
    public TipoTelefone TipoTel { get; set; }
}

