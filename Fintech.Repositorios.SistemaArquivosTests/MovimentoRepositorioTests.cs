using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fintech.Dominio;
using System.Linq;
using System;

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

        [TestMethod]
        public void OrderByTeste()
        {
            var movimentos = repositorio.Selecionar(1, 1)
                .OrderBy(m => m.Valor)
                .OrderByDescending(m => m.Data);

            var primeiro = movimentos.First();

            //Assert.AreEqual(primeiro.Data, Convert.ToDateTime("02/05/2021 19:28:39"));
            
            Console.WriteLine(primeiro.Data);
        }

        [TestMethod]
        public void CountTeste()
        {
            var depositosConta2 = repositorio.Selecionar(2, 2)
                .Count(m => m.Operacao == Operacao.Deposito);

            Assert.IsTrue(depositosConta2 == 1);
        }

        [TestMethod]
        public void LikeTeste()
        {
            var movimentos = repositorio.Selecionar(1, 1)
                .Where(m => m.Data.ToString().Contains("02/05/2021"));

            foreach (var movimento in movimentos)
            {
                Console.WriteLine(movimento.Data);
            }
        }

        [TestMethod]
        public void MinTeste()
        {
            var menorDeposito = repositorio.Selecionar(1, 1)
                .Where(m => m.Operacao == Operacao.Deposito)
                .Min(m => m.Valor);

            Assert.IsTrue(menorDeposito == 1);
        }

        [TestMethod]
        public void SkipTakeTeste()
        {
            var movimentos = repositorio.Selecionar(1, 1).Skip(1).Take(5).ToList();

            Assert.IsTrue(movimentos.Count == 5);
        }
    }
}