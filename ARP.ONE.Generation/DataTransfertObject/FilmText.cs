using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

[Table("film_text")]
public partial class FilmText
{
    [Key]
    [Column("film_id", TypeName = "SMALLINT")]
    public short FilmId { get; set; }

    [Required]
    [Column("title", TypeName = "VARCHAR(255)")]
    public string Title { get; set; }

    [Column("description", TypeName = "BLOB SUB_TYPE TEXT")]
    public string Description { get; set; }
}
