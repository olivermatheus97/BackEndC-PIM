using ConsoleApp1.Models;

Pessoa pessoa = new Pessoa();
Endereco endereco = new Endereco();
Telefone telefone = new Telefone();
TipoTelefone tipoTelefone = new TipoTelefone();

tipoTelefone.Tipo = "Celular";
telefone.TipoTel = tipoTelefone;
pessoa.endereco = endereco;
pessoa.telefones = new Telefone[2];
pessoa.telefones[0] = telefone;

pessoa.Nome = "Testando Update";
pessoa.Cpf = 12345678912;
pessoa.endereco.Logradouro = "a~çoisdhypoisdh aoisdhaoi´shdaoihsdoiash";
pessoa.endereco.NumeroEnd = 29;
pessoa.endereco.Cep = 74450000;
pessoa.endereco.Bairro = "Setor update";
pessoa.endereco.Cidade = "Updatecity";
pessoa.endereco.Estado = "UP";
pessoa.telefones[0].Ddd = 99;
pessoa.telefones[0].NumeroTel = 982141400;

PessoaDAO pessoaDAO = new PessoaDAO();
//pessoaDAO.Insert(pessoa);
//pessoaDAO.Delete(pessoa);
//pessoaDAO.Consultar(55512312395);
pessoaDAO.Update(pessoa);
