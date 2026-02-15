using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

[Table("payment")]
[Index("CustomerId", Name = "idx_fk_customer_id")]
[Index("StaffId", Name = "idx_fk_staff_id")]
public partial class Payment
{
    [Key]
    [Column("payment_id", TypeName = "INT")]
    public int PaymentId { get; set; }

    [Column("customer_id", TypeName = "INT")]
    public int CustomerId { get; set; }

    [Column("staff_id", TypeName = "SMALLINT")]
    public short StaffId { get; set; }

    [Column("rental_id", TypeName = "INT")]
    public int? RentalId { get; set; }

    [Column("amount", TypeName = "DECIMAL(5,2)")]
    public decimal Amount { get; set; }

    [Column("payment_date", TypeName = "TIMESTAMP")]
    public DateTime PaymentDate { get; set; }

    [Column("last_update", TypeName = "TIMESTAMP")]
    public DateTime LastUpdate { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Payments")]
    public virtual Customer Customer { get; set; }

    [ForeignKey("RentalId")]
    [InverseProperty("Payments")]
    public virtual Rental Rental { get; set; }

    [ForeignKey("StaffId")]
    [InverseProperty("Payments")]
    public virtual Staff Staff { get; set; }
}
