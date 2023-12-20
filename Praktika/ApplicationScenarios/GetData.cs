

namespace ApplicationScenarios
{
    public class GetData
    {
        private readonly PDbContextData _context;

        public GetData(PDbContextData context)
        {
            _context = context;
        }
        private class dataFormatUser
        {
            public string Id { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
        }
        private class dataFormatProduct
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Price { get; set; }
        }
        private class dataFormatShop
        {
            public string Id { get; set; }
            public string Addres { get; set; }
        }
        private List<dataFormatUser> GetUsers()
        {
            var users = _context.Users.ToList();
            var data = new List<dataFormatUser>();
            foreach (var user in users)
            {
                data.Add(new dataFormatUser
                {
                    Id = user.Id.ToString(),
                    Login = user.Login,
                    Password = user.Password
                });
            }
            return data;
        }
        private List<dataFormatProduct> GetProducts()
        {
            var products = _context.Products.ToList();
            var data = new List<dataFormatProduct>();
            foreach (var product in products)
            {
                data.Add(new dataFormatProduct
                {
                    Id = product.Id.ToString(),
                    Name = product.Name,
                    Price = product.Price.ToString(),
                });
            }
            return data;
        }
        private List<dataFormatShop> GetShops()
        {
            var shops = _context.Shops.ToList();
            var data = new List<dataFormatShop>();
            foreach (var shop in shops)
            {
                data.Add(new dataFormatShop
                {
                    Id = shop.Id.ToString(),
                    Addres = shop.Addres,
                });
            }
            return data;
        }

        public Dictionary<string, List<object>> GetAllData()
        {
            var data = new Dictionary<string, List<object>>
            {
                { "Users", GetUsers().Cast<object>().ToList() },
                { "Products", GetProducts().Cast<object>().ToList() },
                { "Shops", GetShops().Cast<object>().ToList() },
            };
            return data;
        }
    }
}