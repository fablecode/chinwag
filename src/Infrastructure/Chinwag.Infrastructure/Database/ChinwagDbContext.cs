using Chinwag.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Attribute = Chinwag.Infrastructure.Models.Attribute;
using Type = Chinwag.Infrastructure.Models.Type;

#nullable disable

namespace Chinwag.Infrastructure.Database;

public partial class ChinwagDbContext : DbContext
{

    public ChinwagDbContext(DbContextOptions<ChinwagDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Archetype> Archetypes { get; set; }

    public virtual DbSet<Attribute> Attributes { get; set; }

    public virtual DbSet<Banlist> Banlists { get; set; }

    public virtual DbSet<BanlistCard> BanlistCards { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Deck> Decks { get; set; }

    public virtual DbSet<DeckCard> DeckCards { get; set; }

    public virtual DbSet<DeckType> DeckTypes { get; set; }

    public virtual DbSet<Format> Formats { get; set; }

    public virtual DbSet<Limit> Limits { get; set; }

    public virtual DbSet<LinkArrow> LinkArrows { get; set; }

    public virtual DbSet<SchemaVersion> SchemaVersions { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Archetype>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Url)
                .HasMaxLength(2083)
                .IsUnicode(false);

            entity.HasMany(d => d.Cards).WithMany(p => p.Archetypes)
                .UsingEntity<Dictionary<string, object>>(
                    "ArchetypeCard",
                    r => r.HasOne<Card>().WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ArchetypeCards_Cards"),
                    l => l.HasOne<Archetype>().WithMany()
                        .HasForeignKey("ArchetypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ArchetypeCards_Archetypes"),
                    j =>
                    {
                        j.HasKey("ArchetypeId", "CardId");
                        j.ToTable("ArchetypeCards");
                    });
        });

        modelBuilder.Entity<Attribute>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Banlist>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

            entity.HasOne(d => d.Format).WithMany(p => p.Banlists)
                .HasForeignKey(d => d.FormatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Banlists_Format");
        });

        modelBuilder.Entity<BanlistCard>(entity =>
        {
            entity.HasKey(e => new { e.BanlistId, e.CardId });

            entity.HasOne(d => d.Banlist).WithMany(p => p.BanlistCards)
                .HasForeignKey(d => d.BanlistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BanlistCards_Banlists");

            entity.HasOne(d => d.Card).WithMany(p => p.BanlistCards)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BanlistCards_Cards");

            entity.HasOne(d => d.Limit).WithMany(p => p.BanlistCards)
                .HasForeignKey(d => d.LimitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BanlistCards_Limits");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Cards_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Deck>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.VideoUrl).HasMaxLength(2083);
        });

        modelBuilder.Entity<DeckCard>(entity =>
        {
            entity.HasKey(e => new { e.DeckTypeId, e.DeckId, e.CardId });

            entity.HasOne(d => d.Card).WithMany(p => p.DeckCards)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeckCards_Cards");

            entity.HasOne(d => d.Deck).WithMany(p => p.DeckCards)
                .HasForeignKey(d => d.DeckId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeckCards_Decks");

            entity.HasOne(d => d.DeckType).WithMany(p => p.DeckCards)
                .HasForeignKey(d => d.DeckTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeckCards_DeckTypes");
        });

        modelBuilder.Entity<DeckType>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Format>(entity =>
        {
            entity.Property(e => e.Acronym).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Limit>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<LinkArrow>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<SchemaVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SchemaVersions_Id");

            entity.Property(e => e.Applied).HasColumnType("datetime");
            entity.Property(e => e.ScriptName).HasMaxLength(255);
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubCategories_Category");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
