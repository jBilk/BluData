namespace ContatosVirtual.Content.Enum
{
    public static class EnumTipoPessoa
    {
        static string Fisica;
        static string Juridica;

        public enum TipoPessoa
        {
            Fisica,
            Juridica
        }

        public static object PessoaFisica { get => "Fisica"; set => Fisica = "Fisica"; }
        public static object PessoaJuridica { get => "Juridica"; set => Juridica = "Juridica"; }
    }
}