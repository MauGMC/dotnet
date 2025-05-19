//Validaciones personalizadas para el plugin de validación de jQuery

// Validación para que no se acepten fechas futuras
jQuery.validator.addMethod("todayonwards", function (value, element, params) {
    if (!value) return true; 
    var fechaIngresada = new Date(value);
    var fechaLimite = new Date(params);
    return fechaIngresada <= fechaLimite;
});
jQuery.validator.unobtrusive.adapters.addSingleVal("todayonwards", "today");

// Validación para que no se acepten fechas pasadas dada una fehcha mínima
jQuery.validator.addMethod("mindate", function (value, element, min) {
    if (!value) return true;
    var fechaIngresada = new Date(value);
    var fechaMin = new Date(min);
    return fechaIngresada >= fechaMin;
});
jQuery.validator.unobtrusive.adapters.addSingleVal("mindate", "min");

//Validación para que un archivo no sobre pase un tamaño máximo
jQuery.validator.addMethod("maxfilesize", function (value, element, param) {
    if (!element.files || element.files.length === 0) return true;
    return element.files[0].size <= parseInt(param);
});
jQuery.validator.unobtrusive.adapters.addSingleVal("maxfilesize", "max");

//Validación para que un archivo tenga extensiones dadas
jQuery.validator.addMethod("allowedextensions", function (value, element, param) {
    if (!value) return true;
    const extension = value.substring(value.lastIndexOf('.')).toLowerCase();
    const allowed = param.split(',').map(e => e.trim().toLowerCase());
    return allowed.includes(extension);
});
jQuery.validator.unobtrusive.adapters.addSingleVal("allowedextensions", "extensions");

//Validación para que un archivo no esté vacío
// Validación para que un archivo no esté vacío
jQuery.validator.addMethod("notemptyfile", function (value, element) {
    if (!element.files || element.files.length === 0) return true; // no hay archivo
    return element.files[0].size > 0;
});
jQuery.validator.unobtrusive.adapters.add("notemptyfile", [], function (options) {
    options.rules["notemptyfile"] = true;
    options.messages["notemptyfile"] = options.message;
});
