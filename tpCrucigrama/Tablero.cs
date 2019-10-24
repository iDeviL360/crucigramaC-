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
            //Verificamos primeramente que sea un movimiento valido
            if(tablero.GetLength(0) >= filaDest && tablero.GetLength(1) >= colDest)
            {
            //Verificamos que la posicion este vacia
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

        public void calcularPuntuacion(ref Jugador jugador, string ficha)
        {
            int puntuacion = 0;

            if(ficha == "A" || ficha == "E" || ficha == "I" || ficha == "L" || ficha == "N" || ficha == "O" || ficha == "R" || ficha == "S" || ficha == "T" || ficha == "U")
            {
                puntuacion = 1;

            } else if (ficha == "D" || ficha == "G")
            {
                puntuacion = 2;
            } else if(ficha == "B" || ficha == "C" || ficha == "M" || ficha == "P")
            {
                puntuacion = 3;
            } else if(ficha == "F" || ficha == "H" || ficha == "V" || ficha == "Y")
            {
                puntuacion = 4;
            } else if(ficha == "CH" || ficha == "Q")
            {
                puntuacion = 5;
            } else if(ficha == "J" || ficha == "LL" || ficha == "Ñ" || ficha == "RR" || ficha == "X")
            {
                puntuacion = 8;
            } else if(ficha == "Z")
            {
                puntuacion = 10;
            }
            jugador.Puntaje += puntuacion;
        }

    }
}
