//#region Verifica��es
function verificarCadastros(referecia) {
    if (referecia == "cnpjEmpresa") {
        buscarEmpresaCadastro();
    }
    else if (referecia == "cpfCnpjFornecedor") {
        buscarFornecedorCadastro();
    }
}
//#endregion