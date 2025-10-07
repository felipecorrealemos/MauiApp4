using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp4.Components.model
{
    public class Cliente
    {
        public int id { get; set; }
        public string nome { get; set; } = string.Empty;
        public string cpf { get; set; } = string.Empty;
        public string telefone { get; set; } = string.Empty;

        public static Cliente UltimoCliente { get; set; }

    }

}
