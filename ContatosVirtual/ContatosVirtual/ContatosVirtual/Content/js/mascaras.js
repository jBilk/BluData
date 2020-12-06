function chamadaMascara(o, f) {
    setTimeout(function () {
        var v = f(o.value);
        if (v != o.value) {
            o.value = v;
        }
    }, 1);
}

//#region Mascara Cpf/CNPJ
function numeroCpfCnpj(v) {
    var n = v.replace(/\D/g, "");
    if (n.length > 11) {
        n = n.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2}).*/, "$1.$2.$3/$4-$5");
    } else if (n.length > 10) {
        n = n.replace(/^(\d{3})(\d{3})(\d{3})(\d{2}).*/, "$1.$2.$3-$4");
    } else {
        n = n.replace(/^(\d{3})(\d{0,5})/, "$1.$2");
    }
    return n;
}
function verificarInformacoes(documento) {
    var descricao = "";
    var cnpjCar = /^[0-9]{2}\.[0-9]{3}\.[0-9]{3}\/[0-9]{4}\-[0-9]{2}$/;
    var cpfCar = /^[0-9]{3}\.[0-9]{3}\.[0-9]{3}\-[0-9]{2}$/;

    if (documento.value != "" && $("#pessoaJuridica").prop("checked")) {
        descricao = "cnpj";
        if (descricao == "cnpj" && !cnpjCar.test(documento.value)) {
            new Noty({
                type: 'error',
                text: 'Preencher corretamente o ' + descricao,
                layout: 'topCenter',
                timeout: 2000,
                open: 'animated bounceInTop',
                close: 'animated bounceOutTop'
            }).show();

            $("#" + documento.id).focus();
        } else {
            verificarCadastros("cpfCnpjFornecedor");
        }
    }
    else if (documento.value != "" && $("#pessoaFisica").prop("checked")) {
        descricao = "cpf";
        if (descricao == "cpf" && !cpfCar.test(documento.value)) {
            new Noty({
                type: 'error',
                text: 'Preencher corretamente o ' + descricao,
                layout: 'topCenter',
                timeout: 2000,
                open: 'animated bounceInTop',
                close: 'animated bounceOutTop'
            }).show();

            $("#" + documento.id).focus();
        } else {
            verificarCadastros("cpfCnpjFornecedor");
        }
    } else if (documento.value != "") {
        descricao = "cnpj";
        if (descricao == "cnpj" && !cnpjCar.test(documento.value)) {
            new Noty({
                type: 'error',
                text: 'Preencher corretamente o ' + descricao,
                layout: 'topCenter',
                timeout: 2000,
                open: 'animated bounceInTop',
                close: 'animated bounceOutTop'
            }).show();

            $("#" + documento.id).focus();
        } else {
            verificarCadastros("cnpjEmpresa");
        }
    }
}
//#endregion

//#region Espaços
function semEspaco(v) {
    return v.normalize("NFD").replace(/[^a-zA-Zs]/g, "");
}
//#endregion

//#region Mascara Telefone
function numero(v) {
    var n = v.replace(/\D/g, "");
    n = n.replace(/^0/, "");
    if (n.length > 10) {
        n = n.replace(/^(\d\d)(\d{5})(\d{4}).*/, "($1)$2-$3");
    } else if (n.length > 5) {
        n = n.replace(/^(\d\d)(\d{4})(\d{0,4}).*/, "($1)$2-$3");
    } else {
        n = n.replace(/^(\d\d)(\d{0,5})/, "($1)$2");
    }
    return n;
}
function validarTelefone(documento) {
    var numerof = /^\([0-9]{2}\)[0-9]{4}\-[0-9]{4}$/;
    var numerom = /^\([0-9]{2}\)[0-9]{5}\-[0-9]{4}$/;

    if (documento.value != "") {
        if (!numerof.test(documento.value) && !numerom.test(documento.value)) {
            new Noty({
                type: 'error',
                text: 'Preencher corretamente o número de telefone!',
                layout: 'topCenter',
                timeout: 2000,
                open: 'animated bounceInTop',
                close: 'animated bounceOutTop'
            }).show();

            $("#" + documento.id).focus();
        }
    }
}
//#endregion

//#region Mascara CEP
function formatar(mascara, documento) {
    var i = documento.value.length;
    var saida = mascara.substring(0, 1);
    var texto = mascara.substring(i)

    if (texto.substring(0, 1) != saida) {
        documento.value += texto.substring(0, 1);
    }
}
//#endregion

//#region Mascara Nomes
function semNumeros(v) {
    return v.replace(/[^A-zÀ-ú ' '']/g, "");
}
function verificarNomeMascara(documento) {

    var nomeCar = /^[A-zÀ-ú ' '']+$/;
    var ultimoCar = documento.value.substring(documento.value.lastIndexOf('') -1);

    if (documento.value == "") {
        return;
    }

    if (!nomeCar.test(documento.value) || documento.value.trim().split(' ').length < 2 || ultimoCar == " ") {
        new Noty({
            type: 'error',
            text: 'Preencher corretamente o nome',
            layout: 'topCenter',
            timeout: 1000,
            open: 'animated bounceInTop',
            close: 'animated bounceOutTop'
        }).show();
        $("#" + documento.id).focus();
    } 

}
//#endregion