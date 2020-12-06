namespace ContatosVirtual.Enum
{
    public class EnumStatusUsuario
    {
        static string Ativado;
        static string Desativado;

        public enum StatusEmpresa
        {
            Ativado,
            Desativado
        }

        public static object StatusAtivado { get => "Ativado"; set => Ativado = "Ativado"; }
        public static object StatusDesativado { get => "Desativado"; set => Desativado = "Desativado"; }
    }
}