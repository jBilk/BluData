//#region CEP
function cepNovaEmpresa() {
    $("#cepEmpresa").focusout(function () {
        if ($("#cepEmpresa").val() != "") {
            $.ajax({
                url: 'https://viacep.com.br/ws/' + $(this).val() + '/json/unicode/',
                dataType: 'json',
                success: function (resposta) {
                    $("#enderecoEmpresa").val(resposta.logradouro);
                    $("#complementoEmpresa").val(resposta.complemento);
                    $("#bairroEmpresa").val(resposta.bairro);
                    $("#cidadeEmpresa").val(resposta.localidade);
                    $("#ufEmpresa").val(resposta.uf);
                    $("#numeroEmpresa").focus();
                }
            });
        }
    });
}
//#endregion