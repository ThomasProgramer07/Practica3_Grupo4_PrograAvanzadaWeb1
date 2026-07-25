using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAvanzadaIICuatrimestre.BLL.Dtos;

namespace WebAvanzadaIICuatrimestre.BLL.Services.PartidoPolitico
{
    public interface IPartidoPoliticoServicio
    {
        Task<Respuesta<List<PartidoPoliticoDto>>> GetPartidoPoliticos();
        Task<Respuesta<PartidoPoliticoDto?>> GetPartidoPoliticoById(int id);
        Task<Respuesta<PartidoPoliticoDto>> CreatePartidoPolitico(PartidoPoliticoDto partidoPolitico);
        Task<Respuesta<PartidoPoliticoDto>> UpdatePartidoPolitico(PartidoPoliticoDto partidoPolitico);
        Task<Respuesta<PartidoPoliticoDto>> DeletePartidoPolitico(int id);
    }
}
