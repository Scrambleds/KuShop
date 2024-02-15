using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KuShop.Models;

public partial class Product
{
    [Key]
    [Required(ErrorMessage = "ต้องระบุรหัสสินค้า")]
    [Display(Name = "รหัสสินค้า")]
    public string PdId { get; set; } = null!;

    [Required(ErrorMessage = "ต้องระบุชื่อสินค้า")]
    [Display(Name = "ชื่อสินค้า")]
    public string PdName { get; set; } = null!;

    [Display(Name = "รหัสสินค้า")]
    public byte? PdtId { get; set; }

    [Display(Name = "รหัสสินค้า")]
    public byte? BrandId { get; set; }

    
    [Display(Name = "รหัสสินค้า")]
    public double? PdPrice { get; set; }

    
    [Display(Name = "รหัสสินค้า")]
    public double? PdCost { get; set; }

    
    [Display(Name = "รหัสสินค้า")]
    public double? PdStk { get; set; }

    
    [Display(Name = "รหัสสินค้า")]
    public DateOnly? PdLastBuy { get; set; }

    
    [Display(Name = "รหัสสินค้า")]
    public DateOnly? PdLastSale { get; set; }
}
