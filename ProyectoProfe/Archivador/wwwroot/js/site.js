// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function asyncConfirmProcessForm(form, actionUrl, _title, _confirmText, _successText, _errorText) {
	const formData = new FormData(form); // Crear objeto FormData con el formulario
	
	Swal.fire({
		title: _title,
		text: _confirmText,
		icon: "warning",
		showCancelButton: true,
		confirmButtonColor: "#3085d6",
		cancelButtonColor: "#d33",
		//confirmButtonText: _confirmText,
		showLoaderOnConfirm: true,
		preConfirm: async () => {
			try {
				const response = await fetch(actionUrl, {
					method: "POST",
					headers: {
						"Content-Type": "multipart/form-data"
					},
					body: formData, // Enviar el formulario directamente
				});

				if (!response.ok) {
					throw new Error(`Error: ${response.statusText}`);
				}

				const res = await response.json();

				if (res !== 1) {
					Swal.showValidationMessage(_errorText);
				}
				return res;
			} catch (error) {
			Swal.showValidationMessage(_errorText + ": " + error.message);
			}
		},
		allowOutsideClick: () => !Swal.isLoading()
	}).then((result) => {
		if (result.isConfirmed) {
			Swal.fire({
				icon: "success",
				title: _title,
				text: _successText
			});
		}
	});
}

function asyncConfirmProcessJSONForm(form, actionUrl, _title, _confirmText, _successText, _errorText) {
	// Convertimos el formulario a un objeto JS plano
	const formDataObj = {};
	new FormData(form).forEach((value, key) => {
		formDataObj[key] = value;
	});

	Swal.fire({
		title: _title,
		text: _confirmText,
		icon: "warning",
		showCancelButton: true,
		confirmButtonColor: "#3085d6",
		cancelButtonColor: "#d33",
		showLoaderOnConfirm: true,
		preConfirm: async () => {
			try {
				const response = await fetch(actionUrl, {
					method: "POST",
					headers: {
						"Content-Type": "application/json"
					},
					body: JSON.stringify(formDataObj) // Enviamos JSON
				});

				if (!response.ok) {
					throw new Error(`Error: ${response.statusText}`);
				}

				const res = await response.json();

				if (res !== 1) {
					Swal.showValidationMessage(_errorText);
				}
				return res;
			} catch (error) {
				Swal.showValidationMessage(_errorText + ": " + error.message);
			}
		},
		allowOutsideClick: () => !Swal.isLoading()
	}).then((result) => {
		if (result.isConfirmed) {
			Swal.fire({
				icon: "success",
				title: _title,
				text: _successText
			});
		}
	});
}


/*
 * 
 * 
 
 //Anular el submit del formulario
let formulario = document.getElementById("formulario");
formulario.addEventListener("submit", formulario_submit);

function formulario_submit(e) {
	e.preventDefault();
	
	//proceasmiento asincrono
	asyncConfirmProcessForm(formulario, "ajax.php", "Confirme la inserción de datos",
		"Está seguro de proceder?", "Registro insertado correctamente", "Error en la inserción de registro");
}

 
 
 
 
 */
