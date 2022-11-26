using ConsoleApp1.Models;

Pessoa pessoa = new Pessoa();
Endereco endereco = new Endereco();
Telefone telefone = new Telefone();
TipoTelefone tipoTelefone = new TipoTelefone();

tipoTelefone.Tipo= "Comercial";
telefone.TipoTel = tipoTelefone;
pessoa.endereco = endereco;
pessoa.telefones = new Telefone[2];
pessoa.telefones[0] = telefone;

pessoa.Nome = "Mendes ";
pessoa.Cpf = "12312312395";
pessoa.endereco.Logradouro = "Rua 77";
pessoa.endereco.NumeroEnd = 77;
pessoa.endereco.Cep = 12347778;
pessoa.endereco.Bairro = "Jaco pocas";
pessoa.endereco.Cidade = "bicilandia";
pessoa.endereco.Estado = "doido viagem";
pessoa.telefones[0].Ddd = 77;
pessoa.telefones[0].NumeroTel = 953577909;

PessoaDAO pessoaDAO = new PessoaDAO();
pessoaDAO.Insert(pessoa);

