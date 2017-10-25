using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisGame
{
    class Partido
    {
        // Array con las puntuaciones que se pueden tener durante un juego
        // La puntuación "Juego" indice la finalización de juego, añadida para evitar ecepciones fuera de límite (out of bounds)
        public static readonly string[] puntuaciones = { "0", "15", "30", "40", "Ad", "Juego" };

        // Array con dos índices (uno por jugador) que indica que puntuación actual tiene cada jugador
        // Ejemplo: un array { 0, 1 } indice que el jugador 1 tiene "0" y el jugador 2 "15", indice sobre [puntuaciones]
        public int[] puntosJugadorJuego;

        // Juegos ganados por cada jugador en cada uno de los sets jugados en este partido
        public int[,] juegosJugadorSet;
        
        // Entidades jugador
        public Jugador jugador1;
        public Jugador jugador2;

        // Array de jugadores 
        public Jugador[] jugadores;
                
        // Número de sets a jugar en este partido
        public int nsets;
              
        // Número de sets ganados por cada uno de los jugadores>
        public int[] setsGanados; 

        // Variables enteras para guardar set actual y juego actual dentro del set
        public int setActual;
        public int juegoSet;
        
        /// <summary>
        /// Constructor un partido de tenis en el que participa dos jugadores para jugar un número de sets
        /// </summary>
        /// <param name="j1"> Jugador número 1 que participa en el partido </param>
        /// <param name="j2"> Jugador número 2 que participa en el partido </param>
        /// <param name="nsets"> Número de sets que se van a jugar en el partido</param>
        public Partido(Jugador j1, Jugador j2, int nsets)
        {
            // Inicialización de los jugadores
            jugador1 = j1;
            jugador2 = j2;
            jugadores = new Jugador[] { jugador1, jugador2 };

            // Número de sets
            this.nsets = nsets;

            // Inicialización juegos/set de cada jugador
            juegosJugadorSet = new int[2, nsets];

            // Inicialización puntos jugador dentro de cada juego
            puntosJugadorJuego = new int[] { 0, 0 };

            // Inicialización sets que ha ganado cada jugador
            setsGanados = new int[] { 0, 0 };

            // Inicialización del set y juego
            setActual = 0;
            juegoSet = 1;
        }

        /// <summary>
        /// Método de simulación de un partido de tenis
        /// </summary>
        public void Jugar()
        {
            // Inicialización generador aleatorio
            Random rnd = new Random();

            // Variables para actualizar cada uno de los arrays en función del jugador que puntue 
            // (0 para el primero, 1 para el segundo)
            int jugadorPuntua = -1;
            int jugadorNoPuntua = -1;

            // Randomización del saque
            int saque = rnd.Next(2);

            if (saque == 0)
            {
                Console.WriteLine(jugador1.nombre + " iniciará el partido sacando");
            }
            else
            {
                Console.WriteLine(jugador2.nombre + " iniciará el partido sacando");
            }

            // JUGAR PARTIDO
            // Mientras el set actual sea menor que el número de sets que se deben jugar, se sigue jugando
            // Es menor porque para la indexación requerimos el número 0, intérvalo [0, nsets)
            while (setActual < nsets)
            {
                // JUGAR SET
                // Mientras ninguno de los jugadores llegue a 6, saque ventaja de dos o hayan jugado el tie break,
                // el partido continua
                while ((juegosJugadorSet[0, setActual] < 6 && juegosJugadorSet[1, setActual] < 6)
                    || (juegosJugadorSet[0, setActual] == 6) && (juegosJugadorSet[1, setActual] == 6)
                    || (
                            Math.Abs(juegosJugadorSet[0, setActual] - juegosJugadorSet[1, setActual]) < 2 
                            && (juegosJugadorSet[0, setActual] != 7 && juegosJugadorSet[1, setActual] != 7)
                       )
                    )
                {
                    // Comprobamos si hay que jugar tie break (cambio de sistema de puntuación)
                    if (juegosJugadorSet[0, setActual] == 6 && juegosJugadorSet[1, setActual] == 6){
                        Console.WriteLine();
                        Console.WriteLine("** TIE BREAK - Set " + (setActual + 1));

                        int[] puntosTB = new int[] { 0, 0 }; 

                        // Mientras un jugador no llegue a 7 con ventaja de 2, se sigue jugando
                        while ((puntosTB[0] < 7 && puntosTB[1] < 7) || Math.Abs(puntosTB[0] - puntosTB[1]) < 2)
                        {
                            jugadorPuntua = rnd.Next(2);            // Indice del jugador que puntua

                            puntosTB[jugadorPuntua]++;

                            Console.WriteLine("Punto de " + jugadores[jugadorPuntua].nombre
                            + " (" + puntosTB[0] + "-" + puntosTB[1] + ")\t\t("
                            + escribirPuntuacionSets(juegosJugadorSet) + ")");
                        }

                        Console.Write(jugadores[jugadorPuntua].nombre + " gana el juego\t\t(");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("** Juego " + juegoSet + " - Set " + (setActual + 1));
                        // JUGAR JUEGO
                        // Mientras ninguno de los jugadores haya conseguido la puntuación "Juego" se sigue jugando
                        while (!puntuaciones[puntosJugadorJuego[0]].Equals("Juego")
                            && !puntuaciones[puntosJugadorJuego[1]].Equals("Juego"))
                        {
                            jugadorPuntua = rnd.Next(2);            // Indice del jugador que puntua
                            jugadorNoPuntua = 1 - jugadorPuntua;    // Indice del jugador que no puntua

                            // Incrementamos el índice de la puntuación del jugador que puntua
                            puntosJugadorJuego[jugadorPuntua]++;

                            // Si el jugador esta apuntando a Ad, o ha hecho juego, o tiene ventaja    
                            if (puntuaciones[puntosJugadorJuego[jugadorPuntua]].Equals("Ad"))
                            {
                                // Si el jugador que no ha pntuado tenia menos de 40, se acaba el juego
                                if (!puntuaciones[puntosJugadorJuego[jugadorNoPuntua]].Equals("Ad") ||
                                    !puntuaciones[puntosJugadorJuego[jugadorNoPuntua]].Equals("40"))
                                {
                                    puntosJugadorJuego[jugadorPuntua]++; // Gana el juego el que puntua
                                }
                                else
                                {
                                    // Si el otro jugador estaba en Advantage, ahora están iguales
                                    if (puntuaciones[puntosJugadorJuego[jugadorNoPuntua]].Equals("Ad"))
                                    {
                                        // Disminuimos ambos índices ya que el que ha puntuado está apuntando a ventaja
                                        // pero tienen que estar iguales a 40
                                        puntosJugadorJuego[jugadorNoPuntua]--;
                                        puntosJugadorJuego[jugadorPuntua]--;
                                    }
                                }
                            }

                            // Si el juego ha concluido, se muestra por pantalla
                            if (puntuaciones[puntosJugadorJuego[jugadorPuntua]].Equals("Juego"))
                            {
                                Console.Write(jugadores[jugadorPuntua].nombre + " gana el juego\t\t(");
                                break;
                            }

                            // Mostramos la puntuación actual del juego y la de todos los sets del partido
                            Console.WriteLine("Punto de " + jugadores[jugadorPuntua].nombre
                                + " (" + puntuaciones[puntosJugadorJuego[0]] + "-" + puntuaciones[puntosJugadorJuego[1]] + ")\t\t("
                                + escribirPuntuacionSets(juegosJugadorSet) + ")");

                        } /* while juego */
                    } /* if tie break */

                    // Pasamos al siguiente juego
                    juegoSet++;

                    // Se actualizan los juegos del jugador que ha ganado en este set
                    juegosJugadorSet[jugadorPuntua, setActual]++;

                    // Reinicio de los índices de las puntuaciones
                    puntosJugadorJuego = new int[] { 0, 0 }; // Puntuaciones comienzan en el primer indice de la tabla

                    Console.WriteLine(escribirPuntuacionSets(juegosJugadorSet) + ")");
                } /* while set */

                // Incrementamos el set actual
                setActual++;

                // Reiniciamos el set
                juegoSet = 1;

                // Se incrementa el número de sets del jugador ganador
                setsGanados[jugadorPuntua]++;

                Console.WriteLine(jugadores[jugadorPuntua].nombre + " gana el set");
            } /* while set actual menor que nsets */

            // Mostramos por pantalla el ganador del partido
            if (setsGanados[0] == nsets)
            {
                Console.WriteLine();
                Console.WriteLine("!!" + jugador1.nombre.ToUpper() + " HA GANADO EL PARTIDO¡¡");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("!!" + jugador2.nombre.ToUpper() + " HA GANADO EL PARTIDO¡¡");
            }

        }

        /// <summary>
        /// Método que escribe las puntuaciones de los sets del partido por pantalla
        /// </summary>
        /// <param name="sets"> Array bidmensional con la puntuación de cada jugador en cada uno de los sets </param>
        /// <returns></returns>
        public string escribirPuntuacionSets(int [,] sets)
        {
            string res = "";
            for (int i = 0; i < sets.GetLength(1); i++)
            {
                for (int j = 0; j < sets.GetLength(0); j++)
                {
                    res = res + sets[j,i] + "-";
                }
                res = res.Substring(0, res.Length - 1); // Quitamos guión extra
                res = res + ", ";
            }
            return res.Substring(0, res.Length - 2); // Quitamos coma y espacio extra
        }
    } /* class */
} /* namespace */
