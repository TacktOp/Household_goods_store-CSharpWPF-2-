

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
        private class dataFormatStash
        {
            public string Id { get; set; }
            public string ProductId { get; set; }
            public string TimeLastCheck { get; set; }
            public string BuyPrice { get; set; }
        }
        /*private class dataFormatTransactionHistory
        {
            public string Id { get; set; }
            public string SpendMoney { get; set; }
            public string BalanceAfterTransaction { get; set; }
            public string TimeOfTransaction { get; set; }
        }*/
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
        private List<dataFormatStash> GetStashes()
        {
            var stashes = _context.Stashes.ToList();
            var data = new List<dataFormatStash>();
            foreach (var stash in stashes)
            {
                data.Add(new dataFormatStash
                {
                    Id = stash.Id.ToString(),
                    ProductId = stash.ProductId.ToString(),
                    TimeLastCheck = stash.TimeLastCheck.ToString(),
                    BuyPrice = stash.BuyPrice.ToString()
                });
            }
            return data;
        }
        /*private List<dataFormatTransactionHistory> GetTransactionHistories()
        {
            var transactionHistories = _context.TransactionHistories.ToList();
            var data = new List<dataFormatTransactionHistory>();
            foreach (var transactionHistory in transactionHistories)
            {
                data.Add(new dataFormatTransactionHistory
                {
                    Id = transactionHistory.Id.ToString(),
                    SpendMoney = transactionHistory.SpendMoney.ToString(),
                    BalanceAfterTransaction = transactionHistory.BalanceAfterTransaction.ToString(),
                    TimeOfTransaction = transactionHistory.TimeOfTransaction.ToString()
                });
            }
            return data;
        }*/

        public Dictionary<string, List<object>> GetAllData()
        {
            var data = new Dictionary<string, List<object>>
            {
                { "Users", GetUsers().Cast<object>().ToList() },
                { "Products", GetProducts().Cast<object>().ToList() },
                { "Stashes", GetStashes().Cast<object>().ToList() },
                //{ "TransactionHistories", GetTransactionHistories().Cast<object>().ToList() }
            };
            return data;
        }
    }
}