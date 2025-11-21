using System.Text.Json.Serialization;

public class Conta
{
    public int Numero { get; set; }
    public string Cliente { get; set; }
    public string Cpf { get; set; }
    public string Senha { get; set; }
    public decimal Saldo { get; set; }
    public decimal Limite { get; set; }

    [JsonIgnore]
    public decimal SaldoDisponível => Saldo + Limite;

    public Conta(int numero, string cliente, string cpf, string senha, decimal limite = 0)
    {
        Numero = numero;
        Cliente = cliente;
        Cpf = cpf;
        Senha = senha;
        Limite = limite;
    }

    public bool Sacar(decimal valor)
    {
        if (valor <= 0) return false;
        if (valor > Saldo + Limite) return false;
        Saldo -= valor;
        return true;
    }

    public void Depositar(decimal valor)
    {
        if (valor > 0) Saldo += valor;
    }

    public void AumentarLimite(decimal valor)
    {
        if (valor > 0) Limite += valor;
    }

    public bool DiminuirLimite(decimal valor)
    {
        if (valor <= 0) return false;
        if (valor > Limite) return false;
        Limite -= valor;
        return true;
    }
}
