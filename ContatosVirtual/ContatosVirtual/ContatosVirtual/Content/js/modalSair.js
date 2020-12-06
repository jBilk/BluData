//#region Modal Sair
$("#botao-sair").click(function () {
    var abrirModal = $('#modalSair');
    abrirModal.load("/Modal/Sair", function () {
        abrirModal.modal('show');
    });
});
//#endregion