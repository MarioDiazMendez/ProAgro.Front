var GetUrl = function (controllerName, methodName) {
    /// <summary>Obtiene Url para petición Ajax</summary>  
    /// <param name="controllerName" type="string">Nombre del controlador.</param>  
    /// <param name="methodName" type="string">Nombre de la función.</param> 
    var url =  "../" + controllerName + "/" + methodName;
    return url;
};

var ShowMessage = function (message, messageType, messageErrorCode) {
    /// <summary>Muestra modal con mensaje personalizado.</summary>  
    /// <param name="message" type="string">Mensaje a mostrar.</param>  
    /// <param name="messageType" type="integer">1: Error, 2: Información.</param>
    /// <param name="messageErrorCode" type="integer">Código de Error.</param>
    if (messageType == 1) {
        if (messageErrorCode == 401) {
            window.location.href = "/EmisorUAT/Account/Index";
        }
        else {
            console.log('entro');
            //$.messager.alert("Detectamos una anomalía al procesar tu solicitud<hr>", message);
        }
    }
    else if (messageType == 2) {
        console.log('entro2');
        //$.messager.alert("Información<hr>", message);
    }
    else {
        console.log('entro3');
       // $.messager.popup(message);
    }
};