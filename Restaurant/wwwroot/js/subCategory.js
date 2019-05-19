



function subCategoryAutoComolete() {
    $("#selectedCategory").change(function() {
        $("#searchInput").val("");
    });
    $("#searchInput").autocomplete({
        source: function(request, response) {
            $.ajax({
                url: '@Url.Action("GetSearchValue", "SubCategories")',
                //url: "/SubCategories/GetSearchValue",
                dataType: "json",
                data: { search: $("#searchInput").val(), categoryId: $('#selectedCategory :selected').val() },
                success: function(data) {
                    response($.map(data, function(item) {
                        //return { label: item.Name, value: item.Name };
                        return { label: item.label, value: item.label };
                    }));
                },
                error: function(xhr, status, error) {
                    alert("Error");
                }
            });
        }
    });
}
