using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisGame
{
    /// <summary>
    /// Clase principal de la simulación de un partido de tenis
    /// </summary>
    class Program
    {
        static void Main()
        {
            // Pedimos el nombre de los jugadores al usuario
            Console.Write("Nombre jugador 1: ");
            string nombre1 = Console.ReadLine();
            Console.Write("Nombre jugador 2: ");
            string nombre2 = Console.ReadLine();

            // Control para no tener nombre duplicado, clarifica la diferenciación de los jugadores
            while (nombre2.Equals(nombre1))
            {
                Console.Write("Los jugadores no pueden tener el mismo nombre. Introduzca de nuevo el nombre del segundo jugador: ");
                nombre2 = Console.ReadLine();
            }

            // Creación de dos objetos tipo Jugador, identificados por un string
            Jugador jugador1 = new Jugador(nombre1);
            Jugador jugador2 = new Jugador(nombre2);

            // Pedimos el número de sets que se van a jugar al usuario, controlando que sea 3 ó 5 exclusivamente
            Console.Write("Partido a 3 ó a 5 sets?: ");
            int nsets = Int32.Parse(Console.ReadLine());
            while (nsets != 3 && nsets != 5)
            {
                Console.Write("Lo siento, el partido sólo puede ser a 3 ó 5 sets. ¿Cuantos sets?: ");
                nsets = Int32.Parse(Console.ReadLine());
            }

            Console.WriteLine();

            // Creación del objeto Partido con dos jugadores y el número de sets
            Partido p = new Partido(jugador1, jugador2, nsets);
            // Llamada al método de simulación del partido
            p.Jugar();
            
            // La terminal se cierra cuando al finalizar la simulación, se pulsa cualquier tecla
            Console.ReadKey();
        }
    } /* class */
} /* namespace */
