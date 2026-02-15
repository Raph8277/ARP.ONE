using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

[Table("film")]
[Index("LanguageId", Name = "idx_fk_language_id")]
[Index("OriginalLanguageId", Name = "idx_fk_original_language_id")]
public partial class Film
{
    [Key]
    [Column("film_id", TypeName = "INT")]
    public int FilmId { get; set; }

    [Required]
    [Column("title", TypeName = "VARCHAR(255)")]
    public string Title { get; set; }

    [Column("description", TypeName = "BLOB SUB_TYPE TEXT")]
    public string Description { get; set; }

    [Column("release_year", TypeName = "VARCHAR(4)")]
    public string ReleaseYear { get; set; }

    [Column("language_id", TypeName = "SMALLINT")]
    public short LanguageId { get; set; }

    [Column("original_language_id", TypeName = "SMALLINT")]
    public short? OriginalLanguageId { get; set; }

    [Column("rental_duration", TypeName = "SMALLINT")]
    public short RentalDuration { get; set; }

    [Column("rental_rate", TypeName = "DECIMAL(4,2)")]
    public decimal RentalRate { get; set; }

    [Column("length", TypeName = "SMALLINT")]
    public short? Length { get; set; }

    [Column("replacement_cost", TypeName = "DECIMAL(5,2)")]
    public decimal ReplacementCost { get; set; }

    [Column("rating", TypeName = "VARCHAR(10)")]
    public string Rating { get; set; }

    [Column("special_features", TypeName = "VARCHAR(100)")]
    public string SpecialFeatures { get; set; }

    [Column("last_update", TypeName = "TIMESTAMP")]
    public DateTime LastUpdate { get; set; }

    [InverseProperty("Film")]
    public virtual ICollection<FilmActor> FilmActors { get; set; } = new List<FilmActor>();

    [InverseProperty("Film")]
    public virtual ICollection<FilmCategory> FilmCategories { get; set; } = new List<FilmCategory>();

    [InverseProperty("Film")]
    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    [ForeignKey("LanguageId")]
    [InverseProperty("FilmLanguages")]
    public virtual Language Language { get; set; }

    [ForeignKey("OriginalLanguageId")]
    [InverseProperty("FilmOriginalLanguages")]
    public virtual Language OriginalLanguage { get; set; }
}
