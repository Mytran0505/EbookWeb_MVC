var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#btnClass').DataTable({
        "ajax": {url:'/custommer/cart/index'},
        "columns": [
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                                        <a asp-action="plus" asp-route-cartId="@item.Id" class="btn btn-outline-primary bg-gradient py-2">
                                            <i class="bi bi-plus-square"></i>
                                        </a> &nbsp;
                                        <a asp-action="minus" asp-route-cartId="@item.Id" class="btn btn-outline-primary bg-gradient py-2">
                                            <i class="bi bi-dash-square"></i>
                                        </a>
                                    </div>`
                    
                },
                "width": "25%"
            }
        ]
    });
}

function Remove(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
            //Swal.fire({
            //    title: "Deleted!",
            //    text: "Your file has been deleted.",
            //    icon: "success"
            //});
        }
    });
}

