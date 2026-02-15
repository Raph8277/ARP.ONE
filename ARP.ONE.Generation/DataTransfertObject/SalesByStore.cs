using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

[Keyless]
public partial class SalesByStore
{
    [Column("store_id", TypeName = "INT")]
    public int? StoreId { get; set; }

    [Column("store")]
    public string Store { get; set; }

    [Column("manager")]
    public string Manager { get; set; }

    [Column("total_sales")]
    public double? TotalSales { get; set; }
}
