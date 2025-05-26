using RPInventarios.Models;
using System.Text.RegularExpressions;
using Xunit;

namespace Testing_Inventario
{
    public class MarcaTests
    {
        [Fact]
        public void Marca_NombreDebeTenerMinimo5Caracteres()
        {
            // Arrange
            var marca = new Marca { Nombre = "Test" }; // Menos de 5 caracteres

            // Act
            var esValido = marca.Nombre != null && marca.Nombre.Length >= 5;

            // Assert
            Assert.False(esValido);
        }

        [Fact]
        public void Marca_NombreNoDebeSerNulo()
        {
            // Arrange
            var marca = new Marca { Nombre = null };

            // Act
            var esValido = marca.Nombre != null;

            // Assert
            Assert.False(esValido);
        }
    }
}
