﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.PedidosE;

namespace TiendaUNAC.Domain.DTOs.PedidosDTOs
{
    public class PedidosRegistrosDTOs
    {
        public int IdPedido_Registro { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int IdInventario { get; set; }
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
        public string Talla { get; set; }
        public decimal Valor { get; set; }

        public static PedidosRegistrosDTOs CrearDTOs(PedidosRegistrosE pedidosRegistrosE)
        {
            return new PedidosRegistrosDTOs
            {
                IdPedido_Registro = pedidosRegistrosE.IdPedido_Registro,
                IdPedido = pedidosRegistrosE.IdPedido,
                IdProducto = pedidosRegistrosE.IdProducto,
                IdInventario = pedidosRegistrosE.IdInventario,
                Cantidad = pedidosRegistrosE.Cantidad,
                Nombre = pedidosRegistrosE.Nombre,
                Color = pedidosRegistrosE.Color,
                Talla = pedidosRegistrosE.Talla,
                Valor = pedidosRegistrosE.Valor

            };
        }

        public static PedidosRegistrosE CrearE(PedidosRegistrosDTOs pedidosRegistrosDTOs)
        {
            return new PedidosRegistrosE
            {
                IdPedido_Registro = pedidosRegistrosDTOs.IdPedido_Registro,
                IdPedido = pedidosRegistrosDTOs.IdPedido,
                IdProducto = pedidosRegistrosDTOs.IdProducto,
                IdInventario = pedidosRegistrosDTOs.IdInventario,
                Cantidad = pedidosRegistrosDTOs.Cantidad,
                Nombre = pedidosRegistrosDTOs.Nombre,
                Color = pedidosRegistrosDTOs.Color,
                Talla = pedidosRegistrosDTOs.Talla,
                Valor = pedidosRegistrosDTOs.Valor

            };
        }
    }
   
}