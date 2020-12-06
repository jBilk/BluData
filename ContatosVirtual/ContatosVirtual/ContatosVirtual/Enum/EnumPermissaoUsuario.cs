namespace ContatosVirtual.Content.Enum
{
    public static class EnumPermissaoUsuario
    {
        static string Administrador;
        static string Usuario;

        public enum PermissaoUser
        {
            Administrador,
            Usuario        }

        public static object PermissaoAdministrador { get => "Administrador"; set => Administrador = "Administrador"; }
        public static object PermissaoUsuario { get => "Usuário"; set => Usuario = "Usuário"; }
    }
}