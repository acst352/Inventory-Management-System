using RPInventarios.Models;

namespace RPInventarios.Data
{
    public static class DbInitializer
    {
        public static void Initialize(InventariosContext context)
        {
            // Comprobar si existe alguna marca
            if (context.Marcas.Any())
            {
                return;
            }

            var marcas = new Marca[]
            {
                new Marca{Nombre="Nike"},
                new Marca{Nombre="Huawei"},
                new Marca{Nombre="Ibanez"},
                new Marca{Nombre="Focusrite"},
                new Marca{Nombre="Logitech"},
                new Marca{Nombre="JBL"},
                new Marca{Nombre="Samsung"},
                new Marca{Nombre="HP"},
                new Marca{Nombre="DELL"},
                new Marca{Nombre="Bare Knuckle Pickups"},
                new Marca{Nombre="Gotoh"},
                new Marca{Nombre="AudioTechnica"},
            };
            context.Marcas.AddRange(marcas);
            context.SaveChanges();

            var departamentos = new Departamento[]
            {
                new Departamento{Nombre="Administración General"},
                new Departamento{Nombre="Recursos Humanos"},
                new Departamento{Nombre="Informática"},
                new Departamento{Nombre="Deportes"},
                new Departamento{Nombre="Música"},
            };

            context.Departamentos.AddRange(departamentos);
            context.SaveChanges();

            var productos = new Producto[]
            {
                new Producto
                {
                    Nombre="Nike Jordan Air",
                    Descripcion="Edición especial 1/300",
                    MarcaId = context.Marcas.First(u => u.Nombre == "Nike").Id,
                    Costo=30000M
                },
                new Producto
                {
                    Nombre="Ibanez RGIXL7",
                    Descripcion="Ibanez 27', 7 cuerdas, Dimarzio Fusion Edge Pickups, Black Satin Finish. Woods: Ebony, Natoh, Maple, Bubinga.",
                    MarcaId = context.Marcas.First(u => u.Nombre == "Ibanez").Id,
                    Costo=20000M
                },

                new Producto
                {
                    Nombre="Huawei matebook 14 2020",
                    Descripcion="Huawei Matebook 14 14 inch. Ryzen 5 4600H, 16GB, SSD 512GB, QHD 1440p. Model: klvl wxx9",
                    MarcaId = context.Marcas.First(u => u.Nombre == "Huawei").Id,
                    Costo=27000M
                },
                new Producto
                {
                    Nombre="Logitech Mx Master 3s",
                    Descripcion="Logitech Mx Master 3s **Sensor:** 8,000 DPI optical sensor with tracking on glass. " +
                    "**Connectivity:** Wireless via Bluetooth or **Logi Bolt USB receiver**. " +
                    "**Scrolling:** ",
                    MarcaId = context.Marcas.First(u => u.Nombre == "Logitech").Id,
                    Costo=2000M
                },
            };
            context.Productos.AddRange(productos);
            context.SaveChanges();
        }
    }
}
