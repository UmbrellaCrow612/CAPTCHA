using CAPTCHA.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPTCHA.API.Data
{
    public class CAPTCHADbContext(DbContextOptions<CAPTCHADbContext> options) : DbContext(options)
    {
        public DbSet<TextImgCAPTCHA> TextImgCAPTCHAs {  get; set; }
        public DbSet<AudioCAPTCHA> AudioCAPTCHAs { get; set; }
        public DbSet<TileCAPTCHA> TileCAPTCHAs { get; set; }
    }
}
