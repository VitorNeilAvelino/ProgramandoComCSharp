using Fintech.Dominio;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Fintech.Correntista.Wpf._5
{
    public partial class MainWindow : Window
    {
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();

        public MainWindow()
        {
            InitializeComponent();
            PopularControles();
        }

        private void PopularControles()
        {
            sexoComboBox.Items.Add(Sexo.Masculino);
            sexoComboBox.Items.Add(Sexo.Feminino);

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
            cliente.DataNascimento = DateTime.Parse(dataNascimentoTextBox.Text);
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
    }
}