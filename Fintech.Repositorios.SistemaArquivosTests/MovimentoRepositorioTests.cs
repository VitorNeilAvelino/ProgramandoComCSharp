using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fintech.Dominio;
using System.Linq;
using System;

namespace Fintech.Repositorios.SistemaArquivos.Tests
{
    [TestClass()]
    public class MovimentoRepositorioTests
    {
        private readonly MovimentoRepositorio repositorio = new ("Dados\\Movimento.txt");

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
        public void DelegateActionTeste()
        {
            var movimentos = repositorio.Selecionar(1, 1);

            Action<Movimento> writeLine = m => Console.WriteLine($"{m.Data:d} - {m.Valor}"); // 2

            //foreach (var item in collection) // pode ser usado o .ForEach abaixo.
            //{

            //}

            movimentos.ForEach(EscreverMovimento); // 1
            movimentos.ForEach(writeLine); // 2.1
            movimentos.ForEach(m => Console.WriteLine($"{m.Data:d} - {m.Valor:c}")); // 3
        }

        private void EscreverMovimento(Movimento movimento) // 1
        {
            Console.WriteLine($"{movimento.Data:d} - {movimento.Valor}");
        }

        [TestMethod]
        public void DelegatePredicateTeste()
        {
            var movimentos = repositorio.Selecionar(1, 1);

            Predicate<Movimento> obterDepositos = m => m.Operacao == Operacao.Deposito; // 2

            var depositos = movimentos.FindAll(EncontrarMovimentoDeposito); // 1
            depositos = movimentos.FindAll(obterDepositos); // 2
            depositos = movimentos.FindAll(m => m.Operacao == Operacao.Deposito); // 3

            depositos.ForEach(d => Console.WriteLine(d.Valor));
        }

        private bool EncontrarMovimentoDeposito(Movimento movimento) // 1
        {
            return movimento.Operacao == Operacao.Deposito;
        }

        [TestMethod]
        public void DelegateFunctionTeste()
        {
            var movimentos = repositorio.Selecionar(1, 1);

            Func<Movimento, decimal> obterCampoValor = m => m.Valor; // 2

            var totalDepositos = movimentos.Where(m => m.Operacao == Operacao.Deposito).Sum(RetornarCampoSoma); // 1
            totalDepositos = movimentos.Where(m => m.Operacao == Operacao.Deposito).Sum(obterCampoValor); // 2
            totalDepositos = movimentos.Where(m => m.Operacao == Operacao.Deposito).Sum(m => m.Valor); // 3
            
            Console.WriteLine(totalDepositos);
        }

        private decimal RetornarCampoSoma(Movimento movimento) //1
        {
            return movimento.Valor;
        }

        [TestMethod]
        public void OrderByTeste()
        {
            var movimentos = repositorio.Selecionar(1, 1)
                .OrderBy(m => m.Valor)
                .ThenBy(m => m.Operacao)
                .ThenByDescending(m => m.Data);

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

        [TestMethod]
        public void GroupByTeste()
        {
            var agrupamento = repositorio.Selecionar(1, 1)
                .GroupBy(m => m.Operacao)
                .Select(g => new { Operacao = g.Key, Total = g.Sum(m => m.Valor) });

            foreach (var item in agrupamento)
            {
                Console.WriteLine($"{item.Operacao}: {item.Total:c}");
            }
        }
    }
}