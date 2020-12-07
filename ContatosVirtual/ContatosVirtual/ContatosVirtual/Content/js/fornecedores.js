//#region Fornecedores
var $form = null;

$("#novo-fornecedor").click(function () {
    var abrirModal = $('#adicionarFornecedor');
    abrirModal.load("/Fornecedores/Form", function () {
        visualizarTipoPessoa();
        novoFornecedor();
        abrirModal.modal('show');
    });
});
$(".table").on('click', '.editar-fornecedor', function () {
    var abrirModal = $('#editarFornecedor');
    var codFornecedor = $(this).data("id");
    abrirModal.load("/Fornecedores/Editar/" + codFornecedor, function () {
        visualizarTipoPessoa();
        confirmarEdicaoFornecedor();
        abrirModal.modal('show');
        abrirModal.find("#inputId").val(codFornecedor);
    });
});
$(".table").on('click', '.ativar-fornecedor', function () {
    var abrirModal = $('#ativarFornecedor');
    var codFornecedor = $(this).data("id");
    abrirModal.load("/Fornecedores/Ativar/" + codFornecedor, function () {
        ativarFornecedor();
        abrirModal.modal('show');
        abrirModal.find("#inputId").val(codFornecedor);
    });
});
$(".table").on('click', '.desativar-fornecedor', function () {
    var abrirModal = $('#desativarFornecedor');
    var codFornecedor = $(this).data("id");
    abrirModal.load("/Fornecedores/Desativar/" + codFornecedor, function () {
        desativarFornecedor();
        abrirModal.modal('show');
        abrirModal.find("#inputId").val(codFornecedor);
    });
});
function novoFornecedor() {
    $('#abaPanelTelefone').addClass('disabled');
    $form = $("#adicionarFornecedor").find("form");
    $form.submit(function (e) {
        e.preventDefault();
        var url = "/Fornecedores/Adiciona";
        $.post(url, $form.serialize(), function (data) {
            if (data == "erro") {
                new Noty({
                    type: 'error',
                    text: 'Erro ao cadastrar fornecedor!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            }
            if (data == "EhFornecedorPFMenorIdadeEmpresaParana") {
                new Noty({
                    type: 'error',
                    text: 'Empresa do PR não pode ter um fornecedor menor de idade!',
                    layout: 'topCenter',
                    timeout: 3000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            }
            else {
                new Noty({
                    type: 'success',
                    text: 'Fornecedor cadastrado com sucesso!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
                $("#tabelaFornecedores").DataTable().ajax.reload();
                acaoAbaTelefoneFornecedor();
            }
        });
    });
}
function acaoAbaTelefoneFornecedor() {
    $('#abaPanelTelefone').removeClass('disabled');
    document.getElementById('abaPanelTelefone').click();
    $('#abaPanelFornecedor').addClass('disabled');
    $(document).keypress(function (e) {
        if (e.which == 13)
            return false;
    });
    document.getElementById('numeroTelefone').required = true;
    document.getElementById('botaoCancelarCadastroTelefone').style.display = 'none';
    $("#tabelaTelefones").DataTable().ajax.reload();
    document.getElementById('selectTabelaTelefones').style.display = 'none';
    focoAdicionarTelefone();
}
function confirmarEdicaoFornecedor() {
    $('#abaPanelTelefone').addClass('disabled');
    $form = $("#editarFornecedor").find("form");
    var fecharModal = $('#editarFornecedor');
    $form.submit(function (e) {
        e.preventDefault();
        var url = "/Fornecedores/EditarSalvar";
        $.post(url, $form.serialize(), function (data) {
            if (data == "erro") {
                new Noty({
                    type: 'error',
                    text: 'Erro ao editar fornecedor!',
                    layout: 'topCenter',
                    timeout: 2000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            }
            if (data == "EhFornecedorPFMenorIdadeEmpresaParana") {
                new Noty({
                    type: 'error',
                    text: 'Empresa do PR não pode ter um fornecedor menor de idade!',
                    layout: 'topCenter',
                    timeout: 3000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            }
            else {
                new Noty({
                    type: 'success',
                    text: 'Fornecedor editado com sucesso!',
                    layout: 'topCenter',
                    timeout: 2000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
                fecharModal.modal("hide");
                $("#tabelaFornecedores").DataTable().ajax.reload();
            }
        });
    });
}
function buscarFornecedorCadastro() {
    $.ajax({
        "url": "/Fornecedores/VerificarPorCpfCnpjJahCadastrado",
        "type": "POST",
        "data": {
            "id": $("#inputId").val(),
            "cpfCnpj": $("#cpfCnpjFornecedor").val()
        },
        success: function (data) {
            if (data == "error") {
                $("#cpfCnpjFornecedor").val("");
                $("#cpfCnpjFornecedor").focus();
                new Noty({
                    type: 'error',
                    text: 'CPF/CNPJ já cadastrado!',
                    layout: 'topCenter',
                    timeout: 4000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            }
        }
    });
}
function editarFornecedor() {
    $form = $("#editarFornecedor").find("form");
    var url = "/Fornecedores/EditarSalvar";
    if ($form[0].reportValidity()) {
        $.post(url, $("form").serialize(), function (data) {
            $("#tabelaFornecedores").DataTable().ajax.reload();
        })
    }
}
function ativarFornecedor() {
    var fecharModal = $('#ativarFornecedor');
    $form = $("#ativarFornecedor").find("form");
    $form.submit(function (e) {
        e.preventDefault();
        var url = "/Fornecedores/AtivarConcluido";
        $.post(url, $form.serialize(), function (data) {
            if (data == "erro") {
                new Noty({
                    type: 'error',
                    text: 'Erro ao ativar forncedor!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            }
            else {
                new Noty({
                    type: 'success',
                    text: 'Fornecedor ativado com sucesso!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
                fecharModal.modal("hide");
                setTimeout(function () {
                    $(location).attr("href", data);
                }, 1000);
            }
        });
    });
}
function desativarFornecedor() {
    var fecharModal = $('#desativarFornecedor');
    $form = $("#desativarFornecedor").find("form");
    $form.submit(function (e) {
        e.preventDefault();
        var url = "/Fornecedores/DesativarConcluido";
        $.post(url, $form.serialize(), function (data) {
            if (data == "erro") {
                new Noty({
                    type: 'error',
                    text: 'Erro ao desativar fornecedor!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            }
            else {
                new Noty({
                    type: 'success',
                    text: 'Fornecedor desativado com sucesso!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
                fecharModal.modal("hide");
                setTimeout(function () {
                    $(location).attr("href", data);
                }, 1000);
            }
        });
    });
}
function visualizarTipoPessoa() {
    var campoCpfCnpj = $("#cpfCnpjFornecedor");
    if ($("#pessoaJuridica").prop("checked")) {
        pessoaJuridicaSelecionado(campoCpfCnpj);
    }
    else if ($("#pessoaFisica").prop("checked")) {
        pessoaFisicaSelecionado(campoCpfCnpj);
    }
}
function definirTipoPessoa(doc) {
    if ($("#pessoaJuridica").prop("checked")) {
        somenteNumeros('cnpj', doc)
    } else {
        somenteNumeros('cpf', doc)
    }
}
function limitMe(e) {
    if (e.keyCode == 14) { return true; }
    return this.value.length < $(this).attr("maxLength");
}
function pessoaJuridicaSelecionado(campoCpfCnpj) {
    campoCpfCnpj.attr('maxLength', '18').keypress(limitMe);
    document.getElementById('divRg').style.display = 'none';
    document.getElementById('divDN').style.display = 'none';
    document.getElementById('rg.Fornecedor').required = false;
    document.getElementById('dataNascimento.Fornecedor').required = false;
}
function pessoaFisicaSelecionado(campoCpfCnpj) {
    campoCpfCnpj.attr('maxLength', '14').keypress(limitMe);
    document.getElementById('divRg').style.display = 'block';
    document.getElementById('divDN').style.display = 'block';
    document.getElementById('rg.Fornecedor').required = true;
    document.getElementById('dataNascimento.Fornecedor').required = true;
}
//#endregion