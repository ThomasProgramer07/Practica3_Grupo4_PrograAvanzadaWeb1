using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.DAL.Entidades;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Generico;

namespace WebAvanzadaIICuatrimestre.BLL.Services.RepresentanteLegal
{
    public class RepresentanteLegalServicio : IRepresentanteLegalServicio
    {
        private readonly IMapper _mapper;
        private readonly IRepositorioGenerico<DAL.Entidades.RepresentanteLegal> _repositorioGenerico;

        public RepresentanteLegalServicio(IMapper mapper, IRepositorioGenerico<DAL.Entidades.RepresentanteLegal> repo)
        {
            _mapper = mapper;
            _repositorioGenerico = repo;
        }

        public async Task<Respuesta<RepresentanteLegalDto>> CreateRepresentanteLegal(RepresentanteLegalDto representanteLegal)
        {
            var respuesta = new Respuesta<RepresentanteLegalDto>();

            if (representanteLegal == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Representante Legal inválido";
                respuesta.codigo = 400;
                return respuesta;
            }

            if(representanteLegal.Identificacion == " ")
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "El Representante Legal no puede estar vacío";
                respuesta.codigo = 400;
                return respuesta;
            }

            representanteLegal.Telefonos = (representanteLegal.Telefonos ?? new List<TelefonoDto>())
                .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                .ToList();

            var entity = _mapper.Map<DAL.Entidades.RepresentanteLegal>(representanteLegal);
            _repositorioGenerico.AgregarAsync(entity);
            if (!await _repositorioGenerico.SaveChangesAsync())
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo crear el Representante Legal";
                respuesta.codigo = 500;
                return respuesta;
            }

            respuesta.Dato = representanteLegal;
            return respuesta;
        }

        public async Task<Respuesta<RepresentanteLegalDto>> DeleteRepresentanteLegal(int id)
        {
            var respuesta = new Respuesta<RepresentanteLegalDto>();
            _repositorioGenerico.EliminarAsync(id);
            if (!await _repositorioGenerico.SaveChangesAsync())
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo eliminar el Representante Legal";
                respuesta.codigo = 404;
            }
            return respuesta;
        }

        public async Task<Respuesta<RepresentanteLegalDto?>> GetRepresentanteLegalById(int id)
        {
            var respuesta = new Respuesta<RepresentanteLegalDto?>();
            var entity = await _repositorioGenerico.ObtenerPorIdAsync(id, asNoTracking: true,d => d.Telefonos, d => d.Correos);
            if (entity == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Representante Legal no encontrado";
                respuesta.codigo = 404;
                respuesta.Dato = null;
                return respuesta;
            }

            respuesta.Dato = _mapper.Map<RepresentanteLegalDto>(entity);
            return respuesta;
        }

        public async Task<Respuesta<List<RepresentanteLegalDto>>> GetRepresentanteLegals()
        {
            var respuesta = new Respuesta<List<RepresentanteLegalDto>>();
            var list = await _repositorioGenerico.ObtenerTodosAsync(asNoTracking: true,d => d.Telefonos, d => d.Correos);
            respuesta.Dato = _mapper.Map<List<RepresentanteLegalDto>>(list);
            return respuesta;
        }

        public async Task<Respuesta<RepresentanteLegalDto>> UpdateRepresentanteLegal(RepresentanteLegalDto representanteLegal)
        {
            var respuesta = new Respuesta<RepresentanteLegalDto>();

            if (representanteLegal == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Representante Legal inválido";
                respuesta.codigo = 400;
                return respuesta;
            }

            representanteLegal.Telefonos = (representanteLegal.Telefonos ?? new List<TelefonoDto>())
                .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                .ToList();

            representanteLegal.Correos = (representanteLegal.Correos ?? new List<CorreoDto>())
    .Where(t => !string.IsNullOrWhiteSpace(t.CorreoElectronico))
    .ToList();

            var entity = _mapper.Map<DAL.Entidades.RepresentanteLegal>(representanteLegal);
            var existing = await _repositorioGenerico.BuscarAsync(d => d.Id == entity.Id, asNoTracking: false, d => d.Telefonos);
            if (existing == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo actualizar el Representante Legal";
                respuesta.codigo = 404;
                return respuesta;
            }

            existing.Nombre = entity.Nombre ?? existing.Nombre;
            existing.Edad = entity.Edad;
            existing.Apellido1 = entity.Apellido1 ?? existing.Apellido1;
            existing.Apellido2 = entity.Apellido2 ?? existing.Apellido2;
            existing.Telefonos = (entity.Telefonos ?? new List<DAL.Entidades.Telefono>())
                .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                .Select(t => new DAL.Entidades.Telefono { Numero = t.Numero, FkrepresentanteLegal = existing.Id })
                .ToList();
            existing.Correos = (entity.Correos ?? new List<DAL.Entidades.Correo>())
    .Where(t => !string.IsNullOrWhiteSpace(t.CorreoElectronico))
    .Select(t => new DAL.Entidades.Correo { CorreoElectronico = t.CorreoElectronico, FkrepresentanteLegal = existing.Id })
    .ToList();

            _repositorioGenerico.ActualizarAsync(existing);
            if (!await _repositorioGenerico.SaveChangesAsync())
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo actualizar el Representante Legal";
                respuesta.codigo = 404;
                return respuesta;
            }

            respuesta.Dato = representanteLegal;
            return respuesta;
        }
    }
}
