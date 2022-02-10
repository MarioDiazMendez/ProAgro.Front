$(document).ready(function () {

    var table;

    var funciondeInicio = function () {

        if ($.fn.DataTable.isDataTable('#permiso-list')) {
            //console.log('entro');
        }
        else {
            $.ajax({
                url: "../permiso/Despliega_Permisos",
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    var dv = $('#permiso-list tbody');
                    dv.empty(); //vacia el cuerpo de la tabla de permiso
                    var lstPermisos = res.EstadoUsuarioById;

                    for (var i = 0; i < lstPermisos.length; i++) {
                        var id = JSON.stringify(lstPermisos[i].idEstado);
                        dv.append("<tr>" +
                            " <td class=' v-align-middle '>" +
                            "   <p><label >&nbsp;<strong>" + lstPermisos[i].idEstado + " - " + lstPermisos[i].NombreEstado  + "</strong></label></p></td>" +
                            " <td class=' v-align-middle '>" +
                            "   <p><label >&nbsp;<strong>" + lstPermisos[i].idUsuario + " - " + lstPermisos[i].NombreUsuario + "</strong></label></p></td>" +

                            " <td>" +
                            //"<button title='Editar' id='editar_permiso_" + lstPermisos[i].idEstado + "_" + lstPermisos[i].idUsuario +
                            //"' value='" + lstPermisos[i].idEstado + "_" + lstPermisos[i].idUsuario +
                            //"' class='btn bg-complete-dark-text-white btn-xs btn-animated from-left ' type='button'>" +
                            //" <span><i class='fas fa-edit'></i></span></button>" +
                            "<button title='Eliminar' id='eliminar_permiso_" + lstPermisos[i].idEstado + "_" + lstPermisos[i].idUsuario +
                            "' value='" + lstPermisos[i].idEstado + "_" + lstPermisos[i].idUsuario +
                            "' class='btn bg-complete-dark-text-white btn-xs btn-animated from-left ' type='button'>" +
                            " <span><i class='fa fa-trash'></i></span></button>" +

                            "</td > " +
                            " </tr>" +
                            "");
                    }

                    var tablepermisos = $('#permiso-list').DataTable({
                        "bJQueryUI": true,
                        "stateSave": true,
                        "stateDuration": 0,
                        "bProcessing": true,
                        dom: 'Blfrtip',
                        language: {
                            paginate: {
                                first: "Primero",
                                next: "Siguiente",
                                previous: 'Anterior',
                                last: "Ultimo"
                            },
                            info: "Pagina _PAGE_ de _PAGES_",
                            loadingRecords: "Por favor espere - Cargando...",
                            search: "Buscar:",
                            zeroRecords: "No se encontraron registros.",
                            sLengthMenu: "Muestra _MENU_"
                        },
                        "pagingType": "full_numbers",

                        "responsive": true,
                        "autoWidth": false,
                        "lengthMenu": [5, 10, 15, 20, 25, 50, 75],
                        "pageLength": 5,
                        conditionalPaging: true,
                    });

                    ////click al boton de editar del permisos
                    //$("#permiso-list").on("click", "[id ^= 'editar_permiso_']", function (e) {
                    //    var valores = $(this).val();
                    //    var fields = valores.split('_');
                    //    var params = {
                    //        idEstado: fields[0],
                    //        idUsuario: fields[1]
                    //    }

                    //    var url = GetUrl('permiso', 'Edit');

                    //    $.ajax({
                    //        url: url,
                    //        data: params,
                    //        type: 'get',
                    //        success: function (res) {                               
                    //            $(".card-body").empty();
                    //            $(".card-body").html(res);
                    //        },
                    //        error: function (jqXHR, textStatus, errorThrown) {                              
                    //            ShowMessage(jqXHR.status + " - " + jqXHR.statusText, 1, jqXHR.status);
                    //        }
                    //    });
                    //});

                    $("#permiso-list").on("click", "[id ^= 'eliminar_permiso_']", function (e) {
                        var valores = $(this).val();
                        var fields = valores.split('_');
                        var params = {
                            idEstado: fields[0],
                            idUsuario: fields[1]
                        }

                        $.ajax({
                            url: "../permiso/Elimina_Permiso",
                            dataType: 'json',
                            data: params,
                            type: 'POST',
                            success: function (res) {                              
                                ResetearConsulta(1);
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                ShowMessage(jqXHR.status + " - " + jqXHR.statusText, 1, jqXHR.status);
                            }
                        });
                    });
                }, //no despliega la lista de permisos en la pagina de index
                error: function (jqXHR, textStatus, errorThrown) {
                    ShowMessage(jqXHR.status + " - " + jqXHR.statusText, 1);
                }
                //});
                //} // este se coloco por la funcion de ajax

            });
        }

    }

    funciondeInicio();

    setTimeout(function () {
        $('#Mensaje_validacion').alert('close');
    }, 3500);

    function ResetearConsulta(opcionSel) {
        var url = GetUrl('permiso', 'Index');
        
        $.ajax({
            url: url,
            success: function (res) {

                $("#indexListado").empty();
                $("#indexListado").html(res);
                $("#idfooter").empty();


                $(' <div id="Mensaje_validacion" class="alert alert-success"><a href="#" class="close" data-dismiss="alert">&times;</a> <strong>Se elimino correctamente el permiso</strong>').appendTo('.card-header');
                setTimeout(function () {
                    $('#Mensaje_validacion').alert('close');
                }, 3500);

              
            }, //no despliega la lista de permisos en la pagina de index
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('error');
                ShowMessage(jqXHR.status + " - " + jqXHR.statusText, 1);
            }
        });

    }

}); // fin de document ready