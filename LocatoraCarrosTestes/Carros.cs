using LocadoraCarros.Data.Interfaces;
using LocadoraCarros.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace LocatoraCarrosTestes
{
    public class Carros
    {
        [Fact]
        public void Inserir()
        {
            var mock = new Mock<ICarroRepositorio>();
            mock.Setup(e => e.Inserir(It.IsAny<Carro>()));

            Carro carro = new Carro
            {
                Id = 1,
                Marca = "abc",
                Nome = "abc",
                Foto = "abc",
                PrecoDiaria = 50
            };

            mock.Object.Inserir(carro);
            mock.Verify(e => e.Inserir(It.IsAny<Carro>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Atualizar()
        {
            var mock = new Mock<ICarroRepositorio>();
            mock.Setup(e => e.Atualizar(It.IsAny<Carro>()));

            Carro carro = new Carro
            {
                Id = 1,
                Marca = "abc",
                Nome = "abc",
                Foto = "abc",
                PrecoDiaria = 50
            };

            mock.Object.Atualizar(carro);
            mock.Verify(e => e.Atualizar(It.IsAny<Carro>()), Times.AtLeastOnce);
        }


        [Fact]
        public void Excluir()
        {
            var mock = new Mock<ICarroRepositorio>();
            mock.Setup(e => e.Excluir(It.IsAny<int>()));

            mock.Object.Excluir(2);
            mock.Verify(e => e.Excluir(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void PegarPeloId()
        {
            var mock = new Mock<ICarroRepositorio>();
            mock.Setup(e => e.PegarPeloId(It.IsAny<int>()));

            mock.Object.PegarPeloId(1);
            mock.Object.PegarPeloId(2);
            mock.Object.PegarPeloId(3);
            mock.Object.PegarPeloId(4);

            mock.Verify(e => e.PegarPeloId(It.IsAny<int>()), Times.AtLeastOnce);
        }

        [Fact]
        public void PegarTodos()
        {
            var mock = new Mock<ICarroRepositorio>();
            mock.Setup(e => e.PegarTodos());

            var carros = mock.Object.PegarTodos();
            IQueryable<Carro> car = null;

            Assert.Equal<Carro>(car, carros);
        }
    }
}
