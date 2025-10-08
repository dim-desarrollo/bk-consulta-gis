namespace consultas_gis.api.Models
{
    public class Consulta
    {
                  
            //char / chat / char / char / char / chat / numero / numero / char     

            public string NRO_INTERNO { get; set; } // 
            public string Cuit { get; set; }   
            public string APELLIDO_NOMBRE { get; set; }
            public string Categoria { get; set; }
            public string ANO_CUOTA { get; set; }
            public string NRO_CUOTA { get; set; }
            public float Tributo { get; set; }
            public float Retenciones { get; set; }
            public string Estado { get; set; }
        

    }
}



