//#region Tebela Fornecedores
var fornecedorStatus = $("#fornecedorStatus");

$(".ativado").click(function () {
    fornecedorStatus.val("Ativado");
    $("#tabelaFornecedores").DataTable().ajax.reload();
});
$(".desativado").click(function () {
    fornecedorStatus.val("Desativado");
    $("#tabelaFornecedores").DataTable().ajax.reload();
});

$("#tabelaFornecedores").DataTable({
    "language": {
        "sSearch": "Pesquisar: ",
        "processing": "Carregando..."
    },
    "serverSide": true,
    "bProcessing": true,
    "ajax": {
        "url": "/Fornecedores/Filtro",
        "type": "post",
        "data": function (d) {
            d.status = fornecedorStatus.val(),
                d.pesquisa = $("#tabelaFornecedores_filter").find("input").val()
        }
    },
    "columns": [
        { "data": "Id" },
        { "data": "Nome" },
        { "data": "Empresa.NomeFantasia" },
        { "data": "CpfCnpj" },
        { "data": "DataCadastroBR" },
        {
            "data": function (data, type, row) {
                if (data.Status === "Ativado") {
                    return '<button class="btn btn-primary editar-fornecedor" data-id="' + data.Id + '" aria-label="EX" title="Editar">\
                             <i class="fa fa-pencil" aria-hidden="true"></i>\
                         </button>\
                         <button class="btn btn-dark editar-telefones" data-id="' + data.Id + '" aria-label="EX" title="Editar telefones">\
                             <i class="fa fa-phone-square" aria-hidden="true"></i>\
                         </button>\
                         <button class="btn btn-danger desativar-fornecedor" data-id="' + data.Id + '" aria-label="EX" title="Desativar">\
                             <i class="fa fa-thumbs-o-down" aria-hidden="true"></i>\
                         </button>';
                }
                return '<button class="btn btn-success ativar-fornecedor" data-id="' + data.Id + '" aria-label="EX" title="Ativar">\
                            <i class="fa fa-thumbs-o-up" aria-hidden="true"></i>\
                        </button>';
            }
        }
    ]
});
//#endregion