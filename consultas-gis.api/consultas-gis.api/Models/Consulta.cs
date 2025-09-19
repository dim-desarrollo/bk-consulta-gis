namespace consultas_gis.api.Models
{
    public class Consulta
    {

        
            public string DOCUMENTO { get; set; }
            public string APELLIDO_NOMBRE { get; set; }
            public string CONCEPTO { get; set; }
            public string CLAVE_BIEN { get; set; }
            public int ANO_CUOTA { get; set; }
            public int NRO_CUOTA { get; set; }
            public decimal SALDO { get; set; }
            public string ESTADO { get; set; }
            public string CATEGORIA { get; set; }
            public int MINIMO { get; set; }

    }
}
