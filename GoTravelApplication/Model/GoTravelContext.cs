using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GoTravelApplication.Model
{
    public partial class GoTravelContext : DbContext
    {
        public GoTravelContext()
        {
        }

        public GoTravelContext(DbContextOptions<GoTravelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminResponse> AdminResponses { get; set; }
        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<CartBooking> CartBookings { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerBooking> CustomerBookings { get; set; }
        public virtual DbSet<CustomerReview> CustomerReviews { get; set; }
        public virtual DbSet<ModRequest> ModRequests { get; set; }
        public virtual DbSet<Moderator> Moderators { get; set; }
        public virtual DbSet<ModeratorReview> ModeratorReviews { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Receptionist> Receptionists { get; set; }
        public virtual DbSet<ReceptionistChange> ReceptionistChanges { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AdminResponse>(entity =>
            {
                entity.HasKey(e => e.ResponseId)
                    .HasName("PK__AdminRes__1AAA646CAA62D651");

                entity.ToTable("AdminResponse");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ResponseTime).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.AdminResponses)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ResponseAdminId");

                entity.HasOne(d => d.Moderator)
                    .WithMany(p => p.AdminResponses)
                    .HasForeignKey(d => d.ModeratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ResponseModeratorId");
            });

            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("PK__Administ__719FE48807CDDFD7");

                entity.HasIndex(e => e.UserName, "UQ__Administ__C9F28456C89BB1B0")
                    .IsUnique();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(24)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasIndex(e => e.Title, "UQ__Bookings__2CB664DC9F9121B6")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(240)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(240)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CartBooking>(entity =>
            {
                entity.HasKey(e => e.CartId)
                    .HasName("PK__CartBook__51BCD7B722CA2B6A");

                entity.ToTable("CartBooking");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.CartBookings)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartBookingId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CartBookings)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartCustomerId");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.UserName, "UQ__Customer__C9F2845652FE3EDE")
                    .IsUnique();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(24)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerBooking>(entity =>
            {
                entity.Property(e => e.PurchaseDate).HasColumnType("date");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.CustomerBookings)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustBookBookingId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerBookings)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustBookCustomerId");
            });

            modelBuilder.Entity<CustomerReview>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(240)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerReviews)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustReviewCustomerId");
            });

            modelBuilder.Entity<ModRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__ModReque__33A8517A8654E1D1");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RequestTime).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Moderator)
                    .WithMany(p => p.ModRequests)
                    .HasForeignKey(d => d.ModeratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RequestModeratorId");
            });

            modelBuilder.Entity<Moderator>(entity =>
            {
                entity.HasIndex(e => e.UserName, "UQ__Moderato__C9F284560E3B8711")
                    .IsUnique();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(24)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ModeratorReview>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(240)
                    .IsUnicode(false);

                entity.HasOne(d => d.Moderator)
                    .WithMany(p => p.ModeratorReviews)
                    .HasForeignKey(d => d.ModeratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModReviewModeratorId");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(240)
                    .IsUnicode(false);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Pictures)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PicBookingId");
            });

            modelBuilder.Entity<Receptionist>(entity =>
            {
                entity.HasIndex(e => e.UserName, "UQ__Receptio__C9F28456A7E02AAA")
                    .IsUnique();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(24)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReceptionistChange>(entity =>
            {
                entity.HasKey(e => e.ChangeId)
                    .HasName("PK__Receptio__0E05C5976FB862C6");

                entity.Property(e => e.ChangeTime).HasColumnType("datetime");

                entity.Property(e => e.NewStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OldStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CustomerBooking)
                    .WithMany(p => p.ReceptionistChanges)
                    .HasForeignKey(d => d.CustomerBookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChangeCustomerBookingId");

                entity.HasOne(d => d.Receptionist)
                    .WithMany(p => p.ReceptionistChanges)
                    .HasForeignKey(d => d.ReceptionistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChangeReceptionistId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
