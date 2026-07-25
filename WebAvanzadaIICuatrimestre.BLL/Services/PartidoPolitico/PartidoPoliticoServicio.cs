using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Generico;

namespace WebAvanzadaIICuatrimestre.BLL.Services.PartidoPolitico
{
    public class PartidoPoliticoServicio : IPartidoPoliticoServicio
    {
        private readonly IRepositorioGenerico<DAL.Entidades.PartidoPolitico> _partidoPoliticoRepositorio;
        private readonly IMapper _mapper;

        public PartidoPoliticoServicio(IRepositorioGenerico<DAL.Entidades.PartidoPolitico> partidoPoliticoRepositorio, IMapper mapper)
        {
            _partidoPoliticoRepositorio = partidoPoliticoRepositorio;
            _mapper = mapper;
        }

        public async Task<Respuesta<PartidoPoliticoDto>> CreatePartidoPolitico(PartidoPoliticoDto partidoPolitico)
        {
            var respuesta = new Respuesta<PartidoPoliticoDto>();

            if (string.IsNullOrWhiteSpace(partidoPolitico.Nombre))
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Solo atendemos partidoPoliticos con nombre";
                respuesta.codigo = 1;
                return respuesta;
            }

            if (partidoPolitico.Estado == 0)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No aceptamos partidoPoliticos inactivos";
                respuesta.codigo = 2;
                return respuesta;
            }

            if (string.IsNullOrWhiteSpace(partidoPolitico.Sigla))
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No aceptamos partidoPoliticos sin sigla";
                respuesta.codigo = 3;
                return respuesta;
            }

            var entity = _mapper.Map<DAL.Entidades.PartidoPolitico>(partidoPolitico);
            _partidoPoliticoRepositorio.AgregarAsync(entity);
            if (!await _partidoPoliticoRepositorio.SaveChangesAsync())
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo crear el partidoPolitico";
                respuesta.codigo = 4;
                return respuesta;
            }

            respuesta.Dato = partidoPolitico;
            return respuesta;
        }

        public async Task<Respuesta<PartidoPoliticoDto>> DeletePartidoPolitico(int id)
        {
            var respuesta = new Respuesta<PartidoPoliticoDto>();
            _partidoPoliticoRepositorio.EliminarAsync(id);
            if (!await _partidoPoliticoRepositorio.SaveChangesAsync())
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo eliminar el partidoPolitico";
                respuesta.codigo = 404;
            }
            return respuesta;
        }

        public async Task<Respuesta<PartidoPoliticoDto?>> GetPartidoPoliticoById(int id)
        {
            var respuesta = new Respuesta<PartidoPoliticoDto?>();
            var entity = await _partidoPoliticoRepositorio.ObtenerPorIdAsync(id, asNoTracking: true, p => p.FkrepresentanteLegalNavigation!);
            if (entity == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "PartidoPolitico no encontrado";
                respuesta.codigo = 404;
                respuesta.Dato = null;
                return respuesta;
            }
            respuesta.Dato = _mapper.Map<PartidoPoliticoDto>(entity);
            return respuesta;
        }

        public async Task<Respuesta<List<PartidoPoliticoDto>>> GetPartidoPoliticos()
        {
            var respuesta = new Respuesta<List<PartidoPoliticoDto>>();
            var list = await _partidoPoliticoRepositorio.ObtenerTodosAsync(asNoTracking: true, p => p.FkrepresentanteLegalNavigation!);
            respuesta.Dato = _mapper.Map<List<PartidoPoliticoDto>>(list);
            return respuesta;
        }

        public async Task<Respuesta<PartidoPoliticoDto>> UpdatePartidoPolitico(PartidoPoliticoDto partidoPolitico)
        {
            var respuesta = new Respuesta<PartidoPoliticoDto>();

            if (partidoPolitico == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "PartidoPolitico inválido";
                respuesta.codigo = 400;
                return respuesta;
            }

            if (string.IsNullOrWhiteSpace(partidoPolitico.Nombre))
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Solo atendemos partidoPoliticos con nombre";
                respuesta.codigo = 1;
                return respuesta;
            }

            if (partidoPolitico.Estado == 0)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No aceptamos partidoPoliticos inactivos";
                respuesta.codigo = 2;
                return respuesta;
            }

            if (string.IsNullOrWhiteSpace(partidoPolitico.Sigla))
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No aceptamos partidoPoliticos sin sigla";
                respuesta.codigo = 3;
                return respuesta;
            }

            var entity = _mapper.Map<DAL.Entidades.PartidoPolitico>(partidoPolitico);
            _partidoPoliticoRepositorio.ActualizarAsync(entity);
            if (!await _partidoPoliticoRepositorio.SaveChangesAsync())
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo actualizar el partidoPolitico";
                respuesta.codigo = 404;
                return respuesta;
            }

            respuesta.Dato = partidoPolitico;
            return respuesta;
        }
    }
}