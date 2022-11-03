var dataTable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            /*API call*/
            "url": "/admin/classroom/getall/",
            "type": "GET",
            "datatype": "jsonp",
            "contentType": "application/json; charset=utf-8"
        },
        "columnDefs": [{ "defaultContent": "-", "targets": "_all" }],
        "columns": [
            { "data": "classname", "width": "15%" },
            {
                "data": "classId",
                "render": function (data) {
                    return `<div class="text-center">
&nbsp;
                        <a href="/admin/Classroom/Edit?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete('/admin/Classroom/Delete?id='+${data})>
                            Delete
                        </a>
&nbsp;
                        <a class='btn btn-primary text-white' style='cursor:pointer; width:70px;'
                            href="/admin/Classroom/Detail?id=${data}">
                            Detail
                        </a>
                        </div>`;
                }, "width": "35%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}