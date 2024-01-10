function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: 'Roles/getall',
            type: 'GET',
            dataType: 'Json',
            contentType: 'application/json'
        },
        "columns":
            [
                { data: 'id' },
                { data: 'name' },
                { data: 'normalizedName' },


         

                {
                    data: null,

                    render: function (row) {
                        return '<div class="w-75 btn-group" role="group"> ' +
                            '<a href="/Roles/Roles/Edit/' + row.id + '" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a> ' +
                            '<a href="/Roles/Roles/Delete/' + row.id + '" class="btn btn-danger mx-2"><i class="bi bi-trash3"></i>Delete</a> ' +
                            '</div>';
                    }
                }

            ]
    });
}
