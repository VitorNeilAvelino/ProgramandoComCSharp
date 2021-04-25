﻿using Fintech.Dominio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Fintech.Correntista.Wpf._5
{
    public partial class MainWindow : Window
    {
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public Cliente ClienteSelecionado { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            PopularControles();
        }

        private void PopularControles()
        {
            sexoComboBox.Items.Add(Sexo.Masculino);
            sexoComboBox.Items.Add(Sexo.Feminino);

            tipoContaComboBox.Items.Add(TipoConta.ContaCorrente);
            tipoContaComboBox.Items.Add(TipoConta.ContaEspecial);
            tipoContaComboBox.Items.Add(TipoConta.Poupanca);

            bancoComboBox.Items.Add(new Banco {Nome = "Banco 1", Numero = 177 });
            bancoComboBox.Items.Add(new Banco {Nome = "Banco 2", Numero = 178 });

            clienteDataGrid.ItemsSource = Clientes;
        }

        private void IncluirClienteButtonClick(object sender, RoutedEventArgs e)
        {
            var endereco = new Endereco();
            endereco.Logradouro = logradouroTextBox.Text;
            endereco.Numero = numeroLogradouroTextBox.Text;
            endereco.Cidade = cidadeTextBox.Text;
            endereco.Cep = cepTextBox.Text;            

            Cliente cliente = new();
            cliente.Cpf = cpfTextBox.Text;
            cliente.Nome = nomeTextBox.Text;
            cliente.DataNascimento = Convert.ToDateTime(dataNascimentoTextBox.Text, new CultureInfo("pt-BR"));
            cliente.Sexo = (Sexo)sexoComboBox.SelectedItem;
            cliente.EnderecoResidencial = endereco;            

            Clientes.Add(cliente);            

            MessageBox.Show("Cliente cadastrado com sucesso");
            LimparControlesCliente();
            clienteDataGrid.Items.Refresh();
            pesquisaClienteTabItem.Focus();
        }

        private void LimparControlesCliente()
        {
            cpfTextBox.Clear();
            nomeTextBox.Text = "";
            dataNascimentoTextBox.Text = null;
            sexoComboBox.SelectedIndex = -1;
            logradouroTextBox.Text = string.Empty;
            numeroLogradouroTextBox.Clear();
            cidadeTextBox.Clear();
            cepTextBox.Clear();
        }

        private void SelecionarClienteButtonClick(object sender, RoutedEventArgs e)
        {
            var botaoClicado = (Button)sender;
            var clienteSelecionado = botaoClicado.DataContext;
            
            ClienteSelecionado = (Cliente)clienteSelecionado;

            clienteTextBox.Text = $"{ClienteSelecionado.Nome} - {ClienteSelecionado.Cpf}";

            contasTabItem.Focus();
        }

        private void principalTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var abaSelecionada = (TabItem)principalTabControl.SelectedItem;

            if (abaSelecionada.Header.ToString() == "Contas" && ClienteSelecionado == null)
            {
                MessageBox.Show("Selecione o Cliente na aba Clientes");
                clientesTabItem.Focus();
            }
        }
    }
}