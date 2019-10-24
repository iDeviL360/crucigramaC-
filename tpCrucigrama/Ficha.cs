using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpCrucigrama
{
    static class Ficha
    {
        public static List<string> letras = new List<string>(){ "A", "A", "A", "A", "A", "A", "A", "A", "A", "A", "A", "A", "B", "B", "C", "C", "C", "C", "CH", "D", "D", "D", "D", "D", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "F", "G", "G", "H", "H", "I", "I", "I", "I", "I", "I", "J", "L", "L", "L", "L", "LL", "M", "M", "N", "N", "N", "N", "N", "Ñ", "O", "O", "O", "O", "O", "O", "O", "O", "O", "P", "P", "Q", "R", "R", "R", "R", "R", "RR", "S", "S", "S", "S", "S", "S", "T", "T", "T", "T", "U", "U", "U", "U", "U", "V", "X", "Y", "Z" };
        private static string[] fichas = new string[7];
        static Random random = new Random();


        public static string[] generarFichas()
        {
            for(int i = 0; i < 7; i++)
            {
                int aleatorio = random.Next(0, letras.Count);
                fichas[i] = letras[aleatorio];
                letras.RemoveAt(aleatorio);
            }
            return fichas;
        }

        public static string generarFichaRandom()
        {
            int aleatorio = random.Next(0, letras.Count);
            string letraRandom = letras[aleatorio];
            letras.RemoveAt(aleatorio);
            return letraRandom;
        }

        /* public static void limpiarFichas()
        {
            fichas.Clear();
        } */

    }
}
