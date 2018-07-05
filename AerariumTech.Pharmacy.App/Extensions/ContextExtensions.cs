using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.Data;
using AerariumTech.Pharmacy.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AerariumTech.Pharmacy.App.Extensions
{
    public static class ContextExtensions
    {
        public static bool CategoryExists(this PharmacyContext context, long id) =>
            context.Categories.Any(e => e.Id == id);

        public static bool CustomerExists(this PharmacyContext context, long id) =>
            context.Users.Any(c => c.Id == id);

        public static bool EmployeeExists(this UserManager<User> userManager, long id)
        {
            return Task.Run(() =>
            {
                var employee = userManager.FindByIdAsync(id).Await();
                var roles = userManager.GetRolesAsync(employee).Await();

                return roles.Any();
            }).Await();
        }

        public static bool ProductExists(this PharmacyContext context, long id) =>
            context.Products.Any(p => p.Id == id);

        public static bool StockExists(this PharmacyContext context, long id) =>
            context.Stocks.Any(s => s.Id == id);

        public static bool SupplierExists(this PharmacyContext context, long id) =>
            context.Suppliers.Any(e => e.Id == id);

        public static async Task<int> GetAmountInStockAsync(this DbSet<Batch> batches, long id)
        {
            return (await batches
                .Include(b => b.Stocks)
                .Where(b => b.ProductId == id
                            && b.DateOfExpiration >=
                            DateTime.UtcNow) // can only sell the product until its expiration day
                .ToListAsync()).Sum(b => // ef core couldn't solve this properly, so we're doing it in memory
                b.Stocks.Where(s => s.MovementType == MovementType.In).Sum(s => s.Quantity) -
                b.Stocks.Where(s => s.MovementType == MovementType.Out).Sum(s => s.Quantity));
        }

        public static void InjectData(this PharmacyContext context)
        {
            var categories = new List<Category>
            {
                new Category {Name = "Alergia"},
                new Category {Name = "Antibióticos"},
                new Category {Name = "Inflamação"},
                new Category {Name = "Gripe & Resfriado"},
                new Category {Name = "Vitaminas & Suplementos"},
                new Category {Name = "Dor & Febre"},
                new Category {Name = "Aparelho Respiratório"},
                new Category {Name = "Emagrecimento"},
                new Category {Name = "Pressão Alta"},
                new Category {Name = "Alimentos & Bebidas"},
                new Category {Name = "Dentadura"},
                new Category {Name = "Lentes & Acessórios"},
                new Category {Name = "Suplementos Alimentares"},
                new Category {Name = "Ortopedia"},
                new Category {Name = "Nutricosméticos"},
                new Category {Name = "Meias de Compressão"},
                new Category {Name = "Preservativos & Lubrificantes"},
                new Category {Name = "Produtos Naturais"},
                new Category {Name = "Cabelos"},
                new Category {Name = "Colônias & Perfume"},
                new Category {Name = "Corpo & Rosto"},
                new Category {Name = "Mãos & Pés"},
                new Category {Name = "Maquiagem"},
                new Category {Name = "Máscaras"},
                new Category {Name = "Proteção Solar"},
                new Category {Name = "Tinturas"},
                new Category {Name = "Unhas"}
            };

            // Inject categories
            if (!context.Categories.Any())
            {
                categories.ForEach(c =>
                    {
                        var category = context.Categories.FirstOrDefault(cn => cn.Name == c.Name);

                        if (category == null)
                        {
                            context.Categories.Add(c);
                        }
                    }
                );
            }

            // Inject Brazilian states
            var states = new List<StateProvince>
            {
                new StateProvince {Name = "Acre", Abbreviation = "AC"},
                new StateProvince {Name = "Alagoas", Abbreviation = "AL"},
                new StateProvince {Name = "Amapá", Abbreviation = "AP"},
                new StateProvince {Name = "Amazonas", Abbreviation = "AM"},
                new StateProvince {Name = "Bahia", Abbreviation = "BA"},
                new StateProvince {Name = "Ceará", Abbreviation = "CE"},
                new StateProvince {Name = "Distrito Federal", Abbreviation = "DF"},
                new StateProvince {Name = "Espírito Santo", Abbreviation = "ES"},
                new StateProvince {Name = "Goiás", Abbreviation = "GO"},
                new StateProvince {Name = "Maranhão", Abbreviation = "MA"},
                new StateProvince {Name = "Mato Grosso", Abbreviation = "MT"},
                new StateProvince {Name = "Mato Grosso do Sul", Abbreviation = "MS"},
                new StateProvince {Name = "Minas Gerais", Abbreviation = "MG"},
                new StateProvince {Name = "Pará", Abbreviation = "PA"},
                new StateProvince {Name = "Paraíba", Abbreviation = "PB"},
                new StateProvince {Name = "Paraná", Abbreviation = "PR"},
                new StateProvince {Name = "Pernambuco", Abbreviation = "PE"},
                new StateProvince {Name = "Piauí", Abbreviation = "PI"},
                new StateProvince {Name = "Rio de Janeiro", Abbreviation = "RJ"},
                new StateProvince {Name = "Rio Grande do Norte", Abbreviation = "RN"},
                new StateProvince {Name = "Rio Grande do Sul", Abbreviation = "RS"},
                new StateProvince {Name = "Rondônia", Abbreviation = "RO"},
                new StateProvince {Name = "Roraima", Abbreviation = "RR"},
                new StateProvince {Name = "Santa Catarina", Abbreviation = "SC"},
                new StateProvince {Name = "São Paulo", Abbreviation = "SP"},
                new StateProvince {Name = "Sergipe", Abbreviation = "SE"},
                new StateProvince {Name = "Tocantins", Abbreviation = "TO"}
            };

            if (!context.StateProvinces.Any())
            {
                states.ForEach(s =>
                {
                    var state = context.StateProvinces.FirstOrDefault(sp =>
                        sp.Name == s.Name || sp.Abbreviation == s.Abbreviation);

                    if (state == null)
                    {
                        context.StateProvinces.Add(s);
                    }
                });
            }

            if (!context.ShippingRates.Any())
            {
                states.Select(sp => new ShippingRate
                {
                    StateProvince = sp,
                    Price = sp?.Name?.Length % 2 == 1 ? 19.99m : 24.99m,
                }).ToList().ForEach(sr => context.ShippingRates.Add(sr));
            }

            var suppliers = new List<Supplier>
            {
                new Supplier
                {
                    Name = "Ultrafarma",
                    Address = "Rua Uau",
                    PostCode = "12837-123",
                    Email = "contact@ultrafarma.com",
                    Phone = "(11) 3918-3423",
                    Cnpj = ""
                },
                new Supplier
                {
                    Name = "Medley",
                    Address = "Av. das Nações Unidas, 14401",
                    PostCode = "04794-000",
                    Email = "contact@medley.com",
                    Phone = "0800-703 0014",
                    Cnpj = ""
                }
            };

            if (!context.Suppliers.Any())
            {
                suppliers.ForEach(s => context.Suppliers.Add(s));
            }

            var products = new List<Product>
            {
                new Product
                {
                    Name = "Aspirina",
                    Description =
                        @"A Aspirina é indicada para o alívio sintomático de dores de intensidade leve a moderada,
como dor de cabeça, dor de dente, dor de garganta, dor menstrual, dor muscular, dor nas articulações,
dor nas costas, dor da artrite; o alívio sintomático da dor e da febre nos resfriados ou gripes.",
                    Price = 16.39m,
                    PriceWithDiscount = 12.71m,
                    SerialCode = Guid.NewGuid().ToString(),
                    Supplier = suppliers.FirstOrDefault(),
                    PathToPicture = "\\images\\936f93a0037c43119885e2554ac6f729.jpg",
                    ProductCategories = new List<ProductCategory>
                    {
                        new ProductCategory
                        {
                            Category = categories.FirstOrDefault(c =>
                                c.Name.Equals("Dor & Febre", StringComparison.OrdinalIgnoreCase))
                        },
                        new ProductCategory
                        {
                            Category = categories.FirstOrDefault(c => c.Name.Equals("Gripe & Resfriado", StringComparison.OrdinalIgnoreCase))
                        }
                    }
                },
                new Product
                {
                    Name = "Biotônico Fontoura 400ml",
                    Description =
                        @"O Biotônico Fontoura é um medicamento de uso oral composto por substâncias que fornecem ferro e fósforo,
os quais são importantes para auxiliar no tratamento da anemia ferropriva. O ferro é um elemento
constituinte das hemoglobinas que são responsáveis pelo transporte de oxigênio nos seres vivos.
O fósforo é um mineral necessário para ativar a ação de vitaminas essências, tais como algumas do complexo B.",
                    Price = 32.4m,
                    PriceWithDiscount = 27.1m,
                    SerialCode = Guid.NewGuid().ToString(),
                    Supplier = suppliers.FirstOrDefault(),
                    PathToPicture = "\\images\\468248b1ebc3474e804015d4c6f7ebef.jpg",
                    ProductCategories = new List<ProductCategory>
                    {
                        new ProductCategory
                        {
                            Category = categories.FirstOrDefault(c => c.Name.Equals("Vitaminas & Suplementos", StringComparison.OrdinalIgnoreCase))
                        },
                        new ProductCategory
                        {
                            Category = categories.FirstOrDefault(c => c.Name.Equals("Suplementos Alimentares", StringComparison.OrdinalIgnoreCase))
                        }
                    }
                },
                new Product
                {
                    Name = "Creme Pentear Pantene 240g",
                    Description =
                        @"Creme para Pentear Liso Extremo Pantene possui fórmula sem sal com Pró-Vitaminas e micro selantes.
Protege os cabelos 95% contra danos, repara os danos extremos instantaneamente.",
                    Price = 15.9m,
                    PriceWithDiscount = 12.72m,
                    SerialCode = Guid.NewGuid().ToString(),
                    Supplier = suppliers.FirstOrDefault(),
                    PathToPicture = "\\images\\10a6243f03e449b2baba62f0a92284ed.jpg",
                    ProductCategories = new List<ProductCategory>
                    {
                        new ProductCategory
                        {
                            Category = categories.FirstOrDefault(c => c.Name.Equals("Cabelos", StringComparison.OrdinalIgnoreCase))
                        }
                    }
                },
                new Product
                {
                    Name = "Bandagem Elastica Dorflex Icy Hot",
                    Description =
                        @"O Dorflex Icy Hot em contato com a pele, provoca resfriamento do local com posteriormente o aquecimento,
essa ação diminui a dor. Não utilizar um mesmo adesivo por mais de 8 horas.",
                    Price = 26.5m,
                    PriceWithDiscount = 22.25m,
                    SerialCode = Guid.NewGuid().ToString(),
                    Supplier = suppliers.FirstOrDefault(),
                    PathToPicture = "\\images\\a453d28913c848d2a8e7c8fbc3a4f455.jpg",
                    ProductCategories = new List<ProductCategory>
                    {
                        new ProductCategory
                        {
                            Category = categories.FirstOrDefault()
                        }
                    }
                },
                new Product
                {
                    Name = "Eno Sabor Laranja 100g",
                    Description =
                        @"O Sal de Fruta Eno laranja quando dissolvidos em água,
reagem entre si, produzindo um sal de efeito antiácido, capaz de iniciar a redução da acidez do estômago em 6 segundos.",
                    Price = 5.2m,
                    PriceWithDiscount = 2.99m,
                    SerialCode = Guid.NewGuid().ToString(),
                    Supplier = suppliers.FirstOrDefault(),
                    PathToPicture = "\\images\\c7dab05050b64f1aaf010e1e5882406d.jpg",
                    ProductCategories = new List<ProductCategory>
                    {
                    }
                },
                new Product
                {
                    Name = "Hidratante Nivea 400ml",
                    Description =
                        @"O Hidratante Corporal Nivea Soft Milk é enriquecido
com óleo de amêndoas e proporciona hidratação intensiva que ajuda a reparar até a pele mais seca.",
                    Price = 15.9m,
                    PriceWithDiscount = 13.9m,
                    SerialCode = Guid.NewGuid().ToString(),
                    Supplier = suppliers.FirstOrDefault(),
                    PathToPicture = "\\images\\e2fc0cc86d124089a18407e00311159c.jpg",
                    ProductCategories = new List<ProductCategory>
                    {
                    }
                },
                new Product
                {
                    Name = "Loratamed 30mg",
                    Description =
                        @"A redução da acidez do estômago em 6 segundos.",
                    Price = 10.69m,
                    PriceWithDiscount = 9.09m,
                    SerialCode = Guid.NewGuid().ToString(),
                    Supplier = suppliers.FirstOrDefault(),
                    PathToPicture = "\\images\\8b5eae1af23b482f84a765de8657e32a.jpg",
                    ProductCategories = new List<ProductCategory>
                    {
                    }
                },
                new Product
                {
                    Name = "Maalox Plus sabor Cereja",
                    Description =
                        @"O Maalox é uma formulação com propriedades antiácidas e antiflatulentas,
pois contém hidróxido de alumínio, hidróxido de magnésio e simeticona.
O hidróxido de alumínio e o hidróxido de magnésio neutralizam a acidez gástrica e a simeticona,
um polímero de sílica, é importante no tratamento da aerofagia. Promovendo a eliminação dos gases excessivos
acumulados no trato gastrointestinal, que contribuem para o aumento da acidez local.",
                    Price = 19.72m,
                    PriceWithDiscount = 18.93m,
                    SerialCode = Guid.NewGuid().ToString(),
                    Supplier = suppliers.FirstOrDefault(),
                    PathToPicture = "\\images\\f7df753327e8474099cb740257f15b68.jpg",
                    ProductCategories = new List<ProductCategory>
                    {

                    }
                },
                new Product
                {
                    Name = "Malvatricin Spray 50ml",
                    Description =
                        @"O Malvatricin Spray é destinado ao tratamento de dor de garganta, afecções da boca, aftas.",
                    Price = 36.65m,
                    PriceWithDiscount = 33.04m,
                    SerialCode = Guid.NewGuid().ToString(),
                    Supplier = suppliers.FirstOrDefault(),
                    PathToPicture = "\\images\\050567a371f34a9bb7cffb48e397a344.jpg",
                    ProductCategories = new List<ProductCategory>
                    {

                    }
                },
                new Product
                {
                    Name = "Polaramine Gotas 20ml",
                    Description =
                        @"O Polaramine é um anti-histamínico ou antialérgico,
que ajuda a reduzir os sintomas da alergia, prevenindo os efeitos da histamina,
que é uma substância produzida pelo próprio organismo. Sua ação ocorre em 30 minutos e possui duração de até 48 horas.",
                    Price = 35.9m,
                    PriceWithDiscount = 26.84m,
                    SerialCode = Guid.NewGuid().ToString(),
                    Supplier = suppliers.FirstOrDefault(),
                    PathToPicture = "\\images\\3f85e08d189d4d7e90c34dadabcae761.jpg",
                    ProductCategories = new List<ProductCategory>
                    {
                        new ProductCategory
                        {
                            Category = categories.FirstOrDefault(c => c.Name.Equals("Alergia", StringComparison.OrdinalIgnoreCase))
                        }
                    }
                },
                new Product
                {
                    Name = "Redoxon Zinco 10g",
                    Description =
                        @"A vitamina C é uma importante vitamina hidrossolúvel e antioxidante,
participando de diversas reações metabólicas no organismo. Por se armazenar em baixas quantidades no corpo humano,
a vitamina C precisa ser obtida através de fontes externas de forma regular e em quantidade suficiente.",
                    Price = 20.54m,
                    PriceWithDiscount = 16.43m,
                    SerialCode = Guid.NewGuid().ToString(),
                    Supplier = suppliers.FirstOrDefault(),
                    PathToPicture = "\\images\\9883b5e02f884e94a436796c808f1e5e.jpg",
                    ProductCategories = new List<ProductCategory>{}
                },
                new Product
                {
                    Name = "Vitergyl C 1g",
                    Description =
                        @"Vitergyl C combina em sua formulação a vitamina C (ácido ascórbico) e o zinco,
dois micronutrientes essenciais que desempenham importantes papéis em inúmeros processos metabólicos e
atuam de modo complementar para o adequado funcionamento do sistema imunológico. A deficiência de vitamina C e/ou
zinco pode comprometer o bom funcionamento das defesas do organismo contra doenças. A vitamina C atua no sistema imunológico
(sistema de defesa contra infecções), sendo necessária para a formação e funcionamento das células responsáveis pelas
defesas do organismo. É fundamental para a produção de colágeno que promove a cicatrização de feridas e tem importante
função de barreira contra a entrada de agentes infecciosos (vírus, bactérias, etc.) no organismo, pois o colágeno é parte
integrante da pele e mucosas, além de atuar no combate aos radicais livres. O zinco complementa a ação da vitamina C
no sistema imunológico, pois também participa da produção de anticorpos e da formação e funcionamento das células responsáveis
pela defesa do nosso organismo. O zinco combate os radicais livres e atua na cicatrização de feridas, pois ele é necessário
para a formação de colágeno.",
                    Price = 25.53m,
                    PriceWithDiscount = 14.9m,
                    SerialCode = Guid.NewGuid().ToString(),
                    Supplier = suppliers.FirstOrDefault(),
                    PathToPicture = "\\images\\62f3a51d51d94c17b8b119cdab4c52a4.jpg",
                    ProductCategories = new List<ProductCategory>
                    {

                    }
                }
            };

            if (!context.Products.Any())
            {
                products.ForEach(p => context.Products.Add(p));
            }

            var batches = Enumerable.Range(0, new Random().Next(1000)).Select(x => new Batch
            {
                DateOfFabrication = DateTime.UtcNow.Subtract(TimeSpan.FromDays(new Random().Next(300, 400))),
                DateOfExpiration = DateTime.UtcNow.Add(TimeSpan.FromDays(new Random().Next(100, 200))),
                Product = products.ElementAtOrDefault(new Random().Next(products.Count)),
            }).ToList();

            if (!context.Batches.Any())
            {
                batches.ForEach(b => context.Batches.Add(b));
            }

            var stocks = Enumerable.Range(0, batches.Count * 2).Select(x => new Stock
            {
                MovementType = MovementType.In,
                Quantity = new Random().Next(0, 1000),
                Batch = batches.ElementAtOrDefault(new Random().Next(batches.Count))
            }).ToList();

            if (!context.Stocks.Any())
            {
                stocks.ForEach(s => context.Stocks.Add(s));
            }

            var paymentModes = new List<PaymentMode>
            {
                new PaymentMode
                {
                    Description = "Cartão de crédito"
                },
                new PaymentMode
                {
                    Description = "Boleto"
                }
            };

            if (!context.PaymentModes.Any())
            {
                paymentModes.ForEach(pm =>
                {
                    if (context.PaymentModes.FirstOrDefault(p => p.Description.Equals(pm.Description)) == null)
                    {
                        context.PaymentModes.Add(pm);
                    }
                });
            }

            context.SaveChanges();
        }
    }
}