using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

[Table("language")]
public partial class Language
{
    [Key]
    [Column("language_id", TypeName = "SMALLINT")]
    public short LanguageId { get; set; }

    [Required]
    [Column("name", TypeName = "CHAR(20)")]
    public string Name { get; set; }

    [Column("last_update", TypeName = "TIMESTAMP")]
    public DateTime LastUpdate { get; set; }

    [InverseProperty("Language")]
    public virtual ICollection<Film> FilmLanguages { get; set; } = new List<Film>();

    [InverseProperty("OriginalLanguage")]
    public virtual ICollection<Film> FilmOriginalLanguages { get; set; } = new List<Film>();
}
