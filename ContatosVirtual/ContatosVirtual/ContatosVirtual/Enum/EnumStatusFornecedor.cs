using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContatosVirtual.Enum
{
    public class EnumStatusFornecedor
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