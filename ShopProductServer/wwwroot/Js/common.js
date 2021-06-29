window.ShowToastr = (type, message) => {
    if (type === "success") {
        // Display a success toast, with a title
        toastr.success(message, 'Process Success!');
    }
    if (type === "error") {
        // Display a success toast, with a title
        toastr.error(message, 'Process Failed!');
    }
}

window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire(
            'Notification Successs!',
            message,
            'success'
        )
    }
    if (type === "error") {
        Swal.fire(
            'Notification Failed!',
            message,
            'error'
        )
    }
}