using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

[Keyless]
public partial class FilmList
{
    [Column("FID", TypeName = "INT")]
    public int? Fid { get; set; }

    [Column("title", TypeName = "VARCHAR(255)")]
    public string Title { get; set; }

    [Column("description", TypeName = "BLOB SUB_TYPE TEXT")]
    public string Description { get; set; }

    [Column("category", TypeName = "VARCHAR(25)")]
    public string Category { get; set; }

    [Column("price", TypeName = "DECIMAL(4,2)")]
    public decimal? Price { get; set; }

    [Column("length", TypeName = "SMALLINT")]
    public short? Length { get; set; }

    [Column("rating", TypeName = "VARCHAR(10)")]
    public string Rating { get; set; }

    [Column("actors")]
    public string Actors { get; set; }
}
