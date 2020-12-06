//#region Usuarios
var $form = null;

$("#novo-usuario").click(function () {
    var abrirModal = $('#adicionarUsuario');
    abrirModal.load("/Usuarios/Form", function () {
        setTimeout(function () {
            confirmarNovoUsuario();
            abrirModal.modal('show');
            $("#senhaUsuario").val("");
            $("#emailUsuario").val("");
        }, 1000);
    });

});
$(".table").on('click', '.editar-usuario', function () {
    var abrirModal = $('#editarUsuario');
    var codUsuario = $(this).data("id");
    abrirModal.load("/Usuarios/Editar/" + codUsuario, function () {
        confirmarEdicaoUsuario();
        abrirModal.modal('show');
        abrirModal.find("#inputId").val(codUsuario);
    });
});
$(".table").on('click', '.ativar-usuario', function () {
    var abrirModal = $('#ativarUsuario');
    var codUsuario = $(this).data("id");
    abrirModal.load("/Usuarios/Ativar/" + codUsuario, function () {
        ativarUsuario();
        abrirModal.modal('show');
        abrirModal.find("#inputId").val(codUsuario);
    });
});
$(".table").on('click', '.desativar-usuario', function () {
    var abrirModal = $('#desativarUsuario');
    var codUsuario = $(this).data("id");
    abrirModal.load("/Usuarios/Desativar/" + codUsuario, function () {
        desativarUsuario();
        abrirModal.modal('show');
        abrirModal.find("#inputId").val(codUsuario);
    });
});
$("#editar-perfil").click(function () {
    var abrirModal = $('#perfilUsuario');
    abrirModal.load("/Usuarios/Perfil", function () {
        editarPerfil();
        abrirModal.modal('show');
    });

});
function confirmarNovoUsuario() {
    var fecharModal = $('#adicionarUsuario');
    $form = $("#adicionarUsuario").find("form");
    $form.submit(function (e) {
        e.preventDefault();
        var url = "/Usuarios/Adiciona";
        $.post(url, $form.serialize(), function (data) {
            if (data == "erro") {
                new Noty({
                    type: 'error',
                    text: 'Erro ao cadastrar usuário!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            } else if (data == "nomeUsuario") {
                $("#nomeUsuarioUsuario").focus();
                new Noty({
                    type: 'error',
                    text: 'Nome de usuário já existe!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            } else if (data == "emailUsuario") {
                $("#emailUsuario").focus();
                new Noty({
                    type: 'error',
                    text: 'Email já existe!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            }
            else {
                new Noty({
                    type: 'success',
                    text: 'Usuário cadastrado com sucesso!',
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
function confirmarEdicaoUsuario() {
    $form = $("#editarUsuario").find("form");
    var fecharModal = $('#editarUsuario');
    $form.submit(function (e) {
        e.preventDefault();
        var url = "/Usuarios/EditarSalvar";
        $.post(url, $form.serialize(), function (data) {
            if (data == "erro") {
                new Noty({
                    type: 'error',
                    text: 'Erro ao editar usuário!',
                    layout: 'topCenter',
                    timeout: 2000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            } else {
                new Noty({
                    type: 'success',
                    text: 'Usuário editado com sucesso!',
                    layout: 'topCenter',
                    timeout: 2000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
                $("#tabelaUsuarios").DataTable().ajax.reload();
                fecharModal.modal("hide");
            }
        });
    });
}
function verificarCamposDaNovaSenha(usuarioDescrito, email, senha, confirmarSenha) {
    if (usuarioDescrito == "") {
        $("#nomeUsuario").focus();
        new Noty({
            type: 'error',
            text: 'Preencha seu email!',
            layout: 'topCenter',
            timeout: 1000,
            open: 'animated bounceInTop',
            close: 'animated bounceOutTop'
        }).show();
    } else if (senha == "") {
        $("#senha").focus();
        new Noty({
            type: 'error',
            text: 'Preencha sua nova senha!',
            layout: 'topCenter',
            timeout: 1000,
            open: 'animated bounceInTop',
            close: 'animated bounceOutTop'
        }).show();
    } else if (confirmarSenha == "") {
        $("#senha").focus();
        new Noty({
            type: 'error',
            text: 'Confirme sua nova senha!',
            layout: 'topCenter',
            timeout: 1000,
            open: 'animated bounceInTop',
            close: 'animated bounceOutTop'
        }).show();
    } else if (usuarioDescrito != email) {
        $("#usuarioDescrito").focus();
        new Noty({
            type: 'error',
            text: 'Email não confere com o registro do usuário!',
            layout: 'topCenter',
            timeout: 1000,
            open: 'animated bounceInTop',
            close: 'animated bounceOutTop'
        }).show();
    } else if (senha != confirmarSenha) {
        $("#senha").focus();
        new Noty({
            type: 'error',
            text: 'Senhas diferentes!',
            layout: 'topCenter',
            timeout: 1000,
            open: 'animated bounceInTop',
            close: 'animated bounceOutTop'
        }).show();
    } else {
        return true;
    }
}
function editarSenha() {
    var usuarioDescrito = $("#nomeUsuario").val();
    var email = $("#usuarioEmail").val();
    var senha = $("#senha").val();
    var confirmarSenha = $("#confirmeSenha").val();
    var id = $("#inputId").val();

    var verificado = verificarCamposDaNovaSenha(usuarioDescrito, email, senha, confirmarSenha);

    if (verificado == true) {
        $.ajax({
            "url": "/Usuarios/EditarSalvarNovaSenha",
            "type": "POST",
            "data": {
                "id": id,
                "novaSenha": senha
            },
            success: function (data) {
                $("#nomeUsuario").val("");
                $("#senha").val("");
                $("#confirmeSenha").val("");
                if (data == "erro") {
                    new Noty({
                        type: 'error',
                        text: 'Erro ao cadastrar nova senha, entre em contato com o suporte!',
                        layout: 'topCenter',
                        timeout: 3000,
                        open: 'animated bounceInTop',
                        close: 'animated bounceOutTop'
                    }).show();
                } else if (data == "senhaCara") {
                    $("#senhaUsuario").focus();
                    new Noty({
                        type: 'error',
                        text: 'Sua senha tem que ter entre 4 e 10 caracteres!',
                        layout: 'topCenter',
                        timeout: 3000,
                        open: 'animated bounceInTop',
                        close: 'animated bounceOutTop'
                    }).show();
                }else {
                    new Noty({
                        type: 'success',
                        text: 'Senha alterada com sucesso!',
                        layout: 'topCenter',
                        timeout: 2000,
                        open: 'animated bounceInTop',
                        close: 'animated bounceOutTop'
                    }).show();
                    setTimeout(function () {
                        $(location).attr("href", data);
                    }, 1500);
                }
            }
        });
    }
}
function ativarUsuario() {
    var fecharModal = $('#ativarUsuario');
    $form = $("#ativarUsuario").find("form");
    $form.submit(function (e) {
        e.preventDefault();
        var url = "/Usuarios/AtivarConcluido";
        $.post(url, $form.serialize(), function (data) {
            if (data == "erro") {
                new Noty({
                    type: 'error',
                    text: 'Erro ao ativar usuário!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            }
            else {
                new Noty({
                    type: 'success',
                    text: 'Usuário ativada com sucesso!',
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
function desativarUsuario() {
    var fecharModal = $('#desativarUsuario');
    $form = $("#desativarUsuario").find("form");
    $form.submit(function (e) {
        e.preventDefault();
        var url = "/Usuarios/DesativarConcluido";
        $.post(url, $form.serialize(), function (data) {
            if (data == "erro") {
                new Noty({
                    type: 'error',
                    text: 'Erro ao desativar usuário!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            } else {
                new Noty({
                    type: 'success',
                    text: 'Usuário desativado com sucesso!',
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
function editarPerfil() {
    $form = $("#perfilUsuario").find("form");
    var fecharModal = $('#perfilUsuario');
    $form.submit(function (e) {
        e.preventDefault();
        var url = "/Usuarios/EditarPerfilSalvar";
        $.post(url, $form.serialize(), function (data) {
            if (data == "erro") {
                new Noty({
                    type: 'error',
                    text: 'Erro ao editar perfil, entre em contato com o suporte!',
                    layout: 'topCenter',
                    timeout: 3000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            } else if (data == "nomeUsuario") {
                $("#nomeUsuarioUsuario").focus();
                new Noty({
                    type: 'error',
                    text: 'Nome de usuário já existe!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            } else if (data == "senhaCara") {
                $("#senhaUsuario").focus();
                new Noty({
                    type: 'error',
                    text: 'Sua senha tem que ter entre 4 e 10 caracteres!',
                    layout: 'topCenter',
                    timeout: 3000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            } else if (data == "emailUsuario") {
                $("#emailUsuario").focus();
                new Noty({
                    type: 'error',
                    text: 'Email já existe!',
                    layout: 'topCenter',
                    timeout: 1000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
            } else {
                new Noty({
                    type: 'success',
                    text: 'Perfil editado com sucesso! Realize um novo login',
                    layout: 'topCenter',
                    timeout: 4000,
                    open: 'animated bounceInTop',
                    close: 'animated bounceOutTop'
                }).show();
                fecharModal.modal("hide");
                setTimeout(function () {
                    $(location).attr("href", data);
                }, 4000);
            }
        });
    });
}
//#endregion