$('#btnPlus').click(function (e) {
    e.preventDefault();
    var spotContainer = $("#QualificationContainer");
    $.ajax({
        url: "/Employees/AddNewQualification",
        type: "GET",
        success: function (data) {
            spotContainer.append(data);
        }
    })
})
//
$(document).on("click", "#btnDeleteQualification", function (e) {
    e.preventDefault();
    $(this).parent().parent().remove();
});
//======== Image Show =============
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#ImgFile').attr('src', e.target.result)
        };
        reader.readAsDataURL(input.files[0]);
    }
}