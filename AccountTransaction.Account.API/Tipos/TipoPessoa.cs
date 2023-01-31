namespace AccountTransaction.Account.API.Tipos
{
    public class TipoPessoa
    {
        public const string FISICA = "F";
        public const string JURIDICA = "J";

        public static string GetTipo(string identificador)
        {
            return identificador.TrimStart().TrimEnd().Length == 11 ? FISICA : JURIDICA;
        }
    }
}
