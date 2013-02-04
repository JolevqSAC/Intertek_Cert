$(document).ready(function () {
//    listar();
//    var productoId = "";
//    jQuery('#divMantenimiento').hide();
//    buscar();
//    jQuery('#btnRegistrar').click(function (e) {

//        //    ventanaMantenimiento("Registrar Producto", 0);
//        ventanaRegistrar();
//    });
});


function eliminar() {
    jQuery.ajax({
        type: 'POST',
        dataType: 'json',
        url: baseUrl,
        data: ({
            accion: 'eliminar',
            id: jQuery('#hdnId').val()
            
        }),
        success: function (data) {
            if (data.Id > 0) {

                //  alert(data.Mensaje, "Producto");

              //  jQuery('#divMantenimiento').dialog('close');
                // buscar();
            }
            else {
                //  alert(data.Mensaje, "Producto");
            }
        },
        error: function (data) {
            //    alert(data.Mensaje, ' Producto');

        }

    });
}