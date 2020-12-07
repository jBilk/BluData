using ContatosVirtual.Content.Enum;
using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using System;

namespace ContatosVirtual.Servicos
{
    public class FornecedorServicos
    {
        private readonly IEmpresa _empresa;
        private readonly IEstado _estado;

        public FornecedorServicos(IEmpresa empresa, IEstado estado)
        {
            _empresa = empresa;
            _estado = estado;
        }

        public bool VerificarFornecedorPFMenorIdadeEmpresaParane(Fornecedor fornecedor)
        {
            return fornecedor.PessoaJuridicaFisica.ToString().Equals(EnumTipoPessoa.PessoaFisica.ToString())
                    && EhFornecedorPFMenorIdadeEmpresaParana(fornecedor.PessoaJuridicaFisica.ToString(), fornecedor.DataNascimento.GetValueOrDefault(), fornecedor.EmpresaId);
        }

        private bool EhFornecedorPFMenorIdadeEmpresaParana(string pessoaJuridicaFisica, DateTime dataNascimento, int? empresaId)
        {
            var idEstadoEmpresa = _empresa.BuscaPorId(empresaId.GetValueOrDefault()).EstadoId.GetValueOrDefault();
            var ehDoParana = _estado.BuscaPorId(idEstadoEmpresa).NomeEstado.ToString().Equals(EnumEstados.EstadoParana.ToString());

            if (ehDoParana && pessoaJuridicaFisica == EnumTipoPessoa.PessoaFisica.ToString())
            {
                return EhMenorDeIdade(dataNascimento);
            }

            return false;
        }

        private bool EhMenorDeIdade(DateTime dataNascimento)
        {
            var anos = DateTime.Now.Year - dataNascimento.Year;

            if (anos < 18)
                return true;

            if (anos == 18)
            {
                if (DateTime.Now.Month < dataNascimento.Month ||
                   (DateTime.Now.Month == dataNascimento.Month &&
                    DateTime.Now.Day < dataNascimento.Day))
                {
                    return true;
                }
            }

            return false;
        }

    }
}