using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

[Table("country")]
public partial class Country
{
    [Key]
    [Column("country_id", TypeName = "SMALLINT")]
    public short CountryId { get; set; }

    [Required]
    [Column("country", TypeName = "VARCHAR(50)")]
    public string Country1 { get; set; }

    [Column("last_update", TypeName = "TIMESTAMP")]
    public DateTime? LastUpdate { get; set; }

    [InverseProperty("Country")]
    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
