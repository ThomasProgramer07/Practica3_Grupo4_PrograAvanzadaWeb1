(() => {

    const PartidoPolitico = {
        tabla: null,
        init() {
            this.inicializarTabla();
            this.registrarEventos()
        },
        inicializarTabla() {
            this.tabla = $('#tblPartidoPolitico').DataTable({
                ajax: {
                    url: 'PartidoPolitico/GetPartidoPoliticos',
                    type: 'GET',
                    dataSrc: 'dato'
                },
                columns: [
                    { data: 'id' },
                    { data: 'identificacion' },
                    { data: 'nombre' },
                    { data: 'sigla' },
                    {
                        data: 'estado',
                        render: (data, type, row) => {
                            const val = typeof data === 'string' ? parseInt(data, 10) : data;
                            if (type === 'display' || type === 'filter') {
                                if (val === 1) {
                                    return `<span class="badge bg-success">Activo</span>`;
                                }
                                return `<span class="badge bg-secondary">Inactivo</span>`;
                            }
                            return data;
                        }
                    },
                    { data: 'representanteLegal.nombre', defaultContent: '<span class="text-muted">Sin asignar</span>' },
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

            $('#tblPartidoPolitico').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                PartidoPolitico.cargarPartidoPolitico(id);
            });
            $('#tblPartidoPolitico').on('click', '.btn-eliminar', function () {
                const id = $(this).data('id');
                PartidoPolitico.eliminarPartidoPolitico(id);
            });

            $('#btnGuardarPartidoPolitico').on('click', function () {
                PartidoPolitico.guardarPartidoPolitico();
            });

            $('#btnEditarPartidoPolitico').on('click', function () {
                PartidoPolitico.editarPartidoPolitico();
            });

        },
        guardarPartidoPolitico() {
            let form = $('#formCrearPartidoPolitico');

            if (!form.valid()) {
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {
                    if (respuesta.esCorrecto) {
                        $('#modalCrearPartidoPolitico').modal('hide');
                        form[0].reset();
                        PartidoPolitico.tabla.ajax.reload();
                        Swal.fire({ title: 'Correcto', text: respuesta.mensaje, icon: 'success' });
                    } else {
                        Swal.fire({ title: 'Incorrecto', text: respuesta.mensaje, icon: 'error' });
                    }
                }
            });
        },

        eliminarPartidoPolitico(id) {
            Swal.fire({
                title: "Estas seguro?",
                text: "No podras revertir esta operacion!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Si, eliminar",
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: `/PartidoPolitico/DeletePartidoPolitico?id=${id}`,
                        type: 'DELETE',
                        success: function (respuesta) {
                            if (respuesta.esCorrecto) {
                                PartidoPolitico.tabla.ajax.reload();
                                Swal.fire({ title: 'Correcto', text: respuesta.mensaje || 'Partido Político eliminado correctamente', icon: 'success' });
                            } else {
                                Swal.fire({ title: 'Incorrecto', text: respuesta.mensaje || 'No se pudo eliminar el Partido Político', icon: 'error' });
                            }
                        },
                        error: function () {
                            Swal.fire({ title: 'Error', text: 'Ocurrió un error al intentar eliminar el Partido Político', icon: 'error' });
                        }
                    });
                }
            });
        },

        cargarPartidoPolitico(id) {
            $.get(`/PartidoPolitico/GetPartidoPoliticoById?id=${id}`, function (resultado) {
                if (resultado.esCorrecto) {
                    let data = resultado.dato;

                    $('#editar_Id').val(data.id);
                    $('#editar_Identificacion').val(data.identificacion);
                    $('#editar_Nombre').val(data.nombre);
                    $('#editar_Sigla').val(data.sigla);
                    $('#editar_Estado').val(data.estado);
                    $('#editar_FkrepresentanteLegal').val(data.fkrepresentanteLegal);

                    $('#modalEditarPartidoPolitico').modal('show');
                }
            });
        },

        editarPartidoPolitico() {
            let form = $('#formEditarPartidoPolitico');

            if (!form.valid()) {
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {
                    if (respuesta.esCorrecto) {
                        $('#modalEditarPartidoPolitico').modal('hide');
                        form[0].reset();
                        PartidoPolitico.tabla.ajax.reload();
                        Swal.fire({ title: 'Correcto', text: respuesta.mensaje, icon: 'success' });
                    } else {
                        Swal.fire({ title: 'Incorrecto', text: respuesta.mensaje, icon: 'error' });
                    }
                }
            });
        },

    };
    $(document).ready(() => PartidoPolitico.init());

})();
