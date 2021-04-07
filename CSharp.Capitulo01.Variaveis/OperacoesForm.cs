using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp.Capitulo01.Variaveis
{
    public partial class OperacoesForm : Form
    {
        public OperacoesForm()
        {
            InitializeComponent();
        }

        private void aritmeticasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x = 1;
            var texto = "Texto";
            var data = new DateTime(2021, 04, 07);

            var y = x + texto;
            var z = data + texto;
            //var w = x + data;

            MessageBox.Show(y);
            MessageBox.Show(z);
        }
    }
}
