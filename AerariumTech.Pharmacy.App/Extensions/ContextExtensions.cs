using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.Data;
using AerariumTech.Pharmacy.Domain;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace AerariumTech.Pharmacy.App.Extensions
{
    public static class ContextExtensions
    {
        public static bool CategoryExists(this PharmacyContext context, long id) =>
            context.Categories.Any(e => e.Id == id);

        public static bool ProductExists(this PharmacyContext context, long id) =>
            context.Products.Any(p => p.Id == id);

        public static bool SupplierExists(this PharmacyContext context, long id) =>
            context.Suppliers.Any(e => e.Id == id);

        public static void InjectDataAsync(this PharmacyContext context)
        {
            // Inject categories
            new List<Category>
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
            }.ForEach(c =>
                {
                    var category = context.Categories.FirstOrDefault(cn => cn.Name == c.Name);

                    if (category == null)
                    {
                        context.Categories.Add(c);
                    }
                }
            );

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

            states.ForEach(s =>
            {
                var state = context.StateProvinces.FirstOrDefault(sp =>
                    sp.Name == s.Name || sp.Abbreviation == s.Abbreviation);

                if (state == null)
                {
                    context.StateProvinces.Add(s);
                }
            });

            states.Select(sp => new ShippingRate
            {
                StateProvince = sp,
                Price = sp?.Name?.Length % 2 == 1 ? 9.99m : 14.99m,
            }).ToList().ForEach(sr => context.ShippingRates.Add(sr));

            context.SaveChanges();
        }
    }
}