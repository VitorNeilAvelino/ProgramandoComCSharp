using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSharp.Capitulo02.GeradorSenha.Tests
{
    [TestClass()]
    public class SenhaTests
    {
        [TestMethod()]
        public void GerarSenhaSemParametrosDeveRetornarSenhaPadrao()
        {
            //Senha senha = new Senha();
            //var senha = new Senha();
            Senha senha = new();

            string valorSenha = senha.GerarSenha(); // Pode ser privado depois, remover este método.

            Assert.IsTrue(valorSenha.Length == Senha.TamanhoMinimo);
            Assert.IsTrue(int.TryParse(valorSenha, out int _));

            Console.WriteLine(valorSenha);
        }

        [TestMethod()]
        public void ConstrutorPadraoDeveRetornarSenhaPadrao()
        {
            var senha = new Senha();

            Assert.IsTrue(senha.Valor.Length == Senha.TamanhoMinimo);
            Assert.IsTrue(int.TryParse(senha.Valor, out int _));
        }

        [TestMethod]
        [DataRow(6)]
        [DataRow(3)]
        [DataRow(11)]
        public void ConstrutorParametrizadoDeveRetornarSenhaEspecifica(int tamanho)
        {
            var senha = new Senha(tamanho);

            Assert.IsTrue(senha.Valor.Length == senha.Tamanho);
            Assert.IsTrue(long.TryParse(senha.Valor, out long _));
            Assert.IsTrue(senha.Tamanho >= Senha.TamanhoMinimo);
            Assert.IsTrue(senha.Tamanho <= Senha.TamanhoMaximo);

            Console.WriteLine(senha.Valor);
        }
    }
}