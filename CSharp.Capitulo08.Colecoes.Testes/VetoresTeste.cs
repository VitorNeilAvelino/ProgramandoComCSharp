using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CSharpFundamentos.Capitulo08.VetoresColecoes.Testes
{
    [TestClass]
    public class VetoresTeste
    {
        [TestMethod]
        public void InicializacaoTeste()
        {
            var inteiros = new int[5];
            inteiros[0] = 14;
            inteiros[1] = 1;
            inteiros[2] = 7;
            inteiros[3] = 0;
            inteiros[4] = -14;

            //inteiros[4] = -14;
            //Assert.AreEqual(inteiros[4], 0);

            //inteiros[5] = 4; // IndexOutOfRangeException

            var decimais = new decimal[] { 0.4m, 0.9m, 4, 7.8m }; // É possível omitir o tamanho (4) dentro dos colchetes.

            string[] nomes = { "Vítor", "Avelino" };
            //nomes[-1] = "Teste"; // IndexOutOfRangeException

            var chars = new[] { 'a', 'b', 'c' };

            foreach (var @decimal in decimais)
            {
                Console.WriteLine(@decimal);
            }

            Console.WriteLine($"O tamanho do vetor {nameof(decimais)}: {decimais.Length}");
        }

        [TestMethod]
        public void RedimensionamentoTeste()
        {
            var decimais = new decimal[] { 0.5m, 7, 8.9m };

            Array.Resize(ref decimais, 5);

            decimais[4] = 2.3m;
        }

        [TestMethod]
        public void OrdenacaoTeste() // Usa o quicksort, não duplica o vetor.
        {
            var decimais = new decimal[] { 0.5m, 7, 0.9m, -1.4m };

            Array.Sort(decimais);

            Assert.AreEqual(decimais[0], -1.4m);
        }

        private decimal Media(decimal valor1, decimal valor2) // Precário, usar um vetor de decimais.
        {
            return (valor1 + valor2) / 2;
        }

        private decimal Media(/*params*/ decimal[] valores) // Params - só pode haver um e na última posição.
        {
            var soma = 0m;

            foreach (var valor in valores)
            {
                soma += valor;
            }

            return soma / valores.Length;
        }

        [TestMethod]
        public void ParamsTeste() // Params só funciona com vetores.
        {
            var decimais = new /*decimal*/[] { 0.5m, 7, 0.9m, -1.4m };

            Console.WriteLine(Media(decimais));
            //Console.WriteLine(Media(1.5m, 8, 0.5m, 25)); // Descomentar params de Media para poder usar.
            Console.WriteLine(decimais.Average());
        }

        [TestMethod]
        public void TodaStringEhUmVetorTeste()
        {
            var nome = "Vítor";

            Assert.AreEqual(nome[0], 'V');

            foreach (var @char in nome)
            {
                Console.Write(@char);
            }
        }
    }
}