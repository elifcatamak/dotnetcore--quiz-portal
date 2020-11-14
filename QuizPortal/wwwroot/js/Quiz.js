$(document).ready(function () {
    $('#articleDropDown').change(function () {

        $('#articleDesc').empty();

        var selectedArticleId = $(this).val();

        if (selectedArticleId == "")
            return;

        var desc = $('#' + selectedArticleId).text();

        console.log(desc);

        $('#articleDesc').append("<p class='border bg-success text-white' style='padding:30px; border-radius:20px;'>" + desc + "</p>");
    });

    loadDataTable();
});

var dataTable;
var complete = false;

function loadDataTable() {
    dataTable = $('#quizListTable').DataTable({
        "ajax": {
            "url": "/quiz/getallquizzes",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": null},
            { "data": "title",
                "render": function (data, type, row, meta) {
                    return `<a href="/quiz/quiz?id=${row.id}" style="cursor:pointer; width: 120px;">
                                ${data}
                            </a>`
                },
                "width": "40%" },
            { "data": "articleId", "width": "20%" },
            { "data": "createdStr", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a class="btn btn-danger text-white" style="cursor:pointer; width: 120px;"
                            onclick=Delete('quiz/Delete?id=${data}')>
                            Delete
                        </a>
                    </div>`;
                },
                "width": "20%"
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

function answerOnClick(questionIndex, answer) {
    if (complete == true)
        return;

    var elemList = document.getElementsByClassName("custom+" + questionIndex);

    for (var i=0; i<elemList.length; i++) {
        elemList[i].classList.remove("btn-warning");
    }

    document.getElementById("question+" + questionIndex + '+' + answer).classList.add("btn-warning");
}

function CompleteQuiz() {
    var elemList = document.getElementsByClassName("btn-warning");

    if (elemList.length != 4) {
        toastr.error("Please complete the quiz");

        return;
    }

    for (var i=3; i>=0; i--) {
        var correctAnswer = document.getElementById('correct+' + i).value;
        var givenAnswer = elemList[i].getAttribute("for");

        if (givenAnswer == correctAnswer) {     
            elemList[i].classList.add("btn-success");
        }
        else {
            elemList[i].classList.add("btn-danger");
        }

        elemList[i].classList.remove("btn-warning");
    }

    complete = true;
    document.getElementById("completeBtn").style.visibility = "hidden";
}