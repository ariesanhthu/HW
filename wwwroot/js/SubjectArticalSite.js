var dataTable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {

            "url": "/admin/SubjectArtical/getall/",

            "type": "GET",
            "datatype": "json"
        },
        "columnDefs": [{ "defaultContent": "0", "targets": "_all" }],
        "columns": [
            { "data": "name", "width": "40%" },
            {
                "data": "total", "width": "40%"},
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
&nbsp;

                        <a href="/admin/SubjectArtical/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>

                        &nbsp;

                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete('/admin/SubjectArtical/Delete?id='+${data})>
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