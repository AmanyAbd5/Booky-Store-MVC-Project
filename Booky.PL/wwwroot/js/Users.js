function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: 'Users/getall',
            type: 'GET',
            dataType: 'Json',
            contentType: 'application/json'
        },
        "columns":
            [
                { data: 'id' },
                { data: 'fname' },
                { data: 'lname' },
                { data: "email" },
                { data: 'roles' },
                

                //imageURL

                //{
                //    data: 'imageURL',

                //    render: function (data, type, row) {
                //        return '<img src="/files/images/' + data + '" alt="Product Image" class="img-thumbnail" />';
                //    }
                //},

                {
                    data: null,

                    render: function (row) {
                        return '<div class="w-75 btn-group" role="group"> ' +
                            '<a href="/Users/Users/Edit/' + row.id + '" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a> ' +
                            '<a href="/Users/Users/Delete/' + row.id + '" class="btn btn-danger mx-2"><i class="bi bi-trash3"></i>Delete</a> ' +
                            '</div>';
                    }
                }

            ]
    });
}
