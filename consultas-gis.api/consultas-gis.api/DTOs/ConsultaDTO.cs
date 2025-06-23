namespace consultas_gis.DTOs
{
    public class ConsultaDTO
    {
        public string Documento { get; set; } = string.Empty;
        public string ApellidoyNombre { get; set; } = string.Empty;
        public string Concepto { get; set; } = string.Empty;
        public string ClaveBien { get; set; } = string.Empty;
        public int AnoCuota { get; set; }
        public int NumeroCuota { get; set; }
        public decimal CapitalItem { get; set; }
        public string Estado { get; set; }
    }
}
