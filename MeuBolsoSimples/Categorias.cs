
using static MeuBolsoSimples.Transactions;

namespace MeuBolsoSimples;

internal class Categorias
{
   private Dictionary<CategoryEnum, decimal> category = new Dictionary<CategoryEnum, decimal>();

    public void AddCategorias(CategoryEnum categoryString, decimal value)
    {
        if (category.ContainsKey(categoryString))
        {
            category[categoryString] += value; 
        }
        else
        {
            category.Add(categoryString, value);
        }
    }

    public decimal GetValorPorCategoria(CategoryEnum nomeCategoria)
        
    {
        //está procurando no dicionario a chave (nomeCategoria) e coloca o valor correspondente dentro da variavel valor

        return category.TryGetValue(nomeCategoria, out var valor) ? valor : 0m;
    }

    public void MostrarRelatorio()
    {
        Console.WriteLine("\n=== Relatório por Categoria ===");
        if (category.Count <= 0 )
        {
           Console.WriteLine("Sem Registro");

        } else { 
            foreach (var cat in category)
            {
                Console.WriteLine($"{cat.Key}: R$ {cat.Value:F2}");
            }
            }
    }

    public void ExportarRelatorio()
    {
        string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        string caminhoArquivo = Path.Combine(downloadsPath, "relatorio.txt");

        using (StreamWriter writer = new StreamWriter(caminhoArquivo))
        {
            writer.WriteLine("=== Relatório por Categoria ===");

            if (category.Count > 0)
            {
                foreach (var cat in category)
                {
                    writer.WriteLine($"{cat.Key}: R$ {cat.Value:F2}");
                }
            } else
            {
                writer.WriteLine("Sem dados");

            }
        }

        Console.WriteLine($"\nRelatório exportado com sucesso para: {caminhoArquivo}");
    }


}
