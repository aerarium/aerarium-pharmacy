﻿using System.ComponentModel.DataAnnotations;

namespace AerariumTech.Pharmacy.App.Models.CategoriesViewModels
{
    public class CreateCategoryViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
