using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

[Table("actor")]
[Index("LastName", Name = "idx_actor_last_name")]
public partial class Actor
{
    [Key]
    [Column("actor_id", TypeName = "numeric")]
    public int ActorId { get; set; }

    [Required]
    [Column("first_name", TypeName = "VARCHAR(45)")]
    public string FirstName { get; set; }

    [Required]
    [Column("last_name", TypeName = "VARCHAR(45)")]
    public string LastName { get; set; }

    [Column("last_update", TypeName = "TIMESTAMP")]
    public DateTime LastUpdate { get; set; }

    [InverseProperty("Actor")]
    public virtual ICollection<FilmActor> FilmActors { get; set; } = new List<FilmActor>();
}
