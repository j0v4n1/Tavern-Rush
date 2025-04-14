using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tavern_Rush
{

    enum ProductType
    {
        Ale,
        Sausage,
        Cheese,
        Bread,
        Chicken,
        Wine,
        Steak,
        Pie,
        Shrimps,
        Dessert,
        Mead
    }

    internal class Product
    {
        private readonly Dictionary<ProductType, string> ProductNames = new()
        {
            { ProductType.Ale, "Эль 🍺" },
            { ProductType.Sausage, "Колбаса 🌭" },
            { ProductType.Cheese, "Сыр 🧀" },
            { ProductType.Bread, "Хлеб 🍞" },
            { ProductType.Chicken, "Курица 🍗" },
            { ProductType.Wine, "Вино 🍷" },
            { ProductType.Steak, "Стейк 🥩" },
            { ProductType.Pie, "Пирог 🥧" },
            { ProductType.Shrimps, "Креветки 🍤" },
            { ProductType.Dessert, "Десерт 🍰" },
            { ProductType.Mead, "Медовуха 🍯" }
        };
        private readonly Dictionary<ProductType, int> ProductPrices = new()
        {
            { ProductType.Ale, 3 },
            { ProductType.Sausage, 4 },
            { ProductType.Cheese, 3 },
            { ProductType.Bread, 2 },
            { ProductType.Chicken, 5 },
            { ProductType.Wine, 6 },
            { ProductType.Steak, 7 },
            { ProductType.Pie, 5 },
            { ProductType.Shrimps, 6 },
            { ProductType.Dessert, 4 },
            { ProductType.Mead, 5 }
        };
        private readonly Dictionary<ProductType, int> productAvailabilityByTavernLevel = new()
        {
            { ProductType.Ale, 1 },
            { ProductType.Bread, 1 },
            { ProductType.Sausage, 2 },
            { ProductType.Cheese, 2 },
            { ProductType.Chicken, 3 },
            { ProductType.Wine, 3 },
            { ProductType.Steak, 4 },
            { ProductType.Pie, 4 },
            { ProductType.Shrimps, 5 },
            { ProductType.Dessert, 5 },
            { ProductType.Mead, 6 }
        };
        public string Name { get; private set; }
        public int Price { get; private set; }
        public ProductType ProductType { get; private set; }

        public Product(ProductType productKey)
        {
            Name = ProductNames[productKey];
            Price = ProductPrices[productKey];
            ProductType = productKey;
        }

        public int GetPrice(ProductType product)
        {
            return ProductPrices[product];
        }

        public string GetName(ProductType product)
        {
            return ProductNames[product];
        }

        public int GetAvailabilityByLevel(ProductType product)
        {
            return productAvailabilityByTavernLevel[product];
        }
    }
}
