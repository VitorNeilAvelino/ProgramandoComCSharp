using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharp.Capitulo02.EstruturasControle.Testes
{
    [TestClass]
    public class DecisaoTeste
    {
        [TestMethod]
        public void AvaliacaoFinalReprovadoTeste()
        {
            var notaFinal = 2.9;
            var resultadoFinal = string.Empty;

            if (notaFinal < 3)
            {
                resultadoFinal = "Reprovado";
            }
            else if (notaFinal is >= 3 and < 5)
            {
                resultadoFinal = "Recuperação";
            }
            else
            {
                resultadoFinal = "Aprovado";
            }

            Assert.AreEqual(resultadoFinal, "Reprovado");
        }

        [TestMethod]
        public void AvaliacaoFinalRecuperacao3Teste()
        {
            var notaFinal = 3;
            var resultadoFinal = string.Empty;

            switch (notaFinal)
            {
                case < 3:
                    resultadoFinal = "Reprovado";
                    break;
                case >= 3 and < 5:
                    resultadoFinal = "Recuperação";
                    break;
                default:
                    resultadoFinal = "Aprovado";
                    break;
            }

            Assert.AreEqual(resultadoFinal, "Recuperação");
        }

        [TestMethod]
        public void AvaliacaoFinalRecuperacao49Teste()
        {
            var notaFinal = 4.9;
            var resultadoFinal = string.Empty;

            resultadoFinal = notaFinal switch
            {
                < 3 => "Reprovado",
                >= 3 and < 5 => "Recuperação",
                _ => "Aprovado"
            };

            Assert.AreEqual(resultadoFinal, "Recuperação");
        }

        [TestMethod]
        [DataRow(2.9, "Reprovado")]
        [DataRow(3, "Recuperação")]
        [DataRow(4.9, "Recuperação")]
        [DataRow(5, "Aprovado")]
        public void AvaliacaoFinalTeste(double notaFinal, string resultadoEsperado)
        {
            var resultadoFinal = notaFinal switch
            {
                < 3 => "Reprovado",
                >= 3 and < 5 => "Recuperação",
                _ => "Aprovado"
            };

            Assert.AreEqual(resultadoFinal, resultadoEsperado);
        }
    }
}