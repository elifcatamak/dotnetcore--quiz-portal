$(document).ready(function () {
    $('#articleDropDown').change(function () {

        $('#articleDesc').empty();

        var selectedGuid = $(this).val();

        if (selectedGuid == "")
            return;

        var desc = $('#' + selectedGuid).text();

        console.log(desc);

        $('#articleDesc').append("<p class='border bg-light' style='padding:30px; border-radius:20px;'>" + desc + "</p>");
    });

    loadDataTable();
});

var dataTable;

function loadDataTable() {
    dataTable = $('#quizListTable').DataTable({
        "ajax": {
            "url": "/quiz/getallquizzes",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": null},
            { "data": "title", "width": "30%" },
            { "data": "guid", "width": "15%" },
            { "data": "created", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="" class="btn btn-info text-white" style="cursor:pointer; width: 120px;">
                            Take Quiz
                        </a>
                        &nbsp;
                        <a class="btn btn-danger text-white" style="cursor:pointer; width: 120px;"
                            onclick=Delete('quiz/Delete?id=${data}')>
                            Delete
                        </a>
                    </div>`;
                },
                "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%",
        "columnDefs": [
            {
                "searchable": false,
                "orderable": false,
                "targets": 0
            }
        ],
        "order": [
            [1, 'asc']
        ]
    });

    dataTable.on('order.dt search.dt', function () {
        dataTable.column(0, {
            search: 'applied',
            order: 'applied'
        }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}

function Delete(deleteUrl) {
    swal({
        title: "Are you sure to delete the quiz?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        // If user selected yes
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: deleteUrl,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}