using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;

namespace ConsoleApp1.Models;

internal class Pessoa
{
    public int IdPessoa { get; set; }
    public string Nome { get; set; }
    public long Cpf { get; set; }
    public Endereco endereco { get; set; }
    public Telefone[] telefones { get; set; }

}