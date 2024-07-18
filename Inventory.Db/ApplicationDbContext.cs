

using Inventory.Entities;
using Inventory_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ItemModel> Items { get; set; }

        public DbSet<CustomerModel> Customers { get; set; }

        public DbSet<SalesMasterModel> SalesMaster { get; set; }

        public DbSet<SalesDetailsModel> SalesDetails { get; set; }
        public DbSet<LogInModel> LogInModel { get; set; } = default!;
        

        public DbSet<UserModel> UerModel {  get; set; }

        public DbSet<VendorModel> Vendors {  get; set; }

        public DbSet<ItemCurrentInfo> ItemsCurrentInfo {  get; set; }

        public DbSet<ItemCurrentInfoHistory> ItemsHistoryInfo { get; set; }

        public DbSet<PurchaseMasterModel> PurchaseMaster { get; set; }

        public DbSet<PurchaseDetailModel> PurchaseDetail { get; set;}
    }
}
