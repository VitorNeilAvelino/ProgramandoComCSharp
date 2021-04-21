using System;

namespace CSharp.Capitulo02.GeradorSenha
{
    public class Senha
    {
        /// <summary>
        /// Gera uma senha numérica com o tamanho mínimo.
        /// </summary>
        /// <returns></returns>        
        //public Senha()
        //{
        //    Valor = GerarSenha();
        //}

        /// <summary>
        /// Gera uma senha numérica com o tamanho especificado.
        /// </summary>
        /// <returns></returns>
        public Senha(int tamanho = TamanhoMinimo)
        {
            tamanho = tamanho < TamanhoMinimo ? TamanhoMinimo : tamanho;
            tamanho = tamanho > TamanhoMaximo ? TamanhoMaximo : tamanho;

            Tamanho = tamanho;

            Valor = GerarSenha();
        }

        ~Senha()
        {
            Console.WriteLine("Hasta la vista, baby!");
        }

        public const int TamanhoMinimo = 4;
        public const int TamanhoMaximo = 10;
        public string Valor { get; private set; }
        public int Tamanho { get; set; } /*= TamanhoMinimo;*///Remover depois que usar no construtor parametrizado.

        /// <summary>
        /// Gera uma senha numérica.
        /// </summary>
        /// <returns></returns>
        /*private*/ public string GerarSenha()
        {
            var senha = "";
            var randomico = new Random();

            for (int i = 0; i < Tamanho; i++)
            {
                senha += randomico.Next(10);
            }

            return senha;
        }
    }
}