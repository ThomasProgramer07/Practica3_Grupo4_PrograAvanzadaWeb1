(() => {

    const RepresentanteLegal = {
        tabla: null,
        init() {
            this.inicializarTabla();
            this.registrarEventos()
        },
        inicializarTabla() {
            this.tabla = $('#tblRepresentanteLegal').DataTable({
                ajax: {
                    url: 'RepresentanteLegal/GetRepresentanteLegals',
                    type: 'GET',
                    dataSrc: 'dato'
                },
                columns: [
                    { data: 'id' },
                    { data: 'identificacion' },
                    { data: 'nombre' },
                    { data: 'edad' },
                    { data: 'apellido1' },
                    { data: 'apellido2' },
                    { data: 'sexo' },
                    {
                        data: 'telefonos',
                        render: (data) => {
                            if (!data || data.length === 0) {
                                return '<span class="text-muted">Sin teléfono</span>';
                            }
                            return data.map(t => t.numero).join(', ');
                        }
                    },
                    {
                        data: 'correos',
                        render: (data) => {
                            if (!data || data.length === 0) {
                                return '<span class="text-muted">Sin correo</span>';
                            }
                            return data.map(t => t.correoElectronico).join(', ');
                        }
                    },
                    {
                        data: null,
                        title: 'Acciones',
                        orderable: false,
                        render: (data, type, row) => {
                            return `
                                   <button class="btn btn-sm btn-primary btn-editar" data-id="${row.id}">Editar</button>
                                   <button class="btn btn-sm btn-danger btn-eliminar" data-id="${row.id}">Eliminar</button>
                                    `
                        }
                    }
                ],

                language: {
                    url: 'https://cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
                }

            });
        },
        registrarEventos() {

            $('#tblRepresentanteLegal').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                RepresentanteLegal.cargarRepresentanteLegal(id);
            });
            $('#tblRepresentanteLegal').on('click', '.btn-eliminar', function () {
                const id = $(this).data('id');
                RepresentanteLegal.eliminarRepresentanteLegal(id);
            });

            $('#btnGuardarRepresentanteLegal').on('click', function () {
                RepresentanteLegal.guardarRepresentanteLegal();
            });

            $('#btnEditarRepresentanteLegal').on('click', function () {
                RepresentanteLegal.editarRepresentanteLegal();
            });

            $('#btnAgregarTelefonoCrear').on('click', function () {
                RepresentanteLegal.agregarFilaTelefono('#telefonosCrear');
            });

            $('#btnAgregarTelefonoEditar').on('click', function () {
                RepresentanteLegal.agregarFilaTelefono('#telefonosEditar');
            });

            $('#btnAgregarCorreoCrear').on('click', function () {
                RepresentanteLegal.agregarFilaCorreo('#correosCrear');
            });

            $('#btnAgregarCorreoEditar').on('click', function () {
                RepresentanteLegal.agregarFilaCorreo('#correosEditar');
            });

            $(document).on('click', '.btn-quitar-telefono', function () {
                const contenedor = $(this).closest('.telefono-row').parent();
                $(this).closest('.telefono-row').remove();
                RepresentanteLegal.reindexarTelefonos(contenedor);
            });

            $(document).on('click', '.btn-quitar-correo', function () {
                const contenedor = $(this).closest('.correo-row').parent();
                $(this).closest('.correo-row').remove();
                RepresentanteLegal.reindexarCorreos(contenedor);
            });

            $('#modalCrearRepresentanteLegal').on('hidden.bs.modal', function () {
                $('#telefonosCrear').empty();
                $('#correosCrear').empty();
            });

        },

        agregarFilaTelefono(selectorContenedor, numero = '') {
            const indice = $(selectorContenedor).children('.telefono-row').length;
            const fila = $(`
                <div class="input-group mb-2 telefono-row">
                    <span class="input-group-text"><i class="bi bi-telephone"></i></span>
                    <input type="text" name="Telefonos[${indice}].Numero" class="form-control" placeholder="Ej: 8888-8888" value="${numero}" />
                    <button type="button" class="btn btn-outline-danger btn-quitar-telefono"><i class="bi bi-dash-lg"></i></button>
                </div>
            `);
            $(selectorContenedor).append(fila);
        },

        reindexarTelefonos(selectorContenedor) {
            $(selectorContenedor).children('.telefono-row').each(function (indice) {
                $(this).find('input').attr('name', `Telefonos[${indice}].Numero`);
            });
        },

        agregarFilaCorreo(selectorContenedor, correo = '') {
            const indice = $(selectorContenedor).children('.correo-row').length;
            const fila = $(`
                <div class="input-group mb-2 correo-row">
                    <span class="input-group-text"><i class="bi bi-envelope"></i></span>
                    <input type="email" name="Correos[${indice}].CorreoElectronico" class="form-control" placeholder="Ej: correo@ejemplo.com" value="${correo}" />
                    <button type="button" class="btn btn-outline-danger btn-quitar-correo"><i class="bi bi-dash-lg"></i></button>
                </div>
            `);
            $(selectorContenedor).append(fila);
        },

        reindexarCorreos(selectorContenedor) {
            $(selectorContenedor).children('.correo-row').each(function (indice) {
                $(this).find('input').attr('name', `Correos[${indice}].CorreoElectronico`);
            });
        },

        guardarRepresentanteLegal() {
            let form = $('#formCrearRepresentanteLegal');

            if (!form.valid()) {
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {
                    if (respuesta.esCorrecto) {
                        $('#modalCrearRepresentanteLegal').modal('hide');
                        form[0].reset();
                        RepresentanteLegal.tabla.ajax.reload();
                        Swal.fire({ title: 'Correcto', text: respuesta.mensaje, icon: 'success' });
                    } else {
                        Swal.fire({ title: 'Incorrecto', text: respuesta.mensaje, icon: 'error' });
                    }
                },
                error: function (respuesta) {
                    Swal.fire({ title: 'Incorrecto', text: respuesta.responseJSON.description, icon: 'error' });
                }
            });
        },

        editarRepresentanteLegal() {
            let form = $('#formEditarRepresentanteLegal');

            if (!form.valid()) {
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {
                    if (respuesta.esCorrecto) {
                        $('#modalEditarRepresentanteLegal').modal('hide');
                        form[0].reset();
                        RepresentanteLegal.tabla.ajax.reload();
                        Swal.fire({ title: 'Correcto', text: respuesta.mensaje, icon: 'success' });
                    } else {
                        Swal.fire({ title: 'Incorrecto', text: respuesta.mensaje, icon: 'error' });
                    }
                }
            });
        },

        eliminarRepresentanteLegal(id) {
            Swal.fire({
                title: "Estas seguro?",
                text: "No podras revertir esta operacion!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Si, eliminar",
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: `/RepresentanteLegal/DeleteRepresentanteLegal?id=${id}`,
                        type: 'DELETE',
                        success: function (respuesta) {
                            if (respuesta.esCorrecto) {
                                RepresentanteLegal.tabla.ajax.reload();
                                Swal.fire({ title: 'Correcto', text: respuesta.mensaje || 'Representante Legal eliminado correctamente', icon: 'success' });
                            } else {
                                Swal.fire({ title: 'Incorrecto', text: respuesta.mensaje || 'No se pudo eliminar el Representante Legal', icon: 'error' });
                            }
                        },
                        error: function () {
                            Swal.fire({ title: 'Error', text: 'Ocurrió un error al intentar eliminar el Representante Legal', icon: 'error' });
                        }
                    });
                }
            });
        },

        cargarRepresentanteLegal(id) {
            $.get(`/RepresentanteLegal/GetRepresentanteLegalById?id=${id}`, function (resultado) {
                if (resultado.esCorrecto) {
                    let data = resultado.dato;

                    $('#Id').val(data.id);
                    $('#Identificacion').val(data.identificacion);
                    $('#Nombre').val(data.nombre);
                    $('#Edad').val(data.edad);
                    $('#Apellido1').val(data.apellido1);
                    $('#Apellido2').val(data.apellido2);
                    $('#Sexo').val(data.sexo);

                    $('#telefonosEditar').empty();
                    (data.telefonos || []).forEach(t => RepresentanteLegal.agregarFilaTelefono('#telefonosEditar', t.numero));

                    $('#correosEditar').empty();
                    (data.correos || []).forEach(c => RepresentanteLegal.agregarFilaCorreo('#correosEditar', c.correoElectronico));

                    $('#modalEditarRepresentanteLegal').modal('show');
                }
            });
        },

    };
    $(document).ready(() => RepresentanteLegal.init());

})(); //Encapsulamos el código para evitar conflictos con otras partes del proyecto
