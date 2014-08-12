using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using kozvetito.Areas.allas.Models;
using kozvetito.Areas.allas.Models.Szotar;

namespace kozvetito.Areas.allas.DAL
{
    public class AllasContext : DbContext
    {
        public DbSet<AdminEsKozgaz> AdminEsKozgazes { get; set; }
        public DbSet<BeszeltNyelv> BeszeltNyelvs { get; set; }
        public DbSet<Egyetem> Egyetems { get; set; }
        public DbSet<MunkaKeres> MunkaKereses { get; set; }
        public DbSet<Oneletrajz> Oneletrajzes { get; set; }
        public DbSet<PayLog> PayLogs { get; set; }
        public DbSet<SzakmaiTapasztalat> SzakmaiTapasztalats { get; set; }
        public DbSet<SzgepIsmAdmin> SzgepIsmAdmins { get; set; }
        public DbSet<SzgepIsmFelh> SzgepIsmFelhs { get; set; }
        public DbSet<SzgepIsmProg> SzgepIsmProgs { get; set; }

        public DbSet<SzotarAdminEsKozgaz> SzotarAdminEsKozgazes { get; set; }
        public DbSet<SzotarIskolaSzak> SzotarIskolaSzaks { get; set; }
        public DbSet<SzotarMunkahelyPozicio> SzotarMunkahelyPozicios { get; set; }
        public DbSet<SzotarNyelv> SzotarNyelvs { get; set; }
        public DbSet<SzotarNyelvTudasszint> SzotarNyelvTudasszints { get; set; }
        public DbSet<SzotarSzgepIsmAdmin> SzotarSzgepIsmAdmins { get; set; }
        public DbSet<SzotarSzgepIsmFelh> SzotarSzgepIsmFelhs { get; set; }
        public DbSet<SzotarSzgepIsmProg> SzotarSzgepIsmProgs { get; set; }
        public DbSet<SzotarTudasszint> SzotarTudasszints { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}