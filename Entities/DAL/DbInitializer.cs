using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System.Linq;
using Entities.Entities;

namespace Entities.DAL
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder builder)
        {
           /* using (var serviceScope = builder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<TNRContext>();
                if (!context.StoreRegisters.Any())
                {
                   for(var i = 1; i <= 100; i++)
                    {
                        var storeRegister = new StoreRegister()
                        {
                            FullName = $"Nguyen van {i}",
                            StoreCode = $"Ma LZ{i}",
                            StoreName =$" Name {i}",
                            Address =  $"Addres {i}",
                            PhoneNumber = $"PhoneNumber {i}",
                            Email = $"Email {i}",
                        };
                        context.StoreRegisters.Add(storeRegister);
                    }
                    context.SaveChanges();
                }
          
              

            }*/
        }
    }
}
