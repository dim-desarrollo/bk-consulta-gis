using System.Text.Json.Serialization;

namespace consultas_gis.api.Models
{
    public class Consulta
    {
        [JsonPropertyName("ACTIV_PRINCIPAL")]
        private string _activPrincipal = string.Empty;
        

                [JsonPropertyName("NRO_INTERNO")]
        public string NRO_INTERNO { get; set; } = string.Empty;

        [JsonPropertyName("CUIT")]
        public string CUIT { get; set; } = string.Empty;

        [JsonPropertyName("APELLIDO_NOMBRE")]
        public string APELLIDO_NOMBRE { get; set; } = string.Empty;

        [JsonPropertyName("CATEGORIA")]
        public string CATEGORIA { get; set; } = string.Empty;

        [JsonPropertyName("ANO_CUOTA")]
        public string ANO_CUOTA { get; set; } = string.Empty;

        [JsonPropertyName("NRO_CUOTA")]
        public string NRO_CUOTA { get; set; } = string.Empty;

        [JsonPropertyName("TRIBUTO")]
        public float TRIBUTO { get; set; }

        [JsonPropertyName("RETENCIONES")]
        public float RETENCIONES { get; set; }

        [JsonPropertyName("ESTADO")]
        public string ESTADO { get; set; } = string.Empty;

        [JsonPropertyName("DEUDA")]
        public float DEUDA { get; set; }
        public string ACTIV_PRINCIPAL
        {
            get => _activPrincipal;
            set => _activPrincipal = value?.Trim() ?? string.Empty;
        }

    }
}
