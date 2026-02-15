using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

[Keyless]
public partial class CustomerList
{
    [Column("ID", TypeName = "INT")]
    public int? Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("address", TypeName = "VARCHAR(50)")]
    public string Address { get; set; }

    [Column("zip_code", TypeName = "VARCHAR(10)")]
    public string ZipCode { get; set; }

    [Column("phone", TypeName = "VARCHAR(20)")]
    public string Phone { get; set; }

    [Column("city", TypeName = "VARCHAR(50)")]
    public string City { get; set; }

    [Column("country", TypeName = "VARCHAR(50)")]
    public string Country { get; set; }

    [Column("notes")]
    public string Notes { get; set; }

    [Column("SID", TypeName = "INT")]
    public int? Sid { get; set; }
}
