using System;
using System.Linq;
using Dados.Infra;

namespace Dados.Servicos
{
    public class ContaService
    {
        private readonly IContexto _contexto;
        private ContaCorrente contaCorrente;
        public ContaService(IContexto contexto)
        {
            this._contexto = contexto;
        }

        public ContaCorrente Conta(int conta)
        {
            this.contaCorrente = _contexto.Buscar<ContaCorrente>(m => m.Conta == conta).FirstOrDefault();
            return this.contaCorrente;
        }

        public ContaCorrente Depositar(int conta, double valor)
        {
            contaCorrente = _contexto.Buscar<ContaCorrente>(m => m.Conta == conta).FirstOrDefault();
            if (contaCorrente == null)
            {
                contaCorrente = new ContaCorrente
                {
                    Conta = conta,
                    Saldo = valor
                };
                this._contexto.Salvar(contaCorrente);
            }
            else
            {
                contaCorrente.Saldo += valor;
                this._contexto.Salvar(contaCorrente);
            }

            return contaCorrente;
        }

        public ContaCorrente Sacar(int conta, double valor)
        {
            contaCorrente = _contexto.Buscar<ContaCorrente>(m => m.Conta == conta).FirstOrDefault();

            if (contaCorrente == null)
            {
                return null;
            }

            if (contaCorrente.Saldo < valor)
            {
                throw new Exception("Saldo insuficiente");
            }

            contaCorrente.DataAlteracao = DateTime.Now;
            contaCorrente.Saldo -= valor;
            this._contexto.Salvar(contaCorrente);

            return contaCorrente;
        }
    }
}
