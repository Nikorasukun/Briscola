using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briscola.Models
{
    internal class Giocatore
    {
        //variabili ez
        public int Numero { get; set; }     //esso può essere il giocatore 1 o il giocatore 2
        public string Nome { get; set; }    //il nome del giocatore
        public int Punti { get; set; }      //i punti del giocatore, conoscendo anche solo questi possiamo caprie chi ha vinto

        public List<Carta> Mano { get; set; }   //la mano del giocatore
        public bool Presa { get; set; }

        //costruttore
        public Giocatore(int numero, string nome)
        {
            this.Numero = numero;
            this.Nome = nome;
            this.Mano = new List<Carta>();
            this.Presa = false;
        }

        //riempie la mano
        public void RiempiMano(Mazzo m)
        {
            //variabile momentanea che mi segnala la dimensione della mano all'inizio
            int nAtt = Mano.Count;

            //ciclo per 3 volte meno il numero di carte nella mano del giocatore per riempirgliela di carte col topdeck del mazzo         
            for (int i = 0; i < 3 - nAtt; i++)
            {
                Mano.Add(m.TopDeck());
            }
        }

        //giocata randomica
        public Carta RandomPlay()
        {
            //variabili
            Random rnd = new Random();
            int n = n = rnd.Next(3);
            while(n > Mano.Count-1) { n = rnd.Next(3); }
            Carta c = Mano[n];
            
            

            //rimuovo la carta
            Mano.RemoveAt(n);

            //la ritorno
            return c;
        }

        //giocata umana
        public Carta ThoughtPlay(int index)
        {
            //variabili
            Carta c = Mano[index];

            //rimuovo la carta
            Mano.RemoveAt(index);

            //la ritorno
            return c;
        }
    }
}
