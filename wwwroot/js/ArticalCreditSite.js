var dataTable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {

            "url": "/admin/ArticalCredit/getall/",

            "type": "GET",
            "datatype": "json"
        },
        "columnDefs": [{ "defaultContent": "-", "targets": "_all" }],
        "columns": [
            { "data": "name", "width": "20%" },
            {
                "data": "url", "width": "40%",
                "render": function (data) {
                    return `<a href="${data}" class="text-decoration-none" target="_blank">${data}</a>`;}
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
&nbsp;

                        <a href="/admin/ArticalCredit/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>

                        &nbsp;

                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete('/admin/ArticalCredit/Delete?id='+${data})>
                            Delete
                        </a>

                        </div>`;
                }, "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}