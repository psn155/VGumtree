namespace VGumtree.Model.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Spatial;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;
    

    internal sealed class Configuration : DbMigrationsConfiguration<VGumtree.Model.VGumtreeDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(VGumtree.Model.VGumtreeDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
                        
            SeedMembership(context);

#if DEBUG
            var countries = new List<Country>()
            {
                new Country(){
                    Id = 1,
                    Name = "Việt Nam"
                }
            };

            var adminAreaLevel1s = new List<AdminAreaLevel1>()
            {
                new AdminAreaLevel1(){
                    Id = 1,
                    Name = "Đà Nẵng",
                    Country = countries.FirstOrDefault(x=>x.Id == 1)                    
                },
                new AdminAreaLevel1(){
                    Id = 2,
                    Name = "Hồ Chí Minh",
                    Country = countries.FirstOrDefault(x=>x.Id == 1)                
                },
                new AdminAreaLevel1(){
                    Id = 3,
                    Name = "Hà Nội",
                    Country = countries.FirstOrDefault(x=>x.Id == 1)      
                }
            };

            var adminAreaLevel2s = new List<AdminAreaLevel2>()
            {
                new AdminAreaLevel2(){
                    Id = 1,
                    Name = "Hai Bà Trưng",
                    AdminAreaLevel1 = adminAreaLevel1s.FirstOrDefault(x=>x.Id == 3)
                },
                new AdminAreaLevel2(){
                    Id = 2,
                    Name = "Quận 10",
                    AdminAreaLevel1 = adminAreaLevel1s.FirstOrDefault(x=>x.Id == 2)
                },
                new AdminAreaLevel2(){
                    Id = 3,
                    Name = "Sơn Trà",
                    AdminAreaLevel1 = adminAreaLevel1s.FirstOrDefault(x=>x.Id == 1)
                }
            };

            // Create address/location tables
            foreach (var adr in adminAreaLevel2s)
            {
                context.AdminAreaLevel2s.AddOrUpdate(adr);
            }


            //var users = new List<User>()
            //{
            //    new User(){
            //        UserId = 1,
            //        UserName = "psn155",
            //        FirstName = "Nguyen",
            //        LastName = "Pham",
            //        Email = "psn155@yahoo.com"
            //        //CreatedDate = DateTime.Now,
            //        //ModifiedDate = DateTime.Now,
            //        //IsActive = true                                    
            //    },
            //    new User(){
            //        UserId = 2,
            //        UserName = "tht1710",
            //        FirstName = "Trang",
            //        LastName = "Tran",
            //        Email = "tht1710@yahoo.com"
            //        //CreatedDate = DateTime.Now,
            //        //ModifiedDate = DateTime.Now,
            //        //IsActive = true                                    
            //    }
            //};

            var categories = new List<Category>(){
                new Category(){
                    Id = 1,
                    Name = "Automotive",
                    Description = "Automotive products",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Category(){
                    Id = 2,
                    Name = "Home & Garden",
                    Description = "Home & Garden products",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Category(){
                    Id = 3,
                    Name = "Clothing & Jewellery",
                    Description = "Clothing & Jewellery products",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }
            };

            var subCategories = new List<SubCategory>()
            {
                new SubCategory(){
                    Id = 1,
                    IsActive = true,
                    Name = "Cars, Vans & Utes",
                    Description = "Cars, Vans & Utes",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Category = categories.FirstOrDefault(c => c.Name == "Automotive")
                },
                new SubCategory(){
                    Id = 2,
                    IsActive = true,
                    Name = "Parts & Accessories",
                    Description = "Parts & Accessories",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Category = categories.FirstOrDefault(c => c.Name == "Automotive")
                },
                new SubCategory(){
                    Id = 3,
                    IsActive = true,
                    Name = "Furniture",
                    Description = "Furniture",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Category = categories.FirstOrDefault(c => c.Name == "Home & Garden")
                },
                new SubCategory(){
                    Id = 4,
                    IsActive = true,
                    Name = "Women's Clothing",
                    Description = "Women's Clothing",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Category = categories.FirstOrDefault(c => c.Name == "Clothing & Jewellery")
                }
            };

            var attrs = new List<VGumtree.Model.Attribute>()
            {
                new VGumtree.Model.Attribute(){
                    Id = 1,
                    Name = "Make",
                    SubCategory = subCategories.FirstOrDefault(s => s.Name == "Cars, Vans & Utes")
                },
                new VGumtree.Model.Attribute(){
                    Id = 2,
                    Name = "Model",
                    SubCategory = subCategories.FirstOrDefault(s => s.Name == "Cars, Vans & Utes")
                },
                new VGumtree.Model.Attribute(){
                    Id = 3,
                    Name = "Body Type",
                    SubCategory = subCategories.FirstOrDefault(s => s.Name == "Cars, Vans & Utes")
                },
                new VGumtree.Model.Attribute(){
                    Id = 4,
                    Name = "Year",
                    SubCategory = subCategories.FirstOrDefault(s => s.Name == "Cars, Vans & Utes")
                },
                new VGumtree.Model.Attribute(){
                    Id = 5,
                    Name = "Kilometres",
                    SubCategory = subCategories.FirstOrDefault(s => s.Name == "Cars, Vans & Utes")
                },
                new VGumtree.Model.Attribute(){
                    Id = 6,
                    Name = "Transmission",
                    SubCategory = subCategories.FirstOrDefault(s => s.Name == "Cars, Vans & Utes")
                },
                new VGumtree.Model.Attribute(){
                    Id = 7,
                    Name = "Drive Train",
                    SubCategory = subCategories.FirstOrDefault(s => s.Name == "Cars, Vans & Utes")
                },
                new VGumtree.Model.Attribute(){
                    Id = 8,
                    Name = "Fuel Type",
                    SubCategory = subCategories.FirstOrDefault(s => s.Name == "Cars, Vans & Utes")
                },
                new VGumtree.Model.Attribute(){
                    Id = 9,
                    Name = "Colour",
                    SubCategory = subCategories.FirstOrDefault(s => s.Name == "Cars, Vans & Utes")
                },
                new VGumtree.Model.Attribute(){
                    Id = 10,
                    Name = "Air Conditioning",
                    SubCategory = subCategories.FirstOrDefault(s => s.Name == "Cars, Vans & Utes")
                },
                new VGumtree.Model.Attribute(){
                    Id = 11,
                    Name = "Registered",
                    SubCategory = subCategories.FirstOrDefault(s => s.Name == "Cars, Vans & Utes")
                },
                new VGumtree.Model.Attribute(){
                    Id = 12,
                    Name = "Registration Expiry",
                    SubCategory = subCategories.FirstOrDefault(s => s.Name == "Cars, Vans & Utes")
                }
            };

            var ads = new List<Ad>()
            {
                new Ad(){
                    Id = 1,
                    Name = "Holden vt clubsport 5ltr",
                    Description = "This is the first ad",
                    CreatedDate = DateTime.Parse("Jan 1, 2013"),
                    ModifiedDate =  DateTime.Parse("Jan 1, 2013"),
                    Condition = "Used",
                    ContactEmail = "user1@yahoo.com",
                    ContactPhone = "0432111111",
                    ExpiryDate =  DateTime.Parse("Jan 1, 2014"),
                    Price = 50.0,
                    Locations = new List<Location>(){new Location(){
                        FormattedAddress = "117 Nguyễn Công Trứ, An Hải Bắc, Sơn Trà, Đà Nẵng, Việt Nam",
                        AdminAreaLevel2Id = 3,
                        Latitude = 16.0673517,
                        Longtitude = 108.2340431, 
                        GeoLocation = DbGeography.FromText(string.Format("POINT({1} {0})", "16.0673517", "108.2340431"))
                    }},
                    //User = users.FirstOrDefault(u => u.UserName == "psn155"),
                    UserId = 1,
                    SubCategory = subCategories.FirstOrDefault(s => s.Name == "Cars, Vans & Utes")
                },
                new Ad(){
                    Id = 2,
                    Name = "86 Toyota land cruiser troopy",
                    Description = "This is the second ad",
                    CreatedDate = DateTime.Parse("May 1, 2013"),
                    ModifiedDate = DateTime.Parse("Aug 1, 2013"),
                    Condition = "Used",
                    ContactEmail = "user2@yahoo.com",
                    ContactPhone = "0432122211",
                    ExpiryDate = DateTime.Parse("Jan 1, 2014"),
                    Price = 150.0,
                    Locations = new List<Location>(){new Location(){
                        FormattedAddress = "15 Tô Hiến Thành, phường 14, Quận 10, Hồ Chí Minh, Việt Nam",
                        AdminAreaLevel2Id = 2,
                        Latitude = 10.7721021,
                        Longtitude = 106.6601286, 
                        GeoLocation = DbGeography.FromText(string.Format("POINT({1} {0})", "10.7721021", "106.6601286"))
                    }},
                    //User = users.FirstOrDefault(u => u.UserName == "tht1710"),
                    UserId = 2,
                    SubCategory = subCategories.FirstOrDefault(s => s.Name == "Cars, Vans & Utes")
                },
                new Ad(){
                    Id = 3,
                    Name = "Bed",
                    Description = "This is the third ad",
                    CreatedDate = DateTime.Parse("Mar 11, 2013"),
                    ModifiedDate = DateTime.Parse("Apr 1, 2013"),
                    Condition = "Used",
                    ContactEmail = "user3@yahoo.com",
                    ContactPhone = "0432133311",
                    ExpiryDate = DateTime.Parse("Jan 11, 2014"),
                    Price = 70.0,
                    Locations = new List<Location>(){new Location(){
                        FormattedAddress = "15 Tô Hiến Thành, phường 14, Quận 10, Hồ Chí Minh, Việt Nam",
                        AdminAreaLevel2Id = 2,
                        Latitude = 10.7721021,
                        Longtitude = 106.6601286, 
                        GeoLocation = DbGeography.FromText(string.Format("POINT({1} {0})", "10.7721021", "106.6601286"))
                    }},
                    //User = users.FirstOrDefault(u => u.UserName == "psn155"),
                    UserId = 1,
                    SubCategory = subCategories.FirstOrDefault(s => s.Name == "Furniture")
                },
                new Ad(){
                    Id = 4,
                    Name = "Clothing",
                    Description = "This is the forth ad",
                    CreatedDate = DateTime.Parse("Jul 12, 2013"),
                    ModifiedDate = DateTime.Parse("Sep 19, 2013"),
                    Condition = "New",
                    ContactEmail = "user4@yahoo.com",
                    ContactPhone = "0432133411",
                    ExpiryDate = DateTime.Parse("Jan 11, 2014"),
                    Price = 520.0,
                    Locations = new List<Location>(){new Location(){
                        FormattedAddress = "K36/16 Le Duan, Nguyễn Chí Thanh, Hải Châu, Đà Nẵng, Việt Nam",
                        AdminAreaLevel2Id = 3,
                        Latitude = 16.0697465,
                        Longtitude = 108.2211228, 
                        GeoLocation = DbGeography.FromText(string.Format("POINT({1} {0})", "16.0697465", "108.2211228"))
                    }},
                    //User = users.FirstOrDefault(u => u.UserName == "tht1710"),
                    UserId = 2,
                    SubCategory = subCategories.FirstOrDefault(s => s.Name == "Women's Clothing")
                },
            };

            foreach (var ad in ads)
            {
                context.Ads.AddOrUpdate(ad);
            }

            foreach (var attr in attrs)
            {
                context.Attributes.AddOrUpdate(attr);
            }
            //context.Categories.AddOrUpdate(cat);
#endif
        }

        private void SeedMembership(VGumtree.Model.VGumtreeDb context)
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            // Add custom fields for UserProfile table
            //context.Database.ExecuteSqlCommand("ALTER TABLE UserProfile ADD Email nvarchar(100)");
            
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;
                        
            if (!roles.RoleExists("admin"))
                roles.CreateRole("admin");
            if (!roles.RoleExists("user"))
                roles.CreateRole("user");

            IDictionary<string, object> values = new Dictionary<string, object>();
            values.Add("Email", "psn155@yahoo.com");

            if (membership.GetUser("psn155", false) == null)
                membership.CreateUserAndAccount("psn155", "welcome123", values);

            values.Clear();
            values.Add("Email", "tht1710@yahoo.com");

            if (membership.GetUser("tht1710", false) == null)
                membership.CreateUserAndAccount("tht1710", "welcome123", values);


            if (!roles.GetRolesForUser("psn155").Contains("admin"))
                roles.AddUsersToRoles(new[] { "psn155" }, new[] { "admin" });
            if (!roles.GetRolesForUser("tht1710").Contains("user"))
                roles.AddUsersToRoles(new[] { "tht1710" }, new[] { "user" });




            /*WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            var roles = (SqlRoleProvider)Roles.Provider;
            var membership = (SqlMembershipProvider)Membership.Provider;


            if (!roles.RoleExists("admin"))
                roles.CreateRole("admin");
            if (!roles.RoleExists("user"))
                roles.CreateRole("user");

            MembershipCreateStatus result;                   
            MembershipUser newUser = Membership.CreateUser("tht1710", "welcome123", "psn155@yahoo.com", "", "", true, out result);
            //if (membership.GetUser("psn155", false) == null)         
            //membership.CreateUser("psn155", "welcome123", "psn155@yahoo.com", "", "", true, "", out result);

            //if (membership.GetUser("tht1710", false) == null)
            //    membership.CreateUserAndAccount("tht1710", "welcome123");


            if (!roles.GetRolesForUser("psn155").Contains("admin"))
                roles.AddUsersToRoles(new[] { "psn155" }, new[] { "admin" });
            //if (!roles.GetRolesForUser("tht1710").Contains("user"))
            //    roles.AddUsersToRoles(new[] { "tht1710" }, new[] { "user" });*/


        }

    }
}
