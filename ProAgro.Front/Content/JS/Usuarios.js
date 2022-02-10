$(document).ready(function () {

    //valida que el elemento en el dom exista para poder obtener los valores del dropdownlist
    if ($("#idGeorreferencias").length) {
        var valoresGeo = $('select[name="idGeorreferencias"] option:selected').text();

        var fields = valoresGeo.split(' - ');

        var divMapa = $('#frameMapa');
        divMapa.empty();

        divMapa.append("<div>" +

            " <iframe id='frameMapa' width='600' height='470' frameborder='0' scrolling = 'no' marginheight = '0 marginwidth = '0' " +
            " src='https://maps.google.com/maps?q=" + fields[0] + "," + fields[1] + "&hl=es;z=14&amp;output=embed'> " +
            " </iframe > " +
            " </div>" +
            "");
    }
   

    $('#idGeorreferencias').change(function () {
      
        console.log($('select[name="idGeorreferencias"] option:selected').text());
        var valoresGeo = $('select[name="idGeorreferencias"] option:selected').text();

        var fields = valoresGeo.split(' - ');      

        var divMapa = $('#frameMapa');
        divMapa.empty();

        divMapa.append("<div>" +

            " <iframe id='frameMapa' width='600' height='470' frameborder='0' scrolling = 'no' marginheight = '0 marginwidth = '0' " +
            " src='https://maps.google.com/maps?q=" + fields[0] + "," + fields[1] + "&hl=es;z=14&amp;output=embed'> " +
            " </iframe > " +            
            " </div>" +
            "");
    });  

    var table;

    var funciondeInicio = function () {
     
        if ($.fn.DataTable.isDataTable('#usuario-list')) {
      /*      console.log('entro');*/
        }
        else {
            $.ajax({
                url: "../usuario/Despliega_Usuarios",
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    var dv = $('#usuario-list tbody');
                    dv.empty(); //vacia el cuerpo de la tabla de marcas
                    var lstUsuarios = res.ListaUsuarios;

                    for (var i = 0; i < lstUsuarios.length; i++) {
                        var id = JSON.stringify(lstUsuarios[i].idUsuario);
                        dv.append("<tr>" +
                            " <td class=' v-align-middle '>" +
                            "   <p><label >&nbsp;<strong>" + lstUsuarios[i].Nombre + "</strong></label></p></td>" +
                            " <td class=' v-align-middle '>" +
                            "   <p><label >&nbsp;<strong>" + lstUsuarios[i].RFC + "</strong></label></p></td>" +

                            " <td>" +
                            "<button title='Editar' id='editar_usuario_" + lstUsuarios[i].idUsuario +
                            "' value='" + lstUsuarios[i].idUsuario + "_" + lstUsuarios[i].Nombre + "_" + lstUsuarios[i].RFC +
                            "' class='btn bg-complete-dark-text-white btn-xs btn-animated from-left ' type='button'>" +
                            " <span><i class='fas fa-edit'></i></span></button>" +
                            "<button title='Eliminar' id='eliminar_usuario_" + lstUsuarios[i].idUsuario + "' value='" + lstUsuarios[i].idUsuario +
                            "' class='btn bg-complete-dark-text-white btn-xs btn-animated from-left ' type='button'>" +
                            " <span><i class='fa fa-trash'></i></span></button>" +
                            "<button title='Ver Georreferencia' id='verGeorreferencia_usuario_" + lstUsuarios[i].idUsuario +
                            "' value='" + lstUsuarios[i].idUsuario + "_" + lstUsuarios[i].Nombre +
                            "' class='btn bg-complete-dark-text-white btn-xs btn-animated from-left ' type='button'>" +
                            " <span><i class='fa fa-map-marker'></i></span></button>" +

                            "</td > " +
                            " </tr>" +
                            "");
                    }

                    var tableusuarios = $('#usuario-list').DataTable({
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
                    $("#usuario-list").on("click", "[id ^= 'editar_usuario_']", function (e) {
                        var valores = $(this).val();
                        var fields = valores.split('_');
                        var params = {
                            idUsuario: fields[0],
                            Nombre: fields[1],
                            RFC: fields[2]
                        }


                        var url = GetUrl('usuario', 'Edit');

                        $.ajax({
                            url: url,
                            data: params,
                            type: 'get',
                            success: function (res) {
                              /*  console.log("que pes");*/
                                console.log(res);
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

                    //
                    $("#usuario-list").on("click", "[id ^= 'verGeorreferencia_usuario_']", function (e) {
                        var valores = $(this).val();
                        var fields = valores.split('_');
                        var params = {
                            idUsuario: fields[0]
                        }

                        var url = GetUrl('usuario', 'UsuarioEstadoGeorreferencia');

                        $.ajax({
                            url: url,
                            data: params,
                            type: 'get',
                            success: function (res) {
                                /*  console.log("que pes");*/
                                /*  console.log(res);*/

                                $.getScript('/Content/js/Usuarios.js', function () {
                                    $(".card-header").empty();
                                    $(".card-body").empty();
                                    $(".card-body").html(res);

                                    //$(' <div id="Mensaje_validacion" class="alert alert-success"><a href="#" class="close" data-dismiss="alert">&times;</a> <strong>Se elimino correctamente el usuario</strong>').appendTo('.card-header');
                                    //setTimeout(function () {
                                    //    $('#Mensaje_validacion').alert('close');
                                    //}, 3500);                   
                                });


                                
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                /* console.log("que pes2");*/
                                /*  console.log(res);*/
                                ShowMessage(jqXHR.status + " - " + jqXHR.statusText, 1, jqXHR.status);
                            }
                        });

                    });

                 

                    $("#usuario-list").on("click", "[id ^= 'eliminar_usuario_']", function (e) {
                        var valorID = $(this).val();
                        var params = {                           
                            idUsuario: valorID
                        }

                        //var url = GetUrl('usuario', 'Delete');
                     
                        $.ajax({
                            url: "../usuario/Elimina_Usuario",
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
        var url = GetUrl('usuario', 'Index');
        //window.location.href = url;
        console.log('function');

        $.ajax({
            url: url,
            success: function (res) {
               
                $("#indexListado").empty();
                $("#indexListado").html(res);
                $("#idfooter").empty();
                

                $(' <div id="Mensaje_validacion" class="alert alert-success"><a href="#" class="close" data-dismiss="alert">&times;</a> <strong>Se elimino correctamente el usuario</strong>').appendTo('.card-header');
                setTimeout(function () {
                    $('#Mensaje_validacion').alert('close');
                }, 3500);    
            }, //no despliega la lista de marcas en la pagina de index
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('error');
                ShowMessage(jqXHR.status + " - " + jqXHR.statusText, 1);
            }
        });
    }
}); // fin de document ready