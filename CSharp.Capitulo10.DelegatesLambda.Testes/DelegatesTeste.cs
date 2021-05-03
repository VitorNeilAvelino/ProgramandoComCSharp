using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharp.Capitulo10.DelegatesLambda.Testes
{
    [TestClass]
    public class DelegateTestes
    {
        [TestMethod]
        [DataRow(TipoOperacao.Soma, 1, 2, 3)]
        [DataRow(TipoOperacao.Subtracao, 1, 2, -1)]
        public void CalculadoraTeste(TipoOperacao tipoOperacao, int x, int y, int resultado)
        {
            var operacao = new Calculadora(tipoOperacao).ObterOperacao();

            Assert.IsTrue(operacao(x, y) == resultado);
        }

        [TestMethod]
        public void MetodoAnonimoTeste()
        {
            EfetuarOperacao divisao = delegate (int x, int y)
            {
                return x / y;
            };

            Assert.IsTrue(divisao(6, 3) == 2);
        }

        [TestMethod]
        public void ExpressaoLambdaTeste()
        {
            //EfetuarOperacao multiplicacao = (x, y) =>
            //{
            //    return x * y;
            //};

            EfetuarOperacao multiplicacao = (x, y) => x * y;

            Assert.IsTrue(multiplicacao(6, 3) == 18);
        }
    }
}