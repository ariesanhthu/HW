var dataTable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/admin/postnewfeeds/getall/",
            "type": "GET",
            "datatype": "jsonp",
            "contentType": "application/json; charset=utf-8"
        },
        "columnDefs": [{ "defaultContent": "-", "targets": "_all" }],
        "columns": [
            { "data": "time", "width": "15%" },
            { "data": "title", "width": "15%" },
            { "data": "content", "width": "20%" },
            {
                "data": "subjectArtical.name", "width": "15%",
                "render": function (data) {
                    return `<p>${data}</p>`
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
&nbsp;
                        <a href="/admin/PostNewFeeds/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete('/admin/PostNewFeeds/Delete?id='+${data})>
                            Delete
                        </a>
&nbsp;
                        <a class='btn btn-primary text-white' style='cursor:pointer; width:70px;'
                            href="/admin/PostNewFeeds/Detail?id=${data}">
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