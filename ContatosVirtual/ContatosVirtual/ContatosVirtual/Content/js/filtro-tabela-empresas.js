//#region Tabela Empresas
var empresaStatus = $("#empresaStatus");

$(".ativada").click(function () {
    empresaStatus.val("Ativada");
    $("#tabelaEmpresas").DataTable().ajax.reload();
});
$(".desativada").click(function () {
    empresaStatus.val("Desativada");
    $("#tabelaEmpresas").DataTable().ajax.reload();
});

$("#tabelaEmpresas").DataTable({
    "language": {
        "sSearch": "Pesquisar: ",
        "processing": "Carregando..."
    },
    "serverSide": true,
    "bProcessing": true,
    "ajax": {
        "url": "/Empresas/Filtro",
        "type": "post",
        "data": function (d) {
            d.status = empresaStatus.val(),
                d.pesquisa = $("#tabelaEmpresas_filter").find("input").val()
        }
    },
    "columns": [
        { "data": "Id" },
        { "data": "NomeFantasia" },
        { "data": "Cnpj" },
        { "data": "Estado.Sigla" },
        {
            "data": function (data, type, row) {
                if (data.Status === "Ativada") {
                    return '<button class="btn btn-primary editar-empresa" data-id="' + data.Id + '" aria-label="EX" title="Editar">\
                                <i class="fa fa-pencil" aria-hidden="true"></i>\
                            </button>\
                            <button class="btn btn-success visualizar-empresa" data-id="' + data.Id + '" aria-label="EX" title="Visualizar">\
                                <i class="fa fa-eye" aria-hidden="true"></i>\
                            </button>\
                            <button class="btn btn-danger desativar-empresa" data-id="' + data.Id + '" aria-label="EX" title="Desativar">\
                                <i class="fa fa-thumbs-o-down" aria-hidden="true"></i>\
                            </button>';
                }
                return '<button class="btn btn-success ativar-empresa" data-id="' + data.Id + '" aria-label="EX" title="Ativar">\
                            <i class="fa fa-thumbs-o-up" aria-hidden="true"></i>\
                        </button>';
            }
        }
    ]

});
//#endregion