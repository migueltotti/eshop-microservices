using Marten.Schema;

namespace Catalog.Api.Infrastructure;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(cancellation))
            return;

        session.Store<Product>(GetPreConfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Product> GetPreConfiguredProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "iPhone 15 Pro",
                Category = ["Eletrônicos", "Smartphones", "Apple"],
                Description = "Smartphone premium com processador A17 Pro, câmera tripla de 48MP e tela Super Retina XDR de 6.1 polegadas",
                ImageFile = "iphone15pro.jpg",
                Price = 7999.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Nike Air Max 270",
                Category = ["Esportes", "Calçados", "Tênis"],
                Description = "Tênis esportivo com tecnologia Air Max, ideal para corrida e uso casual. Conforto e estilo em cada passo",
                ImageFile = "nike_airmax270.jpg",
                Price = 599.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "MacBook Air M2",
                Category = ["Eletrônicos", "Computadores", "Notebooks"],
                Description = "Notebook ultraportátil com chip M2, tela Liquid Retina de 13.6 polegadas, 8GB RAM e 256GB SSD",
                ImageFile = "macbook_air_m2.jpg",
                Price = 12999.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Café Especial Torrado",
                Category = ["Alimentação", "Bebidas", "Café"],
                Description = "Grãos 100% arábica de origem única, torra média, notas de chocolate e caramelo. Pacote 500g",
                ImageFile = "cafe_especial.jpg",
                Price = 45.90m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Camiseta Polo Lacoste",
                Category = ["Roupas", "Masculino", "Camisas"],
                Description = "Camiseta polo clássica em algodão piqué, corte regular, disponível em várias cores",
                ImageFile = "polo_lacoste.jpg",
                Price = 399.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "PlayStation 5",
                Category = ["Eletrônicos", "Games", "Consoles"],
                Description = "Console de videogame de nova geração com SSD ultrarrápido, ray tracing e controle DualSense",
                ImageFile = "playstation5.jpg",
                Price = 4199.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Livro: O Nome do Vento",
                Category = ["Livros", "Fantasia", "Literatura"],
                Description = "Primeiro volume da série Crônica do Matador do Rei, de Patrick Rothfuss. Fantasy épica envolvente",
                ImageFile = "nome_do_vento.jpg",
                Price = 49.90m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Smartwatch Samsung Galaxy Watch 6",
                Category = ["Eletrônicos", "Wearables", "Smartwatch"],
                Description = "Relógio inteligente com monitoramento de saúde, GPS, resistência à água e tela Super AMOLED",
                ImageFile = "galaxy_watch6.jpg",
                Price = 1899.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Panela de Pressão Elétrica",
                Category = ["Casa", "Cozinha", "Eletrodomésticos"],
                Description = "Panela de pressão elétrica 6L com 14 funções pré-programadas, timer digital e sistema de segurança",
                ImageFile = "panela_eletrica.jpg",
                Price = 329.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Perfume Chanel No. 5",
                Category = ["Beleza", "Perfumes", "Feminino"],
                Description = "Perfume feminino icônico, eau de parfum 100ml, fragrância floral aldeídica atemporal",
                ImageFile = "chanel_no5.jpg",
                Price = 899.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Violão Yamaha F310",
                Category = ["Música", "Instrumentos", "Cordas"],
                Description = "Violão acústico clássico com tampo em spruce, laterais e fundo em meranti, ideal para iniciantes",
                ImageFile = "violao_yamaha_f310.jpg",
                Price = 599.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Cadeira Gamer DXRacer",
                Category = ["Móveis", "Escritório", "Cadeiras"],
                Description = "Cadeira gamer ergonômica com apoio lombar, braços ajustáveis e reclinação até 180°",
                ImageFile = "cadeira_dxracer.jpg",
                Price = 1299.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kit Maquiagem MAC",
                Category = ["Beleza", "Maquiagem", "Kits"],
                Description = "Kit completo com base, pó, batom, rímel e sombras em cores neutras. Ideal para o dia a dia",
                ImageFile = "kit_mac.jpg",
                Price = 459.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Bicicleta Mountain Bike Caloi",
                Category = ["Esportes", "Ciclismo", "Bicicletas"],
                Description = "Mountain bike aro 29 com 21 marchas, freios a disco e suspensão dianteira. Ideal para trilhas",
                ImageFile = "bike_caloi_mtb.jpg",
                Price = 1899.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Air Fryer Philips Walita",
                Category = ["Casa", "Cozinha", "Eletrodomésticos"],
                Description = "Fritadeira elétrica sem óleo 4.1L com tecnologia Rapid Air, 7 programas pré-definidos",
                ImageFile = "airfryer_philips.jpg",
                Price = 699.99m
            }
        };
    }
}