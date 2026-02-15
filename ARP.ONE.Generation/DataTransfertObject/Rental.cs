using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

[Table("rental")]
[Index("CustomerId", Name = "idx_rental_fk_customer_id")]
[Index("InventoryId", Name = "idx_rental_fk_inventory_id")]
[Index("StaffId", Name = "idx_rental_fk_staff_id")]
[Index("RentalDate", "InventoryId", "CustomerId", Name = "idx_rental_uq", IsUnique = true)]
public partial class Rental
{
    [Key]
    [Column("rental_id", TypeName = "INT")]
    public int RentalId { get; set; }

    [Column("rental_date", TypeName = "TIMESTAMP")]
    public DateTime RentalDate { get; set; }

    [Column("inventory_id", TypeName = "INT")]
    public int InventoryId { get; set; }

    [Column("customer_id", TypeName = "INT")]
    public int CustomerId { get; set; }

    [Column("return_date", TypeName = "TIMESTAMP")]
    public DateTime? ReturnDate { get; set; }

    [Column("staff_id", TypeName = "SMALLINT")]
    public short StaffId { get; set; }

    [Column("last_update", TypeName = "TIMESTAMP")]
    public DateTime LastUpdate { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Rentals")]
    public virtual Customer Customer { get; set; }

    [ForeignKey("InventoryId")]
    [InverseProperty("Rentals")]
    public virtual Inventory Inventory { get; set; }

    [InverseProperty("Rental")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [ForeignKey("StaffId")]
    [InverseProperty("Rentals")]
    public virtual Staff Staff { get; set; }
}
