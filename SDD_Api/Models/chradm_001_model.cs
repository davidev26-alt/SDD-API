namespace SDD_Api.Models
{
    public class chradm_001_model
    {
        public class Consulta_Produto
        {
            public string? Codigo    { get; set; }
            public string? Descricao { get; set; }
        }

        public class Valida_Rastreabilidade
        {
            public DateTime? Data { get; set; }
        }

        public class Consulta_Rastreabilidade
        {
            public string?    Lote      { get; set; }
            public DateTime?  Dh_Rastrea { get; set; }
        }
    }
}
