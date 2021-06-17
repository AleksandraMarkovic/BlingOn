using blingOn.DataAccess;
using blingOn.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace blingOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialController : ControllerBase
    {
        private readonly BlingOnContext _context;

        public InitialController(BlingOnContext context)
        {
            _context = context;
        }

        // POST api/<InitialController>
        [HttpPost]
        public void Post()
        {
            var brands = new List<Brand>
            {
                new Brand
                {
                    Name = "Brand 1"
                },
                new Brand
                {
                    Name = "Brand 2"
                },
                new Brand
                {
                    Name = "Brand 3"
                },
                new Brand
                {
                    Name = "Brand 4"
                },
                new Brand
                {
                    Name = "Brand 5"
                }
            };

            var roles = new List<Role>
            {
                new Role
                {
                    Name = "Admin"
                },
                new Role
                {
                    Name = "User"
                }
            };

            var products = new List<Product>
            {
                new Product
                {
                    Name = "Prod 1",
                    Description = "Desc 1",
                    ImagePath = "0f8361be7fca.png",
                    Price = 100,
                    Brand = brands.ElementAt(0)
                },
                new Product
                {
                    Name = "Prod 2",
                    Description = "Desc 2",
                    ImagePath = "0f8361be7fca.png",
                    Price = 200,
                    Brand = brands.ElementAt(1)
                },
                new Product
                {
                    Name = "Prod 3",
                    Description = "Desc 3",
                    ImagePath = "0f8361be7fca.png",
                    Price = 300,
                    Brand = brands.ElementAt(2)
                },
                new Product
                {
                    Name = "Prod 4",
                    Description = "Desc 4",
                    ImagePath = "0f8361be7fca.png",
                    Price = 400,
                    Brand = brands.ElementAt(0)
                },
                new Product
                {
                    Name = "Prod 5",
                    Description = "Desc 5",
                    ImagePath = "0f8361be7fca.png",
                    Price = 500,
                    Brand = brands.ElementAt(3)
                },
                new Product
                {
                    Name = "Prod 6",
                    Description = "Desc 6",
                    ImagePath = "0f8361be7fca.png",
                    Price = 600,
                    Brand = brands.ElementAt(4)
                },
                new Product
                {
                    Name = "Prod 7",
                    Description = "Desc 7",
                    ImagePath = "0f8361be7fca.png",
                    Price = 700,
                    Brand = brands.ElementAt(1)
                },
                new Product
                {
                    Name = "Prod 8",
                    Description = "Desc 8",
                    ImagePath = "0f8361be7fca.png",
                    Price = 800,
                    Brand = brands.ElementAt(0)
                },
                new Product
                {
                    Name = "Prod 9",
                    Description = "Desc 9",
                    ImagePath = "0f8361be7fca.png",
                    Price = 900,
                    Brand = brands.ElementAt(2)
                },
                new Product
                {
                    Name = "Prod 10",
                    Description = "Desc 10",
                    ImagePath = "0f8361be7fca.png",
                    Price = 150,
                    Brand = brands.ElementAt(3)
                }
            };

            var sizes = new List<Size>
            {
                new Size
                {
                    SizeValue = 4
                },
                new Size
                {
                    SizeValue = 8
                },
                new Size
                {
                    SizeValue = 5
                },
                new Size
                {
                    SizeValue = 6
                },
                new Size
                {
                    SizeValue = 10
                }
            };

            var productSizes = new List<ProductSize>
            {
                new ProductSize
                {
                    Product = products.ElementAt(0),
                    Size = sizes.ElementAt(1),
                    Quanity = 10
                },
                new ProductSize
                {
                    Product = products.ElementAt(1),
                    Size = sizes.ElementAt(2),
                    Quanity = 15
                },
                new ProductSize
                {
                    Product = products.ElementAt(2),
                    Size = sizes.ElementAt(4),
                    Quanity = 26
                },
                new ProductSize
                {
                    Product = products.ElementAt(3),
                    Size = sizes.ElementAt(3),
                    Quanity = 33
                },
                new ProductSize
                {
                    Product = products.ElementAt(4),
                    Size = sizes.ElementAt(0),
                    Quanity = 19
                },
                new ProductSize
                {
                    Product = products.ElementAt(5),
                    Size = sizes.ElementAt(2),
                    Quanity = 17
                },
                new ProductSize
                {
                    Product = products.ElementAt(6),
                    Size = sizes.ElementAt(1),
                    Quanity = 36
                },
                new ProductSize
                {
                    Product = products.ElementAt(7),
                    Size = sizes.ElementAt(1),
                    Quanity = 18
                },
                new ProductSize
                {
                    Product = products.ElementAt(8),
                    Size = sizes.ElementAt(3),
                    Quanity = 14
                },
                new ProductSize
                {
                    Product = products.ElementAt(9),
                    Size = sizes.ElementAt(4),
                    Quanity = 33
                }
            };


            var users = new List<User>
            {
                new User
                {
                    FirstName = "Aleksandra",
                    LastName = "Markovic",
                    Email = "markovic@gmail.com",
                    Password = "sifra123",
                    Address = "Adresa 1",
                    Role = roles.First(),
                    IsActive = true
                },
                new User
                {
                    FirstName = "Jelena",
                    LastName = "Markovic",
                    Email = "jelena@gmail.com",
                    Password = "sifra123",
                    Address = "Adresa 2",
                    Role = roles.Last(),
                    IsActive = true
                }
            };

            _context.Brands.AddRange(brands);
            _context.Roles.AddRange(roles);
            _context.Products.AddRange(products);
            _context.Sizes.AddRange(sizes);
            _context.ProductSizes.AddRange(productSizes);
            _context.Users.AddRange(users);

            _context.SaveChanges();
        }
    }
}
