using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVMPropGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                StringBuilder result = new StringBuilder();

                result.AppendLine("private " + queryTypeBox.Text + " _" + queryBox.Text + ";");
                result.AppendLine(" ");
                result.AppendLine("public " + queryTypeBox.Text + " " + queryBox.Text);
                result.AppendLine("{\n\tget\n\t{\n\t\treturn _" + queryBox.Text + ";" + "\n\t}");
                result.AppendLine("\tset\n\t{\n\t\tif(_" + queryBox.Text + " != value)");
                result.AppendLine("\t\t{\n\t\t\t_" + queryBox.Text + "= value;\n");
                result.AppendLine("\t\t\tRaisePropertyChanged(nameof(" + queryBox.Text + "));\n\t\t}\n\t}\n}");
                             
                resultBox.Text = result.ToString();
            }
        }
    }
}
