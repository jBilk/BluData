namespace ContatosVirtual.Content.Enum
{
    public static class EnumStatusEmpresa
    {
        static string Ativada;
        static string Desativada;

        public enum StatusEmpresa
        {
            Ativada,
            Desativada        }

        public static object StatusAtivada { get => "Ativada"; set => Ativada = "Ativada"; }
        public static object StatusDesativada { get => "Desativada"; set => Desativada = "Desativada"; }
    }
}