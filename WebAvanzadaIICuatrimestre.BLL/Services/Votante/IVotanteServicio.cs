using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAvanzadaIICuatrimestre.BLL.Dtos;

namespace WebAvanzadaIICuatrimestre.BLL.Services.Votante
{
    public interface IVotanteServicio
    {
        Task<Respuesta<List<VotanteDto>>> GetVotantes();
        Task<Respuesta<VotanteDto?>> GetVotanteById(int id);
        Task<Respuesta<VotanteDto>> CreateVotante(VotanteDto votante);
        Task<Respuesta<VotanteDto>> UpdateVotante(VotanteDto votante);
        Task<Respuesta<VotanteDto>> DeleteVotante(int id);
    }
}
