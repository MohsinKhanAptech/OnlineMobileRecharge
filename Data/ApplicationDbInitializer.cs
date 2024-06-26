﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineMobileRecharge.Models;

namespace OnlineMobileRecharge.Data
{
    public class ApplicationDbInitializer
    {
        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();

                //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                if (!await context.Packages.AnyAsync())
                {
                    await context.Packages.AddRangeAsync(new List<Package>()
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
                            Package_Type = EnumPackageType.Postpaid,
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
                            Package_Type = EnumPackageType.Postpaid,
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
                            Package_Type = EnumPackageType.Postpaid,
                        },
                    });
                    await context.SaveChangesAsync();
                }

                if (!await context.Recharges.AnyAsync())
                {
                    await context.Recharges.AddRangeAsync(new List<Recharge>()
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
                            Recharge_Type = EnumRechargeType.Top_up,
                            Recharge_Taxed_Amount = 1500 / 100 * 15,
                            Recharge_Amount = 1500 - (1500 / 100 * 15),
                        },
                        new Recharge {
                            Recharge_Name = "Rs.1000",
                            Recharge_Description = "Monthly Max package for maximum fun",
                            Recharge_Price = 1000,
                            Recharge_Tax_Rate = 15,
                            Recharge_Type = EnumRechargeType.Top_up,
                            Recharge_Taxed_Amount = 1000 / 100 * 15,
                            Recharge_Amount = 1000 - (1000 / 100 * 15),
                        },
                        new Recharge {
                            Recharge_Name = "Rs.500",
                            Recharge_Description = "Monthly Max package for maximum fun",
                            Recharge_Price = 500,
                            Recharge_Tax_Rate = 15,
                            Recharge_Type = EnumRechargeType.Top_up,
                            Recharge_Taxed_Amount = 500 / 100 * 15,
                            Recharge_Amount = 500 - (500 / 100 * 15),
                        },
                        new Recharge {
                            Recharge_Name = "Rs.1500",
                            Recharge_Description = "Monthly Max package for maximum fun",
                            Recharge_Price = 1500,
                            Recharge_Tax_Rate = 15,
                            Recharge_Type = EnumRechargeType.Postpaid,
                            Recharge_Taxed_Amount = 1500 / 100 * 15,
                            Recharge_Amount = 1500 - (1500 / 100 * 15),
                        },
                        new Recharge {
                            Recharge_Name = "Rs.1000",
                            Recharge_Description = "Monthly Max package for maximum fun",
                            Recharge_Price = 1000,
                            Recharge_Tax_Rate = 15,
                            Recharge_Type = EnumRechargeType.Postpaid,
                            Recharge_Taxed_Amount = 1000 / 100 * 15,
                            Recharge_Amount = 1000 - (1000 / 100 * 15),
                        },
                        new Recharge {
                            Recharge_Name = "Rs.500",
                            Recharge_Description = "Monthly Max package for maximum fun",
                            Recharge_Price = 500,
                            Recharge_Tax_Rate = 15,
                            Recharge_Type = EnumRechargeType.Postpaid,
                            Recharge_Taxed_Amount = 500 / 100 * 15,
                            Recharge_Amount = 500 - (500 / 100 * 15),
                        },
                    });
                    await context.SaveChangesAsync();
                }

                if (!await context.CallerTunes.AnyAsync())
                {
                    await context.CallerTunes.AddRangeAsync(new List<CallerTune>()
                    {
                        new CallerTune
                        {
                            Tune_Name = "Default Tune",
                            Tune_Description = "Default Caller Tune",
                            Tune_Path = "/CallerTunes/Default.mp3",
                            Tune_Price = 200,
                        },
                        new CallerTune
                        {
                            Tune_Name = "desperado",
                            Tune_Description = "desperado theme song ring tone",
                            Tune_Path = "/CallerTunes/desperado-theme.mp3",
                            Tune_Price = 200,
                        },
                        new CallerTune
                        {
                            Tune_Name = "Vivo X80 Pro",
                            Tune_Description = "Vivo X80 Pro Stock Ringtone",
                            Tune_Path = "/CallerTunes/vivo.mp3",
                            Tune_Price = 200,
                        },
                        new CallerTune
                        {
                            Tune_Name = "Samsung Galaxy",
                            Tune_Description = "Samsung Galaxy Stock Ringtone",
                            Tune_Path = "/CallerTunes/samsung_galaxy.mp3",
                            Tune_Price = 200,
                        },
                        new CallerTune
                        {
                            Tune_Name = "OPPO Reno",
                            Tune_Description = "OPPO Reno Stock Ringtone",
                            Tune_Path = "/CallerTunes/oppo_reno.mp3",
                            Tune_Price = 200,
                        },
                        new CallerTune
                        {
                            Tune_Name = "Sad Instrumental",
                            Tune_Description = "Sad Instrumental Ringtone",
                            Tune_Path = "/CallerTunes/reelaudio.mp3",
                            Tune_Price = 200,
                        },
                        new CallerTune
                        {
                            Tune_Name = "Let Me Down Slowly",
                            Tune_Description = "Let Me Down Slowly Song Ringtone",
                            Tune_Path = "/CallerTunes/let_me_down_slowly.mp3",
                            Tune_Price = 200,
                        },
                        new CallerTune
                        {
                            Tune_Name = "IPhone Remix",
                            Tune_Description = "Stock IPhone Ringtone Remix",
                            Tune_Path = "/CallerTunes/iphone_remix.mp3",
                            Tune_Price = 250,
                        },
                    });
                    await context.SaveChangesAsync();
                }

                if (!await context.TaxRates.AnyAsync())
                {
                    await context.TaxRates.AddRangeAsync(new List<Models.TaxRate>()
                    {
                        new Models.TaxRate
                        {
                            Tax_Name = "Sales Tax",
                            Tax_Description = "15% Sales Tax",
                            Tax_Rate = 15,
                        }
                    });
                    await context.SaveChangesAsync();
                }

                // Users & Roles
                if (!await roleManager.Roles.AnyAsync())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }

                string adminName = "admin";
                string adminPhone = "03012345678";
                string adminEmail = "admin@admin.com";
                string adminPassword = "Admin!123";

                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var admin = new IdentityUser();
                    admin.UserName = adminName;
                    admin.PhoneNumber = adminPhone;
                    admin.Email = adminEmail;
                    admin.EmailConfirmed = true;
                    admin.PhoneNumberConfirmed = true;

                    await userManager.CreateAsync(admin, adminPassword);

                    await userManager.AddToRoleAsync(admin, "Admin");

                    // add service for user
                    await context.Services.AddAsync(new Service()
                    {
                        User_Id = admin.Id,
                        IdentityUser = admin,
                        Caller_Tune_Id = 1,
                        Caller_Tune = context.CallerTunes.Find(1),
                        Do_Not_Disturb = false,
                    });
                    await context.SaveChangesAsync();
                }

                string userName = "user";
                string userPhone = "03123456789";
                string userEmail = "user@user.com";
                string userPassword = "User!123";

                if (await userManager.FindByEmailAsync(userEmail) == null)
                {
                    var newUser = new IdentityUser();
                    newUser.UserName = userName;
                    newUser.PhoneNumber = userPhone;
                    newUser.Email = userEmail;
                    newUser.EmailConfirmed = true;
                    newUser.PhoneNumberConfirmed = true;

                    await userManager.CreateAsync(newUser, userPassword);

                    await userManager.AddToRoleAsync(newUser, "User");

                    // add service for user
                    await context.Services.AddAsync(new Service()
                    {
                        User_Id = newUser.Id,
                        IdentityUser = newUser,
                        Caller_Tune_Id = 1,
                        Caller_Tune = context.CallerTunes.Find(1),
                        Do_Not_Disturb = false,
                    });
                    await context.SaveChangesAsync();
                }

                // add transactions
                var user = await context.Users.FindAsync(userManager.FindByNameAsync(userName).Result.Id);

                if (!await context.PackageTransactions.AnyAsync())
                {
                    foreach (var package in context.Packages)
                    {
                        await context.PackageTransactions.AddAsync(new PackageTransaction()
                        {
                            Package_Id = package.Package_Id,
                            Package = await context.Packages.FindAsync(package.Package_Id),
                            Mobile_Number = userPhone,
                            User_Id = user.Id,
                            IdentityUser = await context.Users.FindAsync(user.Id),
                            Payment_Completed = true,
                            Session_Id = $"{package.Package_Id}Session",
                            Transaction_Date = DateTime.UtcNow,
                        });
                    }
                    await context.SaveChangesAsync();
                }

                if (!await context.RechargeTransactions.AnyAsync())
                {
                    foreach (var recharge in context.Recharges)
                    {
                        await context.RechargeTransactions.AddAsync(new RechargeTransaction()
                        {
                            Recharge_Id = recharge.Recharge_Id,
                            Recharge = await context.Recharges.FindAsync(recharge.Recharge_Id),
                            Mobile_Number = userPhone,
                            User_Id = user.Id,
                            IdentityUser = await context.Users.FindAsync(user.Id),
                            Payment_Completed = true,
                            Session_Id = $"{recharge.Recharge_Id}Session",
                            Transaction_Date = DateTime.UtcNow,
                        });
                    }
                    await context.SaveChangesAsync();
                }

                if (!await context.ServiceTransactions.AnyAsync())
                {
                    foreach (var tune in context.CallerTunes)
                    {
                        await context.ServiceTransactions.AddAsync(new ServiceTransaction()
                        {
                            Tune_Id = tune.Tune_Id,
                            CallerTune = await context.CallerTunes.FindAsync(tune.Tune_Id),
                            Mobile_Number = userPhone,
                            User_Id = user.Id,
                            IdentityUser = await context.Users.FindAsync(user.Id),
                            Session_Id = $"{tune.Tune_Id}Session",
                            Transaction_Date = DateTime.UtcNow,
                        });
                    }
                    await context.SaveChangesAsync();
                }

                if (!await context.CustomRechargeTransactions.AnyAsync())
                {
                    var taxRate = await context.TaxRates.FirstAsync();
                    for (int i = 200; i < 1000; i = i + 100)
                    {
                        await context.CustomRechargeTransactions.AddAsync(new CustomRechargeTransaction()
                        {
                            Tax_Id = taxRate.Tax_Id,
                            TaxRate = taxRate,
                            Recharge_Price = i,
                            Recharge_Amount = i - i * taxRate.Tax_Rate / 100,
                            Mobile_Number = userPhone,
                            User_Id = user.Id,
                            IdentityUser = await context.Users.FindAsync(user.Id),
                            Session_Id = $"{i}Session",
                            Transaction_Date = DateTime.UtcNow,
                        });
                    }
                    await context.SaveChangesAsync();
                }

                // others
                if (!await context.Newsletter.AnyAsync())
                {
                    for (int i = 200; i < 1000; i = i + 100)
                    {
                        await context.Newsletter.AddAsync(new Newsletter()
                        {
                            Newsletter_Email = $"DummyEmail{i}@gmail.com",
                            Date_Added = DateTime.UtcNow,
                        });
                    }
                    await context.SaveChangesAsync();
                }
                if (!await context.Feedbacks.AnyAsync())
                {
                    for (int i = 200; i < 1000; i = i + 100)
                    {
                        await context.Feedbacks.AddAsync(new Feedback()
                        {
                            Feedback_Name = $"Dummy Name {i}",
                            Feedback_Email = $"DummyEmail{i}@gmail.com",
                            Feedback_Message = $"Dummy Message {i}",
                            Date_Added = DateTime.UtcNow,
                        });
                    }
                    await context.SaveChangesAsync();
                }
                if (!await context.Contacts.AnyAsync())
                {
                    for (int i = 200; i < 1000; i = i + 100)
                    {
                        await context.Contacts.AddAsync(new Contact()
                        {
                            Contact_Name = $"Dummy Name {i}",
                            Contact_Phone = $"03001234{i}",
                            Contact_Email = $"DummyEmail{i}@gmail.com",
                            Contact_Intrest = $"DummyIntrest {i}",
                            Contact_Message = $"Dummy Message {i}",
                            Date_Added = DateTime.UtcNow,
                        });
                    }
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
