using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Briscola.Models
{
    internal class Partita
    {
        Random rnd;
        Mazzo mazzo;
        Giocatore g1;
        Giocatore g2;
        int giocatoreCheInizia = 0;
        int vittoria = 0;
        public string? SemeBriscola { get; set; }

        public Partita()
        {
            //variabili
            this.rnd = new Random();
            this.mazzo = new Mazzo();
            this.g1 = new Giocatore(1, "Nikolas");
            this.g2 = new Giocatore(2, "Faccetta");
        }

        //sceglie la briscola della partita
        public void ScegliBriscola()
        {
            SemeBriscola = mazzo.ListaCarte.Last().Seme;
        }

        //Gestione Partita
        public string AvvioPartita(out string punti1, out string punti2)
        {
            //0. Resetto i punti dei giocatori dalla partita precedente
            g1.Punti = 0;
            g2.Punti = 0;

            //1. Determino chi inizia la partita
            giocatoreCheInizia = rnd.Next(1, 3);

            //2. Inizializzo e mescolo il mazzo
            mazzo = new Mazzo();
            this.mazzo.Shuffle();

            //3. Scelgo la Briscola
            ScegliBriscola();

            //4. Riempio le mani
            g1.RiempiMano(mazzo);
            g2.RiempiMano(mazzo);

            //10. Ripeto il tutto finché il mazzo e le mani non sono vuote
            while (mazzo.ListaCarte.Count > 0 || g1.Mano.Count > 0 || g2.Mano.Count > 0)
            {
                //5. G1 butta giù la carta            
                Carta giocata1 = g1.RandomPlay();

                //6. G2 butta giù la carta
                Carta giocata2 = g2.RandomPlay();

                //7. Determino chi vince
                //8. Assegno i punti
                if(giocatoreCheInizia == 1)
                {
                    if (giocata1.Confronto(giocata2, SemeBriscola) == 1)
                        g1.Punti += giocata1.Punteggio + giocata2.Punteggio;
                    else
                        g2.Punti += giocata1.Punteggio + giocata2.Punteggio;
                }
                else
                {
                    if (giocata2.Confronto(giocata1, SemeBriscola) == 1)
                        g2.Punti += giocata1.Punteggio + giocata2.Punteggio;
                    else
                        g1.Punti += giocata1.Punteggio + giocata2.Punteggio;
                }
                

                //9. Ripopolo le mani
                if(mazzo.ListaCarte.Count > 0)      //se non entra qua, vuol dire che sono le ultime 3 mani
                {
                    g1.RiempiMano(mazzo);
                    g2.RiempiMano(mazzo);
                }
            }

            //11. Conto i punti di un giocatore
            if (g1.Punti > g2.Punti) vittoria = 1;
            if (g2.Punti > g1.Punti) vittoria = 2;
            if (g1.Punti == g2.Punti) vittoria = 0;

            //12. Determino vittoria/sconfitta/pareggio
            switch (vittoria)
            {
                case 0: 
                    punti1 = g1.Punti.ToString(); punti2 = g2.Punti.ToString();
                    return "Pareggio.";

                case 1:
                    punti1 = g1.Punti.ToString(); punti2 = g2.Punti.ToString();
                    return $"Vittora {g1.Nome}.";

                case 2:
                    punti1 = g1.Punti.ToString(); punti2 = g2.Punti.ToString();
                    return $"Vittoria {g2.Nome}.";

                default:
                    punti1 = g1.Punti.ToString(); punti2 = g2.Punti.ToString();
                    return string.Empty;
            }

            //servono i thread


        }

    }
}
