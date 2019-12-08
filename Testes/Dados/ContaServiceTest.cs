using System;
using Dados;
using Dados.Infra;
using Dados.Servicos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testes.Dados
{
    [TestClass]
    public class ContaServiceTest
    {
        private ContaService sConta;
        private ContextoMock<ContaCorrente> _contexto;
        [TestInitialize]
        public void Startup()
        {
            if (this.sConta == null)
            {
                _contexto = new ContextoMock<ContaCorrente>();
                this.sConta = new ContaService(_contexto);

                _contexto.Salvar<ContaCorrente>(new ContaCorrente { Conta = 1, Saldo = 100d });
                _contexto.Salvar<ContaCorrente>(new ContaCorrente { Conta = 2, Saldo = 100d });
            }            
        }

        [TestMethod]
        public void ContaDeveNaoDeveSerLocaliazada()
        {
            var conta = sConta.Conta(999999);
            Assert.IsNull(conta);
        }

        [TestMethod]
        public void ContaNaoDeveSerLocaliazada()
        {
            var conta = sConta.Conta(1);
            Assert.IsNotNull(conta);
        }

        [TestMethod]
        public void DepositoComContaValidaDeveRetornarSaldo()
        {
            var conta = sConta.Depositar(1, 100d);
            Assert.IsNotNull(conta);
        }

        [TestMethod]
        public void SaqueComContaInvalidaDeveRetornarNulo()
        {
            var conta = sConta.Sacar(0, 100d);
            Assert.IsNull(conta);
        }

        [TestMethod]
        public void SaqueComContaValidaNaoDeveRetornarNulo()
        {
            var conta = sConta.Sacar(2, 1d);
            Assert.IsNotNull(conta);
        }

        [TestMethod]
        public void DeveCriarContaLogoAposDepoisitar()
        {
            var conta = sConta.Depositar(3, 100d);
            Assert.IsNotNull(conta);
        }

        [TestMethod]
        public void DeveTrazerSaldoCorretoAposDepositar()
        {
            sConta.Depositar(4, 100d);
            var conta = sConta.Depositar(4, 50d);
            Assert.AreEqual(150d, conta.Saldo);
        }

        [TestMethod]
        public void DeveTrazerSaldoCorretoAposSacar()
        {
            sConta.Depositar(5, 200d);
            var conta = sConta.Sacar(5, 50d);
            Assert.AreEqual(150d, conta.Saldo);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeveOcorrerExcecaoAoSacarValorMaiorQueSaldo()
        {
            sConta.Depositar(6, 200d);
            sConta.Sacar(6, 300d);
        }
    }
}
