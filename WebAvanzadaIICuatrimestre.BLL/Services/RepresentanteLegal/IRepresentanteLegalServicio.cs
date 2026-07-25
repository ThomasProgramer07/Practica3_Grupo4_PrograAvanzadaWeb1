using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAvanzadaIICuatrimestre.BLL.Dtos;

namespace WebAvanzadaIICuatrimestre.BLL.Services.RepresentanteLegal
{
    public interface IRepresentanteLegalServicio
    {
        Task<Respuesta<List<RepresentanteLegalDto>>> GetRepresentanteLegals();
        Task<Respuesta<RepresentanteLegalDto?>> GetRepresentanteLegalById(int id);
        Task<Respuesta<RepresentanteLegalDto>> CreateRepresentanteLegal(RepresentanteLegalDto representanteLegal);
        Task<Respuesta<RepresentanteLegalDto>> UpdateRepresentanteLegal(RepresentanteLegalDto representanteLegal);
        Task<Respuesta<RepresentanteLegalDto>> DeleteRepresentanteLegal(int id);
    }
}
