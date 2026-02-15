using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ARP.ONE.Domain.DataTransfertObject;

public partial class GenerationdbContext : DbContext
{
    public GenerationdbContext(DbContextOptions<GenerationdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerList> CustomerLists { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<FilmActor> FilmActors { get; set; }

    public virtual DbSet<FilmCategory> FilmCategories { get; set; }

    public virtual DbSet<FilmList> FilmLists { get; set; }

    public virtual DbSet<FilmText> FilmTexts { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Rental> Rentals { get; set; }

    public virtual DbSet<SalesByFilmCategory> SalesByFilmCategories { get; set; }

    public virtual DbSet<SalesByStore> SalesByStores { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<StaffList> StaffLists { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.Property(e => e.ActorId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.Property(e => e.AddressId).ValueGeneratedNever();
            entity.Property(e => e.Address2).HasDefaultValueSql("NULL");
            entity.Property(e => e.PostalCode).HasDefaultValueSql("NULL");

            entity.HasOne(d => d.City).WithMany(p => p.Addresses).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryId).ValueGeneratedNever();
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.Property(e => e.CityId).ValueGeneratedNever();

            entity.HasOne(d => d.Country).WithMany(p => p.Cities).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.CountryId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.CustomerId).ValueGeneratedNever();
            entity.Property(e => e.Active).HasDefaultValue("Y");
            entity.Property(e => e.Email).HasDefaultValueSql("NULL");

            entity.HasOne(d => d.Address).WithMany(p => p.Customers).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Store).WithMany(p => p.Customers).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<CustomerList>(entity =>
        {
            entity.ToView("customer_list");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.Property(e => e.FilmId).ValueGeneratedNever();
            entity.Property(e => e.Description).HasDefaultValueSql("NULL");
            entity.Property(e => e.Length).HasDefaultValueSql("NULL");
            entity.Property(e => e.OriginalLanguageId).HasDefaultValueSql("NULL");
            entity.Property(e => e.Rating).HasDefaultValue("G");
            entity.Property(e => e.ReleaseYear).HasDefaultValueSql("NULL");
            entity.Property(e => e.RentalDuration).HasDefaultValue((short)3);
            entity.Property(e => e.RentalRate).HasDefaultValueSql("4.99");
            entity.Property(e => e.ReplacementCost).HasDefaultValueSql("19.99");
            entity.Property(e => e.SpecialFeatures).HasDefaultValueSql("NULL");

            entity.HasOne(d => d.Language).WithMany(p => p.FilmLanguages).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<FilmActor>(entity =>
        {
            entity.HasOne(d => d.Actor).WithMany(p => p.FilmActors).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Film).WithMany(p => p.FilmActors).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<FilmCategory>(entity =>
        {
            entity.HasOne(d => d.Category).WithMany(p => p.FilmCategories).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Film).WithMany(p => p.FilmCategories).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<FilmList>(entity =>
        {
            entity.ToView("film_list");
        });

        modelBuilder.Entity<FilmText>(entity =>
        {
            entity.Property(e => e.FilmId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.Property(e => e.InventoryId).ValueGeneratedNever();

            entity.HasOne(d => d.Film).WithMany(p => p.Inventories).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Store).WithMany(p => p.Inventories).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.Property(e => e.LanguageId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.Property(e => e.PaymentId).ValueGeneratedNever();
            entity.Property(e => e.RentalId).HasDefaultValueSql("NULL");

            entity.HasOne(d => d.Customer).WithMany(p => p.Payments).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Rental).WithMany(p => p.Payments).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Staff).WithMany(p => p.Payments).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.Property(e => e.RentalId).ValueGeneratedNever();
            entity.Property(e => e.ReturnDate).HasDefaultValueSql("NULL");

            entity.HasOne(d => d.Customer).WithMany(p => p.Rentals).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Inventory).WithMany(p => p.Rentals).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Staff).WithMany(p => p.Rentals).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SalesByFilmCategory>(entity =>
        {
            entity.ToView("sales_by_film_category");
        });

        modelBuilder.Entity<SalesByStore>(entity =>
        {
            entity.ToView("sales_by_store");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.Property(e => e.StaffId).ValueGeneratedNever();
            entity.Property(e => e.Active).HasDefaultValue((short)1);
            entity.Property(e => e.Email).HasDefaultValueSql("NULL");
            entity.Property(e => e.Password).HasDefaultValueSql("NULL");
            entity.Property(e => e.Picture).HasDefaultValueSql("NULL");

            entity.HasOne(d => d.Address).WithMany(p => p.Staff).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Store).WithMany(p => p.Staff).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<StaffList>(entity =>
        {
            entity.ToView("staff_list");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.Property(e => e.StoreId).ValueGeneratedNever();

            entity.HasOne(d => d.Address).WithMany(p => p.Stores).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ManagerStaff).WithMany(p => p.Stores).OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
