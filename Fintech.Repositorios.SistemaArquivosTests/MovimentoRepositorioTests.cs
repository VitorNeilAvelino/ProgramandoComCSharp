using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fintech.Dominio;

namespace Fintech.Repositorios.SistemaArquivos.Tests
{
    [TestClass()]
    public class MovimentoRepositorioTests
    {
        [TestMethod()]
        public void IncluirTest()
        {
            var conta = new ContaCorrente();
            conta.Agencia = new Agencia { Numero = 1 };
            conta.Numero = 1;

            var movimento = new Movimento(Operacao.Deposito, 100);
            movimento.Conta = conta;

            var repositorio = new MovimentoRepositorio();
            repositorio.Inserir(movimento);
        }
    }
}