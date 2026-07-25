using AutoMapper;
using Moq;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.BLL.Services.PartidoPolitico;
using WebAvanzadaIICuatrimestre.DAL.Entidades;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Generico;

namespace ProyectoPruebas;

public class PartidoPoliticoServicioTEST
{
    [Fact]
    public async Task CreatePartidoPolitico_ShouldReturnSuccess_WhenDataIsValid()
    {
        var repo = new Mock<IRepositorioGenerico<PartidoPolitico>>();
        var mapper = new Mock<IMapper>();
        var servicio = new PartidoPoliticoServicio(repo.Object, mapper.Object);

        var dto = new PartidoPoliticoDto
        {
            Identificacion = "PP-001",
            Nombre = "Partido Ejemplo",
            Sigla = "PE",
            Estado = 1
        };

        var entity = new PartidoPolitico
        {
            Identificacion = "PP-001",
            Nombre = "Partido Ejemplo",
            Sigla = "PE",
            Estado = 1
        };

        mapper.Setup(m => m.Map<PartidoPolitico>(It.IsAny<PartidoPoliticoDto>())).Returns(entity);
        repo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);
        repo.Setup(r => r.AgregarAsync(It.IsAny<PartidoPolitico>()));

        var result = await servicio.CreatePartidoPolitico(dto);

        Assert.True(result.esCorrecto);
        Assert.Equal(200, result.codigo);
        repo.Verify(r => r.AgregarAsync(It.IsAny<PartidoPolitico>()), Times.Once);
        repo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
