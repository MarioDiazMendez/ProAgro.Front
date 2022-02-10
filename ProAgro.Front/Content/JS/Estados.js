$(document).ready(function () {

    var table;

    var funciondeInicio = function () {
       
        if ($.fn.DataTable.isDataTable('#estado-list')) {
            //console.log('entro');
        }
        else {
            $.ajax({
                url: "../estado/Despliega_Estados",
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    var dv = $('#estado-list tbody');
                    dv.empty(); //vacia el cuerpo de la tabla de marcas
                    var lstEstados = res.ListaEstados;

                    for (var i = 0; i < lstEstados.length; i++) {
                        var id = JSON.stringify(lstEstados[i].idEstado);
                        dv.append("<tr>" +
                            " <td class=' v-align-middle '>" +
                            "   <p><label >&nbsp;<strong>" + lstEstados[i].NombreEstado + "</strong></label></p></td>" +
                            " <td class=' v-align-middle '>" +
                            "   <p><label >&nbsp;<strong>" + lstEstados[i].Abreviatura + "</strong></label></p></td>" +

                            " <td>" +
                            "<button title='Editar' id='editar_estado_" + lstEstados[i].idEstado +
                            "' value='" + lstEstados[i].idEstado + "_" + lstEstados[i].NombreEstado + "_" + lstEstados[i].Abreviatura +
                            "' class='btn bg-complete-dark-text-white btn-xs btn-animated from-left ' type='button'>" +
                            " <span><i class='fas fa-edit'></i></span></button>" +
                            "<button title='Eliminar' id='eliminar_estado_" + lstEstados[i].idEstado + "' value='" + lstEstados[i].idEstado +
                            "' class='btn bg-complete-dark-text-white btn-xs btn-animated from-left ' type='button'>" +
                            " <span><i class='fa fa-trash'></i></span></button>" +

                            "</td > " +
                            " </tr>" +
                            "");
                    }

                    var tableestados = $('#estado-list').DataTable({
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

                    ////click al boton de editar del usuario
                    $("#estado-list").on("click", "[id ^= 'editar_estado_']", function (e) {
                        var valores = $(this).val();
                        var fields = valores.split('_');
                        var params = {
                            idEstado: fields[0],
                            NombreEstado: fields[1],
                            Abreviatura: fields[2]
                        }


                        var url = GetUrl('estado', 'Edit');

                        $.ajax({
                            url: url,
                            data: params,
                            type: 'get',
                            success: function (res) {
                                /*  console.log("que pes");*/
                                /*console.log(res);*/
                                $(".card-body").empty();
                                $(".card-body").html(res);
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                /* console.log("que pes2");*/
                                /*  console.log(res);*/
                                ShowMessage(jqXHR.status + " - " + jqXHR.statusText, 1, jqXHR.status);
                            }
                        });

                    });

                    $("#estado-list").on("click", "[id ^= 'eliminar_estado_']", function (e) {
                        var valorID = $(this).val();
                        var params = {
                            idEstado: valorID
                        }

                        //var url = GetUrl('usuario', 'Delete');

                        $.ajax({
                            url: "../estado/Elimina_Estado",
                            dataType: 'json',
                            data: params,
                            type: 'POST',
                            success: function (res) {
                                //$(".card-body").empty();
                                //$(".card-body").html(res);

                                ResetearConsulta(1); //opcionSel = 1 (eliminar)
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                ShowMessage(jqXHR.status + " - " + jqXHR.statusText, 1, jqXHR.status);
                            }
                        });
                    });
                }, //no despliega la lista de usuarios en la pagina de index
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
        var url = GetUrl('estado', 'Index');
        //window.location.href = url;
        //console.log('function');

        $.ajax({
            url: url,
            success: function (res) {

                $("#indexListado").empty();
                $("#indexListado").html(res);
                $("#idfooter").empty();


                $(' <div id="Mensaje_validacion" class="alert alert-success"><a href="#" class="close" data-dismiss="alert">&times;</a> <strong>Se elimino correctamente el estado</strong>').appendTo('.card-header');
                setTimeout(function () {
                    $('#Mensaje_validacion').alert('close');
                }, 3500);

                //$.getScript('/Content/js/Usuarios.js', function () {
                //    $(".card-body").empty();
                //    $(".card-body").html(res);

                //    $(' <div id="Mensaje_validacion" class="alert alert-success"><a href="#" class="close" data-dismiss="alert">&times;</a> <strong>Se elimino correctamente el usuario</strong>').appendTo('.card-header');
                //    setTimeout(function () {
                //        $('#Mensaje_validacion').alert('close');
                //    }, 3500);                   
                //});

            }, //no despliega la lista de marcas en la pagina de index
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('error');
                ShowMessage(jqXHR.status + " - " + jqXHR.statusText, 1);
            }
        });

    }

}); // fin de document ready