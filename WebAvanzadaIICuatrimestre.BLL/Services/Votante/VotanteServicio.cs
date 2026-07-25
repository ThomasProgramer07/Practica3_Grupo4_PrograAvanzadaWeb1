using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.DAL.Entidades;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Generico;

namespace WebAvanzadaIICuatrimestre.BLL.Services.Votante
{
    public class VotanteServicio : IVotanteServicio
    {
        private readonly IMapper _mapper;
        private readonly IRepositorioGenerico<DAL.Entidades.Votante> _repositorioGenerico;

        public VotanteServicio(IMapper mapper, IRepositorioGenerico<DAL.Entidades.Votante> repo)
        {
            _mapper = mapper;
            _repositorioGenerico = repo;
        }

        public async Task<Respuesta<VotanteDto>> CreateVotante(VotanteDto votante)
        {
            var respuesta = new Respuesta<VotanteDto>();

            if (votante == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Votante inválido";
                respuesta.codigo = 400;
                return respuesta;
            }

            if(votante.Identificacion == " ")
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "El Votante no puede estar vacío";
                respuesta.codigo = 400;
                return respuesta;
            }

            votante.Telefonos = (votante.Telefonos ?? new List<TelefonoDto>())
                .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                .ToList();

            var entity = _mapper.Map<DAL.Entidades.Votante>(votante);
            _repositorioGenerico.AgregarAsync(entity);
            if (!await _repositorioGenerico.SaveChangesAsync())
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo crear el Votante";
                respuesta.codigo = 500;
                return respuesta;
            }

            respuesta.Dato = votante;
            return respuesta;
        }

        public async Task<Respuesta<VotanteDto>> DeleteVotante(int id)
        {
            var respuesta = new Respuesta<VotanteDto>();
            _repositorioGenerico.EliminarAsync(id);
            if (!await _repositorioGenerico.SaveChangesAsync())
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo eliminar el Votante";
                respuesta.codigo = 404;
            }
            return respuesta;
        }

        public async Task<Respuesta<VotanteDto?>> GetVotanteById(int id)
        {
            var respuesta = new Respuesta<VotanteDto?>();
            var entity = await _repositorioGenerico.ObtenerPorIdAsync(id, asNoTracking: true, d => d.Telefonos, d => d.Correos);
            if (entity == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Votante no encontrado";
                respuesta.codigo = 404;
                respuesta.Dato = null;
                return respuesta;
            }

            respuesta.Dato = _mapper.Map<VotanteDto>(entity);
            return respuesta;
        }

        public async Task<Respuesta<List<VotanteDto>>> GetVotantes()
        {
            var respuesta = new Respuesta<List<VotanteDto>>();
            var list = await _repositorioGenerico.ObtenerTodosAsync(asNoTracking: true, d => d.Telefonos, d => d.Correos);
            respuesta.Dato = _mapper.Map<List<VotanteDto>>(list);
            return respuesta;
        }

        public async Task<Respuesta<VotanteDto>> UpdateVotante(VotanteDto votante)
        {
            var respuesta = new Respuesta<VotanteDto>();

            if (votante == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Votante inválido";
                respuesta.codigo = 400;
                return respuesta;
            }

            votante.Telefonos = (votante.Telefonos ?? new List<TelefonoDto>())
                .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                .ToList();

            votante.Correos = (votante.Correos ?? new List<CorreoDto>())
    .Where(t => !string.IsNullOrWhiteSpace(t.CorreoElectronico))
    .ToList();

            var entity = _mapper.Map<DAL.Entidades.Votante>(votante);
            var existing = await _repositorioGenerico.BuscarAsync(d => d.Id == entity.Id, asNoTracking: false, d => d.Telefonos);
            if (existing == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo actualizar el Votante";
                respuesta.codigo = 404;
                return respuesta;
            }

            existing.Nombre = entity.Nombre ?? existing.Nombre;
            existing.Edad = entity.Edad;
            existing.Estado = entity.Estado;
            existing.Sexo = entity.Sexo;
            existing.Apellido1 = entity.Apellido1 ?? existing.Apellido1;
            existing.Apellido2 = entity.Apellido2 ?? existing.Apellido2;
            existing.Telefonos = (entity.Telefonos ?? new List<DAL.Entidades.Telefono>())
                .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                .Select(t => new DAL.Entidades.Telefono { Numero = t.Numero, Fkvotante = existing.Id })
                .ToList();
            existing.Correos = (entity.Correos ?? new List<DAL.Entidades.Correo>())
    .Where(t => !string.IsNullOrWhiteSpace(t.CorreoElectronico))
    .Select(t => new DAL.Entidades.Correo { CorreoElectronico = t.CorreoElectronico, Fkvotante = existing.Id })
    .ToList();

            _repositorioGenerico.ActualizarAsync(existing);
            if (!await _repositorioGenerico.SaveChangesAsync())
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo actualizar el Votante";
                respuesta.codigo = 404;
                return respuesta;
            }

            respuesta.Dato = votante;
            return respuesta;
        }
    }
}
