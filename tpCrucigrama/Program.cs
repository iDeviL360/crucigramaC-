using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpCrucigrama
{
    class Program
    {
        static void Main(string[] args)
        {
            //var
            Tablero tabla = new Tablero(15, 15);
            Jugador j1 = new Jugador("Jugador numero 1");
            Jugador j2 = new Jugador("Jugador numero 2");
            Jugador jugadorActual;
            
            int posicionArr, filaDest, colDest;
            bool turno = true;

            while(tabla.verificarTablero() > 0 && Ficha.letras.Count > 0)
            {

                tabla.dibujarTabla();

                //Vemos quien es el jugador actual
                jugadorActual = turno == true ? j1 : j2;
                Console.Write("Turno " + jugadorActual.NumeroJugador + ": ");
                jugadorActual.imprimirFichas();
                Console.Write("\nPuntaje: {0}", jugadorActual.Puntaje);


                //Falta mostrar las opciones que va a tener un jugador, por ejemplo si quiere cambiar de fichas, o si quiere pasar de turno, etc.

                Console.WriteLine("\nSi desar pasar de turno presione Space o para continuar presione enter");
                if (Console.ReadKey().Key != ConsoleKey.Spacebar) 
                {



                   /*
                     
                    
                    Una vez que entremos aca tenemos que ver que el jugador coloque las fichas hasta que el decida que se termine su turno se pueden ingresar varias fichas en un turno y no solo una vez, CAMBIAR ESE COMPORTAMIENTO!!!!!!!!! 
                     
                     
                   */



                    //Validamos que la ficha se elija en la posicion correcta
                    do
                    {
                        Console.Write("\nEscoja la ficha que desea agregar en el tablero: ");

                        //_Validamos que solo se ingresen numeros con tryParse
                        while(!int.TryParse(Console.ReadLine(), out posicionArr))
                        {
                            Console.WriteLine("Debe ser un numero, intente de nuevo!");
                            Console.Write("Escoja la ficha que desea agregar en el tablero: ");
                        }
                    } while (posicionArr > jugadorActual.Fichas.Length - 1);

                    Console.Write("Se escogio la ficha: [{0}]", jugadorActual.Fichas[posicionArr]);


                    //Se valida que el movimiento sea correcto
                    do
                    {
                        Console.Write("\nIngrese la fila en donde desea agregar la ficha: ");
                        filaDest = int.Parse(Console.ReadLine());

                        Console.Write("\nIngrese la columna en donde desea agregar la ficha: ");
                        colDest = int.Parse(Console.ReadLine());
                    } while (!tabla.validarMovimiento(filaDest, colDest));


                    //Sì pasa todas las validaciones se inserta la ficha
                    tabla.insertarFicha(filaDest, colDest, jugadorActual.Fichas[posicionArr]);

                    //Calculamos el puntaje del jugador actual
                    tabla.calcularPuntuacion(ref jugadorActual, jugadorActual.Fichas[posicionArr]);

                    //Se reemplaza la ficha que se elijio
                    jugadorActual.Fichas[posicionArr] = Ficha.generarFichaRandom();

                }
                else
                {
                    //Si se llega a este punto significa que el jugador paso de turno y se le debe entregar nuevas fichas y restarle los puntos
                    int i = 0;
                    foreach (var ficha in jugadorActual.Fichas)
                    {
                        Ficha.letras.Add(ficha);
                        jugadorActual.Fichas[i] = Ficha.generarFichaRandom();
                        i++;
                    }
                    jugadorActual.Puntaje -= 5;
                }

                //Cambiamos el turno
                turno = !turno;
                tabla.dibujarTabla();
            }

            Console.WriteLine("La tabla esta llena o ya no quedan fichas, fin del juego!!!!!");
            Console.ReadKey();
        }
    }
}
