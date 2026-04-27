using System;
using Microsoft.EntityFrameworkCore;
using SistemaEPI.Data;
using SistemaEPI.Models;

// ==========================================
// ROTEADOR - MENU PRINCIPAL
// ==========================================
while (true)
{
    Console.WriteLine("\n=========================================");
    Console.WriteLine("   SISTEMA DE GESTÃO - MENU PRINCIPAL");
    Console.WriteLine("=========================================");
    Console.WriteLine("1 - Módulo de EPIs");
    Console.WriteLine("2 - Módulo de Funcionários");
    Console.WriteLine("3 - Módulo de Usuários");
    Console.WriteLine("0 - Desligar sistema");
    Console.Write("\nDigite o módulo desejado: ");

    string opcaoPrincipal = Console.ReadLine() ?? "";

    if (opcaoPrincipal == "0")
    {
        Console.WriteLine("Desligando as máquinas... Até logo!");
        break;
    }
    else if (opcaoPrincipal == "1")
    {
        MenuEpis();
    }
    else if (opcaoPrincipal == "2")
    {
        MenuFuncionarios();
    }
    else if (opcaoPrincipal == "3")
    {
        MenuUsuarios();
    }
    else
    {
        Console.WriteLine("\n=> ERRO: Módulo inválido.");
    }
}

// ==========================================
// MÉTODOS DOS MÓDULOS (FUNÇÕES)
// ==========================================

static void MenuEpis()
{
    while (true)
    {
        Console.WriteLine("\n--- MÓDULO DE EPIs ---");
        Console.WriteLine("1 - Cadastrar novo EPI");
        Console.WriteLine("2 - Consultar estoque");
        Console.WriteLine("3 - Excluir EPI");
        Console.WriteLine("4 - Atualizar EPI");
        Console.WriteLine("0 - Voltar ao Menu Principal");
        Console.Write("\nDigite a opção desejada: ");

        string opcao = Console.ReadLine() ?? "";

        if (opcao == "0") break; 

        using (var db = new ApplicationDbContext())
        {
            if (opcao == "1")
            {
                Console.WriteLine("\n--- NOVO CADASTRO ---");
                Console.Write("Digite o Nome do equipamento: ");
                string nomeDigitado = Console.ReadLine() ?? "";

                Console.Write("Digite o número do CA: ");
                string caDigitado = Console.ReadLine() ?? "";

                var novoEpi = new Epi
                {
                    Nome = nomeDigitado,
                    NumeroCA = caDigitado,
                    ValidadeCA = new DateTime(2028, 1, 1),
                    PeriodicidadeTrocaDias = 180,
                    IsAtivo = true
                };

                db.Epis.Add(novoEpi);
                db.SaveChanges();
                Console.WriteLine("=> SUCESSO: EPI salvo no banco de dados!");
            }
            else if (opcao == "2")
            {
                Console.WriteLine("\n--- ESTOQUE ATUAL ---");
                var listaDeEpis = db.Epis.ToList();

                if (listaDeEpis.Count == 0)
                {
                    Console.WriteLine("O estoque está vazio.");
                }
                else
                {
                    foreach (var epi in listaDeEpis)
                    {
                        Console.WriteLine($"[ID: {epi.Id}] {epi.Nome} | CA: {epi.NumeroCA} | Status: {(epi.IsAtivo ? "Ativo" : "Inativo")}");
                    }
                }
            }
            else if (opcao == "3")
            {
                Console.WriteLine("\n--- EXCLUIR EQUIPAMENTO ---");
                Console.Write("Digite o ID do EPI que deseja excluir: ");
                string idDigitado = Console.ReadLine() ?? "";

                if (int.TryParse(idDigitado, out int idParaExcluir))
                {
                    var EpiParaRemover = db.Epis.IgnoreQueryFilters().FirstOrDefault(e => e.Id == idParaExcluir);

                    if (EpiParaRemover != null)
                    {
                        if (EpiParaRemover.DeletedAt != null)
                        {
                            Console.WriteLine($"=> AVISO: O EPI '{EpiParaRemover.Nome}' já se encontra na lixeira desde {EpiParaRemover.DeletedAt}.");
                        }
                        else
                        {
                            EpiParaRemover.DeletedAt = DateTime.Now;
                            db.SaveChanges();
                            Console.WriteLine($"=> SUCESSO: O EPI '{EpiParaRemover.Nome}' foi movido para a lixeira permanentemente das consultas!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("=> ERRO: Nenhum EPI encontrado com esse ID no sistema.");
                    }
                }
                else
                {
                    Console.WriteLine("=> ERRO: ID numérico inválido.");
                }
            }
            else if (opcao == "4")
            {
                Console.WriteLine("\n--- ATUALIZAR EQUIPAMENTO ---");
                Console.Write("Digite o ID do EPI que deseja atualizar: ");
                string idDigitado = Console.ReadLine() ?? "";

                if (int.TryParse(idDigitado, out int idParaEditar))
                {
                    var epi = db.Epis.Find(idParaEditar);

                    if (epi != null)
                    {
                        Console.WriteLine($"\nEditando: {epi.Nome} | CA atual: {epi.NumeroCA}");
                        Console.WriteLine("(Deixe em branco e aperte Enter para manter o valor atual)");

                        Console.Write("Novo Nome: ");
                        string novoNome = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(novoNome)) 
                        {
                            epi.Nome = novoNome;
                        }

                        Console.Write("Novo CA: ");
                        string novoCA = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(novoCA)) 
                        {
                            epi.NumeroCA = novoCA;
                        }

                        db.SaveChanges(); 
                        Console.WriteLine("\n=> SUCESSO: Equipamento atualizado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("=> ERRO: Nenhum EPI encontrado com esse ID.");
                    }
                }
                else
                {
                    Console.WriteLine("=> ERRO: ID numérico inválido.");
                }
            }
            else
            {
                Console.WriteLine("\n=> ERRO: Comando inválido. Tente novamente.");
            }
        }
    }
}

static void MenuFuncionarios()
{
    while (true)
    {
        Console.WriteLine("\n--- MÓDULO DE FUNCIONÁRIOS ---");
        Console.WriteLine("1 - Cadastrar Funcionário");
        Console.WriteLine("2 - Consultar Funcionários");
        Console.WriteLine("3 - Excluir Funcionário");
        Console.WriteLine("4 - Atualizar Funcionário");
        Console.WriteLine("0 - Voltar ao Menu Principal");
        Console.Write("\nDigite a opção desejada: ");

        string opcao = Console.ReadLine() ?? "";

        if (opcao == "0") break;

        using (var db = new ApplicationDbContext())
        {
            if (opcao == "1")
            {
                Console.WriteLine("\n--- CADASTRO DE NOVO FUNCIONÁRIO ---");
                
                Console.Write("Nome Completo: ");
                string nome = Console.ReadLine() ?? "";

                string cpf = "";
                while (string.IsNullOrWhiteSpace(cpf))
                {
                    Console.Write("CPF: ");
                    cpf = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(cpf))
                    {
                        Console.WriteLine("=> ERRO: O CPF não pode ficar vazio. Tente novamente.");
                    }
                }
                
                Console.Write("Cargo: ");
                string cargo = Console.ReadLine() ?? "";

                Console.Write("Setor: ");
                string setor = Console.ReadLine() ?? "";

                var novoFuncionario = new Funcionario
                {
                    Nome = nome,
                    Cpf = cpf,
                    Cargo = cargo,
                    Setor = setor,
                    DataAdmissao = DateTime.Now,
                    IsAtivo = true
                };

                db.Funcionarios.Add(novoFuncionario);
                db.SaveChanges();
                Console.WriteLine("\n=> SUCESSO: Funcionário cadastrado com sucesso!");
            }

            else if (opcao == "2")
            {
                Console.WriteLine("\n--- LISTA DE FUNCIONÁRIOS ---");
                var listaFuncionarios = db.Funcionarios.ToList();

                if (listaFuncionarios.Count == 0)
                {
                    Console.WriteLine("Nenhum funcionário ativo encontrado no sistema.");
                }
                else
                {
                    foreach (var func in listaFuncionarios)
                    {
                        Console.WriteLine($"[ID: {func.Id}] {func.Nome} | Cargo: {func.Cargo} | Setor: {func.Setor}");
                    }
                }
            }

            else if (opcao == "3")
            {
                Console.WriteLine("\n--- EXCLUIR FUNCIONÁRIO ---");
                Console.Write("Digite o ID do funcionário que deseja excluir: ");
                string idDigitado = Console.ReadLine() ?? "";

                if (int.TryParse(idDigitado, out int idParaExcluir))
                {
                    var funcionarioParaRemover = db.Funcionarios.IgnoreQueryFilters().FirstOrDefault(f => f.Id == idParaExcluir);

                    if (funcionarioParaRemover != null)
                    {
                        if (funcionarioParaRemover.DeletedAt != null)
                        {
                            Console.WriteLine($"=> AVISO: O(a) funcionário(a) '{funcionarioParaRemover.Nome}' já se encontra na lixeira desde {funcionarioParaRemover.DeletedAt}.");
                        }
                        else
                        {
                            funcionarioParaRemover.DeletedAt = DateTime.Now;
                            db.SaveChanges();
                            Console.WriteLine($"=> SUCESSO: O(a) funcionário(a) '{funcionarioParaRemover.Nome}' foi movido(a) para a lixeira permanentemente!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("=> ERRO: Nenhum funcionário encontrado com esse ID no sistema.");
                    }
                }
                else
                {
                    Console.WriteLine("=> ERRO: ID numérico inválido.");
                }
            }

            else if (opcao == "4")
            {
                Console.WriteLine("\n--- ATUALIZAR FUNCIONÁRIO ---");
                Console.Write("Digite o ID do funcionário que deseja atualizar: ");
                string idDigitado = Console.ReadLine() ?? "";

                if (int.TryParse(idDigitado, out int idParaEditar))
                {
                    var func = db.Funcionarios.Find(idParaEditar);

                    if (func != null)
                    {
                        Console.WriteLine($"\nEditando: {func.Nome} | Cargo atual: {func.Cargo} | Setor atual: {func.Setor}");
                        Console.WriteLine("(Deixe em branco e aperte Enter para manter o valor atual)");

                        Console.Write("Novo Nome: ");
                        string novoNome = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(novoNome)) func.Nome = novoNome;

                        Console.Write("Novo Cargo: ");
                        string novoCargo = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(novoCargo)) func.Cargo = novoCargo;

                        Console.Write("Novo Setor: ");
                        string novoSetor = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(novoSetor)) func.Setor = novoSetor;

                        db.SaveChanges(); 
                        Console.WriteLine("\n=> SUCESSO: Cadastro do funcionário atualizado!");
                    }
                    else
                    {
                        Console.WriteLine("=> ERRO: Nenhum funcionário encontrado com esse ID.");
                    }
                }
                else
                {
                    Console.WriteLine("=> ERRO: ID numérico inválido.");
                }
            }
            else
            {
                Console.WriteLine("\n=> ERRO: Comando inválido. Tente novamente.");
            }
        }
    }
}

static void MenuUsuarios()
{
    while (true)
    {
        Console.WriteLine("\n--- MÓDULO DE USUÁRIOS (SISTEMA) ---");
        Console.WriteLine("1 - Cadastrar Usuário");
        Console.WriteLine("2 - Consultar Usuários");
        Console.WriteLine("3 - Excluir Usuário");
        Console.WriteLine("4 - Atualizar Usuário");
        Console.WriteLine("0 - Voltar ao Menu Principal");
        Console.Write("\nDigite a opção desejada: ");

        string opcao = Console.ReadLine() ?? "";

        if (opcao == "0") break;

        using (var db = new ApplicationDbContext())
        {
            if (opcao == "1")
            {
                Console.WriteLine("\n--- NOVO CADASTRO DE USUÁRIO ---");
                
                Console.Write("Digite o Nome: ");
                string nomeDigitado = Console.ReadLine() ?? "";

                Console.Write("Digite o E-mail: ");
                string emailDigitado = Console.ReadLine() ?? "";

                Console.Write("Digite a Senha (temporário sem criptografia): ");
                string senhaDigitada = Console.ReadLine() ?? "";

                Console.WriteLine("Níveis de acesso comuns: Admin, Almoxarifado, RH");
                Console.Write("Digite o Nível de Acesso: ");
                string nivelDigitado = Console.ReadLine() ?? "";

                var novoUsuario = new Usuario
                {
                    Nome = nomeDigitado,
                    Email = emailDigitado,
                    SenhaHash = senhaDigitada,
                    NivelAcesso = nivelDigitado,
                    IsAtivo = true
                };

                db.Usuarios.Add(novoUsuario);
                db.SaveChanges();
                Console.WriteLine($"=> SUCESSO: Usuário '{novoUsuario.Nome}' criado com permissão de {novoUsuario.NivelAcesso}!");
            }
            else if (opcao == "2")
            {
                Console.WriteLine("\n--- LISTA DE USUÁRIOS ---");
                var listaUsuarios = db.Usuarios.ToList(); 

                if (listaUsuarios.Count == 0)
                {
                    Console.WriteLine("Nenhum usuário ativo no sistema.");
                }
                else
                {
                    foreach (var user in listaUsuarios)
                    {
                        Console.WriteLine($"[ID: {user.Id}] {user.Nome} | E-mail: {user.Email} | Acesso: {user.NivelAcesso}");
                    }
                }
            }
            else if (opcao == "3")
            {
                Console.WriteLine("\n--- EXCLUIR USUÁRIO ---");
                Console.Write("Digite o ID do usuário que deseja excluir: ");
                string idDigitado = Console.ReadLine() ?? "";

                if (int.TryParse(idDigitado, out int idParaExcluir))
                {
                    var userParaRemover = db.Usuarios.IgnoreQueryFilters().FirstOrDefault(u => u.Id == idParaExcluir);

                    if (userParaRemover != null)
                    {
                        if (userParaRemover.DeletedAt != null)
                        {
                            Console.WriteLine($"=> AVISO: O usuário '{userParaRemover.Nome}' já se encontra desativado desde {userParaRemover.DeletedAt}.");
                        }
                        else
                        {
                            userParaRemover.DeletedAt = DateTime.Now;
                            db.SaveChanges();
                            Console.WriteLine($"=> SUCESSO: O acesso do usuário '{userParaRemover.Nome}' foi revogado permanentemente!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("=> ERRO: Nenhum usuário encontrado com esse ID no sistema.");
                    }
                }
                else
                {
                    Console.WriteLine("=> ERRO: ID numérico inválido.");
                }
            }
            else if (opcao == "4")
            {
                Console.WriteLine("\n--- ATUALIZAR USUÁRIO ---");
                Console.Write("Digite o ID do usuário que deseja atualizar: ");
                string idDigitado = Console.ReadLine() ?? "";

                if (int.TryParse(idDigitado, out int idParaEditar))
                {
                    var user = db.Usuarios.Find(idParaEditar);

                    if (user != null)
                    {
                        Console.WriteLine($"\nEditando: {user.Nome} | Nível atual: {user.NivelAcesso}");
                        Console.WriteLine("(Deixe em branco e aperte Enter para manter o valor atual)");

                        Console.Write("Novo Nome: ");
                        string novoNome = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(novoNome)) user.Nome = novoNome;

                        Console.Write("Novo E-mail: ");
                        string novoEmail = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(novoEmail)) user.Email = novoEmail;

                        Console.Write("Novo Nível de Acesso: ");
                        string novoNivel = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(novoNivel)) user.NivelAcesso = novoNivel;

                        db.SaveChanges(); 
                        Console.WriteLine("\n=> SUCESSO: Cadastro do usuário atualizado!");
                    }
                    else
                    {
                        Console.WriteLine("=> ERRO: Nenhum usuário encontrado com esse ID.");
                    }
                }
                else
                {
                    Console.WriteLine("=> ERRO: ID numérico inválido.");
                }
            }
            else
            {
                Console.WriteLine("\n=> ERRO: Comando inválido. Tente novamente.");
            }
        }
    }
}