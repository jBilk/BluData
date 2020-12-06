//#region Tabela Telefones
var fornId = $("#fornId");

$("#tabelaTelefones").DataTable({
    "iDisplayLength": 3,
    "language": {
        "sSearch": "Pesquisar: ",
        "processing": "Carregando...",
        "lengthMenu": '<select id="selectTabelaTelefones">' +
            '<option value="3">3</option>' +
            '</select>'
    },
    "serverSide": true,
    "bProcessing": true,
    "ajax": {
        "url": "/Telefones/ListaTelefonesPorFornecedorComFiltro",
        "type": "post",
        "data": function (d) {
            d.fornecedorId = fornId.val(),
                d.pesquisa = $("#tabelaTelefones_filter").find("input").val()
        }
    },
    "columns": [
        { "data": "Id" },
        { "data": "Numero" },
        {
            "data": function (data, type, row) {
                return '<button class="btn btn-danger excluir-telefone" data-id="' + data.Id + '" aria-label="EX" title="Excluir">\
                        <i class="fa fa-trash-o" aria-hidden="true"></i>\
                    </button>';
            }
        }
    ]
});

$(".table").on('click', '.excluir-telefone', function () {
    var abrirModal = $('#excluirTelefone');
    var codTelefone = $(this).data("id");
    abrirModal.load("/Telefones/Excluir/" + codTelefone, function () {
        excluirTelefone();
        abrirModal.modal('show');
        abrirModal.find("#inputId").val(codTelefone);
    });
});

function atualizarTabelaTelefone(fornecedorId) {
    fornId.val(fornecedorId);
    document.getElementById('selectTabelaTelefones').style.display = 'none';
    $("#tabelaTelefones").DataTable().ajax.reload();
}
function fecharModalExcluirTelefone() {
    var fecharModal = $('#excluirTelefone');
    fecharModal.modal('hide');
}
//#endregion