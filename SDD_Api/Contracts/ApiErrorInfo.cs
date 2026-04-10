namespace SDD_Api.Contracts
{
    public class ApiErrorInfo
    {
        public int ErroNum { get; set; }
        public string? ErroDes { get; set; }
        public ApiErrorInfo(int erroNum, string? erroDes)
        {
            ErroNum = erroNum;
            ErroDes = erroDes;
        }
    }
}
