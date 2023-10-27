let token = $("body").find('input[name="__RequestVerificationToken"]').val();
let table = new DataTable('#employeeTable');
let id = 0;
let employeeTypeId = 0;

$(document).ready(function () {
    init_btnDelete();
    init_btnEdit();
    init_btnCalculate();
});

$("#btnCreate").on("click", function () {
    $("#createEmployeeModal").modal("show");
});

$("#createEmployeeModal").on("show.bs.modal", function () {
    init_btnSaveCreate();
});

$("#editEmployeeModal").on("show.bs.modal", function () {
    init_btnSaveUpdate();
});

$("#computeModal").on("show.bs.modal", function () {
    $("#basicRate").val("");
    $("#daysPresent").val("");
    $("#daysAbsent").val("");
    $("#hoursWorked").val("");
    $("#totalPay").val("0.00");

    $("#daysPresent").parent().addClass("d-none");
    $("#daysAbsent").parent().addClass("d-none");
    $("#hoursWorked").parent().addClass("d-none");

    $("#daysPresent").removeAttr("required");
    $("#daysAbsent").removeAttr("required");
    $("#hoursWorked").removeAttr("required");

    switch (employeeTypeId) {
        case 1:
            $("#daysAbsent").parent().removeClass("d-none");
            $("#daysAbsent").attr("required", true);
            break;
        case 2:
            $("#daysPresent").parent().removeClass("d-none");
            $("#daysPresent").attr("required", true);
            break;
        case 3:
            $("#daysAbsent").parent().removeClass("d-none");
            $("#daysAbsent").attr("required", true);
            break;
        case 4:
            $("#daysPresent").parent().removeClass("d-none");
            $("#hoursWorked").parent().removeClass("d-none");
            $("#daysPresent").attr("required", true);
            $("#hoursWorked").attr("required", true);
            break;
        default:
            break;
    }

    init_btnCompute();
});

function init_btnSaveCreate() {
    $("#btnSaveCreate").off().on("click", function () {
        let isValid = $("#createEmployeeForm")[0].checkValidity();

        if (isValid) {
            let model = {
                fullName: $("#fullName").val()
                , birthDate: $("#birthDate").val()
                , tin: $("#tin").val()
                , employeeTypeId: $("#employeeType").val()
                , __RequestVerificationToken: token
            };

            $.post("/Employee/Add", model, function (response, status) {
                console.log(response);
                if (response) {
                    Swal.fire({
                        title: 'Employee successfully created!',
                        showCancelButton: false,
                        icon: 'success',
                        confirmButtonText: 'Ok'
                    }).then((result) => {
                        window.location.reload();
                    });
                }
            });

        } else {
            Swal.fire('All fields are required.', '', 'info');
        }
    });
}

function init_btnSaveUpdate() {
    $("#btnSaveUpdate").off().on("click", function () {
        let isValid = $("#editEmployeeForm")[0].checkValidity();

        if (isValid) {
            let model = {
                id: id
                , fullName: $("#editFullName").val()
                , birthDate: $("#editBirthDate").val()
                , tin: $("#editTin").val()
                , employeeTypeId: $("#editEmployeeType").val()
                , __RequestVerificationToken: token
            };

            $.post("/Employee/Update", model, function (response, status) {
                console.log(response);
                if (response) {
                    Swal.fire({
                        title: 'Employee successfully updated!',
                        showCancelButton: false,
                        icon: 'success',
                        confirmButtonText: 'Ok'
                    }).then((result) => {
                        window.location.reload();
                    });
                }
            });

        } else {
            Swal.fire('All fields are required.', '', 'info');
        }
    });
}

function init_btnCompute() {
    $("#btnCompute").off().on("click", function () {
        let isValid = $("#computeForm")[0].checkValidity();
        if (isValid) {
            let model = {
                employeeTypeId: employeeTypeId
                , basicSalary: parseInt($("#basicRate").val())
                , daysAbsent: 0
                , daysPresent: 0
                , hoursPresent: 0
                , __RequestVerificationToken: token
            };

            switch (employeeTypeId) {
                case 1:
                    model.daysAbsent = parseInt($("#daysAbsent").val());
                    break;
                case 2:
                    model.daysPresent = parseInt($("#daysPresent").val());
                    break;
                case 3:
                    model.daysAbsent = parseInt($("#daysAbsent").val());
                    break;
                case 4:
                    model.daysPresent = parseInt($("#daysPresent").val());
                    model.hoursPresent = parseInt($("#hoursWorked").val());
                    break;
                default:
                    break;
            }

            console.log(model);
            $.post("/Employee/Compute", model, function (response, status) {
                console.log(response);
                if (response > 0) {
                    $("#totalPay").val(response);
                }
            });

        }
        else {
            Swal.fire('All fields are required.', '', 'info');
        }
    })
}

function init_btnEdit() {
    $(".btnEdit").off().on("click", function () {
        id = $(this).attr("data-id");
        let row = table.row($(this).closest('tr'));
        let data = row.data();
        let fullName = data[0];
        let birthDate = new Date(data[1]);
        let tin = data[2];
        employeeTypeId = $(this).attr("data-type");

        birthDate.setUTCDate(birthDate.getUTCDate() + 1);

        $("#editFullName").val(fullName);
        $("#editBirthDate").val(birthDate.toISOString().split('T')[0]).trigger("change");
        $("#editTin").val(tin);
        $("#editEmployeeType").val(employeeTypeId).trigger("change");
        $("#editEmployeeModal").modal("show");
    })


}

function init_btnDelete() {
    $(".btnDelete").off().on("click", function () {
        id = $(this).attr("data-id");
        let model = {
            id: id,
            __RequestVerificationToken: token
        };

        Swal.fire({
            title: 'Are you sure you want to delete this employee?',
            showCancelButton: true,
            confirmButtonText: 'Yes'
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $.post("/Employee/Remove", model, function (response, status) {
                    if (response) {
                        Swal.fire({
                            title: 'Employee successfully deleted!',
                            showCancelButton: false,
                            icon: 'success',
                            confirmButtonText: 'Ok'
                        }).then((result) => {
                            window.location.reload();
                        });
                    }
                });
            } else if (result.isDenied) {
                Swal.fire('Changes are not saved', '', 'info')
            }
        })
    })
}

function init_btnCalculate() {
    $(".btnCalculate").off().on("click", function () {
        let row = table.row($(this).closest('tr'));
        let data = row.data();
        let fullName = data[0];
        employeeTypeId = parseInt($(this).attr("data-type"));

        $("#computeFullName").val(fullName);
        $("#computeEmployeeType").val(employeeTypeId).trigger("change");

        $("#basicRate").parent().removeClass("d-none");
        $("#computeModal").modal("show");
    })
}

$(".decimal").on('keypress', function (e) {
    var key = e.which;
    if (!(key >= 48 && key <= 57) && key !== 46) {
        e.preventDefault();
    }
});

