
namespace MeuBolsoSimples;



internal class Transactions
{
   public enum CategoryEnum
    {
        Alimentação,
        Transporte,
        Lazer,
        Outros
    }

    public enum TypeEnum
    {
        Despesa = 1,
        Receita = 2
    }

    public TypeEnum Type { get; set; }
    public CategoryEnum Categoria { get; set; }
    public string Description { get; set; }
    public decimal Valor { get; set; }

    public Transactions(TypeEnum type, CategoryEnum categoria, string description, decimal valor)
    {
        Type = type;
        Categoria = categoria;
        Description = description;
        Valor = valor;
    }
}
