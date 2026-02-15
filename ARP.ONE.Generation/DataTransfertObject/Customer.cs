using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

[Table("customer")]
[Index("AddressId", Name = "idx_customer_fk_address_id")]
[Index("StoreId", Name = "idx_customer_fk_store_id")]
[Index("LastName", Name = "idx_customer_last_name")]
public partial class Customer
{
    [Key]
    [Column("customer_id", TypeName = "INT")]
    public int CustomerId { get; set; }

    [Column("store_id", TypeName = "INT")]
    public int StoreId { get; set; }

    [Required]
    [Column("first_name", TypeName = "VARCHAR(45)")]
    public string FirstName { get; set; }

    [Required]
    [Column("last_name", TypeName = "VARCHAR(45)")]
    public string LastName { get; set; }

    [Column("email", TypeName = "VARCHAR(50)")]
    public string Email { get; set; }

    [Column("address_id", TypeName = "INT")]
    public int AddressId { get; set; }

    [Required]
    [Column("active", TypeName = "CHAR(1)")]
    public string Active { get; set; }

    [Column("create_date", TypeName = "TIMESTAMP")]
    public DateTime CreateDate { get; set; }

    [Column("last_update", TypeName = "TIMESTAMP")]
    public DateTime LastUpdate { get; set; }

    [ForeignKey("AddressId")]
    [InverseProperty("Customers")]
    public virtual Address Address { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [InverseProperty("Customer")]
    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    [ForeignKey("StoreId")]
    [InverseProperty("Customers")]
    public virtual Store Store { get; set; }
}
