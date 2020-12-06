//#region Login
$(document).keypress(function (e) {
    if (e.which == 13)
        $("#botaoLogin").click();
});

function verificarLogin() {

    var nomeUsuario = $("#nomeUsuario").val();
    var senha = $("#senha").val();

    if (nomeUsuario == "") {
        $("#nomeUsuario").focus();
        new Noty({
            type: 'error',
            text: 'Preencha o usuário!',
            layout: 'topCenter',
            timeout: 1000,
            open: 'animated bounceInTop',
            close: 'animated bounceOutTop'
        }).show();
    } else if (senha == "") {
        $("#senha").focus();
        new Noty({
            type: 'error',
            text: 'Preencha a senha!',
            layout: 'topCenter',
            timeout: 1000,
            open: 'animated bounceInTop',
            close: 'animated bounceOutTop'
        }).show();
    } else {
        $.ajax({
            "url": "/Login/Autenticar",
            "type": "POST",
            "data": {
                "nomeUsuario": nomeUsuario,
                "senha": senha
            },
            success: function (data) {
                if (data == "erroUsuario") {
                    new Noty({
                        type: 'error',
                        text: 'Usuário ou senha incorreta!',
                        layout: 'topCenter',
                        timeout: 1000,
                        open: 'animated bounceInTop',
                        close: 'animated bounceOutTop'
                    }).show();
                } else if (data == "error") {
                    new Noty({
                        type: 'error',
                        text: 'Erro ao realizar login, entre com contato com o suporte!',
                        layout: 'topCenter',
                        timeout: 3000,
                        open: 'animated bounceInTop',
                        close: 'animated bounceOutTop'
                    }).show();
                } else {
                    $(location).attr("href", data);
                }
            }
        });
    };
}
function novaSenha() {

    var email = $("#emailRecuperarSenha").val();

    if (email == "") {
        $("#emailRecuperarSenha").focus();
        new Noty({
            type: 'error',
            text: 'Preencha seu email!',
            layout: 'topCenter',
            timeout: 1000,
            open: 'animated bounceInTop',
            close: 'animated bounceOutTop'
        }).show();
    } else {
        $.ajax({
            "url": "/Email/BuscarPorEmailEnviarMensagem",
            "type": "POST",
            "data": {
                "email": email
            },
            success: function (data) {
                if (data == "erro") {
                    $("#emailRecuperarSenha").focus();
                    new Noty({
                        type: 'error',
                        text: 'Email não localizado!',
                        layout: 'topCenter',
                        timeout: 1000,
                        open: 'animated bounceInTop',
                        close: 'animated bounceOutTop'
                    }).show();
                } else if (data == "erro") {
                    $("#emailRecuperarSenha").focus();
                    new Noty({
                        type: 'error',
                        text: 'Erro ao enviar email. Entre em contato com o suporte!',
                        layout: 'topCenter',
                        timeout: 3000,
                        open: 'animated bounceInTop',
                        close: 'animated bounceOutTop'
                    }).show();
                } else if (data == "enviado") {
                    $("#emailRecuperarSenha").val("");
                    new Noty({
                        type: 'success',
                        text: 'Confira sua caixa de entrada, email enviado com sucesso!',
                        layout: 'topCenter',
                        timeout: 3000,
                        open: 'animated bounceInTop',
                        close: 'animated bounceOutTop'
                    }).show();
                }
            }
        });
    };
}
//#endregion