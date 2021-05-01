using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fintech.Dominio;
using System.Linq;

namespace Fintech.Repositorios.SistemaArquivos.Tests
{
    [TestClass()]
    public class MovimentoRepositorioTests
    {
        private readonly MovimentoRepositorio repositorio = new MovimentoRepositorio("Dados\\Movimento.txt");

        [TestMethod()]
        public void IncluirTest()
        {
            var conta = new ContaCorrente();
            conta.Agencia = new Agencia { Numero = 1 };
            conta.Numero = 1;

            var movimento = new Movimento(Operacao.Deposito, 100);
            movimento.Conta = conta;

            repositorio.Inserir(movimento);
        }

        [TestMethod]
        public void SelecionarTeste()
        {
            var movimentos = repositorio.Selecionar(1, 1);
            var totalDepositos = movimentos.Where(m => m.Operacao == Operacao.Deposito).Sum(m => m.Valor);
            var totalSaques = movimentos.Where(m => m.Operacao == Operacao.Saque).Sum(m => m.Valor);

            var contaCorrente = new ContaCorrente();
            contaCorrente.Movimentos.AddRange(movimentos);

            Assert.AreEqual(contaCorrente.Saldo, totalDepositos - totalSaques);
        }
    }
}