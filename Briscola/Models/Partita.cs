using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Briscola.Models
{
    internal class Partita
    {
        public TaskCompletionSource ts = new TaskCompletionSource();
        Random rnd;
        internal Mazzo mazzo { get; set; }
        public Giocatore giocatore;
        public Giocatore ai;
        int giocatoreCheInizia = 0;
        int vittoria = 0;
        MainWindow mainWindow;
        public Carta InputGiocatore { get; set; }
        public string Vittoria { get; set; }

        public string? SemeBriscola { get; set; }
        public int carteGiocate = 0;

        public Partita(MainWindow m)
        {
            //variabili
            this.rnd = new Random();
            this.mazzo = new Mazzo();
            this.giocatore = new Giocatore(1, "Giocatore");
            this.ai = new Giocatore(2, "Bot");
            this.mainWindow = m;
        }

        //sceglie la briscola della partita
        public void ScegliBriscola()
        {
            SemeBriscola = mazzo.ListaCarte.Last().Seme;
        }

        //Gestione Partita
        public async void AvvioPartita()
        {
            Carta giocataGiocatore;
            Carta giocataAi;

            //0. Resetto i punti dei giocatori dalla partita precedente
            giocatore.Punti = 0;
            ai.Punti = 0;

            //1. Determino chi inizia la partita
            giocatoreCheInizia = rnd.Next(1, 3);
            if (giocatoreCheInizia == 1)
                giocatore.Presa = true;
            else
                ai.Presa = true;

            //2. Inizializzo e mescolo il mazzo
            mazzo = new Mazzo();
            this.mazzo.Shuffle();

            //3. Scelgo la Briscola
            ScegliBriscola();

            //3a. Setto all'immagine Briscola la sua source
            mainWindow.img_briscola.Source = new BitmapImage(new Uri($"{mazzo.ListaCarte.Last().Percorso}", UriKind.RelativeOrAbsolute));

            //4. Riempio le mani
            giocatore.RiempiMano(mazzo);
            ai.RiempiMano(mazzo);

            //4a. Setto le source alle immagini della mano del giocatore
            mainWindow.btn0.Content = new Image { Source = new BitmapImage(new Uri($"{giocatore.Mano[0].Percorso}", UriKind.RelativeOrAbsolute)), Stretch = Stretch.Fill};
            mainWindow.btn1.Content = new Image { Source = new BitmapImage(new Uri($"{giocatore.Mano[1].Percorso}", UriKind.RelativeOrAbsolute)), Stretch = Stretch.Fill };
            mainWindow.btn2.Content = new Image { Source = new BitmapImage(new Uri($"{giocatore.Mano[2].Percorso}", UriKind.RelativeOrAbsolute)), Stretch = Stretch.Fill };

            //10. Ripeto il tutto finché il mazzo e le mani non sono vuote
            while (mazzo.ListaCarte.Count > 0 || giocatore.Mano.Count > 0 || ai.Mano.Count > 0)
            {
                if (giocatore.Presa)
                {
                    //5. G1 butta giù la carta
                    await ts.Task;
                    giocataGiocatore = InputGiocatore;

                    //6. G2 butta giù la carta
                    giocataAi = ai.RandomPlay();

                    carteGiocate += 2;
                }
                else
                {
                    //6. G2 butta giù la carta
                    giocataAi = ai.RandomPlay();
                    mainWindow.img_placeholder2.Source = new BitmapImage(new Uri($"{giocataAi.Percorso}", UriKind.RelativeOrAbsolute));
                    mainWindow.img2.Source = null;

                    //5. G1 butta giù la carta
                    await ts.Task;
                    giocataGiocatore = InputGiocatore;

                    carteGiocate += 2;
                }


                //6a. Metto le carte nei placeholder
                if(carteGiocate <= 36)
                {
                    mainWindow.img2.Source = null;
                }
                if(carteGiocate == 38)
                {
                    mainWindow.img1.Source = null;
                }
                if(carteGiocate == 40)
                {
                    mainWindow.img0.Source = null;
                }

                mainWindow.img_placeholder1.Source = new BitmapImage(new Uri($"{giocataGiocatore.Percorso}", UriKind.RelativeOrAbsolute));
                mainWindow.img_placeholder2.Source = new BitmapImage(new Uri($"{giocataAi.Percorso}", UriKind.RelativeOrAbsolute));

                //aspetto un po' per far vedere le carte
                await Task.Delay(700);
                ts = new();                

                if(carteGiocate <= 36)
                {
                    mainWindow.img2.Source = new BitmapImage(new Uri($"carte/legend.png", UriKind.RelativeOrAbsolute));
                }
                if(carteGiocate < 38)
                {
                    mainWindow.img1.Source = new BitmapImage(new Uri($"carte/legend.png", UriKind.RelativeOrAbsolute));
                }
                if(carteGiocate < 40)
                {
                    mainWindow.img0.Source = new BitmapImage(new Uri($"carte/legend.png", UriKind.RelativeOrAbsolute));
                }

                //6b. Svuoto i placeholder
                mainWindow.img_placeholder1.Source = null;
                mainWindow.img_placeholder2.Source = null;

                //7. Determino chi vince
                //8. Assegno i punti
                //8a. Aggiorno il textblock
                if (giocatore.Presa == true)
                {
                    if (giocataGiocatore.Confronto(giocataAi, SemeBriscola) == 1)
                    {
                        giocatore.Punti += giocataGiocatore.Punteggio + giocataAi.Punteggio;
                        mainWindow.tbx_presa.Text = "Ha preso il giocatore!";
                        giocatore.Presa = true;
                        ai.Presa = false;
                    }
                    else
                    {
                        ai.Punti += giocataGiocatore.Punteggio + giocataAi.Punteggio;
                        mainWindow.tbx_presa.Text = "Ha preso il bot!";
                        giocatore.Presa = false;
                        ai.Presa = true;
                    }
                }
                else
                {
                    if (giocataAi.Confronto(giocataGiocatore, SemeBriscola) == 1)
                    {
                        ai.Punti += giocataGiocatore.Punteggio + giocataAi.Punteggio;
                        mainWindow.tbx_presa.Text = "Ha preso il bot!";
                        giocatore.Presa = false;
                        ai.Presa = true;
                    }
                    else
                    {
                        giocatore.Punti += giocataGiocatore.Punteggio + giocataAi.Punteggio;
                        mainWindow.tbx_presa.Text = "Ha preso il giocatore!";
                        giocatore.Presa = true;
                        ai.Presa = false;
                    }
                }
                

                //9. Ripopolo le mani
                if(mazzo.ListaCarte.Count > 0)      //se non entra qua, vuol dire che sono le ultime 3 mani
                {
                    giocatore.RiempiMano(mazzo);
                    ai.RiempiMano(mazzo);
                }
                mainWindow.Debug();

                //9a. Faccio in modo che le immagini scalino e vengano effettivamente mostrate
                if (giocatore.Mano.Count > 0)
                    mainWindow.btn0.Content = new Image { Source = new BitmapImage(new Uri($"{giocatore.Mano[0].Percorso}", UriKind.RelativeOrAbsolute)), Stretch = Stretch.Fill };
                else
                {
                    mainWindow.btn0.Visibility = Visibility.Hidden;
                    mainWindow.btn0.Content = null;
                }

                if(giocatore.Mano.Count > 1)
                    mainWindow.btn1.Content = new Image { Source = new BitmapImage(new Uri($"{giocatore.Mano[1].Percorso}", UriKind.RelativeOrAbsolute)), Stretch = Stretch.Fill };
                else
                {
                    mainWindow.btn1.Visibility = Visibility.Hidden;
                    mainWindow.btn1.Content = null;
                }

                if(giocatore.Mano.Count > 2)
                    mainWindow.btn2.Content = new Image { Source = new BitmapImage(new Uri($"{giocatore.Mano[2].Percorso}", UriKind.RelativeOrAbsolute)), Stretch = Stretch.Fill };
                else
                {
                    mainWindow.btn2.Visibility = Visibility.Hidden;
                    mainWindow.btn2.Content = null;
                }

                if(mazzo.ListaCarte.Count == 0)
                {
                    mainWindow.imgMazzo.Source = null;
                    mainWindow.img_briscola.Source = null;
                }
            }

            //11. Conto i punti di un giocatore
            if (giocatore.Punti > ai.Punti) vittoria = 1;
            if (ai.Punti > giocatore.Punti) vittoria = 2;
            if (giocatore.Punti == ai.Punti) vittoria = 0;

            //12. Determino vittoria/sconfitta/pareggio
            switch (vittoria)
            {
                case 0:
                    Vittoria = "Pareggio.";
                    break;

                case 1:
                    Vittoria = $"Vittoria {giocatore.Nome}.";
                    break;

                case 2:
                    Vittoria =  $"Vittoria {ai.Nome}.";
                    break;

                default:
                    Vittoria = string.Empty;
                    break;
            }

            mainWindow.tbx_presa.Text = Vittoria;
        }

    }
}
