using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AerariumTech.Pharmacy.Validations;

namespace AerariumTech.Pharmacy.Models.ShoppingCartViewModels
{
    public class ShoppingCartViewModel : List<ShoppingCartViewModel.ShoppingCartItemViewModel>
    {
        public class ShoppingCartItemViewModel
        {
            public long ProductId { get; set; }

            [Display(Name = "Produto")]
            public string ProductName { get; set; }

            public string ProductPicture { get; set; }

            [Display(Name = "Quantidade")]
            public int Quantity { get; set; }

            [Price]
            [Display(Name = "Preço")]
            public decimal Price { get; set; }

            [Price]
            [Display(Name = "Subtotal")]
            public decimal SubTotal => Quantity * Price;
        }

        [Price]
        public decimal Total => this.Sum(e => e.SubTotal);

        public ShoppingCartViewModel()
        {
        }

        public ShoppingCartViewModel(IEnumerable<ShoppingCartItemViewModel> collection) : base(collection)
        {
        }
    }
}