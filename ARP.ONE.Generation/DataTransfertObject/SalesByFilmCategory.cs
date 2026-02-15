using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

[Keyless]
public partial class SalesByFilmCategory
{
    [Column("category", TypeName = "VARCHAR(25)")]
    public string Category { get; set; }

    [Column("total_sales")]
    public double? TotalSales { get; set; }
}
