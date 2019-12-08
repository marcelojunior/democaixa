using System;
using System.Collections.Generic;
using Api.Controllers;
using Api.GraphQL;
using Dados;
using Dados.Infra;
using Dados.Servicos;
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Testes.Api
{
    [TestClass]
    public class ControllerTest
    {
        private ContaService sConta;
        private GraphQLController _controller;
        private ContextoMock<ContaCorrente> _contexto;

        [TestInitialize]
        public void Startup()
        {
            if (this._controller == null)
            {
                this._contexto = new ContextoMock<ContaCorrente>();
                this._controller = new GraphQLController(this._contexto);
                this.sConta = new ContaService(_contexto);

                _contexto.Salvar<ContaCorrente>(new ContaCorrente { Conta = 1, Saldo = 300d });
                _contexto.Salvar<ContaCorrente>(new ContaCorrente { Conta = 2, Saldo = 300d });
            }
        }


        [TestMethod]
        public void DeveRetornarSaldoCorreto()
        {
            sConta.Depositar(3, 100d);
            var param = new GraphQLQuery
            {
                Query = @"query { saldo(conta: 3) }"
            };
            var result = this._controller.Post(param).Result;
            if (result != null)
            {
                var dados = ((ExecutionResult)((OkObjectResult)result).Value).Data as Dictionary<string, object>;
                if (dados.Count > 0)
                {
                    var saldo = Convert.ToDouble(dados["saldo"]);
                    Assert.AreEqual(100d, saldo);
                }

            }
            else
            {
                Assert.Fail("Falha ao retornar dados");
            }
        }

        [TestMethod]
        public void DeveSacarSaldoCorreto()
        {
            sConta.Depositar(4, 100d);
            var param = new GraphQLQuery
            {
                Query = @"mutation {sacar(conta: 4, valor: 50) {conta, saldo}}"
            };
            var result = this._controller.Post(param).Result;
            if (result != null)
            {
                var dados = ((ExecutionResult)((OkObjectResult)result).Value).Data as Dictionary<string, object>;
                if (dados.Count > 0)
                {
                    var sacar = ((Dictionary<string, object>)dados["sacar"]);
                    Assert.AreEqual(4,Convert.ToInt32(sacar["conta"]));
                    Assert.AreEqual(50d,Convert.ToDouble(sacar["saldo"]));
                }
            }
            else
            {
                Assert.Fail("Falha ao retornar dados");
            }
        }

        [TestMethod]
        public void NaoDeveDeixarSacarSaldoIncorreto()
        {
            sConta.Depositar(5, 100d);
            var param = new GraphQLQuery
            {
                Query = @"mutation {sacar(conta: 5, valor: 300) {conta, saldo}}"
            };
            var result = this._controller.Post(param).Result;
            if (result != null)
            {
                var dados = ((ExecutionErrors)((BadRequestObjectResult)result).Value);
                if (dados.Count > 0)
                {
                    var msg = dados[0].Message;
                    Assert.IsTrue(!string.IsNullOrWhiteSpace(msg));
                }
            }
            else
            {
                Assert.Fail("Falha ao retornar dados");
            }
        }

        [TestMethod]
        public void DeveDepositarERetornarSaldo()
        {
            var param = new GraphQLQuery
            {
                Query = @"mutation {depositar(conta: 6, valor: 300) {conta, saldo}}"
            };
            var result = this._controller.Post(param).Result;
            if (result != null)
            {
                var dados = ((ExecutionResult)((OkObjectResult)result).Value).Data as Dictionary<string, object>;
                if (dados.Count > 0)
                {
                    var sacar = ((Dictionary<string, object>)dados["depositar"]);
                    Assert.AreEqual(6, Convert.ToInt32(sacar["conta"]));
                    Assert.AreEqual(300d, Convert.ToDouble(sacar["saldo"]));
                }
            }
            else
            {
                Assert.Fail("Falha ao retornar dados");
            }
        }
    }
}
