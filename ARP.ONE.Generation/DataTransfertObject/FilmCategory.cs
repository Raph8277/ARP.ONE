using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

[PrimaryKey("FilmId", "CategoryId")]
[Table("film_category")]
[Index("CategoryId", Name = "idx_fk_film_category_category")]
[Index("FilmId", Name = "idx_fk_film_category_film")]
public partial class FilmCategory
{
    [Key]
    [Column("film_id", TypeName = "INT")]
    public int FilmId { get; set; }

    [Key]
    [Column("category_id", TypeName = "SMALLINT")]
    public short CategoryId { get; set; }

    [Column("last_update", TypeName = "TIMESTAMP")]
    public DateTime LastUpdate { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("FilmCategories")]
    public virtual Category Category { get; set; }

    [ForeignKey("FilmId")]
    [InverseProperty("FilmCategories")]
    public virtual Film Film { get; set; }
}
