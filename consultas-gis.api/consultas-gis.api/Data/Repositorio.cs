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
                          SELECT  DOCUMENTO, p.APELLIDO_NOMBRE, b.CONCEPTO, CLAVE_BIEN, ANO_CUOTA, NRO_CUOTA
,case when pb.TIPO_BIEN='CICI' then
sum(case when TIPO_ITEM='CICITEM2' then isnull(CAPITAL_ITEM,0) end)
-sum(case when TIPO_ITEM='CICIRETE' then isnull(CAPITAL_ITEM,0)*-1 end) 
else sum(CAPITAL_ITEM) end as SALDO
                        ,CASE 
                            WHEN ESTADO_DEUDA = 'PT' THEN 'PAGADO'
                            WHEN ESTADO_DEUDA = 'LI' THEN 'DEUDA' 
                            ELSE ESTADO_DEUDA
                        END AS ESTADO
						,CASE
							WHEN pb.TIPO_BIEN<>'CICI' THEN '' else DBO.CICI_CATEGORIA_VIGENTE(pb.ID_BIEN) end as CATEGORIA
--							WHEN pb.TIPO_BIEN='CICI' THEN DBO.CICI_CATEGORIA_VIGENTE(pb.ID_BIEN) else '' end as CATEGORIA
						,CASE
							WHEN pb.TIPO_BIEN='CICI' and DBO.CICI_CATEGORIA_VIGENTE(pb.ID_BIEN)='A' THEN 5670
							WHEN pb.TIPO_BIEN='CICI' and DBO.CICI_CATEGORIA_VIGENTE(pb.ID_BIEN)='B' THEN 9156
							WHEN pb.TIPO_BIEN='CICI' and DBO.CICI_CATEGORIA_VIGENTE(pb.ID_BIEN)='C' THEN 13398
							WHEN pb.TIPO_BIEN='CICI' and DBO.CICI_CATEGORIA_VIGENTE(pb.ID_BIEN)='Gran Contribuyente' THEN 61446
							WHEN pb.TIPO_BIEN='CICI' and DBO.CICI_CATEGORIA_VIGENTE(pb.ID_BIEN)='D' THEN 17346
							WHEN pb.TIPO_BIEN='CICI' and DBO.CICI_CATEGORIA_VIGENTE(pb.ID_BIEN)='E' THEN 24570
							else '' end as MINIMO
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
                            'OB08CONT','OB03CONT','OB01CONT','CICIRETE'
                        )
                        AND DOCUMENTO IS NOT NULL 
                        --AND DOCUMENTO in ('30623893096','30708757175')
                        --AND DOCUMENTO <> ''
group by DOCUMENTO, p.APELLIDO_NOMBRE, b.CONCEPTO, CLAVE_BIEN, ANO_CUOTA, NRO_CUOTA,pb.TIPO_BIEN,f.ESTADO_DEUDA,pb.ID_BIEN
                    ORDER BY DOCUMENTO, b.CONCEPTO, CLAVE_BIEN, ANO_CUOTA, NRO_CUOTA
                          ";


            using var connection = new SqlConnection(connectionString);
                var consultas = connection.Query<Consulta>(sql,
                commandTimeout: 300,
                buffered: false).ToList();

                return consultas;
            
        }


    }
}
