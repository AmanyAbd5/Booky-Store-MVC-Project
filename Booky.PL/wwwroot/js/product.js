function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: 'product/getAll',
            type: 'GET',
            dataType: 'Json',
            contentType: 'application/json'
        },
        "columns":
            [
                { data: 'isbn'},
                { data: 'title'},
                { data: 'description'},
                { data: "author"},
                { data: 'category.name'},
                { data: 'listPrice'},
                { data: 'price'},
                { data: 'price50'},
                { data: 'price100' },
                
                //imageURL

                {
                    data: 'imageURL',

                    render: function (data, type, row) {
                        return '<img src="/files/images/'+data+'" alt="Product Image" class="img-thumbnail" />';
                    }
                },

                {
                    data: null,

                    render: function (row) {
                        return '<div class="w-75 btn-group" role="group"> ' +
                            '<a href="/Admin/Product/Edit/' + row.id + '" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a> ' +
                            '<a href="/Admin/Product/Delete/' + row.id + '" class="btn btn-danger mx-2"><i class="bi bi-trash3"></i>Delete</a> ' +
                            '</div>';
                    }
                }

            ]
    });
}
