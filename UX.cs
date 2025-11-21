using static System.Console;

public class UX
{
    private readonly Banco _banco;
    private readonly string _titulo;

    public UX(string titulo, Banco banco)
    {
        _titulo = titulo;
        _banco = banco;
    }

    public void Executar()
    {
        CriarTitulo(_titulo);
        WriteLine(" [1] Criar Conta");
        WriteLine(" [2] Listar Contas");
        WriteLine(" [3] Efetuar Saque");
        WriteLine(" [4] Efetuar Depósito");
        WriteLine(" [5] Aumentar Limite");
        WriteLine(" [6] Diminuir Limite");

        ForegroundColor = ConsoleColor.Red;
        WriteLine("\n [9] Sair");
        ForegroundColor = ConsoleColor.White;

        CriarLinha();
        ForegroundColor = ConsoleColor.Yellow;
        Write(" Digite a opção desejada: ");
        var opcao = ReadLine() ?? "";
        ForegroundColor = ConsoleColor.White;

        switch (opcao)
        {
            case "1": CriarConta(); break;
            case "2": MenuListarContas(); break;
            case "3": EfetuarSaque(); break;
            case "4": EfetuarDeposito(); break;
            case "5": AumentarLimiteConta(); break;
            case "6": DiminuirLimiteConta(); break;
        }

        if (opcao != "9")
        {
            Executar();
        }

        _banco.SaveContas();
    }

    private void CriarConta()
    {
        CriarTitulo(_titulo + " - Criar Conta");
        Write(" Numero:  ");
        var numero = Convert.ToInt32(ReadLine());
        Write(" Cliente: ");
        var cliente = ReadLine() ?? "";
        Write(" CPF:     ");
        var cpf = ReadLine() ?? "";
        Write(" Senha:   ");
        var senha = ReadLine() ?? "";
        Write(" Limite:  ");
        var limite = Convert.ToDecimal(ReadLine());

        var conta = new Conta(numero, cliente, cpf, senha, limite);
        _banco.Contas.Add(conta);

        CriarRodape("Conta criada com sucesso!");
    }

    private void MenuListarContas()
    {
        CriarTitulo(_titulo + " - Listar Contas");
        foreach (var conta in _banco.Contas)
        {
            WriteLine($" Conta: {conta.Numero} - {conta.Cliente}");
            WriteLine($" Saldo: {conta.Saldo:C} | Limite: {conta.Limite:C}");
            WriteLine($" Saldo Disponível: {conta.SaldoDisponível:C}\n");
        }
        CriarRodape();
    }

    private void CriarLinha()
    {
        WriteLine("-------------------------------------------------");
    }

    private void CriarTitulo(string titulo)
    {
        Clear();
        ForegroundColor = ConsoleColor.White;
        CriarLinha();
        ForegroundColor = ConsoleColor.Yellow;
        WriteLine(" " + titulo);
        ForegroundColor = ConsoleColor.White;
        CriarLinha();
    }

    private void CriarRodape(string? mensagem = null)
    {
        CriarLinha();
        ForegroundColor = ConsoleColor.Green;
        if (mensagem != null)
            WriteLine(" " + mensagem);
        Write(" ENTER para continuar");
        ForegroundColor = ConsoleColor.White;
        ReadLine();
    }

    private Conta? BuscarConta()
    {
        Write(" Número da conta: ");
        int numero = Convert.ToInt32(ReadLine());
        return _banco.Contas.FirstOrDefault(c => c.Numero == numero);
    }

    private void EfetuarSaque()
    {
        CriarTitulo(_titulo + " - Saque");

        var conta = BuscarConta();
        if (conta == null)
        {
            CriarRodape("Conta não encontrada!");
            return;
        }

        Write(" Valor do saque: ");
        decimal valor = Convert.ToDecimal(ReadLine());

        if (conta.Sacar(valor))
            CriarRodape("Saque realizado com sucesso!");
        else
            CriarRodape("Falha no saque: valor insuficiente.");
    }

    private void EfetuarDeposito()
    {
        CriarTitulo(_titulo + " - Depósito");

        var conta = BuscarConta();
        if (conta == null)
        {
            CriarRodape("Conta não encontrada!");
            return;
        }

        Write(" Valor do depósito: ");
        decimal valor = Convert.ToDecimal(ReadLine());

        conta.Depositar(valor);
        CriarRodape("Depósito realizado!");
    }

    private void AumentarLimiteConta()
    {
        CriarTitulo(_titulo + " - Aumentar Limite");

        var conta = BuscarConta();
        if (conta == null)
        {
            CriarRodape("Conta não encontrada!");
            return;
        }

        Write(" Valor do aumento: ");
        decimal valor = Convert.ToDecimal(ReadLine());

        conta.AumentarLimite(valor);
        CriarRodape("Limite aumentado!");
    }

    private void DiminuirLimiteConta()
    {
        CriarTitulo(_titulo + " - Diminuir Limite");

        var conta = BuscarConta();
        if (conta == null)
        {
            CriarRodape("Conta não encontrada!");
            return;
        }

        Write(" Valor da redução: ");
        decimal valor = Convert.ToDecimal(ReadLine());

        if (conta.DiminuirLimite(valor))
            CriarRodape("Limite reduzido!");
        else
            CriarRodape("Falha: valor maior do que o limite atual.");
    }
}
