using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebTuyenSinh.Data.Entityes
{
   public class HeThongTuyenSinhDB:DbContext
    {
        public HeThongTuyenSinhDB(DbContextOptions<HeThongTuyenSinhDB> options) : base(options) { }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Addmisstion_Major_Block> Addmisstion_Major_Block { get; set; }
        public virtual DbSet<Admisstion> Admisstions { get; set; }
        public virtual DbSet<Admisstion_Major> Admisstion_Major { get; set; }
        public virtual DbSet<Advise> Advises { get; set; }
        public virtual DbSet<Block> Blocks { get; set; }
        public virtual DbSet<Catergory> Catergories { get; set; }
        public virtual DbSet<CatergoryDetail> CatergoryDetails { get; set; }

        public virtual DbSet<file> files { get; set; }
        public virtual DbSet<InforMationProflie> InforMationProflies { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<ProfileStudent> ProfileStudents { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Silde> Sildes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AppRole>()
               .HasKey(x => x.Id);
            modelBuilder.Entity<Account>()
                .Property(e => e.Telephone)
                .IsFixedLength();
            modelBuilder.Entity<Account>()
               .HasKey(x => x.Id);
            modelBuilder.Entity<Account>()
                .HasMany(e => e.ProfileStudents)
                .WithOne(e => e.Account)
                .HasForeignKey(e => e.idAccount);

            modelBuilder.Entity<Addmisstion_Major_Block>()
                .Property(e => e.idBlock)
                .IsFixedLength();
            modelBuilder.Entity<Addmisstion_Major_Block>()
            .HasKey(e => e.id);
            modelBuilder.Entity<Addmisstion_Major_Block>()
           .Property(e => e.id).UseIdentityColumn();
            modelBuilder.Entity<Addmisstion_Major_Block>()
          .HasKey(x => x.id);

            modelBuilder.Entity<Admisstion>()
     .HasKey(e => e.id);
            modelBuilder.Entity<Admisstion>()
           .Property(e => e.id).UseIdentityColumn();
            modelBuilder.Entity<Admisstion>()
                .HasMany(e => e.Admisstion_Major)
                .WithOne(e => e.Admisstion)
                .HasForeignKey(e => e.idAdmisstion);


            modelBuilder.Entity<Admisstion>()
                .HasMany(e => e.ProfileStudents)
                .WithOne(e => e.Admisstion)
                .HasForeignKey(e => e.idAdmisstion);

            modelBuilder.Entity<Admisstion_Major>()
.HasKey(e => e.id);
            modelBuilder.Entity<Admisstion_Major>()
           .Property(e => e.id).UseIdentityColumn();
            modelBuilder.Entity<Admisstion_Major>()
                .Property(e => e.idMajor)
                .IsFixedLength();

            

            modelBuilder.Entity<Admisstion_Major>()
                .HasMany(e => e.Addmisstion_Major_Block)
                .WithOne(e => e.Admisstion_Major)
                .HasForeignKey(e => e.idAdmisstion);

         

            modelBuilder.Entity<Advise>()
.HasKey(e => e.id);
            modelBuilder.Entity<Advise>()
           .Property(e => e.id).UseIdentityColumn();
            modelBuilder.Entity<Advise>()
                .Property(e => e.Telephone)
                .IsFixedLength();

            modelBuilder.Entity<Block>()
.HasKey(e => e.id);

            modelBuilder.Entity<Block>()
                .Property(e => e.id)
                .IsFixedLength();

            modelBuilder.Entity<Block>()
                .HasMany(e => e.Addmisstion_Major_Block)
                .WithOne(e => e.Block)
                .HasForeignKey(e => e.idBlock);

            modelBuilder.Entity<Block>()
                .HasMany(e => e.InforMationProflies)
                .WithOne(e => e.Block)
                .HasForeignKey(e => e.idBlock);

            modelBuilder.Entity<Catergory>()
.HasKey(e => e.id);
            modelBuilder.Entity<Catergory>().Property(e => e.id).UseIdentityColumn();
            modelBuilder.Entity<Catergory>()
                .HasMany(e => e.CatergoryDetails)
                .WithOne(e => e.Catergory)
                .HasForeignKey(e => e.idCatergory);


            modelBuilder.Entity<CatergoryDetail>()
.HasKey(e => e.id);
            modelBuilder.Entity<CatergoryDetail>().Property(e => e.id).UseIdentityColumn();
            modelBuilder.Entity<CatergoryDetail>()
                .HasMany(e => e.Posts)
                .WithOne(e => e.CatergoryDetail)
                .HasForeignKey(e => e.idCategory);




            modelBuilder.Entity<FileProfile>().Property(e => e.id).UseIdentityColumn();

            modelBuilder.Entity<InforMationProflie>()
.HasKey(e => e.id);
            modelBuilder.Entity<InforMationProflie>().Property(e => e.id).UseIdentityColumn();
            modelBuilder.Entity<InforMationProflie>()
                .Property(e => e.idBlock)
                .IsFixedLength();


            modelBuilder.Entity<Major>()
.HasKey(e => e.id);
  

            modelBuilder.Entity<Major>()
                .Property(e => e.id)
                .IsFixedLength();

            modelBuilder.Entity<Major>()
                .HasMany(e => e.Admisstion_Major)
                .WithOne(e => e.Major)
                .HasForeignKey(e => e.idMajor);

            modelBuilder.Entity<Post>()
.HasKey(e => e.id);
            modelBuilder.Entity<Post>().Property(e => e.id).UseIdentityColumn();
            modelBuilder.Entity<Post>()
                .Property(e => e.TypePost)
                .IsFixedLength();

            modelBuilder.Entity<Post>()
                .HasMany(e => e.files)
                .WithOne(e => e.Post)
                .HasForeignKey(e => e.idPost);
      
            modelBuilder.Entity<ProfileStudent>()
.HasKey(e => e.id);
            modelBuilder.Entity<ProfileStudent>().Property(e => e.id).UseIdentityColumn();
            modelBuilder.Entity<ProfileStudent>()
                .Property(e => e.Teletephone)
                .IsFixedLength()
                .IsUnicode(false);
            modelBuilder.Entity<ProfileStudent>()
                .Property(e => e.CMND)
                .IsFixedLength();

            modelBuilder.Entity<ProfileStudent>()
       .HasMany(e => e.FileProfiles)
       .WithOne(e => e.ProfileStudent)
       .HasForeignKey(e => e.idProfile);

            modelBuilder.Entity<ProfileStudent>()
                .HasMany(e => e.InforMationProflies)
                .WithOne(e => e.ProfileStudent)
                .HasForeignKey(e => e.idProfile);


            modelBuilder.Entity<School>()
.HasKey(e => e.id);
            modelBuilder.Entity<School>()
.Property(e => e.id).UseIdentityColumn();

            modelBuilder.Entity<School>()
                .Property(e => e.idShool)
                .IsFixedLength();

            modelBuilder.Entity<School>()
                .Property(e => e.idDistrict)
                .IsFixedLength();


            modelBuilder.Entity<Silde>()
.HasKey(e => e.id);
            modelBuilder.Entity<Silde>()
.Property(x=>x.id).UseIdentityColumn();
            modelBuilder.Entity<Silde>()
                .Property(e => e.CreateDate)
                .IsFixedLength();
            modelBuilder.Entity<Silde>()
              .Property(e => e.CreateDate)
              .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Silde>()
                .Property(e => e.delete)
                .IsFixedLength();

            modelBuilder.Entity<Silde>()
                .Property(e => e.Status)
                .IsFixedLength();

            modelBuilder.Entity<Silde>()
                .Property(e => e.OpenTime)
                .IsFixedLength();

            modelBuilder.Entity<Silde>()
                .Property(e => e.CloseTime)
                .IsFixedLength();

           
        }

      
    }
}
