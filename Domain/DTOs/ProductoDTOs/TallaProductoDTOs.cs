﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.ProductoE;

namespace TiendaUNAC.Domain.DTOs.ProductoDTOs
{
    public class TallaProductoDTOs
    {
        public int IdTallaProducto { get; set; }
        public string Talla { get; set; }
        public int PorcentajeValor { get; set; }

        public static TallaProductoDTOs CrearDTOs(TallaProductoE tallaProductoE)
        {
            return new TallaProductoDTOs
            {
                IdTallaProducto = tallaProductoE.IdTallaProducto,
                Talla = tallaProductoE.Talla,
                PorcentajeValor = tallaProductoE.PorcentajeValor
            };
        }

        public static TallaProductoE CrearE(TallaProductoDTOs tallaProductoDTOs)
        {
            return new TallaProductoE
            {
                IdTallaProducto = tallaProductoDTOs.IdTallaProducto,
                Talla = tallaProductoDTOs.Talla,
                PorcentajeValor = tallaProductoDTOs.PorcentajeValor
            };
        }
    }
}