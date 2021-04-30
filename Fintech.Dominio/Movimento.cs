﻿using System;

namespace Fintech.Dominio
{
    public class Movimento
    {
        public Movimento(Operacao operacao, decimal valor)
        {
            Operacao = operacao;
            Valor = valor;
        }

        public Conta Conta { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public Operacao Operacao { get; set; }
        public decimal Valor { get; set; }
    }
}