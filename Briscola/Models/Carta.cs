using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briscola.Models
{
    internal class Carta
    {
        //variabili
        public int Numero { get; set; }
        public string? Seme { get; set; } //1 = Bastoni, 2 = Coppe, 3 = Denare, 4 = Spade
        public string Percorso { get; set; }
        public int Punteggio { get; set; }

        //costruttore
        public Carta(int n, int s)
        {
            //assegnazione variabili semplici
            this.Numero = n;

            //decisione del seme
            switch (s)
            {
                case 1:
                    this.Seme = "Bastoni"; break;

                case 2:
                    this.Seme = "Coppe"; break;

                case 3:
                    this.Seme = "Denare"; break;

                case 4:
                    this.Seme = "Spade"; break;
            }

            //assegnazione punteggio
            switch (n)
            {
                case 1:
                    this.Punteggio = 11; break;
                case 3:
                    this.Punteggio = 10; break;
                case 10:
                    this.Punteggio = 4; break;
                case 9:
                    this.Punteggio = 3; break;
                case 8:
                    this.Punteggio = 2; break;
                default:
                    this.Punteggio = 0; break;
            }

            //percorso
            this.Percorso = $@"carte\_{Seme.ToLower().Substring(0, 1)}.{Numero}.png";
        }

        //override del ToString()
        public override string ToString()
        {
            //creo lo stringBuilder
            StringBuilder sb = new StringBuilder();

            //aggiunge il numero alla carta
            switch (Numero)
            {
                case 1:
                    sb.Append("Asso");
                    break;

                case 8:
                    sb.Append("Fante");
                    break;

                case 9:
                    sb.Append("Cavallo");
                    break;

                case 10:
                    sb.Append("Re");
                    break;

                default:
                    sb.Append(Numero.ToString());
                    break;
            }

            //aggiunge il seme
            sb.Append($" di {Seme}");

            //fa il ritorno
            return sb.ToString();
        }

        //overload del maggiore e minore
        public static bool operator >(Carta c1, Carta c2)
        {
            if (c1.Seme != c2.Seme) return true;
            if (c1.Punteggio > c2.Punteggio) return true;

            return false;
        }

        public static bool operator <(Carta c1, Carta c2)
        {
            if (c1.Seme != c2.Seme) return false;
            if (c1.Punteggio > c2.Punteggio) return false;

            return true;
        }

        //ritorna 1 se vince this, 2 se vince l'altra
        //Briscola vs Briscola, Briscola vs Normale, Normale vs SemeSbagliato, Normale vs Normale
        public int Confronto(Carta c, string SemeBriscola)
        {
            //Briscola vs Briscola
            if (this.Seme == SemeBriscola && c.Seme == SemeBriscola && this.Punteggio > c.Punteggio) return 1;
            if (this.Seme == SemeBriscola && c.Seme == SemeBriscola && this.Punteggio < c.Punteggio) return 2;

            //Briscola vs Normale
            if (this.Seme == SemeBriscola && c.Seme != SemeBriscola) return 1;
            if (this.Seme != SemeBriscola && c.Seme == SemeBriscola) return 2;

            //Normale vs SemeSbagliato && Normale vs Normale
            if (this > c) return 1;
            if (this < c) return 2;

            return 1;
        }

    }
}
