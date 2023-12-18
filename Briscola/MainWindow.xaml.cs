using Briscola.Models;
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
using System.Threading;

namespace Briscola
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int n;
        Mazzo m;
        Partita p;

        public MainWindow()
        {
            InitializeComponent();
            n = 0;
            p = new Partita();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (n++ == 0) { m = new Mazzo(); }
            else { n--; m.Shuffle(); }

            Listbox.Items.Clear();
            Listbox.Items.Add(m);

            Image.Source = null;
            Image.Source = new BitmapImage(new Uri($"{m.ListaCarte[0].Percorso}", UriKind.RelativeOrAbsolute));
            */

            string punti1, punti2;
            btn.Content = p.AvvioPartita(out punti1, out punti2);
            lblG1Punti.Content = punti1;
            lblG2Punti.Content= punti2;            
        }

    }
}
