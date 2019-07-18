//CODIGO PARA EMPRESAS
var formEmpresas = $('#empresasForm');

formEmpresas.Validador({
    razonSocialEmpresa: { required: [true, 'Debe Ingresar la Razon Social de la empresa'] },
    rutEmpresa: { required: [true, 'Debe Ingresar el Rut de la empresa'], rut: [true, 'Rut debe estar en formato xx.xxx.xxx-x'] },
    correoEmpresa: { required: [true, 'Debe Ingresar el email de la empresa'], email: [true, 'debe Ingresar un formato de email valido'] },
    direccionEmpresa: { required: [true, 'Debe Ingresar la Direccion de la empresa'] },
    telefonoEmpresa: { required: [true, 'Debe Ingresar el Telefono de la empresa'] },
    nombreRepEmpresa: { required: [true, 'Debe Ingresar el Nombre del Representante la empresa'] },
    apellidoRepEmpresa: { required: [true, 'Debe Ingresar el Apellido del Representante la empresa'] }
});

function nuevaEmpresa() {
    $('#modalEmpresas').LimpiarModal();
    $('#modal-title').html("Ingresar Empresas");
    $('#button-modal').attr('onclick', 'ingresarEmpresas()');
    $('#button-modal').html("Ingresar Empresas");
    formEmpresas.validate().resetForm();
}

function ingresarEmpresas() {

    formEmpresas.validate();
    if (formEmpresas.valid()) {

        var razon_social = $('#razonSocialEmpresa').val();
        var rut_empresa = $('#rutEmpresa').val();
        var correo = $('#correoEmpresa').val();
        var direccion = $('#direccionEmpresa').val();
        var telefono = $('#telefonoEmpresa').val();
        var nombreRep = $('#nombreRepEmpresa').val();
        var apellidoRep = $('#apellidoRepEmpresa').val();

        Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
        $.post('ingresarEmpresa',
            {
                razon_social: razon_social,
                rut_empresa: rut_empresa,
                correo: correo,
                direccion: direccion,
                telefono: telefono,
                nombreRep: nombreRep,
                apellidoRep: apellidoRep

            },
            function (rdata) {
                if (rdata["response"] === "success") {
                    Metronic.unblockUI();
                    swal({
                        title: "Mantenedor de Empresas",
                        text: "Empresa Agregada",
                        type: "success",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Continuar"
                    }, function () {
                        location.reload();
                    });

                } else {
                    Metronic.unblockUI();
                    swal({
                        title: "Mantenedor de Empresas",
                        text: rdata["message"],
                        type: "error",
                        confirmButtonText: "Continuar"
                    });
                }


            });





    }
}

//function obtenerEmpresa(rut) {


//    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
//    $('#modalEmpresas').LimpiarModal();
//    formEmpresas.validate().resetForm();
//    $.post('obtenerEmpresa',
//              {
//                  rutEmpresa: rut

//              },
//              function (rdata) {

//                  $('#razonSocialEmpresa').val(rdata["razonSocial"]);
//                  $('#rutEmpresa').val(rdata["rutEmpresa"]);
//                  $('#correoEmpresa').val(rdata["correoEmpresa"]);
//                  $('#direccionEmpresa').val(rdata["direccionEmpresa"]);
//                  $('#telefonoEmpresa').val(rdata["telefono"]);
//                  $('#nombreRepEmpresa').val(rdata["nombreRep"]);
//                  $('#apellidoRepEmpresa').val(rdata["apellidoRep"]);

//                  $('#modalEmpresas').modal("show");
//                  $('#modal-title').html("Editar Empresas");
//                  $('#button-modal').attr('onclick', 'editarEmpresa()');
//                  $('#button-modal').html("Editar Empresa");
//                  $('#empresaId').val(rdata["empresaId"]);
//                  Metronic.unblockUI();




//              });
//}

//function editarEmpresa() {


//    formEmpresas.validate();
//    if (formEmpresas.valid()) {
//        var empresaId = $('#empresaId').val();
//        var razon_social = $('#razonSocialEmpresa').val();
//        var rut_empresa = $('#rutEmpresa').val();
//        var correo = $('#correoEmpresa').val();
//        var direccion = $('#direccionEmpresa').val();
//        var telefono = $('#telefonoEmpresa').val();
//        var nombreRep = $('#nombreRepEmpresa').val();
//        var apellidoRep = $('#apellidoRepEmpresa').val();



//        swal({
//            title: "Mantenedor de Empresas",
//            text: "Desea grabar modificaciones?",
//            type: "warning",
//            showCancelButton: true,
//            confirmButtonColor: "#DD6B55",
//            confirmButtonText: "Continuar",
//            cancelButtonText: "Cancelar",
//            closeOnConfirm: false
//        }, function (isConfirm) {
//            if (isConfirm) {
//                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
//                $.post('editarEmpresa',
//                    {
//                        empresaId: empresaId,
//                        razon_social: razon_social,
//                        rut_empresa: rut_empresa,
//                        correo: correo,
//                        direccion: direccion,
//                        telefono: telefono,
//                        nombreRep: nombreRep,
//                        apellidoRep: apellidoRep

//                    },
//                    function (rdata) {
//                        if (rdata["response"] == "success") {
//                            Metronic.unblockUI();
//                            swal({
//                                title: "Mantenedor de Empresas",
//                                text: "Empresa Editada",
//                                type: "success",
//                                confirmButtonColor: "#DD6B55",
//                                confirmButtonText: "Continuar",
//                            }, function () {
//                                location.reload();
//                            });

//                        } else {
//                            Metronic.unblockUI();
//                            swal({
//                                title: "Mantenedor de Empresas",
//                                text: rdata["message"],
//                                type: "error",
//                                confirmButtonText: "Continuar"
//                            });
//                        }


//                    });

//            }
//        });

//    }

//}

//function eliminarEmpresa(empresaId, empresaRazonSocial) {


//    swal({
//        title: "Mantenedor de Empresas",
//        text: 'Realmente desea Eliminar  la Empresa "' + empresaRazonSocial + '"? ',
//        type: "warning",
//        showCancelButton: true,
//        confirmButtonColor: "#DD6B55",
//        confirmButtonText: "Continuar",
//        cancelButtonText: "Cancelar",
//        closeOnConfirm: false
//    }, function (isConfirm) {
//        if (isConfirm) {
//            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
//            $.post('eliminarEmpresa',
//                {
//                    empresaId: empresaId

//                },
//                function (rdata) {
//                    if (rdata["response"] == "success") {
//                        Metronic.unblockUI();
//                        swal({
//                            title: "Mantenedor de Empresas",
//                            text: "Empresa Eliminada",
//                            type: "success",
//                            confirmButtonColor: "#DD6B55",
//                            confirmButtonText: "Continuar",
//                        }, function () {
//                            location.reload();
//                        });

//                    } else {
//                        Metronic.unblockUI();
//                        swal({
//                            title: "Mantenedor de Empresas",
//                            text: rdata["message"],
//                            type: "error",
//                            confirmButtonText: "Continuar"
//                        });
//                    }


//                });

//        }
//    });
//}

//CODIGO PARA EL HUESPED
var formHuesped = $('#HuespedForm');

formHuesped.Validador({
    huespedNombre: { required: [true, 'Debe Ingresar nombre del huesped'] },
    huespedApellido: { required: [true, 'Debe Ingresar apellido del huesped'] },
    huespedRut: { required: [true, 'Debe Ingresar el Rut del huesped'], rut: [true, 'Rut debe estar en formato xx.xxx.xxx-x'] },
    huespedTelefono: { required: [true, 'Debe Ingresar el Telefono del huesped'] },
    huespedCorreo: { required: [true, 'Debe Ingresar el correo del huesped'], email: [true, 'debe Ingresar un formato de correo valido'] },
    
});


function nuevoHuesped() {
    $('#modalHuesped').LimpiarModal();
    $('#modal-title').html("Ingresar Huesped");
    $('#button-modal').attr('onclick', 'ingresarHuesped()');
    $('#button-modal').html("Ingresar Huesped");
    formHuesped.validate().resetForm();
}

function ingresarHuesped() {

    formHuesped.validate();
    if (formHuesped.valid()) {

        var huespedNombre = $('#huespedNombre').val();
        var huespedApellido = $('#huespedApellido').val();
        var huespedRut = $('#huespedRut').val();
        var huespedTelefono = $('#huespedTelefono').val();
        var huespedCorreo = $('#huespedCorreo').val();
       
       

       Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
       $.post('ingresarHuesped',
            {
                huespedNombre: huespedNombre,
                huespedApellido: huespedApellido,
                huespedRut: huespedRut,
                huespedTelefono: huespedTelefono,
                huespedCorreo: huespedCorreo
                

            },
            function (rdata) {
                if (rdata["response"] === "success") {
                    Metronic.unblockUI();
                    swal({
                        title: "Mantenedor de Huesped",
                        text: "Huesped Agregado",
                        type: "success",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Continuar"
                    }, function () {
                        location.reload();
                    });

                } else {
                   Metronic.unblockUI();
                    swal({
                        title: "Mantenedor de Huesped",
                        text: rdata["message"],
                        type: "error",
                        confirmButtonText: "Continuar"
                    });
                }


            });





    }
}


function obtenerHuesped(rut) {


    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
    $('#modalHuesped').LimpiarModal();
    formHuesped.validate().resetForm();
    $.post('obtenerHuesped',
              {
                  rutHuesped: rut

              },
              function (rdata) {

                  $('#huespedNombre').val(rdata["huespedNombre"]);
                  $('#huespedApellido').val(rdata["huespedApellido"]);
                  $('#huespedRut').val(rdata["huespedRut"]);
                  $('#huespedTelefono').val(rdata["huespedTelefono"]);
                  $('#huespedCorreo').val(rdata["huespedCorreo"]);


                  $('#modalHuesped').modal("show");
                  $('#modal-title').html("Editar Huesped");
                  $('#button-modal').attr('onclick', 'editarHuesped()');
                  $('#button-modal').html("Editar Huesped");
                  $('#huespedId').val(rdata["huespedId"]);
                  Metronic.unblockUI();




              });
}


function editarHuesped() {


    formHuesped.validate();
    if (formHuesped.valid()) {

        var huespedId =  $('#huespedId').val();
        var huespedNombre = $('#huespedNombre').val();
        var huespedApellido = $('#huespedApellido').val();
        var huespedRut = $('#huespedRut').val();
        var huespedTelefono = $('#huespedTelefono').val();
        var huespedCorreo = $('#huespedCorreo').val();



        swal({
            title: "Mantenedor de Huesped",
            text: "Desea grabar modificaciones?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Continuar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                $.post('editarHuesped',
                    {
                        huespedId: huespedId,
                        huespedNombre: huespedNombre,
                        huespedApellido: huespedApellido,
                        huespedRut: huespedRut,
                        huespedTelefono: huespedTelefono,
                        huespedCorreo: huespedCorreo

                    },
                    function (rdata) {
                        if (rdata["response"] == "success") {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Huesped",
                                text: "Huesped Editado",
                                type: "success",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Continuar",
                            }, function () {
                                location.reload();
                            });

                        } else {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Huesped",
                                text: rdata["message"],
                                type: "error",
                                confirmButtonText: "Continuar"
                            });
                        }


                    });

            }
        });

    }

}


function eliminarHuesped(huespedId, huespedNombre) {


    swal({
        title: "Mantenedor de Huesped",
        text: 'Realmente desea Eliminar  Al Huesped "' + huespedNombre + '"? ',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
            $.post('eliminarHuesped',
                {
                    huespedId: huespedId

                },
                function (rdata) {
                    if (rdata["response"] == "success") {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Huesped",
                            text: "Huesped Eliminado",
                            type: "success",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "Continuar",
                        }, function () {
                            location.reload();
                        });

                    } else {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Huesped",
                            text: rdata["message"],
                            type: "error",
                            confirmButtonText: "Continuar"
                        });
                    }


                });

        }
    });
}

//CODIGO PARA EMPLEADOS
var formEmpleados = $('#EmpleadosForm');

formEmpleados.Validador({
    empleadoNombre: { required: [true, 'Debe Ingresar el nombre del Empleado'] },
    empleadoApellido: { required: [true, 'Debe Ingresar el apellido del Empleado'] },
    empleadoTelefono: { required: [true, 'Debe Ingresar el Telefono del Empleado'] },
    empleadoCorreo: { required: [true, 'Debe Ingresar el email del Empleado'], email: [true, 'debe Ingresar un formato de email valido'] },
    empleadoEdad: { required: [true, 'Debe Ingresar la edad del Empleado'] },
    empleadoDireccion: { required: [true, 'Debe Ingresar la Direccion del Empleado'] },
   
});


function nuevoEmpleado() {
    $('#modalEmpleado').LimpiarModal();
    $('#modal-title').html("Ingresar Empleado");
    $('#button-modal').attr('onclick', 'ingresarEmpleado()');
    $('#button-modal').html("Ingresar Empleados");
    formEmpleados.validate().resetForm();
}


function ingresarEmpleado() {

    formEmpleados.validate();
    if (formEmpleados.valid()) {

        var empleadoNombre = $('#empleadoNombre').val();
        var empleadoApellido = $('#empleadoApellido').val();
        var empleadoTelefono = $('#empleadoTelefono').val();
        var empleadoCorreo = $('#empleadoCorreo').val();
        var empleadoEdad = $('#empleadoEdad').val();
        var empleadoDireccion = $('#empleadoDireccion').val();


        Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
        $.post('ingresarEmpleado',
             {
                 empleadoNombre: empleadoNombre,
                 empleadoApellido: empleadoApellido,
                 empleadoTelefono: empleadoTelefono,
                 empleadoCorreo: empleadoCorreo,
                 empleadoEdad: empleadoEdad,
                 empleadoDireccion: empleadoDireccion

             },
             function (rdata) {
                 if (rdata["response"] === "success") {
                     Metronic.unblockUI();
                     swal({
                         title: "Mantenedor de Empleados",
                         text: "Empleado Agregado",
                         type: "success",
                         confirmButtonColor: "#DD6B55",
                         confirmButtonText: "Continuar"
                     }, function () {
                         location.reload();
                     });

                 } else {
                     Metronic.unblockUI();
                     swal({
                         title: "Mantenedor de Empleados",
                         text: rdata["message"],
                         type: "error",
                         confirmButtonText: "Continuar"
                     });
                 }


             });





    }
}


function obtenerEmpleado(correo) {


    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
    $('#modalEmpleado').LimpiarModal();
    formEmpleados.validate().resetForm();
    $.post('obtenerEmpleado',
              {
                  correoEmpleado: correo

              },
              function (rdata) {

                  $('#empleadoNombre').val(rdata["empleadoNombre"]);
                  $('#empleadoApellido').val(rdata["empleadoApellido"]);
                  $('#empleadoTelefono').val(rdata["empleadoTelefono"]);
                  $('#empleadoCorreo').val(rdata["empleadoCorreo"]);
                  $('#empleadoEdad').val(rdata["empleadoEdad"]);
                  $('#empleadoDireccion').val(rdata["empleadoDireccion"]);



                  $('#modalEmpleado').modal("show");
                  $('#modal-title').html("Editar Empleado");
                  $('#button-modal').attr('onclick', 'EditarEmpleado()');
                  $('#button-modal').html("Editar Empleado");
                  $('#empleadoId').val(rdata["empleadoId"]);
                  Metronic.unblockUI();



              });
}


function EditarEmpleado() {


    formEmpleados.validate();
    if (formEmpleados.valid()) {

        var empleadoId = $('#empleadoId').val();
        var empleadoNombre = $('#empleadoNombre').val();
        var empleadoApellido = $('#empleadoApellido').val();
        var empleadoTelefono = $('#empleadoTelefono').val();
        var empleadoCorreo = $('#empleadoCorreo').val();
        var empleadoEdad = $('#empleadoEdad').val();
        var empleadoDireccion = $('#empleadoDireccion').val();


        swal({
            title: "Mantenedor de Empleados",
            text: "Desea grabar modificaciones?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Continuar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                $.post('editarEmpleado',
                    {
                        
                         empleadoId: empleadoId,
                        empleadoNombre: empleadoNombre,
                        empleadoApellido: empleadoApellido,
                        empleadoTelefono: empleadoTelefono,
                        empleadoCorreo: empleadoCorreo,
                        empleadoEdad: empleadoEdad,
                        empleadoDireccion:empleadoDireccion

                    },
                    function (rdata) {
                        if (rdata["response"] == "success") {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Empleados",
                                text: "Empleado Editado",
                                type: "success",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Continuar",
                            }, function () {
                                location.reload();
                            });

                        } else {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Empleado",
                                text: rdata["message"],
                                type: "error",
                                confirmButtonText: "Continuar"
                            });
                        }


                    });

            }
        });

    }

}


function EliminarEmpleado(empleadoId, empleadoNombre) {


    swal({
        title: "Mantenedor de Empleados",
        text: 'Realmente desea Eliminar  Al Empleado "' + empleadoNombre + '"? ',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
            $.post('EliminarEmpleado',
                {
                    empleadoId: empleadoId

                },
                function (rdata) {
                    if (rdata["response"] == "success") {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Empleados",
                            text: "Empleado Eliminado",
                            type: "success",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "Continuar",
                        }, function () {
                            location.reload();
                        });

                    } else {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Empleado",
                            text: rdata["message"],
                            type: "error",
                            confirmButtonText: "Continuar"
                        });
                    }


                });

        }
    });
}

//PROVEEDOR
var formProveedor = $('#ProveedoresForm');

formProveedor.Validador({
    proveedorNombre: { required: [true, 'Debe Ingresar el nombre del Proveedor'] },
    proveedorContacto: { required: [true, 'Debe Ingresar contacto del Proveedor'] },
    proveedorRubro: { required: [true, 'Debe Ingresar el Rubro del Proveedor'] },
    proveedorDireccion: { required: [true, 'Debe Ingresar Direccion del Proveedor'] },
    proveedorCorreo: { required: [true, 'Debe Ingresar el email del Proveedor'], email: [true, 'debe Ingresar un formato de email valido'] },
    
});

function nuevoProveedor() {
    $('#modalProveedor').LimpiarModal();
    $('#modal-title').html("Ingresar Proveedor");
    $('#button-modal').attr('onclick', 'ingresarProveedor()');
    $('#button-modal').html("Ingresar Proveedor");
    formProveedor.validate().resetForm();
}
    
 function ingresarProveedor() {

    formProveedor.validate();
    if (formProveedor.valid()) {

        var proveedorNombre = $('#proveedorNombre').val();
        var proveedorContacto = $('#proveedorContacto').val();
        var proveedorRubro = $('#proveedorRubro').val();
        var proveedorDireccion = $('#proveedorDireccion').val();
        var proveedorCorreo = $('#proveedorCorreo').val();
       
        Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
        $.post('ingresarProveedor',
          // cabro ql hnajahjajahja ponle los mismo nombres  ingresarProveedor
             {
                 proveedorNombre: proveedorNombre,
                 proveedorContacto: proveedorContacto,
                 proveedorRubro: proveedorRubro,
                 proveedorDireccion: proveedorDireccion,
                 proveedorCorreo: proveedorCorreo
                
              },
             function (rdata) {
                 if (rdata["response"] === "success") {
                     Metronic.unblockUI();
                     swal({
                         title: "Mantenedor de Proveedores",
                         text: "Proveedor Agregado",
                         type: "success",
                         confirmButtonColor: "#DD6B55",
                         confirmButtonText: "Continuar"
                     }, function () {
                         location.reload();
                     });

                 } else {
                     Metronic.unblockUI();
                     swal({
                         title: "Mantenedor de Proveedores",
                         text: rdata["message"],
                         type: "error",
                         confirmButtonText: "Continuar"
                     });
                 }


             });





    }
}

function obtenerProveedor(correo) {


    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
    $('#modalProveedor').LimpiarModal();
    formProveedor.validate().resetForm();
    $.post('obtenerProveedor',
              {
                  correoProveedor: correo

              },
              function (rdata) {

                  $('#proveedorNombre').val(rdata["proveedorNombre"]);
                  $('#proveedorContacto').val(rdata["proveedorContacto"]);
                  $('#proveedorRubro').val(rdata["proveedorRubro"]);
                  $('#proveedorDireccion').val(rdata["proveedorDireccion"]);
                  $('#proveedorCorreo').val(rdata["proveedorCorreo"]);
                

                  $('#modalProveedor').modal("show");
                  $('#modal-title').html("Editar Proveedor");
                  $('#button-modal').attr('onclick', 'EditarProveedor()');
                  $('#button-modal').html("Editar Proveedor");
                  $('#proveedorId').val(rdata["proveedorId"]);
                  Metronic.unblockUI();



              });
}

function EditarProveedor() {


    formProveedor.validate();
    if (formProveedor.valid()) {

        var proveedorId = $('#proveedorId').val();
        var proveedorNombre = $('#proveedorNombre').val();
        var proveedorContacto = $('#proveedorContacto').val();
        var proveedorRubro = $('#proveedorRubro').val();
        var proveedorDireccion = $('#proveedorDireccion').val();
        var proveedorCorreo = $('#proveedorCorreo').val();
        
        swal({
            title: "Mantenedor de Proveedores",
            text: "Desea grabar modificaciones?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Continuar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                $.post('EditarProveedor',
                    {

                        proveedorId: proveedorId,
                        proveedorNombre: proveedorNombre,
                        proveedorContacto: proveedorContacto,
                        proveedorRubro: proveedorRubro,
                        proveedorDireccion: proveedorDireccion,
                        proveedorCorreo: proveedorCorreo                       

                    },
                    function (rdata) {
                        if (rdata["response"] == "success") {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Proveedores",
                                text: "Proveedor Editado",
                                type: "success",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Continuar",
                            }, function () {
                                location.reload();
                            });

                        } else {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Proveedor",
                                text: rdata["message"],
                                type: "error",
                                confirmButtonText: "Continuar"
                            });
                        }


                    });

            }
        });

    }

}

function EliminarProveedor(proveedorId, proveedorNombre) {


    swal({
        title: "Mantenedor de Proveedores",
        text: 'Realmente desea Eliminar  Al Proveedor "' + proveedorNombre + '"? ',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
            $.post('EliminarProveedor',
                {
                    proveedorId: proveedorId

                },
                function (rdata) {
                    if (rdata["response"] == "success") {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Proveedores",
                            text: "Proveedor Eliminado",
                            type: "success",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "Continuar",
                        }, function () {
                            location.reload();
                        });

                    } else {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Proveedores",
                            text: rdata["message"],
                            type: "error",
                            confirmButtonText: "Continuar"
                        });
                    }


                });

        }
    });
}


//HABITACIONES

var formHabitaciones = $('#habitacionesForm');


formHabitaciones.Validador({
    nombreHabitacion: { required: [true, 'Debe Ingresar  El nombre de La Habitacion'] },
    tipoCamaHabitacion: { required: [true, 'Debe Ingresar El tipo de Cama'] },
    accesorioHabitacion: { required: [true, 'Debe Ingresar Los Accesorios de la Habitacion '] },
    valorHabitacion: { required: [true, 'Debe Ingresar el Valor de la Habitacion'] }
});


function nuevaHabitacion() {
    $('#modalHabitaciones').LimpiarModal();
    $('#modal-title').html("Ingresar Habitaciones");
    $('#button-modal').attr('onclick', 'ingresarHabitacion()');
    $('#button-modal').html("Ingresar Habitaciones");
    formHabitaciones.validate().resetForm();
}


function ingresarHabitacion() {

    formHabitaciones.validate();
    if (formHabitaciones.valid()) {



        var nombreHabitacion = $('#nombreHabitacion').val();
        var tipoCamaHabitacion = $('#tipoCamaHabitacion').val();
        var accesorioHabitacion = $('#accesorioHabitacion').val();
        var valorHabitacion = $('#valorHabitacion').val();


        Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
        $.post('ingresarHabitacion',
            {
                nombreHabitacion: nombreHabitacion,
                tipoCamaHabitacion: tipoCamaHabitacion,
                accesorioHabitacion: accesorioHabitacion,
                valorHabitacion: valorHabitacion

            },
            function (rdata) {
                if (rdata["response"] === "success") {
                    Metronic.unblockUI();
                    swal({
                        title: "Mantenedor de Habitaciones",
                        text: "Habitacion Agregada",
                        type: "success",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Continuar"
                    }, function () {
                        location.reload();
                    });

                } else {
                    Metronic.unblockUI();
                    swal({
                        title: "Mantenedor de Habitaciones",
                        text: rdata["message"],
                        type: "error",
                        confirmButtonText: "Continuar"
                    });
                }
            });
            }
}


function obtenerHabitacion(idHabitacion) {


    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
    $('#modalHabitaciones').LimpiarModal();
    formHabitaciones.validate().resetForm();
    $.post('obtenerHabitacion',
              {
                  idHabitacion: idHabitacion

              },
              function (rdata) {




                  $('#nombreHabitacion').val(rdata["nombreHabitacion"]);
                  $('#tipoCamaHabitacion').val(rdata["tipoCamaHabitacion"]);
                  $('#accesorioHabitacion').val(rdata["accesorioHabitacion"]);
                  $('#valorHabitacion').val(rdata["valorHabitacion"]);


                  $('#modalHabitaciones').modal("show");
                  $('#modal-title').html("Editar Habitacion");
                  $('#button-modal').attr('onclick', 'editarHabitacion()');
                  $('#button-modal').html("Editar Habitacion");
                  $('#habitacionId').val(rdata["idHabitacion"]);
                  Metronic.unblockUI();




              });
}


function editarHabitacion() {


    formHabitaciones.validate();
    if (formHabitaciones.valid()) {


        var habitacionId = $('#habitacionId').val();
        var nombreHabitacion = $('#nombreHabitacion').val();
        var tipoCamaHabitacion = $('#tipoCamaHabitacion').val();
        var accesorioHabitacion = $('#accesorioHabitacion').val();
        var valorHabitacion = $('#valorHabitacion').val();

        swal({
            title: "Mantenedor de Habitaciones",
            text: "Desea grabar modificaciones?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Continuar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                $.post('editarHabitacion',
                    {

                        habitacionId: habitacionId,
                        nombreHabitacion: nombreHabitacion,
                        tipoCamaHabitacion: tipoCamaHabitacion,
                        accesorioHabitacion: accesorioHabitacion,
                        valorHabitacion: valorHabitacion

                    },
                    function (rdata) {
                        if (rdata["response"] == "success") {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Habitacion",
                                text: "Habitacion Editada",
                                type: "success",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Continuar",
                            }, function () {
                                location.reload();
                            });

                        } else {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Habitacion",
                                text: rdata["message"],
                                type: "error",
                                confirmButtonText: "Continuar"
                            });
                        }


                    });

            }
        });

    }

}



function eliminarHabitacion(idHabitacion, nombreHabitacion) {


    swal({
        title: "Mantenedor de Habitaciones",
        text: 'Realmente desea Eliminar  la Habitacion "' + nombreHabitacion + '"? ',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
            $.post('EliminarHabitacion',
                {
                    habitacionId: idHabitacion

                },
                function (rdata) {
                    if (rdata["response"] == "success") {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Habitaciones",
                            text: "Habitacion Eliminaad",
                            type: "success",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "Continuar",
                        }, function () {
                            location.reload();
                        });

                    } else {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Habitaciones",
                            text: rdata["message"],
                            type: "error",
                            confirmButtonText: "Continuar"
                        });
                    }


                });

        }
    });
}

//COMEDORES

//var formComedores = $('#comedoresForm');

//formComedores.Validador({
//    nombreComedor: { required: [true, 'Debe Ingresar  El nombre del Comedor'] },
//    capacidadComedor: { required: [true, 'Debe Ingresar El tipo de comedor'] }
  
//});

//function nuevaComedor() {
//    $('#modalComedores').LimpiarModal();
//    $('#modal-title').html("Ingresar Comedores");
//    $('#button-modal').attr('onclick', 'ingresarComedor()');
//    $('#button-modal').html("Ingresar Comedores");
//    formComedores.validate().resetForm();
//}

//function ingresarComedor() {

//    formComedores.validate();
//    if (formComedores.valid()) {

//        var nombreComedor = $('#nombreComedor').val();
//        var capacidadComedor = $('#capacidadComedor').val();
      

//        Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
//        $.post('ingresarComedor',
//            {
//                nombreComedor: nombreComedor,
//                capacidadComedor: capacidadComedor
               
//            },
//            function (rdata) {
//                if (rdata["response"] === "success") {
//                    Metronic.unblockUI();
//                    swal({
//                        title: "Mantenedor de Comedores",
//                        text: "Comedor Agregado",
//                        type: "success",
//                        confirmButtonColor: "#DD6B55",
//                        confirmButtonText: "Continuar"
//                    }, function () {
//                        location.reload();
//                    });

//                } else {
//                    Metronic.unblockUI();
//                    swal({
//                        title: "Mantenedor de Comedores",
//                        text: rdata["message"],
//                        type: "error",
//                        confirmButtonText: "Continuar"
//                    });
//                }
//            });
//    }
//}

//function obtenerComedor(idComedor) {


//    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
//    $('#modalComedores').LimpiarModal();
//    formComedores.validate().resetForm();
//    $.post('obtenerComedor',
//              {
//                  idComedor: idComedor

//              },
//              function (rdata) {




//                  $('#nombreComedor').val(rdata["nombreComedor"]);
//                  $('#capacidadComedor').val(rdata["capacidadComedor"]);
               

//                  $('#modalComedores').modal("show");
//                  $('#modal-title').html("Editar Comedor");
//                  $('#button-modal').attr('onclick', 'editarComedor()');
//                  $('#button-modal').html("Editar Comedor");
//                  $('#comedorId').val(rdata["idComedor"]);
//                  Metronic.unblockUI();




//              });
//}

//MENU


var formMenu = $('#menuForm');


 formMenu.Validador({
    nombreMenu: { required: [true, 'Debe Ingresar  El nombre del Menu'] },
    tipoMenu: { required: [true, 'Debe Ingresar jornada de Menu'] },
    estiloMenu: { required: [true, 'Debe Ingresar El estilo de Menu'] },
    descripcionMenu: { required: [true, 'Debe Ingresar descripcion del Menu '] },
    valorMenu: { required: [true, 'Debe Ingresar el Valor del Menu'] },
    cantidadMenu: { required: [true, 'Debe Ingresar cantidad del Menu'] }
    });


 function nuevoMenu() {
     $('#modalMenu').LimpiarModal();
    $('#modal-title').html("Ingresar Menu");
    $('#button-modal').attr('onclick', 'ingresarMenu()');
    $('#button-modal').html("Ingresar Menu");
    formMenu.validate().resetForm();
}


 function ingresarMenu() {

     formMenu.validate();
     if (formMenu.valid()) {
     
         var nombreMenu = $('#nombreMenu').val();
         var tipoMenu = $('#tipoMenu').val();
         //alert(tipoMenu);
         var estiloMenu = $('#estiloMenu').val();
         //alert(estiloMenu);
         var descripcionMenu = $('#descripcionMenu').val();
         var valorMenu = $('#valorMenu').val();
         var cantidadMenu = $('#cantidadMenu').val();


        Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
        $.post('ingresarMenu',
            {
                nombreMenu: nombreMenu,
                tipoMenu: tipoMenu,
                estiloMenu:estiloMenu,
                descripcionMenu: descripcionMenu,
                valorMenu: valorMenu,
                cantidadMenu: cantidadMenu

            },
            function (rdata) {
                if (rdata["response"] === "success") {
                    Metronic.unblockUI();
                    swal({
                        title: "Mantenedor de Menu",
                        text: "Menu Agregado",
                        type: "success",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Continuar"
                    }, function () {
                        location.reload();
                    });

                } else {
                    Metronic.unblockUI();
                    swal({
                        title: "Mantenedor de Menu",
                        text: rdata["message"],
                        type: "error",
                        confirmButtonText: "Continuar"
                    });
                }
            });
    }
}


function obtenerMenu(idMenu) {


    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
    $('#modalMenu').LimpiarModal();
    formMenu.validate().resetForm();
    $.post('obtenerMenu',
              {
                  idMenu: idMenu

              },
              function (rdata) {
                
                  $('#nombreMenu').val(rdata["nombreMenu"]);
                  //$('#tipoMenu').val(rdata["tipoMenu"]);
                 // $('#estiloMenu').val(rdata["estiloMenu"]);
                  $('#descripcionMenu').val(rdata["descripcionMenu"]);
                  $('#valorMenu').val(rdata["valorMenu"]);
                  $('#cantidadMenu').val(rdata["cantidadMenu"]);

                  $('#DivtipoMenu').empty();
                  $('#DivtipoMenu').append(rdata["SELECT_MENU"]);

                  $('#DivestiloMenu').empty();
                  $('#DivestiloMenu').append(rdata["ESTILO_MENU"]);
                  

                  $('#modalMenu').modal("show");
                  $('#modal-title').html("Editar Menu");
                  $('#button-modal').attr('onclick', 'editarMenu()');
                  $('#button-modal').html("Editar Menu");
                  $('#menuId').val(rdata["idMenu"]);
                  Metronic.unblockUI();
              });
}

function editarMenu() {


    formMenu.validate();
    if (formMenu.valid()) {
       
        var menuId = $('#menuId').val();
        var nombreMenu = $('#nombreMenu').val();
        var tipoMenu = $('#tipoMenu').val();
        var estiloMenu = $('#estiloMenu').val();
        var descripcionMenu = $('#descripcionMenu').val();
        var valorMenu = $('#valorMenu').val();
        var cantidadMenu = $('#cantidadMenu').val();

        swal({
            title: "Mantenedor de Menu",
            text: "Desea grabar modificaciones?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Continuar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                $.post('editarMenu',
                    {
                       
                        menuId: menuId,
                        nombreMenu: nombreMenu,
                        tipoMenu: tipoMenu,
                        estiloMenu:estiloMenu,
                        descripcionMenu: descripcionMenu,
                        valorMenu: valorMenu,
                        cantidadMenu: cantidadMenu

                    },
                    function (rdata) {
                        if (rdata["response"] == "success") {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Menu",
                                text: "Menu Editado",
                                type: "success",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Continuar",
                            }, function () {
                                location.reload();
                            });

                        } else {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Menu",
                                text: rdata["message"],
                                type: "error",
                                confirmButtonText: "Continuar"
                            });
                        }


                    });

            }
        });

    }

}

function eliminarMenu(idMenu, nombreMenu) {


    swal({
        title: "Mantenedor de Menu",
        text: 'Realmente desea Eliminar  el Menu "' + nombreMenu + '"? ',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
            $.post('eliminarMenu',
                {
                    menuId: idMenu

                },
                function (rdata) {
                    if (rdata["response"] == "success") {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Menu",
                            text: "Menu Eliminado",
                            type: "success",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "Continuar",
                        }, function () {
                            location.reload();
                        });

                    } else {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Menu",
                            text: rdata["message"],
                            type: "error",
                            confirmButtonText: "Continuar"
                        });
                    }


                });

        }
    });
}



