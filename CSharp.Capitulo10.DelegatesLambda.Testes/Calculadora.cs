using System;

namespace CSharp.Capitulo10.DelegatesLambda.Testes
{
    public delegate int EfetuarOperacao(int valor1, int valor2);

    public class Calculadora
    {
        public Calculadora(TipoOperacao tipoOperacao)
        {
            TipoOperacao = tipoOperacao;
        }

        private TipoOperacao TipoOperacao { get; set; }

        private int Somar(int x, int y)
        {
            return x + y;
        }

        private int Subtrair(int x, int y)
        {
            return x - y;
        }

        private int Multiplicar(int x, int y, int z)
        {
            return x * y * z;
        }

        public EfetuarOperacao ObterOperacao()
        {
            switch (TipoOperacao)
            {
                case TipoOperacao.Soma:
                    return Somar;
                case TipoOperacao.Subtracao:
                    return Subtrair;
                //case TipoOperacao.Multiplicacao:
                //    return Multiplicar;
            }

            throw new Exception();
        }
    }
}