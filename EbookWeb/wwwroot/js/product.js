
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {url:'/admin/product/getall'},
        "columns": [
            { data: 'title', "width":"25%"},
            { data: 'author', "width":"15%"},
            { data: 'isbn', "width":"10%"},
            { data: 'listPrice', "width": "20%" },
            { data: 'category.name', "width": "15%" }
        ]
    });
}

