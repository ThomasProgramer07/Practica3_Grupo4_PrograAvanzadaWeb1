namespace WebAvanzadaIICuatrimestre.BLL.Services.Votacion;

using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Generico;

public class VotacionServicio : IVotacionServicio
{
    private readonly IRepositorioGenerico<DAL.Entidades.Votante> _votanteRepo;
    private readonly IRepositorioGenerico<DAL.Entidades.Voto> _votoRepo;
    private readonly IRepositorioGenerico<DAL.Entidades.PartidoPolitico> _partidoRepo;

    public VotacionServicio(
        IRepositorioGenerico<DAL.Entidades.Votante> votanteRepo,
        IRepositorioGenerico<DAL.Entidades.Voto> votoRepo,
        IRepositorioGenerico<DAL.Entidades.PartidoPolitico> partidoRepo)
    {
        _votanteRepo = votanteRepo;
        _votoRepo = votoRepo;
        _partidoRepo = partidoRepo;
    }

    public async Task<Respuesta<VotoDto>> RegistrarVoto(VotoDto voto)
    {
        var respuesta = new Respuesta<VotoDto>();

        // 1. ¿La cédula está inscrita?
        var votante = await _votanteRepo.BuscarAsync(
            v => v.Identificacion == voto.Identificacion, asNoTracking: true);
        if (votante == null)
        {
            respuesta.esCorrecto = false;
            respuesta.mensaje = "El votante no está inscrito.";
            respuesta.codigo = 404;
            return respuesta;
        }

        // 2. ¿Ya votó?
        var yaVoto = await _votoRepo.BuscarAsync(
            x => x.Fkvotante == votante.Id, asNoTracking: true);
        if (yaVoto != null)
        {
            respuesta.esCorrecto = false;
            respuesta.mensaje = "Este votante ya emitió su voto.";
            respuesta.codigo = 400;
            return respuesta;
        }

        // 3. Registrar el voto
        _votoRepo.AgregarAsync(new DAL.Entidades.Voto
        {
            Fkvotante = votante.Id,
            FkpartidoPolitico = voto.FkpartidoPolitico,
            Fecha = DateTime.Now
        });

        if (!await _votoRepo.SaveChangesAsync())
        {
            respuesta.esCorrecto = false;
            respuesta.mensaje = "No se pudo registrar el voto.";
            respuesta.codigo = 500;
            return respuesta;
        }

        respuesta.esCorrecto = true;
        respuesta.mensaje = "Voto registrado correctamente.";
        respuesta.Dato = voto;
        return respuesta;
    }

    public async Task<Respuesta<List<ResultadoDto>>> GetResultados()
    {
        var respuesta = new Respuesta<List<ResultadoDto>>();
        var partidos = await _partidoRepo.ObtenerTodosAsync();
        var votos = await _votoRepo.ObtenerTodosAsync();

        respuesta.Dato = partidos.Select(p => new ResultadoDto
        {
            PartidoPoliticoId = p.Id,
            Nombre = p.Nombre,
            Sigla = p.Sigla,
            CantidadVotos = votos.Count(v => v.FkpartidoPolitico == p.Id)
        }).ToList();

        return respuesta;
    }
}