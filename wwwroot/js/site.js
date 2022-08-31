function readURL(input) {
    if (input.files && input.files[0]) {
        let reader = new FileReader();

        reader.onload = function (e) {
            $("img#imgpreview").attr("src", e.target.result).width(200);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
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

function SavePost(url) {
    $.ajax({
        type: "GET",
        url: url,
        success: function (data)
        {
            toastr.success(data.message);
        }
    });
}

function Clear(url) {
    swal({
        title: "Bạn có chắc muốn xóa hết không?",
        text: "Các bài viết sau khi xóa sẽ không thể khôi phục lại!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "GET",
                url: url,
                success: function ()
                {
                    toastr.success("Xóa thành công!");
                    location.reload();
                }
            });
        }
    });
}
