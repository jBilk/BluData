//#region Empresas
var $form = null;

$("#nova-empresa").click(function () {
    var abrirModal = $('#adicionarEmpresa');
    abrirModal.load("/Empresas/Form", function () {
        cepNovaEmpresa();
        novaEmpresa();
        abrirModal.modal('show');
    });
});
$(".table").on('click', '.editar-empresa', function () {
    var abrirModal = $('#editarEmpresa');
    var codEmpresa = $(this).data("id");
    abrirModal.load("/Empresas/Editar/" + codEmpresa, function () {
        cepNovaEmpresa();
        confirmarEdicaoEmpresa();
        abrirModal.modal('show');
        abrirModal.find("#inputId").val(codEmpresa);
    });
});
$(".table").on('click', '.visualizar-empresa', function () {
    var abrirModal = $('#visualizarEmpresa');
    var codEmpresa = $(this).data("id");
    abrirModal.load("/Empresas/Visualizar/" + codEmpresa, function () {
        abrirModal.modal('show');
        abrirModal.find("#inputId").val(codEmpresa);
    });
});
$(".table").on('click', '.ativar-empresa', function () {
    var abrirModal = $('#ativarEmpresa');
    var codEmpresa = $(this).data("id");
    abrirModal.load("/Empresas/Ativar/" + codEmpresa, function () {
        ativarEmpresa();
        abrirModal.modal('show');
        abrirModal.find("#inputId").val(codEmpresa);
    });
});
$(".table").on('click', '.desativar-empresa', function () {
    var abrirModal = $('#desativarEmpresa');
    var codEmpresa = $(this).data("id");
    abrirModal.load("/Empresas/Desativar/" + codEmpresa, function () {
        desativarEmpresa();
        abrirModal.modal('show');
        abrirModal.find("#inputId").val(codEmpresa);
    });
});
function novaEmpresa() {
    var fecharModal = $('#adicionarEmpresa');
    $form = $("#adicionarEmpresa").find("form");
    $form.submit(function (e) {
        e.preventDefault();
        var url = "/Empresas/Adiciona";
        $.post(url, $form.serialize(), function (data) {
            if (data == "erro") {
                new Noty({
                    type: 'error',
                    text: 'Erro ao cadastrar empresa!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            }
            else {
                new Noty({
                    type: 'success',
                    text: 'Empresa cadastrada com sucesso!',
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
function confirmarEdicaoEmpresa() {
    $form = $("#editarEmpresa").find("form");
    var fecharModal = $('#editarEmpresa');
    $form.submit(function (e) {
        e.preventDefault();
        var url = "/Empresas/EditarSalvar";
        $.post(url, $form.serialize(), function (data) {
            if (data == "erro") {
                new Noty({
                    type: 'error',
                    text: 'Erro ao editar empresa!',
                    layout: 'topCenter',
                    timeout: 2000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            }
            else {
                new Noty({
                    type: 'success',
                    text: 'Empresa editada com sucesso!',
                    layout: 'topCenter',
                    timeout: 2000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
                fecharModal.modal("hide");
            }
        });
    });
}
function buscarEmpresaCadastro() {
    $.ajax({
        "url": "/Empresas/BuscaPorCnpj",
        "type": "POST",
        "data": {
            "id": $("#inputId").val(),
            "cnpj": $("#cnpjEmpresa").val()
        },
        success: function (data) {
            if (data == "error") {
                $("#cnpjEmpresa").val("");
                $("#cnpjEmpresa").focus();
                new Noty({
                    type: 'error',
                    text: 'CNPJ já cadastrado!',
                    layout: 'topCenter',
                    timeout: 4000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            }
        }
    });
}
function editarEmpresa() {
    $form = $("#editarEmpresa").find("form");
    var url = "/Empresas/EditarSalvar";
    if ($form[0].reportValidity()) {
        $.post(url, $("form").serialize(), function (data) {
            $("#tabelaEmpresas").DataTable().ajax.reload();
        })
    }
}
function ativarEmpresa() {
    var fecharModal = $('#ativarEmpresa');
    $form = $("#ativarEmpresa").find("form");
    $form.submit(function (e) {
        e.preventDefault();
        var url = "/Empresas/AtivarConcluido";
        $.post(url, $form.serialize(), function (data) {
            if (data == "erro") {
                new Noty({
                    type: 'error',
                    text: 'Erro ao ativar empresa!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            }
            else {
                new Noty({
                    type: 'success',
                    text: 'Empresa ativada com sucesso!',
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
function desativarEmpresa() {
    var fecharModal = $('#desativarEmpresa');
    $form = $("#desativarEmpresa").find("form");
    $form.submit(function (e) {
        e.preventDefault();
        var url = "/Empresas/DesativarConcluido";
        $.post(url, $form.serialize(), function (data) {
            if (data == "erro") {
                new Noty({
                    type: 'error',
                    text: 'Erro ao desativar empresa!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            } else {
                new Noty({
                    type: 'success',
                    text: 'Empresa desativada com sucesso!',
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
//#endregion