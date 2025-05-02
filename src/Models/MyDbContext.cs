using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

namespace api_transacciones.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AutorizacionesCapitulosPago> AutorizacionesCapitulosPagos { get; set; }

    public virtual DbSet<Capitulo> Capitulos { get; set; }

    public virtual DbSet<CapitulosScan> CapitulosScans { get; set; }

    public virtual DbSet<Colaboladore> Colaboladores { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<ComprobantesDeCompra> ComprobantesDeCompras { get; set; }

    public virtual DbSet<Donacione> Donaciones { get; set; }

    public virtual DbSet<EstadosPedido> EstadosPedidos { get; set; }

    public virtual DbSet<Favorito> Favoritos { get; set; }

    public virtual DbSet<Guardado> Guardados { get; set; }

    public virtual DbSet<Historiale> Historiales { get; set; }

    public virtual DbSet<HistorialesObra> HistorialesObras { get; set; }

    public virtual DbSet<Idioma> Idiomas { get; set; }

    public virtual DbSet<Obra> Obras { get; set; }

    public virtual DbSet<ObrasArtista> ObrasArtistas { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<PagesScan> PagesScans { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Preferencia> Preferencias { get; set; }

    public virtual DbSet<Retencione> Retenciones { get; set; }

    public virtual DbSet<Solicitude> Solicitudes { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Tipospedido> Tipospedidos { get; set; }

    public virtual DbSet<Vista> Vistas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
    // coneccion a la bd 
    Env.Load();
    optionsBuilder.UseNpgsql($"{Environment.GetEnvironmentVariable("DATABASE_CONNECTION")}");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("postgres_fdw")
            .HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<AutorizacionesCapitulosPago>(entity =>
        {
            entity.HasKey(e => new { e.IdObra, e.Numero, e.IdUser }).HasName("autorizaciones_capitulos_pagos_pk");

            entity.ToTable("autorizaciones_capitulos_pagos", "transacciones");

            entity.HasIndex(e => e.FechaDeCompra, "autorizaciones_capitulos_pagos_fecha_de_compra_idx");

            entity.HasIndex(e => e.IdUser, "autorizaciones_capitulos_pagos_id_user_idx");

            entity.HasIndex(e => new { e.Numero, e.IdObra }, "autorizaciones_capitulos_pagos_numero_idx");

            entity.Property(e => e.IdObra).HasColumnName("id_obra");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.FechaDeCompra).HasColumnName("fecha_de_compra");

            entity.HasOne(d => d.Capitulo).WithMany(p => p.AutorizacionesCapitulosPagos)
                .HasForeignKey(d => new { d.IdObra, d.Numero })
                .HasConstraintName("autorizaciones_capitulos_pagos_capitulos_fk");
        });

        modelBuilder.Entity<Capitulo>(entity =>
        {
            entity.HasKey(e => new { e.IdObra, e.Numero }).HasName("pk_capitulo");

            entity.ToTable("capitulos");

            entity.Property(e => e.IdObra).HasColumnName("id_obra");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdIdioma)
                .HasColumnType("character varying")
                .HasColumnName("id_idioma");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.IdIdiomaNavigation).WithMany(p => p.Capitulos)
                .HasForeignKey(d => d.IdIdioma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("capitulos_idiomas_fk");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.Capitulos)
                .HasForeignKey(d => d.IdObra)
                .HasConstraintName("id_obra");
        });

        modelBuilder.Entity<CapitulosScan>(entity =>
        {
            entity.HasKey(e => new { e.IdObra, e.Numero, e.IdScan }).HasName("capitulos_scans_pk");

            entity.ToTable("capitulos_scans");

            entity.Property(e => e.IdObra).HasColumnName("id_obra");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.IdScan).HasColumnName("id_scan");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdIdioma)
                .HasColumnType("character varying")
                .HasColumnName("id_idioma");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.IdIdiomaNavigation).WithMany(p => p.CapitulosScans)
                .HasForeignKey(d => d.IdIdioma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("capitulos_scans_idiomas_fk");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.CapitulosScans)
                .HasForeignKey(d => d.IdObra)
                .HasConstraintName("capitulos_scans_obras_fk");
        });

        modelBuilder.Entity<Colaboladore>(entity =>
        {
            entity.HasKey(e => new { e.IdObra, e.IdArts }).HasName("obras_artistas_pk");

            entity.ToTable("colaboladores");

            entity.HasIndex(e => e.IdArts, "obras_artistas_id_arts_idx");

            entity.HasIndex(e => e.IdObra, "obras_artistas_id_obra_idx");

            entity.Property(e => e.IdObra).HasColumnName("id_obra");
            entity.Property(e => e.IdArts).HasColumnName("id_arts");
            entity.Property(e => e.Push)
                .HasDefaultValue(false)
                .HasColumnName("push");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.Colaboladores)
                .HasForeignKey(d => d.IdObra)
                .HasConstraintName("obras_artistas_obras_fk");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comentarios_pk");

            entity.ToTable("comentarios");

            entity.HasIndex(e => e.IdUser, "comentarios_id_user_idx");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Capitulo).HasColumnName("capitulo");
            entity.Property(e => e.Comentario1).HasColumnName("comentario");
            entity.Property(e => e.Fecha)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha");
            entity.Property(e => e.IdObra).HasColumnName("id_obra");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.CapituloNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => new { d.IdObra, d.Capitulo })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comentarios_capitulos_fk");
        });

        modelBuilder.Entity<ComprobantesDeCompra>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("comprobantes_de_compra", "transacciones");
        });

        modelBuilder.Entity<Donacione>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("donaciones", "transacciones");

            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.IdDonador).HasColumnName("id_donador");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.Idioma)
                .HasColumnType("character varying")
                .HasColumnName("idioma");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany()
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("donaciones_pedidos_fk");

            entity.HasOne(d => d.IdiomaNavigation).WithMany()
                .HasForeignKey(d => d.Idioma)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("donaciones_idiomas_fk");
        });

        modelBuilder.Entity<EstadosPedido>(entity =>
        {
            entity.HasKey(e => e.Estado).HasName("estados_pedidos_pk");

            entity.ToTable("estados_pedidos", "transacciones");

            entity.Property(e => e.Estado)
                .HasColumnType("character varying")
                .HasColumnName("estado");
        });

        modelBuilder.Entity<Favorito>(entity =>
        {
            entity.HasKey(e => new { e.IdUser, e.IdObra }).HasName("favoritos_pk");

            entity.ToTable("favoritos");

            entity.HasIndex(e => e.IdObra, "favoritos_id_obra_idx");

            entity.HasIndex(e => e.IdUser, "favoritos_id_user_idx");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IdObra).HasColumnName("id_obra");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.IdObra)
                .HasConstraintName("favoritos_obras_fk");
        });

        modelBuilder.Entity<Guardado>(entity =>
        {
            entity.HasKey(e => new { e.IdObra, e.IdUser }).HasName("guardados_pk");

            entity.ToTable("guardados");

            entity.HasIndex(e => e.IdObra, "guardados_id_obra_idx");

            entity.HasIndex(e => e.IdUser, "guardados_id_user_idx");

            entity.Property(e => e.IdObra).HasColumnName("id_obra");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.Guardados)
                .HasForeignKey(d => d.IdObra)
                .HasConstraintName("guardados_obras_fk");
        });

        modelBuilder.Entity<Historiale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("historiales_pk");

            entity.ToTable("historiales");

            entity.HasIndex(e => e.Fecha, "historiales_fecha_idx");

            entity.HasIndex(e => new { e.IdObra, e.Numero }, "historiales_id_obra_idx");

            entity.HasIndex(e => e.IdUser, "historiales_id_user_idx");

            entity.HasIndex(e => new { e.Tiempo, e.Fecha }, "historiales_tiempo_idx");

            entity.HasIndex(e => new { e.IdUser, e.IdObra, e.Fecha, e.Numero, e.IdScan }, "historiales_unique").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdObra).HasColumnName("id_obra");
            entity.Property(e => e.IdScan).HasColumnName("id_scan");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.Tiempo).HasColumnName("tiempo");

            entity.HasOne(d => d.Capitulo).WithMany(p => p.Historiales)
                .HasForeignKey(d => new { d.IdObra, d.Numero })
                .HasConstraintName("historiales_capitulos_fk");

            entity.HasOne(d => d.CapitulosScan).WithMany(p => p.Historiales)
                .HasForeignKey(d => new { d.IdObra, d.Numero, d.IdScan })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("historiales_capitulos_scans_fk");
        });

        modelBuilder.Entity<HistorialesObra>(entity =>
        {
            entity.HasKey(e => new { e.IdObra, e.Fecha }).HasName("historial_retencion_pk");

            entity.ToTable("historiales_obras");

            entity.Property(e => e.IdObra).HasColumnName("id_obra");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha");
            entity.Property(e => e.Retencion).HasColumnName("retencion");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.HistorialesObras)
                .HasForeignKey(d => d.IdObra)
                .HasConstraintName("historial_retencion_obras_fk");
        });

        modelBuilder.Entity<Idioma>(entity =>
        {
            entity.HasKey(e => e.Nombre).HasName("idiomas_pk");

            entity.ToTable("idiomas");

            entity.Property(e => e.Nombre)
                .HasColumnType("character varying")
                .HasColumnName("nombre");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
        });

        modelBuilder.Entity<Obra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("obras_pkey");

            entity.ToTable("obras");

            entity.HasIndex(e => e.Madure, "obras_madure_idx");

            entity.HasIndex(e => e.Oneshot, "obras_oneshot_idx");

            entity.HasIndex(e => e.Titulo, "obras_titulo_idx");

            entity.HasIndex(e => e.Titulo, "obras_unique").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Artista).HasColumnName("artista");
            entity.Property(e => e.Madure)
                .HasDefaultValue(true)
                .HasColumnName("madure");
            entity.Property(e => e.Oneshot)
                .HasDefaultValue(false)
                .HasColumnName("oneshot");
            entity.Property(e => e.Portada)
                .HasColumnType("character varying")
                .HasColumnName("portada");
            entity.Property(e => e.Titulo)
                .HasColumnType("character varying")
                .HasColumnName("titulo");
            entity.Property(e => e.TituloSecundario)
                .HasColumnType("character varying")
                .HasColumnName("titulo_secundario");

            entity.HasMany(d => d.Tags).WithMany(p => p.IdObras)
                .UsingEntity<Dictionary<string, object>>(
                    "ObrasTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("Tag")
                        .HasConstraintName("obras_tags_tags_fk"),
                    l => l.HasOne<Obra>().WithMany()
                        .HasForeignKey("IdObra")
                        .HasConstraintName("obras_tags_obras_fk"),
                    j =>
                    {
                        j.HasKey("IdObra", "Tag").HasName("obras_tags_pk");
                        j.ToTable("obras_tags");
                        j.HasIndex(new[] { "IdObra" }, "obras_tags_id_obra_idx");
                        j.HasIndex(new[] { "Tag" }, "obras_tags_tag_idx");
                        j.IndexerProperty<Guid>("IdObra").HasColumnName("id_obra");
                        j.IndexerProperty<string>("Tag")
                            .HasColumnType("character varying")
                            .HasColumnName("tag");
                    });
        });

        modelBuilder.Entity<ObrasArtista>(entity =>
        {
            entity.HasKey(e => new { e.IdObra, e.IdArtist }).HasName("oapk");

            entity.ToTable("obras_artistas");

            entity.Property(e => e.IdObra).HasColumnName("id_obra");
            entity.Property(e => e.IdArtist).HasColumnName("id_artist");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Notificado)
                .HasDefaultValue(false)
                .HasColumnName("notificado");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.ObrasArtista)
                .HasForeignKey(d => d.IdObra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("obras_artistas_obras_fk");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => new { e.Orden, e.IdObra, e.NumeroCap }).HasName("pages_pk");

            entity.ToTable("pages");

            entity.Property(e => e.Orden).HasColumnName("orden");
            entity.Property(e => e.IdObra).HasColumnName("id_obra");
            entity.Property(e => e.NumeroCap).HasColumnName("numero_cap");
            entity.Property(e => e.Imagen)
                .HasColumnType("character varying")
                .HasColumnName("imagen");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.Pages)
                .HasForeignKey(d => d.IdObra)
                .HasConstraintName("pages_obras_fk");

            entity.HasOne(d => d.Capitulo).WithMany(p => p.Pages)
                .HasForeignKey(d => new { d.IdObra, d.NumeroCap })
                .HasConstraintName("pages_capitulos_fk");
        });

        modelBuilder.Entity<PagesScan>(entity =>
        {
            entity.HasKey(e => new { e.IdObra, e.Numero, e.IdScan, e.Orden }).HasName("pages_scans_pk");

            entity.ToTable("pages_scans");

            entity.HasIndex(e => new { e.IdObra, e.Numero }, "pages_scans_id_obra_idx");

            entity.HasIndex(e => e.IdScan, "pages_scans_id_scan_idx");

            entity.Property(e => e.IdObra).HasColumnName("id_obra");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.IdScan).HasColumnName("id_scan");
            entity.Property(e => e.Orden).HasColumnName("orden");
            entity.Property(e => e.Imagen)
                .HasColumnType("character varying")
                .HasColumnName("imagen");

            entity.HasOne(d => d.CapitulosScan).WithMany(p => p.PagesScans)
                .HasForeignKey(d => new { d.IdObra, d.Numero, d.IdScan })
                .HasConstraintName("pages_scans_capitulos_scans_fk");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pedidos_pk");

            entity.ToTable("pedidos", "transacciones");

            entity.HasIndex(e => e.IdArtista, "pedidos_id_artista_idx");

            entity.HasIndex(e => e.IdIdiomaTraducir, "pedidos_id_idioma_traducir_idx");

            entity.HasIndex(e => new { e.IdObra, e.NumeroCap }, "pedidos_id_obra_idx");

            entity.HasIndex(e => e.Tipo, "pedidos_tipo_idx");

            entity.HasIndex(e => new { e.IdObra, e.NumeroCap, e.IdIdiomaTraducir }, "pedidos_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'libre'::character varying")
                .HasColumnType("character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdArtista).HasColumnName("id_artista");
            entity.Property(e => e.IdIdiomaTraducir)
                .HasColumnType("character varying")
                .HasColumnName("id_idioma_traducir");
            entity.Property(e => e.IdObra).HasColumnName("id_obra");
            entity.Property(e => e.NumeroCap).HasColumnName("numero_cap");
            entity.Property(e => e.Panel)
                .HasColumnType("json")
                .HasColumnName("panel");
            entity.Property(e => e.Texto)
                .HasColumnType("json")
                .HasColumnName("texto");
            entity.Property(e => e.Tipo)
                .HasColumnType("character varying")
                .HasColumnName("tipo");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.Estado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pedidos_estados_pedidos_fk");

            entity.HasOne(d => d.IdIdiomaTraducirNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdIdiomaTraducir)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pedidos_idiomas_fk");

            entity.HasOne(d => d.TipoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.Tipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pedidos_tipospedidos_fk");

            entity.HasOne(d => d.Capitulo).WithMany(p => p.Pedidos)
                .HasForeignKey(d => new { d.IdObra, d.NumeroCap })
                .HasConstraintName("pedidos_capitulos_fk");
        });

        modelBuilder.Entity<Preferencia>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("preferencias_pk");

            entity.ToTable("preferencias");

            entity.HasIndex(e => e.IdUser, "preferencias_id_user_idx");

            entity.HasIndex(e => e.Tag1, "preferencias_tag_name_idx");

            entity.HasIndex(e => new { e.IdUser, e.Tag1, e.Tag2, e.Tag3, e.Tag4, e.Tag5, e.Tag6 }, "preferencias_unique").IsUnique();

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnName("id_user");
            entity.Property(e => e.Tag1)
                .HasColumnType("character varying")
                .HasColumnName("tag1");
            entity.Property(e => e.Tag2)
                .HasColumnType("character varying")
                .HasColumnName("tag2");
            entity.Property(e => e.Tag3)
                .HasColumnType("character varying")
                .HasColumnName("tag3");
            entity.Property(e => e.Tag4)
                .HasColumnType("character varying")
                .HasColumnName("tag4");
            entity.Property(e => e.Tag5)
                .HasColumnType("character varying")
                .HasColumnName("tag5");
            entity.Property(e => e.Tag6)
                .HasColumnType("character varying")
                .HasColumnName("tag6");
            entity.Property(e => e.V1).HasColumnName("v1");
            entity.Property(e => e.V2).HasColumnName("v2");
            entity.Property(e => e.V3).HasColumnName("v3");
            entity.Property(e => e.V4).HasColumnName("v4");
            entity.Property(e => e.V5).HasColumnName("v5");
            entity.Property(e => e.V6).HasColumnName("v6");

            entity.HasOne(d => d.Tag1Navigation).WithMany(p => p.PreferenciaTag1Navigations)
                .HasForeignKey(d => d.Tag1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("preferencias_tags_fk");

            entity.HasOne(d => d.Tag2Navigation).WithMany(p => p.PreferenciaTag2Navigations)
                .HasForeignKey(d => d.Tag2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("preferencias_tags_fk_1");

            entity.HasOne(d => d.Tag3Navigation).WithMany(p => p.PreferenciaTag3Navigations)
                .HasForeignKey(d => d.Tag3)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("preferencias_tags_fk_2");

            entity.HasOne(d => d.Tag4Navigation).WithMany(p => p.PreferenciaTag4Navigations)
                .HasForeignKey(d => d.Tag4)
                .HasConstraintName("preferencias_tags_fk_3");

            entity.HasOne(d => d.Tag5Navigation).WithMany(p => p.PreferenciaTag5Navigations)
                .HasForeignKey(d => d.Tag5)
                .HasConstraintName("preferencias_tags_fk_4");

            entity.HasOne(d => d.Tag6Navigation).WithMany(p => p.PreferenciaTag6Navigations)
                .HasForeignKey(d => d.Tag6)
                .HasConstraintName("preferencias_tags_fk_5");
        });

        modelBuilder.Entity<Retencione>(entity =>
        {
            entity.HasKey(e => new { e.ObraId, e.FechaMensual }).HasName("retenciones_pk");

            entity.ToTable("retenciones");

            entity.HasIndex(e => e.FechaMensual, "retenciones_fecha_mensual_idx");

            entity.HasIndex(e => e.ObraId, "retenciones_obra_id_idx");

            entity.HasIndex(e => e.Promedio, "retenciones_promedio_idx");

            entity.Property(e => e.ObraId).HasColumnName("obra_id");
            entity.Property(e => e.FechaMensual).HasColumnName("fecha_mensual");
            entity.Property(e => e.Promedio).HasColumnName("promedio");

            entity.HasOne(d => d.Obra).WithMany(p => p.Retenciones)
                .HasForeignKey(d => d.ObraId)
                .HasConstraintName("retenciones_obras_fk");
        });

        modelBuilder.Entity<Solicitude>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("solicitudes", "transacciones");

            entity.HasIndex(e => e.IdPedido, "solicitudes_id_pedido_idx");

            entity.HasIndex(e => e.Price, "solicitudes_price_idx");

            entity.Property(e => e.Aceptado)
                .HasDefaultValue(false)
                .HasColumnName("aceptado");
            entity.Property(e => e.Descripcion)
                .HasDefaultValueSql("'\"\"'::text")
                .HasColumnName("descripcion");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.IdTraductor).HasColumnName("id_traductor");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany()
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("solicitudes_pedidos_fk");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Nombre).HasName("tags_pk");

            entity.ToTable("tags");

            entity.Property(e => e.Nombre)
                .HasColumnType("character varying")
                .HasColumnName("nombre");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Madure).HasColumnName("madure");
            entity.Property(e => e.NameJapanese)
                .HasColumnType("character varying")
                .HasColumnName("name_japanese");
        });

        modelBuilder.Entity<Tipospedido>(entity =>
        {
            entity.HasKey(e => e.Tipo).HasName("tipospedidos_pk");

            entity.ToTable("tipospedidos", "transacciones");

            entity.Property(e => e.Tipo)
                .HasColumnType("character varying")
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Vista>(entity =>
        {
            entity.HasKey(e => e.IdObra).HasName("vistas_pk");

            entity.ToTable("vistas");

            entity.HasIndex(e => e.Comentarios, "vistas_comentarios_idx");

            entity.HasIndex(e => e.Favoritos, "vistas_favoritos_idx");

            entity.HasIndex(e => e.Guardados, "vistas_guardados_idx");

            entity.HasIndex(e => e.Visualizacion, "vistas_visualizacion_idx");

            entity.Property(e => e.IdObra)
                .ValueGeneratedNever()
                .HasColumnName("id_obra");
            entity.Property(e => e.Comentarios)
                .HasDefaultValue(0)
                .HasColumnName("comentarios");
            entity.Property(e => e.Favoritos)
                .HasDefaultValue(0)
                .HasColumnName("favoritos");
            entity.Property(e => e.Guardados)
                .HasDefaultValue(0)
                .HasColumnName("guardados");
            entity.Property(e => e.Retencion).HasColumnName("retencion");
            entity.Property(e => e.Visualizacion)
                .HasDefaultValue(0)
                .HasColumnName("visualizacion");

            entity.HasOne(d => d.IdObraNavigation).WithOne(p => p.Vista)
                .HasForeignKey<Vista>(d => d.IdObra)
                .HasConstraintName("vistas_obras_fk");
        });
        modelBuilder.HasSequence("id_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
