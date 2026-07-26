using WebAvanzadaIICuatrimestre.BLL.Dtos;

public interface IVotacionServicio
{
    Task<Respuesta<VotoDto>> RegistrarVoto(VotoDto voto);
    Task<Respuesta<List<ResultadoDto>>> GetResultados();
}