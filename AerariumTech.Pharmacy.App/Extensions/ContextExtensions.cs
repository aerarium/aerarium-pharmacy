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
                    Address = "Rua asda",
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
                    Email = "",
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
                    SerialCode = "18723178",
                    Supplier = suppliers.FirstOrDefault(s =>
                        s.Name.Equals("ultrafarma", StringComparison.OrdinalIgnoreCase)),
                    PathToPicture = "\\images\\936f93a0037c43119885e2554ac6f729.jpg",
                    ProductCategories = new List<ProductCategory>
                    {
                        new ProductCategory
                        {
                            Category = categories.FirstOrDefault(c =>
                                c.Name.Equals("Dor & Febre", StringComparison.OrdinalIgnoreCase))
                        }
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