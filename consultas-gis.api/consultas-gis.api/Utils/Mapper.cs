using consultas_gis.api.Models;
using consultas_gis.DTOs;

namespace consultas_gis.Utils
{
    public static class Mapper
    {

        public static List<ConsultaDTO> ConsultaModelToConsultaDTO(List<Consulta> consultas)
        {

            var newList = consultas.Select(x => new ConsultaDTO
            {

                Documento = x.DOCUMENTO.Trim(),
                ApellidoyNombre = x.APELLIDO_NOMBRE.Trim(),
                Concepto = x.CONCEPTO.Trim(),
                ClaveBien = x.CLAVE_BIEN.Trim(),
                AnoCuota = x.ANO_CUOTA,
                NumeroCuota = x.NRO_CUOTA,
                CapitalItem = x.SALDO,
                Estado = x.ESTADO.Trim(),
                Categoria = x.CATEGORIA.Trim(),
                Minimo = x.MINIMO
                

            }).ToList();

            return newList;  

        }

    }
}
