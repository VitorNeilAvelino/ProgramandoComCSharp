using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Fintech.Dominio.Tests
{
    [TestClass]
    public class ContaEspecialTests
    {
        [TestMethod]
        public void EfetuarOperacoesTeste()
        {
            var conta = new ContaEspecial() { Limite = 1000 };            

            conta.EfetuarOperacao(50m, Operacao.Deposito);
            Assert.IsTrue(conta.Saldo == 50m);

            conta.EfetuarOperacao(20m, Operacao.Saque);
            Assert.IsTrue(conta.Saldo == 30m);

            conta.EfetuarOperacao(40m, Operacao.Saque);
            Assert.IsTrue(conta.Saldo == -10m);

            conta.EfetuarOperacao(990m, Operacao.Saque);
            Assert.IsTrue(conta.Saldo == -1000m);

            conta.EfetuarOperacao(10m, Operacao.Saque);
            Assert.IsTrue(conta.Saldo == -1000m);

            conta.EfetuarOperacao(1000m, Operacao.Deposito);
            Assert.IsTrue(conta.Saldo == 0m);

            Assert.AreEqual(conta.Movimentos.Count, 5);

            var depositos = conta.Movimentos.Where(m => m.Operacao == Operacao.Deposito).Sum(m => m.Valor);
            var saques = conta.Movimentos.Where(m => m.Operacao == Operacao.Saque).Sum(m => m.Valor);

            Assert.AreEqual(conta.Saldo, depositos - saques);
        }
    }
}