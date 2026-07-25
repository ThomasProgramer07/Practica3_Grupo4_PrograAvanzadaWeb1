(() => {

    const Votante = {
        tabla: null,
        init() {
            this.inicializarTabla();
            this.registrarEventos()
        },
        inicializarTabla() {
            this.tabla = $('#tblVotante').DataTable({
                ajax: {
                    url: 'Votante/GetVotantes',
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
                    { data: 'estado' },
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

            $('#tblVotante').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                Votante.cargarVotante(id);
            });
            $('#tblVotante').on('click', '.btn-eliminar', function () {
                const id = $(this).data('id');
                Votante.eliminarVotante(id);
            });

            $('#btnGuardarVotante').on('click', function () {
                Votante.guardarVotante();
            });

            $('#btnEditarVotante').on('click', function () {
                Votante.editarVotante();
            });

            $('#btnAgregarTelefonoCrear').on('click', function () {
                Votante.agregarFilaTelefono('#telefonosCrear');
            });

            $('#btnAgregarTelefonoEditar').on('click', function () {
                Votante.agregarFilaTelefono('#telefonosEditar');
            });

            $('#btnAgregarCorreoCrear').on('click', function () {
                Votante.agregarFilaCorreo('#correosCrear');
            });

            $('#btnAgregarCorreoEditar').on('click', function () {
                Votante.agregarFilaCorreo('#correosEditar');
            });

            $(document).on('click', '.btn-quitar-telefono', function () {
                const contenedor = $(this).closest('.telefono-row').parent();
                $(this).closest('.telefono-row').remove();
                Votante.reindexarTelefonos(contenedor);
            });

            $(document).on('click', '.btn-quitar-correo', function () {
                const contenedor = $(this).closest('.correo-row').parent();
                $(this).closest('.correo-row').remove();
                Votante.reindexarCorreos(contenedor);
            });

            $('#modalCrearVotante').on('hidden.bs.modal', function () {
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

        guardarVotante() {
            let form = $('#formCrearVotante');

            if (!form.valid()) {
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {
                    if (respuesta.esCorrecto) {
                        $('#modalCrearVotante').modal('hide');
                        form[0].reset();
                        Votante.tabla.ajax.reload();
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

        editarVotante() {
            let form = $('#formEditarVotante');

            if (!form.valid()) {
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {
                    if (respuesta.esCorrecto) {
                        $('#modalEditarVotante').modal('hide');
                        form[0].reset();
                        Votante.tabla.ajax.reload();
                        Swal.fire({ title: 'Correcto', text: respuesta.mensaje, icon: 'success' });
                    } else {
                        Swal.fire({ title: 'Incorrecto', text: respuesta.mensaje, icon: 'error' });
                    }
                }
            });
        },

        eliminarVotante(id) {
            Swal.fire({
                title: "Estas seguro?",
                text: "No podras revertir esta operacion!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Si, eliminar",
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: `/Votante/DeleteVotante?id=${id}`,
                        type: 'DELETE',
                        success: function (respuesta) {
                            if (respuesta.esCorrecto) {
                                Votante.tabla.ajax.reload();
                                Swal.fire({ title: 'Correcto', text: respuesta.mensaje || 'Votante eliminado correctamente', icon: 'success' });
                            } else {
                                Swal.fire({ title: 'Incorrecto', text: respuesta.mensaje || 'No se pudo eliminar el Votante', icon: 'error' });
                            }
                        },
                        error: function () {
                            Swal.fire({ title: 'Error', text: 'Ocurrió un error al intentar eliminar el Votante', icon: 'error' });
                        }
                    });
                }
            });
        },

        cargarVotante(id) {
            $.get(`/Votante/GetVotanteById?id=${id}`, function (resultado) {
                if (resultado.esCorrecto) {
                    let data = resultado.dato;

                    $('#Id').val(data.id);
                    $('#Identificacion').val(data.identificacion);
                    $('#Nombre').val(data.nombre);
                    $('#Edad').val(data.edad);
                    $('#Apellido1').val(data.apellido1);
                    $('#Apellido2').val(data.apellido2);
                    $('#Sexo').val(data.sexo);
                    $('#Estado').val(data.estado);

                    $('#telefonosEditar').empty();
                    (data.telefonos || []).forEach(t => Votante.agregarFilaTelefono('#telefonosEditar', t.numero));

                    $('#correosEditar').empty();
                    (data.correos || []).forEach(c => Votante.agregarFilaCorreo('#correosEditar', c.correoElectronico));

                    $('#modalEditarVotante').modal('show');
                }
            });
        },

    };
    $(document).ready(() => Votante.init());

})(); //Encapsulamos el código para evitar conflictos con otras partes del proyecto
