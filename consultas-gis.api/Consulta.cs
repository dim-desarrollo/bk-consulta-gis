using System.Text.Json.Serialization;

namespace consultas_gis.api.Models
{
    public class Consulta
    {
        [JsonPropertyName("nrO_INTERNO")]
        public string NRO_INTERNO { get; set; } = string.Empty;

        [JsonPropertyName("cuit")]
        public string CUIT { get; set; } = string.Empty;

        [JsonPropertyName("apellidO_NOMBRE")]
        public string APELLIDO_NOMBRE { get; set; } = string.Empty;

        [JsonPropertyName("categoria")]
        public string CATEGORIA { get; set; } = string.Empty;

        [JsonPropertyName("anO_CUOTA")]
        public string ANO_CUOTA { get; set; } = string.Empty;

        [JsonPropertyName("nrO_CUOTA")]
        public string NRO_CUOTA { get; set; } = string.Empty;

        [JsonPropertyName("tributo")]
        public float TRIBUTO { get; set; }

        [JsonPropertyName("retenciones")]
        public float RETENCIONES { get; set; }

        [JsonPropertyName("estado")]
        public string ESTADO { get; set; } = string.Empty;

        [JsonPropertyName("deuda")]
        public float DEUDA { get; set; }
    }
}
