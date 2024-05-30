using OnlineMobileRecharge.Models;

namespace OnlineMobileRecharge.Data
{
    public class ApplicationDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Packages.Any())
                {
                    context.Packages.AddRange(new List<Package>()
                    {
                        new Package {
                            Package_Name = "Monthly Max",
                            Package_Description = "Monthly Max package for maximum fun",
                            Package_Off_Net_Mins = 250,
                            Package_On_Net_Mins = 1000,
                            Package_SMS = 10000,
                            Package_Data = 25000,
                            Package_Duration = 30,
                            Package_Price = 1000,
                            Package_Type = EnumPackageType.Prepaid,
                        },
                        new Package {
                            Package_Name = "Monthly Internet Data",
                            Package_Description = "Monthly Internet Data package for maximum fun",
                            Package_Off_Net_Mins = 100,
                            Package_On_Net_Mins = 100,
                            Package_SMS = 100,
                            Package_Data = 50000,
                            Package_Duration = 30,
                            Package_Price = 1200,
                            Package_Type = EnumPackageType.Prepaid,
                        },
                        new Package {
                            Package_Name = "Weekly Max",
                            Package_Description = "Weekly Max package for maximum fun for the week",
                            Package_Off_Net_Mins = 150,
                            Package_On_Net_Mins = 300,
                            Package_SMS = 1000,
                            Package_Data = 5000,
                            Package_Duration = 7,
                            Package_Price = 680,
                            Package_Type = EnumPackageType.Prepaid,
                        },
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
