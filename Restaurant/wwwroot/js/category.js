
function getSubCategory() {
        //var selectedItem = $(this).val();

        var selecteCategoryId = $('#CategoryId').val();
        var subCategory = $("#SubCategoryId");
        


        $.ajax({
            cache: false,
            type: "GET",
            //url: '@Url.Action("GetSubCategory", "MenuItems")',
            url: '/MenuItems/GetSubCategory',
            dataType: 'json',
            data: { "categoryId": selecteCategoryId },
            success: function (data) {
                subCategory.html('');
                $.each(data, function (id, option) {
                    subCategory.append($('<option></option>').val(option.value).html(option.text));
                });

            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.sta);

            }
        });
}



