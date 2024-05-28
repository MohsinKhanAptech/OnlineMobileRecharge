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

                //if (!context.Categories.Any())
                //{
                //    context.Categories.AddRange(new List<Category>()
                //    {
                //        new Category {Name = "Electronics" },
                //        new Category {Name = "Clothing" },
                //        new Category {Name = "Foods" },
                //    });
                //    context.SaveChanges();
                //}
            }
        }
    }
}
