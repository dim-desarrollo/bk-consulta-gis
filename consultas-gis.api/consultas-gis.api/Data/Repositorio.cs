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
                          SELECT top 50 f.NRO_INTERNO,CLAVE_BIEN as CUIT,p.APELLIDO_NOMBRE
                --,DBO.CICI_CATEGORIA_VIGENTE(pb.ID_BIEN) as CATEGORIA
                ,case when DBO.CICI_CATEGORIA_VIGENTE(pb.ID_BIEN)='Gran Contribuyente' then 'GC' else DBO.CICI_CATEGORIA_VIGENTE(pb.ID_BIEN) 
                end as categoria
                ,f.ANO_CUOTA, f.NRO_CUOTA
                ,sum(case when TIPO_ITEM='CICITEM2' then CAPITAL_ITEM end) as tributo
                ,isnull(sum(case when TIPO_ITEM='CICIRETE' then CAPITAL_ITEM*-1 end),0) as Retenciones
                --else sum(CAPITAL_ITEM) end as SALDO
                ,CASE 
                WHEN ESTADO_DEUDA IN ('PT','FP') THEN 'PAGADO'
                WHEN sum(case when TIPO_ITEM='CICITEM2' then CAPITAL_ITEM end)-isnull(sum(case when TIPO_ITEM='CICIRETE' then CAPITAL_ITEM*-1 end),0)=0 THEN 'SALDADO'
                WHEN ESTADO_DEUDA = 'LI' THEN 'DEUDA' 
                WHEN ESTADO_DEUDA = 'FI' THEN 'EN PLAN DE PAGO' 
                    ELSE ESTADO_DEUDA
                END AS ESTADO
                --,cast(FECHA_VENCIMIENTO1 as date) as FEC_VTO
                FROM RT_FACTURAS f
                JOIN RT_PADRON_BASE pb ON pb.ID_BIEN = f.ID_BIEN and pb.TIPO_BIEN='CICI'
                JOIN RT_FACTURAS_DETALLE fd ON fd.NRO_INTERNO = f.NRO_INTERNO
                JOIN PERSONAS p ON p.IDENTIFICADOR = pb.IDENTIFICADOR
                JOIN RT_BIENES b ON b.TIPO_BIEN = pb.TIPO_BIEN
                join RT_CONDICIONES_LIQUIDACION cl on cl.TIPO_BIEN=f.TIPO_BIEN and cl.TIPO_PLAN=f.TIPO_PLAN  and cl.TIPO_CUOTA=f.TIPO_CUOTA and cl.ANO_CUOTA=f.ANO_CUOTA and cl.NRO_CUOTA=f.NRO_CUOTA 
                WHERE ACTIVO = 1 
                AND f.ANO_CUOTA = 2025 and f.NRO_CUOTA<=9
                AND ESTADO_DEUDA <> 'CA'
                --AND ESTADO_DEUDA = 'LI'
                AND f.TIPO_CUOTA IN ('40','41','42','43','44') 
                --AND fd.TIPO_ITEM IN ('CICITEM2','CICIRETE')
                AND DOCUMENTO IS NOT NULL 
                --AND CLAVE_BIEN in ('30717316408') --,''
                --AND DOCUMENTO <> ''
                --Exenciones--and pb.ID_BIEN not in (select ID_BIEN from RT_EXENCIONES where TIPO_EXENCION in ('CICI','CICI0007','CICI0008','CICI0012'))
                group by f.NRO_INTERNO,CLAVE_BIEN,pb.ID_BIEN,APELLIDO_NOMBRE,DBO.CICI_CATEGORIA_VIGENTE(pb.ID_BIEN),f.ANO_CUOTA,f.NRO_CUOTA,ESTADO_DEUDA,FECHA_VENCIMIENTO1
                --having sum(case when TIPO_ITEM='CICITEM2' then CAPITAL_ITEM end)-isnull(sum(case when TIPO_ITEM='CICIRETE' then CAPITAL_ITEM*-1 end),0)>10
                ORDER BY CLAVE_BIEN, f.ANO_CUOTA, f.NRO_CUOTA   
                ";


            using var connection = new SqlConnection(connectionString);
                var consultas =  connection.Query<Consulta>(sql,
                commandTimeout: 300,
                buffered: false).ToList();

                return consultas;
            
        }


    }
}
