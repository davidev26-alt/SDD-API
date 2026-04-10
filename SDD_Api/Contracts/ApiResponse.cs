namespace SDD_Api.Contracts
{
    public class ApiResponse<T>
    {
        public bool Sucesso { get; set; }

        public ApiErrorInfo? Erro { get; set; }
        public T? Dados { get; set; }

        public static ApiResponse<T> Ok(T dados) => new()

        {

            Sucesso = true,

            Erro = null,

            Dados = dados
        };


        public static ApiResponse<T> FromError(int erroNum, string erroDes) => new()

        {

            Sucesso = false,

            Erro = new ApiErrorInfo(erroNum, erroDes),

            Dados = default
        };
    }
}
