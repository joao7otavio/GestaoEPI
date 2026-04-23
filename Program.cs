using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using SistemaEPI.Data;
using SistemaEPI.Models;

while (true)
{
    Console.WriteLine("\n==================================");
    Console.WriteLine("   SISTEMA DE GESTÃO DE EPIs");
    Console.WriteLine("==================================");
    Console.WriteLine("1 - Cadastrar novo EPI");
    Console.WriteLine("2 - Consultar estoque");
    Console.WriteLine("3 - Excluir EPI");
    Console.WriteLine("4 - Atualizar EPI");
    Console.WriteLine("0 - Desligar sistema");
    Console.Write("\nDigite a opção desejada: ");

    string opcao = Console.ReadLine() ?? "";

    if (opcao == "0")
    {
        
        Console.WriteLine("Desligando as máquinas... Até logo!");
        break;
    }

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
                        Console.WriteLine($"=> SUCESSO: O EPI '{EpiParaRemover.Nome}' foi excluído permanentemente das consultas!");
                    }
                }
                else
                {
                    Console.WriteLine("=> ERRO: Nenhum EPI encontrado com esse ID no sistema.");

                }
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