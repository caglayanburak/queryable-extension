using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DynamicLinqSelectSample
{
    class Program
    {
        static void Main(string[] args)
        {

            var list = new List<Product>()
            {
                new Product{
                    Id = 1,
                    Name = "Gömlek",
                    Price = 19.99m,
                    IsActive = true
                }
            };

            // var t = list.Select(new DynamicLinq<Product>().CreateNewStatement<ProductDTO>());
            var t = new ECommerceContext().Products.Select(new DynamicLinq<Product>().CreateNewStatement<ProductDTO>()).ToList();
            Console.ReadLine();
        }
    }
}
