namespace SDD_Api.Models
{
    public class chradm_001_model
    {
        public class Consulta_Produto
        {
            public string? Descricao { get; set; }
        }

        public class Valida_Rastreabilidade
        {
            public decimal Cont { get; set; }
        }

        public class Consulta_Rastreabilidade
        {
            public string?    Lote      { get; set; }
            public DateTime?  Dh_Rastrea { get; set; }
        }
    }
}
