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
                            Package_Name = "100 GB Data",
                            Package_Description = "100 GB Data for maximum fun",
                            Package_Off_Net_Mins = 100,
                            Package_On_Net_Mins = 100,
                            Package_SMS = 100,
                            Package_Data = 100000,
                            Package_Duration = 30,
                            Package_Price = 1500,
                            Package_Type = EnumPackageType.Prepaid,
                        },
                        new Package {
                            Package_Name = "50 GB Data",
                            Package_Description = "100 GB Data for maximum fun",
                            Package_Off_Net_Mins = 100,
                            Package_On_Net_Mins = 100,
                            Package_SMS = 100,
                            Package_Data = 50000,
                            Package_Duration = 30,
                            Package_Price = 800,
                            Package_Type = EnumPackageType.Prepaid,
                        },
                        new Package {
                            Package_Name = "10 GB Data",
                            Package_Description = "10 GB Data for maximum fun",
                            Package_Off_Net_Mins = 100,
                            Package_On_Net_Mins = 100,
                            Package_SMS = 100,
                            Package_Data = 10000,
                            Package_Duration = 30,
                            Package_Price = 200,
                            Package_Type = EnumPackageType.Prepaid,
                        },
                        new Package {
                            Package_Name = "25 GB Data",
                            Package_Description = "25 GB Data for maximum fun",
                            Package_Off_Net_Mins = 100,
                            Package_On_Net_Mins = 100,
                            Package_SMS = 100,
                            Package_Data = 25000,
                            Package_Duration = 30,
                            Package_Price = 400,
                            Package_Type = EnumPackageType.Prepaid,
                        },
                    });
                    context.SaveChanges();
                }

                if (!context.Recharges.Any())
                {
                    context.Recharges.AddRange(new List<Recharge>()
                    {
                        new Recharge {
                            Recharge_Name = "Rs.2500",
                            Recharge_Description = "Monthly Max package for maximum fun",
                            Recharge_Price = 2500,
                            Recharge_Tax_Rate = 15,
                            Recharge_Type = EnumRechargeType.Special,
                            Recharge_Taxed_Amount = 2500 / 100 * 15,
                            Recharge_Amount = 2500 - (2500 / 100 * 15),
                        },
                        new Recharge {
                            Recharge_Name = "Rs.2000",
                            Recharge_Description = "Monthly Max package for maximum fun",
                            Recharge_Price = 2000,
                            Recharge_Tax_Rate = 15,
                            Recharge_Type = EnumRechargeType.Special,
                            Recharge_Taxed_Amount = 2000 / 100 * 15,
                            Recharge_Amount = 2000 - (2000 / 100 * 15),
                        },
                        new Recharge {
                            Recharge_Name = "Rs.1500",
                            Recharge_Description = "Monthly Max package for maximum fun",
                            Recharge_Price = 1500,
                            Recharge_Tax_Rate = 15,
                            Recharge_Type = EnumRechargeType.Special,
                            Recharge_Taxed_Amount = 1500 / 100 * 15,
                            Recharge_Amount = 1500 - (1500 / 100 * 15),
                        },
                        new Recharge {
                            Recharge_Name = "Rs.1000",
                            Recharge_Description = "Monthly Max package for maximum fun",
                            Recharge_Price = 1000,
                            Recharge_Tax_Rate = 15,
                            Recharge_Type = EnumRechargeType.Special,
                            Recharge_Taxed_Amount = 1000 / 100 * 15,
                            Recharge_Amount = 1000 - (1000 / 100 * 15),
                        },
                        new Recharge {
                            Recharge_Name = "Rs.500",
                            Recharge_Description = "Monthly Max package for maximum fun",
                            Recharge_Price = 500,
                            Recharge_Tax_Rate = 15,
                            Recharge_Type = EnumRechargeType.Special,
                            Recharge_Taxed_Amount = 500 / 100 * 15,
                            Recharge_Amount = 500 - (500 / 100 * 15),
                        },
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
