//#region Telefones
$(".table").on('click', '.editar-telefones', function () {
    var abrirModal = $('#editarFornecedor');
    var codFornecedor = $(this).data("id");
    abrirModal.load("/Fornecedores/Editar/" + codFornecedor, function () {
        acaoAbaTelefoneFornecedor();
        verificarNumeroCadastro();
        atualizarTabelaTelefone(codFornecedor);
        abrirModal.modal('show');
        abrirModal.find("#inputId").val(codFornecedor);
    });
});
function novoTelefone() {
    var cpfCnpj = $("#cpfCnpjFornecedor").val();
    $.ajax({
        "url": "/Fornecedores/BuscarFornecedorPorCpfCnpj",
        "type": "POST",
        "data": {
            "cpfCnpj": cpfCnpj
        },
        success: function (data) {
            cadastrarTelefone(data.Id);
        }
    });
}
function cadastrarTelefone(id) {
    var fornecedorId = id;
    var numero = $("#numeroTelefone").val();
    $.ajax({
        "url": "/Telefones/Adiciona",
        "type": "POST",
        "data": {
            "FornecedorId": fornecedorId,
            "Numero": numero
        },
        success: function (data) {
            if (data == "numeroVazio") {
                new Noty({
                    type: 'error',
                    text: 'Preencher corretamente o número de telefone!',
                    layout: 'topCenter',
                    timeout: 3000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
                verificarNumeroCadastro();
                $("#numeroTelefone").focus();
            } else if (data == "numeroJahExiste") {
                new Noty({
                    type: 'error',
                    text: 'Esse número já foi cadastrado anteriormente!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
                verificarNumeroCadastro();
                $("#numeroTelefone").focus();
            } else if (data == "erro") {
                new Noty({
                    type: 'error',
                    text: 'Erro ao cadastrar telefone!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
                verificarNumeroCadastro();
                $("#numeroTelefone").focus();
            } else {
                atualizarTabelaTelefone(fornecedorId);
                new Noty({
                    type: 'success',
                    text: 'Telefone cadastrado com sucesso!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
                verificarNumeroCadastro
                $(document).keypress(function (e) {
                    if (e.which == 13)
                        return false;
                });
                $("#numeroTelefone").val("");
                $("#numeroTelefone").focus();
                verificarNumeroCadastro();
            };
        }
    });
}
function excluirTelefone() {
    var fecharModal = $('#excluirTelefone');
    $form = $("#excluirTelefone").find("form");
    $form.submit(function (e) {
        e.preventDefault();
        var url = "/Telefones/ExcluirConcluido";
        $.post(url, $form.serialize(), function (data) {
            if (data == "erro") {
                new Noty({
                    type: 'error',
                    text: 'Erro ao excluir telefone!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            }
            else {
                new Noty({
                    type: 'success',
                    text: 'Telefone excluido com sucesso!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
                fecharModal.modal("hide");
                $("#tabelaTelefones").DataTable().ajax.reload();
                verificarNumeroCadastro();
            }
        });
    });
}
function focoAdicionarTelefone() {
    $(document).keypress(function (e) {
        if (e.which == 13) {
            $('#botaoAdicionarTelefone').click();
            $("#numeroTelefone").focus();
        }
    });
}
function verificarNumeroCadastro() {
    var cpfCnpj = $("#cpfCnpjFornecedor").val();
    $.ajax({
        "url": "/Fornecedores/BuscarFornecedorPorCpfCnpj",
        "type": "POST",
        "data": {
            "cpfCnpj": cpfCnpj
        },
        success: function (data) {
            $.ajax({
                "url": "/Telefones/BuscarTelefonePorIdFornecedor",
                "type": "POST",
                "data": {
                    "idFornecedor": data.Id
                },
                success: function (data) {
                    document.getElementById('botaoCancelarCadastroTelefone').style.display = 'block';
                    document.getElementById('numeroTelefone').required = false;
                },
                error: function (data) {
                    document.getElementById('botaoCancelarCadastroTelefone').style.display = 'none';
                }
            });
        }
    });
}
function fecharModalFornecedorAbaTelefone() {
    var fecharModal = $('#adicionarFornecedor');
    document.location.reload(true);
    fecharModal.modal('hide');
}
//#endregion