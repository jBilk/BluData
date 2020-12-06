//#region Tebela Usuarios
var usuarioStatus = $("#usuarioStatus");

$(".ativado").click(function () {
    usuarioStatus.val("Ativado");
    $("#tabelaUsuarios").DataTable().ajax.reload();
});
$(".desativado").click(function () {
    usuarioStatus.val("Desativado");
    $("#tabelaUsuarios").DataTable().ajax.reload();
});

$("#tabelaUsuarios").DataTable({
    "language": {
        "sSearch": "Pesquisar: ",
        "processing": "Carregando..."
    },
    "serverSide": true,
    "bProcessing": true,
    "ajax": {
        "url": "/Usuarios/Filtro",
        "type": "post",
        "data": function (d) {
            d.status = usuarioStatus.val(),
                d.pesquisa = $("#tabelaUsuarios_filter").find("input").val()
        }
    },
    "columns": [
        { "data": "Id" },
        { "data": "Nome" },
        { "data": "NomeUsuario" },
        { "data": "Permissao" },
        {
            "data": function (data, type, row) {
                if (data.Status === "Ativado") {
                    return '<button class="btn btn-primary editar-usuario" data-id="' + data.Id + '" aria-label="EX" title="Editar">\
                             <i class="fa fa-pencil" aria-hidden="true"></i>\
                         </button>\
                         <button class="btn btn-danger desativar-usuario" data-id="' + data.Id + '" aria-label="EX" title="Desativar">\
                             <i class="fa fa-thumbs-o-down" aria-hidden="true"></i>\
                         </button>';
                }
                return '<button class="btn btn-success ativar-usuario" data-id="' + data.Id + '" aria-label="EX" title="Ativar">\
                            <i class="fa fa-thumbs-o-up" aria-hidden="true"></i>\
                        </button>';
            }
        }
    ]
});
//#endregion