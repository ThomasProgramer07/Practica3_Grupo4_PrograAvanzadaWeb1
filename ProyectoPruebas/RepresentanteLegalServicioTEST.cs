using System;
using System.Linq.Expressions;
using AutoMapper;
using Moq;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.BLL.Services.RepresentanteLegal;
using WebAvanzadaIICuatrimestre.DAL.Entidades;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Generico;

namespace ProyectoPruebas;

public class RepresentanteLegalServicioTEST
{
    [Fact]
    public async Task CreateRepresentanteLegal_ShouldReturnError_WhenRepresentanteLegalIsNull()
    {
        var repo = new Mock<IRepositorioGenerico<RepresentanteLegal>>();
        var mapper = new Mock<IMapper>();
        var servicio = new RepresentanteLegalServicio(mapper.Object, repo.Object);

        var result = await servicio.CreateRepresentanteLegal(null);

        Assert.False(result.esCorrecto);
        Assert.Equal(400, result.codigo);
        Assert.Equal("RepresentanteLegal inválido", result.mensaje);
        repo.Verify(r => r.AgregarAsync(It.IsAny<RepresentanteLegal>()), Times.Never);
    }

    [Fact]
    public async Task CreateRepresentanteLegal_ShouldReturnSuccess_WhenRepresentanteLegalIsValid()
    {
        var repo = new Mock<IRepositorioGenerico<RepresentanteLegal>>();
        var mapper = new Mock<IMapper>();
        var servicio = new RepresentanteLegalServicio(mapper.Object, repo.Object);

        var dto = new RepresentanteLegalDto { Nombre = "Juan", Apellido1 = "Perez", Apellido2 = "Lopez", Edad = 25 };
        var entity = new RepresentanteLegal { Nombre = "Juan", Edad = 25 };

        mapper.Setup(m => m.Map<RepresentanteLegal>(It.IsAny<RepresentanteLegalDto>())).Returns(entity);
        repo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

        var result = await servicio.CreateRepresentanteLegal(dto);

        Assert.True(result.esCorrecto);
        Assert.Equal(200, result.codigo);
        repo.Verify(r => r.AgregarAsync(It.IsAny<RepresentanteLegal>()), Times.Once);
        repo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task GetRepresentanteLegalById_ShouldReturnError_WhenNotFound()
    {
        var repo = new Mock<IRepositorioGenerico<RepresentanteLegal>>();
        var mapper = new Mock<IMapper>();
        var servicio = new RepresentanteLegalServicio(mapper.Object, repo.Object);

        repo.Setup(r => r.ObtenerPorIdAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<Expression<Func<RepresentanteLegal, object>>[]>() ))
            .ReturnsAsync((RepresentanteLegal)null);

        var result = await servicio.GetRepresentanteLegalById(999);

        Assert.False(result.esCorrecto);
        Assert.Equal(404, result.codigo);
        Assert.Equal("Dueño no encontrado", result.mensaje);
    }

    [Fact]
    public async Task UpdateRepresentanteLegal_ShouldReturnSuccess_WhenRepresentanteLegalIsValid()
    {
        var repo = new Mock<IRepositorioGenerico<RepresentanteLegal>>();
        var mapper = new Mock<IMapper>();
        var servicio = new RepresentanteLegalServicio(mapper.Object, repo.Object);

        var dto = new RepresentanteLegalDto { Id = 1, Nombre = "Juan", Apellido1 = "Perez", Apellido2 = "Lopez", Edad = 25 };
        var entity = new RepresentanteLegal { Id = 1, Nombre = "Juan", Edad = 25 };

        mapper.Setup(m => m.Map<RepresentanteLegal>(It.IsAny<RepresentanteLegalDto>())).Returns(entity);
        repo.Setup(r => r.BuscarAsync(It.IsAny<Expression<Func<RepresentanteLegal, bool>>>(), It.IsAny<bool>(), It.IsAny<Expression<Func<RepresentanteLegal, object>>[]>() ))
            .ReturnsAsync(new RepresentanteLegal { Id = 1 });
        repo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

        var result = await servicio.UpdateRepresentanteLegal(dto);

        Assert.True(result.esCorrecto);
        Assert.Equal(200, result.codigo);
        repo.Verify(r => r.ActualizarAsync(It.IsAny<RepresentanteLegal>()), Times.Once);
    }
}

