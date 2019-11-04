using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpCrucigrama
{
    class Tablero
    {
        private string[,] tablero;

        private int[] posicionPuntajeDoble = { 0, 2, 3, 6, 7, 8, 11, 12, 14 };
        private int[] posicionPuntajeTriple = { 1, 5, 9, 13 };
        private int[] posicionPalabraDoble = { 1, 2, 3, 4, 10, 11, 12, 13 };
        private int[] posicionPalabraTriple = { 0, 7, 14 };


        List<string> palabras = new List<string>() { "LANA", "RATON", "ZANAHORIA", "TELEVISOR", "COCHE", "GATO", "PERRO", "ESPEJO", "BICICLETA", "REVISTA", "TELEFONO", "LLAVES" };


        public Tablero() { }

        public Tablero(int filas, int columnas)
        {
            this.tablero = new string[filas, columnas];
            this.LimpiarTabla();
        }


        public void dibujarTabla()
        {
            Console.Clear();
            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                Console.WriteLine("------------------------------------------------------------");
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    if( tablero[i, j] != "" )
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("| {0} |", string.Join("", tablero[i, j]));
                        Console.ResetColor();
                    } else
                    {
                        Console.Write("|  |");
                    }
                }
                Console.Write("\n");
                Console.WriteLine("------------------------------------------------------------");
            }
        }

        public void LimpiarTabla()
        {
            for(int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(0); j++)
                {
                    tablero[i, j] = "";
                }
            }
        }

        public void insertarFicha(int fila, int col, string ficha)
        {
            if(this.tablero.GetLength(0) >= fila && this.tablero.GetLength(1) >= col)
            {
                if(this.tablero[fila, col] == "")
                {
                    this.tablero[fila, col] = ficha;
                } else
                {
                    Console.WriteLine("Movimiento no valido!");
                }
            } else
            {
                Console.WriteLine("Posicion no existe, favor verifique");
            }
        }

        public int verificarTablero()
        {
            //Con esto se verifica si el tablero ya esta lleno o no
            int valor = 0;
            for(int i = 0; i < this.tablero.GetLength(0); i++)
            {
                for(int j = 0; j < this.tablero.GetLength(1); j++)
                {
                    if (tablero[i, j] == "")
                    {
                        valor += 1;
                    }
                }
            }
            return valor;
        }

        public bool validarMovimiento(int filaDest, int colDest)
        {
            //Verificamos primeramente que sea un movimiento valido y dentro de los limites del tablero
            if(tablero.GetLength(0) >= filaDest && tablero.GetLength(1) >= colDest)
            {
            //Verificamos que la posicion no este ocupada
                if(tablero[filaDest, colDest] != "")
                {
                    Console.WriteLine("Casilla ya ocupada");
                    return false;
                } else
                {
                    return true;
                }
            } else
            {
                Console.WriteLine("Movimiento no valido");
                return false;
            }
        }

        public void calcularPuntuacion(ref Jugador jugador, string ficha, int filaDest, int colDest)
        {
            int puntuacion = this.calcularPuntoFicha(ficha);

            //Dependiendo de la ficha que eligio el usuario se asigna una puntuacion a esa letra

            calcularLetraBonus(ref puntuacion, filaDest, colDest, posicionPuntajeDoble, 2);
            calcularLetraBonus(ref puntuacion, filaDest, colDest, posicionPuntajeTriple, 3);

            string palabraEncontrada = this.buscarPalabras(ref jugador, filaDest, colDest);

            if (palabraEncontrada != "")
            {
                char[] letras = palabraEncontrada.ToCharArray();
                int puntajePalabra = 0;

                foreach(var letra in letras)
                {
                    puntajePalabra += this.calcularPuntoFicha(letra.ToString());
                }


                calcularPalabraBonus(ref puntuacion, puntajePalabra, filaDest, colDest, posicionPalabraDoble, 2);
                calcularPalabraBonus(ref puntuacion, puntajePalabra, filaDest, colDest, posicionPalabraTriple, 3);
            }


            jugador.Puntaje += puntuacion;
        }


        private string buscarPalabras(ref Jugador jugador, int filaDest, int colDest)
        {

            /*En este metodo recibimos la fila y columna que el jugador ingreso su ficha ficha para verificar si ya armo alguna palabra en esa fila o en esa columna*/

            string palabraEncontrada = "";
            string textoFila = "";
            string textoCol = "";


            //Primero recorremos las columnas de arriba a abajo en busca de alguna palabra
            for (int i = 0; i < this.tablero.GetLength(0); i++)
            {
                if(tablero[i, colDest] == "")
                {
                    textoCol += "-";
                } else
                {
                    textoCol += tablero[i, colDest];
                }
            }

            //Segundo recorremos la fila de izquierda a derecha para buscar alguna palabra
            for (int i = 0; i < this.tablero.GetLength(1); i++)
            {
                if(tablero[filaDest, i] == "")
                {
                    textoFila += "-";
                } else
                {
                    textoFila += tablero[filaDest, i];
                }
            }

            //Aca se compara si el string que se obtuvo del recorrido coincide con alguna palabra en nuestro array de palabras, si coincide se borra de la lista, sino no se hace nada
            for(int i = 0; i < this.palabras.Count; i++)
            {
                if(textoCol.Contains(palabras[i])) {
                    jugador.PalabrasEncontradas += 1;
                    palabraEncontrada = palabras[i];
                    palabras.RemoveAt(i);
                } else if(textoFila.Contains(palabras[i]))
                {
                    jugador.PalabrasEncontradas += 1;
                    palabraEncontrada = palabras[i];
                    palabras.RemoveAt(i);
                }
            }
            return palabraEncontrada;
        }

        private void calcularLetraBonus(ref int puntuacion, int filaDest, int colDest, int[] posicionBonus, int puntajeBonus)
        {
            if(posicionBonus.Contains(filaDest) && posicionBonus.Contains(colDest))
            {
                puntuacion *= puntajeBonus;
            }
        }


        private void calcularPalabraBonus(ref int puntuacion, int puntajePalabra, int filaDest, int colDest, int[] posicionesBonus, int puntajeBonus)
        {
            if(posicionesBonus.Contains(filaDest) && posicionesBonus.Contains(colDest))
            {
                puntuacion += puntajePalabra * puntajeBonus;
            }
        }


        private int calcularPuntoFicha(string ficha)
        {
            int punto = 0;

            if (ficha == "A" || ficha == "E" || ficha == "I" || ficha == "L" || ficha == "N" || ficha == "O" || ficha == "R" || ficha == "S" || ficha == "T" || ficha == "U")
            {
                punto = 1;
            }
            else if (ficha == "D" || ficha == "G")
            {
                punto = 2;
            }
            else if (ficha == "B" || ficha == "C" || ficha == "M" || ficha == "P")
            {
                punto = 3;
            }
            else if (ficha == "F" || ficha == "H" || ficha == "V" || ficha == "Y")
            {
                punto = 4;
            }
            else if (ficha == "CH" || ficha == "Q")
            {
                punto = 5;
            }
            else if (ficha == "J" || ficha == "LL" || ficha == "Ñ" || ficha == "RR" || ficha == "X")
            {
                punto = 8;
            }
            else if (ficha == "Z")
            {
                punto = 10;
            }

            return punto;
        }

    }
}
