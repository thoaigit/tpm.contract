using Microsoft.EntityFrameworkCore;
using tpm.web.contract.Models;
namespace tpm.web.contract.Data
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options)
        : base(options)
        {
        }

        // Khai báo DbSet để truy vấn bảng "DichVu" trong cơ sở dữ liệu
        public DbSet<DichVu> DichVus { get; set; }
    }
}
