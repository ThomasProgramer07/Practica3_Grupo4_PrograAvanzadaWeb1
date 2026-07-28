(() => {
    const Votacion = {
        init() {
            this.registrarEventos();
        },

        registrarEventos() {
            $('#formVotacion').on('submit', (e) => {
                e.preventDefault();
                Votacion.emitirVoto();
            });
        },

        emitirVoto() {
            let form = $('#formVotacion');

            if (form.valid && !form.valid()) {
                return;
            }

            const partidoSeleccionado = $('input[name="FkpartidoPolitico"]:checked').val();
            const cedula = $('#identificacion').val().trim();

            if (!partidoSeleccionado) {
                Swal.fire({
                    title: 'Atención',
                    text: 'Por favor seleccione un partido político.',
                    icon: 'warning'
                });
                return;
            }

            $.ajax({
                url: '/Votacion/Votar',
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {
                    if (respuesta.esCorrecto) {
                        form[0].reset();
                        Swal.fire({
                            title: '¡Voto Registrado!',
                            text: respuesta.mensaje || 'Su voto ha sido emitido con éxito.',
                            icon: 'success'
                        });
                    } else {
                        Swal.fire({
                            title: 'No se pudo emitir el voto',
                            text: respuesta.mensaje,
                            icon: 'error'
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        title: 'Error de conexión',
                        text: 'No se pudo comunicar con el servidor.',
                        icon: 'error'
                    });
                }
            });
        }
    };

    $(document).ready(() => Votacion.init());
})();