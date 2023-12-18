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
        Partita p;

        public MainWindow()
        {
            InitializeComponent();
            p = new Partita(this);
            Start();
        }

        private async void Start()
        {
            p.AvvioPartita();
            Debug();
        }

        private async void btn_Click(object sender, RoutedEventArgs e)
        {
            if(p.ts.Task.Status != TaskStatus.RanToCompletion)
            {
                //registro la carta giocata dal giocatore
                p.InputGiocatore = p.giocatore.Mano[int.Parse(((Button)sender).Name.Substring(3))];

                //rimuovo la carta dalla mano effettiva del giocatore
                p.giocatore.Mano.RemoveAt(int.Parse(((Button)sender).Name.Substring(3)));

                //setto l'immagine a vuota
                ((Button)sender).Content = null;

                //dico all'altro threade di proseguire
                p.ts.SetResult();
            }
        }

        internal void Debug()
        {
            lbl_debugAi.Items.Clear();
            for (int i = 0; i < p.ai.Mano.Count; i++)
            {
                lbl_debugAi.Items.Add(p.ai.Mano[i].ToString());
            }
            lbl_debugAi.Items.Add(p.ai.Punti.ToString() + " Punti");
            lbl_debugAi.Items.Add(p.carteGiocate.ToString());

            lbl_debugGiocatore.Items.Clear();
            for (int i = 0; i < p.giocatore.Mano.Count; i++)
            {
                lbl_debugGiocatore.Items.Add(p.giocatore.Mano[i].ToString());
            }
            lbl_debugGiocatore.Items.Add(p.giocatore.Punti.ToString() + " Punti");
        }
    }
}
