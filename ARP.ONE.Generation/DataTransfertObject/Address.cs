using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

[Table("address")]
[Index("CityId", Name = "idx_fk_city_id")]
public partial class Address
{
    [Key]
    [Column("address_id", TypeName = "INT")]
    public int AddressId { get; set; }

    [Required]
    [Column("address", TypeName = "VARCHAR(50)")]
    public string Address1 { get; set; }

    [Column("address2", TypeName = "VARCHAR(50)")]
    public string Address2 { get; set; }

    [Required]
    [Column("district", TypeName = "VARCHAR(20)")]
    public string District { get; set; }

    [Column("city_id", TypeName = "INT")]
    public int CityId { get; set; }

    [Column("postal_code", TypeName = "VARCHAR(10)")]
    public string PostalCode { get; set; }

    [Required]
    [Column("phone", TypeName = "VARCHAR(20)")]
    public string Phone { get; set; }

    [Column("last_update", TypeName = "TIMESTAMP")]
    public DateTime LastUpdate { get; set; }

    [ForeignKey("CityId")]
    [InverseProperty("Addresses")]
    public virtual City City { get; set; }

    [InverseProperty("Address")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [InverseProperty("Address")]
    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();

    [InverseProperty("Address")]
    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
