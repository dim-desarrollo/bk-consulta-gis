using consultas_gis.api.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace consultas_gis.api.Data
{
    public class Repositorio
    {

        string connectionString = "Server=172.20.254.236;Database=PROGRAM;User Id=fbono;Password=fbono;TrustServerCertificate=True;";


        public List<Consulta> ConsultaPadron()
        {

               var sql = @"
                    SELECT TOP 10 DOCUMENTO, p.APELLIDO_NOMBRE, b.CONCEPTO, CLAVE_BIEN, ANO_CUOTA, NRO_CUOTA, CAPITAL_ITEM,
                        CASE 
                            WHEN ESTADO_DEUDA = 'PT' THEN 'PAGADO'
                            WHEN ESTADO_DEUDA = 'LI' THEN 'DEUDA' 
                            ELSE ESTADO_DEUDA
                        END AS ESTADO
                    FROM RT_FACTURAS f
                    JOIN RT_PADRON_BASE pb ON pb.ID_BIEN = f.ID_BIEN
                    JOIN RT_FACTURAS_DETALLE fd ON fd.NRO_INTERNO = f.NRO_INTERNO
                    JOIN PERSONAS p ON p.IDENTIFICADOR = pb.IDENTIFICADOR
                    JOIN RT_BIENES b ON b.TIPO_BIEN = pb.TIPO_BIEN
                    WHERE ACTIVO = 1 
                        AND ANO_CUOTA = 2025 
                        AND ESTADO_DEUDA <> 'CA'
                        AND TIPO_CUOTA IN ('BA','40','41','42','43','44','00','01','02','04','06','07','50','AT') 
                        AND fd.TIPO_ITEM IN (
                            'CICITEM2','ININBASI','OBPPBASI','OB16CONT','OB13CONT',
                            'OB09CONS','OB05MANT','OB05OBRA','OB06CONT','OB07CONT',
                            'OB08CONT','OB03CONT','OB01CONT'
                        )
                        AND DOCUMENTO IS NOT NULL 
                        AND DOCUMENTO <> ''
                    ORDER BY DOCUMENTO, b.CONCEPTO, CLAVE_BIEN, ANO_CUOTA, NRO_CUOTA";


            using var connection = new SqlConnection(connectionString);
            var consultas = connection.Query<Consulta>(sql).ToList();
            return consultas;
        }


    }
}
