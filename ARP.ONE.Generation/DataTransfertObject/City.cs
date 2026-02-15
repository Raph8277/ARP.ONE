using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

[Table("city")]
[Index("CountryId", Name = "idx_fk_country_id")]
public partial class City
{
    [Key]
    [Column("city_id", TypeName = "INT")]
    public int CityId { get; set; }

    [Required]
    [Column("city", TypeName = "VARCHAR(50)")]
    public string City1 { get; set; }

    [Column("country_id", TypeName = "SMALLINT")]
    public short CountryId { get; set; }

    [Column("last_update", TypeName = "TIMESTAMP")]
    public DateTime LastUpdate { get; set; }

    [InverseProperty("City")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [ForeignKey("CountryId")]
    [InverseProperty("Cities")]
    public virtual Country Country { get; set; }
}
