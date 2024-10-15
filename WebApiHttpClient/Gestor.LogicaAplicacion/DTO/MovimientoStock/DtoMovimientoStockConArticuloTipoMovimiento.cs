﻿using Gestor.LogicaNegocio.Articulos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.DTO.MovimientoStock
{
    public class DtoMovimientoStockConArticuloTipoMovimiento
    {
        public int Id { get; set; }

        public int Cantidad { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime FechaMovimiento { get; set; }

        //Articulo
        public int IdArticulo { get; set; }
        public string NombreArticulo { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioActualArticulo { get; set; }

        //TipoMovimiento

        public int IdTipoMovimiento { get; set; }
        public string NombreTipoMovimiento { get; set; }

        public int CoeficienteTipoMovimiento { get; set; }


    }
}
