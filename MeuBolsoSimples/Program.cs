using System.Drawing;
using static MeuBolsoSimples.Transactions;

namespace MeuBolsoSimples;
class Program
{
    static List<Transactions> transacoes = new List<Transactions>();
     static decimal saldo = 0m;
    static Categorias categorias = new();
    static void Main()
    {
        Console.WriteLine("=== MeuBolsoSimples ===");
        Console.WriteLine("");

        Console.Write("Informe seu saldo atual: R$");
        if (decimal.TryParse(Console.ReadLine(), out decimal valor))
        {
            saldo = valor;
        }
        else
        {
            Console.WriteLine("Valor inválido! Certifique-se de digitar um número válido (ex: 123,45).");
            return;
        }

        while (true)
            {

                Console.Clear();
                Console.WriteLine("=== MeuBolsoSimples ===");
                Console.WriteLine("1. Adicionar Receita");
                Console.WriteLine("2. Adicionar Despesa");
                Console.WriteLine("3. Ver Saldo Atual");
                Console.WriteLine("4. Ver Relatório Geral das Categorias");
                Console.WriteLine("5. Ver Relatório por Categoria");
            Console.WriteLine("6. Exportar Relatório (.txt)");
                Console.WriteLine("7. Sair");
                Console.Write("Escolha uma opção: ");
                var escolha = Console.ReadLine();
            Console.WriteLine("");

            switch (escolha)
                {
                    case "1":
                        AdicionarTransacao(TypeEnum.Receita);
                        break;
                    case "2":
                        AdicionarTransacao(TypeEnum.Despesa);
                        break;
                    case "3":
                        VerSaldo();
                        break;
                case "4":
                    VerCategorias();
                    break;
                case "5":
                    VerCategoria();
                    break;
                case "6":
                    ExportarRelatorio();
                    break;
                case "7":
                    Console.WriteLine("Encerrando...");
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }

    }

    static void AdicionarTransacao(TypeEnum tipo)
    {
        Console.WriteLine($"Adicionando {tipo}");
        Console.Write("Descrição: ");
        string description = Console.ReadLine();

        Console.Write("Valor: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal valor))
        {
            Console.WriteLine("Valor inválido.");
            return;
        }

        Console.WriteLine("Categorias disponíveis:");
        foreach (var cat in Enum.GetValues(typeof(CategoryEnum)))
        {
            Console.WriteLine($"- {cat}");
        }


        Console.Write("Categoria: ");
        string categoriaInput = Console.ReadLine();

        if (!Enum.TryParse<CategoryEnum>(categoriaInput, true, out var categoria))
        {
            Console.WriteLine("Categoria inválida.");
            return;
        }

        categorias.AddCategorias(categoria, valor);


        var transacao = new Transactions(tipo, categoria, description, valor);
        transacoes.Add(transacao);

        if (tipo.Equals(TypeEnum.Receita))
        {
            saldo = saldo + transacao.Valor;
        } else
        {
            saldo = saldo - transacao.Valor;
        }

        Console.WriteLine("Transação adicionada com sucesso!");
    }

    static void VerSaldo()
    {
        Console.WriteLine($"Seu Saldo");

        decimal receitas = transacoes.Where(t => t.Type == TypeEnum.Receita).Sum(t => t.Valor);
        decimal despesas = transacoes.Where(t => t.Type == TypeEnum.Despesa).Sum(t => t.Valor);

        Console.WriteLine($"Receitas: {receitas.ToString("C")}");
        Console.WriteLine($"Despesas: {despesas.ToString("C")}");
        Console.WriteLine($"Saldo Atual: {saldo.ToString("C")}");

    }

    static void VerCategorias()
    {
        categorias.MostrarRelatorio();

    }

    static void VerCategoria()
    {
        foreach (var cat in Enum.GetValues(typeof(CategoryEnum)))
        {
            Console.WriteLine($"- {cat}");
        }
        Console.WriteLine("");

        Console.Write("Qual categoria você gostaria de ver ?: ");
        string categoriaInput = Console.ReadLine();

        if (!Enum.TryParse<CategoryEnum>(categoriaInput, true, out var categoria))
        {
            Console.WriteLine("Categoria inválida.");
            return;
        }

        decimal value = categorias.GetValorPorCategoria(categoria);

        Console.WriteLine($"{categoria}: {value:C}");

    }

    static void ExportarRelatorio()
    {
        categorias.ExportarRelatorio();
    }

}
