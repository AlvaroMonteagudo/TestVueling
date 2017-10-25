using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisGame
{
    /// <summary>
    /// Clase instancia de un jugador que participa en un partido de tenis
    /// </summary>
    class Jugador
    {
        // Atributo tipo string para guardar el nombre dle jugador
        public string nombre { get; set; }

        /// <summary>
        /// // Constructor de un jugador por su nombre
        /// </summary>
        /// <param name="nombre"> string que referencia el nombre del jugador </param>
        public Jugador(string nombre)
        {
            this.nombre = nombre;
        }
    }
}
