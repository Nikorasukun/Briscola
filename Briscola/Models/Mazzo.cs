using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briscola.Models
{
    internal class Mazzo
    {
        //variabili ez
        public List<Carta> ListaCarte { get; set; }

        public Mazzo()
        {
            ListaCarte = new List<Carta>();
            CreaMazzo();
        }

        //creo le 40 carte del mazzo
        public void CreaMazzo()
        {
            //creo una carta fittizia
            Carta c;

            //due for per ciclare il seme e il numero
            //seme
            for(int n = 1; n < 5 ; n++)
            {
                //numero
                for(int s = 1; s < 11; s++)
                {
                    c = new Carta(s,n);             //da implementare il percorso

                    //aggiungo la carta al mazzo
                    ListaCarte.Add(c);
                }
            }
        }

        //mescolo il mazzo 100 volte
        public void Shuffle()
        {
            Carta c;
            Random rnd = new Random();
            int n = 0;

            for(int i = 0; i < 100 ; i++)
            {
                n = rnd.Next(0,40);
                c = ListaCarte[n];
                ListaCarte.RemoveAt(n);
                ListaCarte.Add(c);
            }
        }

        //elimino la carta in cima e la ritorno
        public Carta TopDeck()
        {
            Carta c = ListaCarte[0];
            ListaCarte.RemoveAt(0);
            return c;
        }

        //override del tostring per displayare le carte
        public override string ToString()
        {
            //creo lo stringbuilder
            StringBuilder sb = new StringBuilder();

            //lo popolo di tostring delle carte
            for(int i = 0; i < ListaCarte.Count; i++)
            {
                //faccio l'append di ogni singola carta
                sb.Append(ListaCarte[i] + "\n");
            }

            //ritorno lo stringbuilder
            return sb.ToString();
        }
    }
}
