using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

[Table("store")]
[Index("AddressId", Name = "idx_fk_store_address")]
[Index("ManagerStaffId", Name = "idx_store_fk_manager_staff_id")]
public partial class Store
{
    [Key]
    [Column("store_id", TypeName = "INT")]
    public int StoreId { get; set; }

    [Column("manager_staff_id", TypeName = "SMALLINT")]
    public short ManagerStaffId { get; set; }

    [Column("address_id", TypeName = "INT")]
    public int AddressId { get; set; }

    [Column("last_update", TypeName = "TIMESTAMP")]
    public DateTime LastUpdate { get; set; }

    [ForeignKey("AddressId")]
    [InverseProperty("Stores")]
    public virtual Address Address { get; set; }

    [InverseProperty("Store")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [InverseProperty("Store")]
    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    [ForeignKey("ManagerStaffId")]
    [InverseProperty("Stores")]
    public virtual Staff ManagerStaff { get; set; }

    [InverseProperty("Store")]
    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
