using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities.DAL
{
    public class TNRContext : IdentityDbContext<ApplicationUser>
    {

        #region AspNetIdentity
       // public DbSet<ApplicationUser> ApplicationUsers { get; set; } //2
      // public DbSet<ApplicationRole> ApplicationRoles { get; set; } //2
        public DbSet<RoleMenuPermission> RoleMenuPermission { get; set; }

        public DbSet<NavigationMenu> NavigationMenu { get; set; }

      
        #endregion
    
        #region Common
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<StoreRegister> StoreRegisters { get; set; }
       
        public DbSet<Email> Emails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        //DB Task Management Angular
        public DbSet<Client> Clients { get; set; }
        public DbSet<Comment> Comments { get; set; }    
        public DbSet<Member> Members { get; set; }  
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectDetail> ProjectDetails { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RoleMember> RoleMembers { get; set; }
        public DbSet<TaskMember> TaskMembers { get; set; }  
        #endregion

        public TNRContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);   
            
            #region Permission
            modelBuilder.ApplyConfiguration(new PermissionConfig());
            modelBuilder.ApplyConfiguration(new AspNetUserConfig());
         //   modelBuilder.ApplyConfiguration(new AspNetUserRoleConfig());
            #endregion
                   
            modelBuilder.ApplyConfiguration(new NonFunctionConfig());
            modelBuilder.ApplyConfiguration(new EmailConfig());
        
            modelBuilder.ApplyConfiguration(new StoreConfig());
            modelBuilder.ApplyConfiguration(new SettingConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new GradeConfig());
            //Configuration Model Builder
            modelBuilder.ApplyConfiguration(new ClientConfig());
            modelBuilder.ApplyConfiguration(new CommentConfig());
            modelBuilder.ApplyConfiguration(new ProjectConfig());
            modelBuilder.ApplyConfiguration(new ProjectDetailConfig());
            modelBuilder.ApplyConfiguration(new RegionConfig());
            modelBuilder.ApplyConfiguration(new RoleMemberConfig());
            modelBuilder.ApplyConfiguration(new MemberConfig());
            modelBuilder.ApplyConfiguration(new TaskMemberConfig());
        }
    }
}
