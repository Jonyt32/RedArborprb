const path = "https://localhost:5000/api/";

function showErrorMessage(message) {
    alert(message);
}

function validateEmail(email) {

    const regex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    return regex.test(email);
}

function validateDateOfBirth(dateOfBirth) {
    const today = new Date();
    const dob = new Date(dateOfBirth);

    if (isNaN(dob.getTime())) {
        return false;
    }

    let age = today.getFullYear() - dob.getFullYear();
    const m = today.getMonth() - dob.getMonth();

    if (m < 0 || (m === 0 && today.getDate() < dob.getDate())) {
        age--;
    }

    return age >= 18 && age <= 65;
}

function loadRoles() {
    $.get(path + "Role/List")
        .done(function(data) {
            const roleSelect = $('#employeeRole');
            roleSelect.empty();
            roleSelect.append('<option value="" >Seleccione un rol</option>');
            data.forEach(function(role) {
                roleSelect.append(`<option value="${role.roleId}">${role.roleName}</option>`);
            });
        })
        .fail(function(jqXHR, textStatus, errorThrown) {
            showErrorMessage(`Error loading roles: ${errorThrown}`);
        });
}

function initEmployeeTable() {
    $('#employeeTable').DataTable({
        "ajax": {
            "url": path + "Employee/List",
            "dataSrc": "",
            "error": function(jqXHR, textStatus, errorThrown) {
                showErrorMessage(`Error loading employee data: ${errorThrown}`);
            }
        },
        "columns": [
            { "data": "employeeId" },
            { "data": "name" },
            { "data": "surname" },
            { "data": "email" },
            {
                "data": "dateOfBirth",
                "render": function(data, type, row) {
                    return formatDate(data);
                }
            },
            {
                "data": null,
                "render": function(data, type, row) {
                    return `
                        <button class="btn btn-primary editBtn" data-id="${row.employeeId}">Edit</button>
                        <button class="btn btn-danger deleteBtn" data-id="${row.employeeId}">Delete</button>
                    `;
                }
            }
        ]
    });
}

function formatDate(dateString) {
    const date = new Date(dateString);
    const day = ("0" + date.getDate()).slice(-2);
    const month = ("0" + (date.getMonth() + 1)).slice(-2);
    const year = date.getFullYear();
    return `${day}/${month}/${year}`;
}

function showEmployeeModal(employee = {}) {
    $('#employeeForm')[0].reset();
    $('#employeeModalLabel').text(employee.employeeId ? 'Edit Employee' : 'Create Employee');
    $('#employeeId').val(employee.employeeId || '');
    $('#employeeName').val(employee.name || '');
    $('#employeeSurname').val(employee.surname || '');
    $('#employeeEmail').val(employee.email || '');
    $('#employeeDob').val(employee.dateOfBirth || '');

    if (employee.dateOfBirth) {
        const dateOfBirth = new Date(employee.dateOfBirth);
        const formattedDate = dateOfBirth.toISOString().split('T')[0]; // Convierte la fecha al formato YYYY-MM-DD
        $('#employeeDob').val(formattedDate);
    }

    loadRoles();
    $('#employeeRole').val(String(employee.roleId) || ''); // Establece el rol seleccionado
    $('#employeeModal').modal('show');

}

function saveEmployee(employeeData) {
    const url = employeeData.employeeId ? `${path}Employee/Update` : `${path}Employee/Add`;
    const method = employeeData.employeeId ? 'PUT' : 'POST';

    $.ajax({
        url: url,
        method: method,
        contentType: 'application/json',
        data: JSON.stringify(employeeData),
        success: function() {
            $('#employeeModal').modal('hide');
            $('#employeeTable').DataTable().ajax.reload();
        },
        error: function(jqXHR, textStatus, errorThrown) {
            showErrorMessage(`Error saving employee: ${errorThrown}`);
            console.error(jqXHR.responseText);
        }
    });
}

function getEmployeeById(employeeId) {
    $.get(`${path}Employee/GetId?employeeId=${employeeId}`)
        .done(function(data) {
            showEmployeeModal(data);
        })
        .fail(function(jqXHR, textStatus, errorThrown) {
            showErrorMessage(`Error fetching employee: ${errorThrown}`);
        });
}

function deleteEmployee(employeeId) {
    if (confirm('Are you sure you want to delete this employee?')) {
        $.ajax({
            url: `${path}Employee/Delete?employeeId=${employeeId}`,
            method: 'DELETE',
            success: function() {
                $('#employeeTable').DataTable().ajax.reload();
            },
            error: function(jqXHR, textStatus, errorThrown) {
                showErrorMessage(`Error deleting employee: ${errorThrown}`);
            }
        });
    }
}

$(document).ready(function() {
    initEmployeeTable();

    $('#createEmployeeBtn').click(function() {
        showEmployeeModal();
    });

    $('#saveEmployeeBtn').click(function() {
        const name = $('#employeeName').val();
        const surname = $('#employeeSurname').val();
        const email = $('#employeeEmail').val();
        const roleId = $('#employeeRole').val();
        const dateOfBirth = $("#employeeDob").val();

        if (!name || name.length > 100) {
            showErrorMessage("Name is required and should be less than 100 characters.");
            return;
        }

        if (!email || !validateEmail(email)) {
            showErrorMessage("A valid email is required.");
            return;
        }

        if (!roleId || roleId <= 0) {
            showErrorMessage("Role is required and must be valid.");
            return;
        }

        if (!dateOfBirth || !validateDateOfBirth(dateOfBirth)) {
            showErrorMessage("Employee must be between 18 and 65 years old.");
            return;
        }

        let employeeData = {
            employeeId: $('#employeeId').val(),
            name: name,
            surname: surname,
            email: email,
            roleId: roleId,
            dateOfBirth: dateOfBirth
        };

        saveEmployee(employeeData);
    });

    $(document).on('click', '.editBtn', function() {
        let employeeId = $(this).data('id');
        getEmployeeById(employeeId);
    });

    $(document).on('click', '.deleteBtn', function() {
        let employeeId = $(this).data('id');
        deleteEmployee(employeeId);
    });
});