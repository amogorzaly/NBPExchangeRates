using Microsoft.EntityFrameworkCore;
using NBPExchangeRates.Models;
namespace NBPExchangeRates
{
    public class NBPContext : DbContext
    {

        public NBPContext(DbContextOptions<NBPContext> options)
            : base(options)
        {
            if (Database.EnsureCreated())
            {
                this.Database.ExecuteSqlRaw("INSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'bat (Tajlandia)', N'THB', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'dolar amerykański', N'USD', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'dolar australijski', N'AUD', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'dolar Hongkongu', N'HKD', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'dolar kanadyjski', N'CAD', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'dolar nowozelandzki', N'NZD', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'dolar singapurski', N'SGD', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'euro', N'EUR', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'forint (Węgry)', N'HUF', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'frank szwajcarski', N'CHF', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'funt szterling', N'GBP', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'hrywna (Ukraina)', N'UAH', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'jen (Japonia)', N'JPY', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'korona czeska', N'CZK', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'korona duńska', N'DKK', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'korona islandzka', N'ISK', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'korona norweska', N'NOK', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'korona szwedzka', N'SEK', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'lej rumuński', N'RON', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'lew (Bułgaria)', N'BGN', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'lira turecka', N'TRY', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'nowy izraelski szekel', N'ILS', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'peso chilijskie', N'CLP', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'peso filipińskie', N'PHP', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'peso meksykańskie', N'MXN', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'rand (Republika Południowej Afryki)', N'ZAR', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'real (Brazylia)', N'BRL', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'ringgit (Malezja)', N'MYR', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'rupia indonezyjska', N'IDR', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'rupia indyjska', N'INR', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'won południowokoreański', N'KRW', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'yuan renminbi (Chiny)', N'CNY', 1, GETDATE())\r\nINSERT [dbo].[Currency] ([Name], [Code], [IsActive], [AddDate]) VALUES (N'SDR (MFW)', N'XDR', 1, GETDATE())");
            };
        }


        public DbSet<Currency> Currency { get; set; }
        public DbSet<Effective> Effective { get; set; }
        public DbSet<ExchangeRate> ExchangeRate { get; set; }
        
    }
}
