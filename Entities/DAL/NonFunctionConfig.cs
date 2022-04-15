using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DAL
{
    class NonFunctionConfig : IEntityTypeConfiguration<TrackingAgreeCondition>
    {
        public void Configure(EntityTypeBuilder<TrackingAgreeCondition> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasMaxLength(255);
            builder.Property(e => e.UserId).IsRequired().HasMaxLength(450);
            builder.Property(e => e.HISCode).HasMaxLength(100);
        }
    }

    class EmailConfig : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.EmailTo).HasMaxLength(255);
            builder.Property(e => e.Subject).HasMaxLength(255);
            builder.Property(e => e.NameTo).HasMaxLength(255);
        }
    }



    class StoreConfig : IEntityTypeConfiguration<StoreRegister>
    {
        public void Configure(EntityTypeBuilder<StoreRegister> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.FullName).HasMaxLength(255);
            builder.Property(e => e.StoreCode).IsRequired().HasMaxLength(255);
            builder.Property(e => e.StoreName).IsRequired().HasMaxLength(255);
            builder.Property(e => e.PhoneNumber).HasMaxLength(100);
            builder.Property(e => e.CreateDate).HasDefaultValueSql("getdate()");
        }
    }

    class SettingConfig : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

        }
    }

    class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);

        }
    }

    class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
            builder.Property(e => e.CurrentGradeId).IsRequired().HasMaxLength(50);
            builder.HasOne(sc => sc.Grade)
            .WithMany(s => s.Students)
            .HasForeignKey(sc => sc.CurrentGradeId);
        }
    }

    class GradeConfig : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Section).IsRequired().HasMaxLength(100);
        }
    }
    class RegionConfig : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Create_At).HasDefaultValueSql("getdate()");


        }
    }
    class MemberConfig : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(255);
            builder.Property(e => e.IdRegion).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Create_At).HasDefaultValueSql("getdate()");
            builder.Property(e => e.Update_At).HasDefaultValueSql("getdate()");
            builder.HasOne(sc => sc.Region)
                    .WithMany(s => s.Members)
                    .HasForeignKey(sc => sc.IdRegion);
        }
    }

    class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Create_At).HasDefaultValueSql("getdate()");
        }
    }

    class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).IsRequired().HasMaxLength(255);
            builder.Property(e => e.ProjectCost).IsRequired().HasMaxLength(255);
            builder.Property(e => e.CurrentExpenditure).IsRequired().HasMaxLength(255);
            builder.Property(e => e.AvailableFunds).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Start_At).HasDefaultValueSql("getdate()");
            builder.Property(e => e.IdClient).IsRequired().HasMaxLength(255);

            builder.HasOne(sc => sc.Client)
                .WithOne(s => s.Projects)
                .HasForeignKey<Project>(sc => sc.IdClient);

        }
    }

    class RoleMemberConfig : IEntityTypeConfiguration<RoleMember>
    {
        public void Configure(EntityTypeBuilder<RoleMember> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Create_At).HasDefaultValueSql("getdate()");
            builder.Property(e => e.Update_At).HasDefaultValueSql("getdate()");
        }
    }

    class ProjectDetailConfig : IEntityTypeConfiguration<ProjectDetail>
    {
        public void Configure(EntityTypeBuilder<ProjectDetail> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.IdProject).IsRequired().HasMaxLength(255);
            builder.Property(e => e.IdMember).IsRequired().HasMaxLength(255);
            builder.Property(e => e.IdRole).IsRequired().HasMaxLength(255);

            builder.HasOne(sc => sc.Project)
                .WithOne(s => s.ProjectDetail)
                .HasForeignKey<ProjectDetail>(sc => sc.IdProject)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sc => sc.Member)
                    .WithMany(s => s.ProjectDetails)
                    .HasForeignKey(sc => sc.IdMember)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sc => sc.RoleMember)
                    .WithMany(s => s.ProjectDetails)
                    .HasForeignKey(sc => sc.IdRole)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }

    class TaskMemberConfig : IEntityTypeConfiguration<TaskMember>
    {
        public void Configure(EntityTypeBuilder<TaskMember> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Title).IsRequired().HasMaxLength(255);
            builder.Property(e => e.ContentTask).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Create_At).HasDefaultValueSql("getdate()");
            builder.Property(e => e.IdProject).IsRequired().HasMaxLength(255);
            builder.Property(e => e.IdMember).IsRequired().HasMaxLength(255);

            builder.HasOne(sc => sc.Project)
                    .WithMany(s => s.TaskMembers)
                    .HasForeignKey(sc => sc.IdProject)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sc => sc.Member)
                    .WithMany(s => s.TaskMembers)
                    .HasForeignKey(sc => sc.IdMember)
                    .OnDelete(DeleteBehavior.Restrict);

        }
    }
    class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.ContentMember).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Create_At).HasDefaultValueSql("getdate()");
            builder.Property(e => e.IdTaskMember).IsRequired().HasMaxLength(255);
            builder.Property(e => e.IdMember).IsRequired().HasMaxLength(255);

            builder.HasOne(sc => sc.TaskMember)
                    .WithMany(s => s.Comments)
                    .HasForeignKey(sc => sc.IdTaskMember)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sc => sc.Member)
                    .WithMany(s => s.Comments)
                    .HasForeignKey(sc => sc.IdMember)
                    .OnDelete(DeleteBehavior.Restrict); 
        }
    }

}
