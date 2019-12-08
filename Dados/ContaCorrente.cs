using Dados.Infra;

namespace Dados
{
    public class ContaCorrente : EntidadeBase
    {
        public int Conta { get; set; }
        public double Saldo { get; set; } = 0d;
    }
}
