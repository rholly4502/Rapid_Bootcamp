// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using RapidBootcamp.ETLSample.DbDataWarehouse;
using DbDataWarehouse = RapidBootcamp.ETLSample.DbDataWarehouse;
using DbTransaction = RapidBootcamp.ETLSample.DbTransactional;


//dbcontext untuk DbTransaction
var dbTransaction = new DbTransaction.RapidDbContext();

//dbcontext untuk DbDataWarehouse
var dbDataWarehouse = new DbDataWarehouse.RapidDbContext();

var products = dbTransaction.Products.Include(p => p.Category).ToList();
List<DbDataWarehouse.DimProduct> dimProducts = new List<DbDataWarehouse.DimProduct>();
foreach (var item in products)
{
    dimProducts.Add(new DbDataWarehouse.DimProduct
    {
        ProductName = item.ProductName,
        ProductOriginalKey = item.ProductId,
        Price = item.Price,
        Stock = item.Stock,
        CategoryName = item.Category.CategoryName
    });
}


//mengisi dim products
try
{
    //raw sql with ef core
    dbDataWarehouse.Database.ExecuteSqlRaw("delete from Fact_Sales");
    dbDataWarehouse.Database.ExecuteSqlRaw("delete from Dim_Product");
    dbDataWarehouse.AddRange(dimProducts);
    dbDataWarehouse.SaveChanges();
    Console.WriteLine("Migrasi ke DimProducs selesai");
}
catch (Exception ex)
{
    Console.WriteLine(ex.InnerException.Message);
}

//mengisi dim wallet
var wallets = dbTransaction.Wallets.Include(p => p.Customer)
    .Include(p => p.WalletType).ToList();
List<DbDataWarehouse.DimWallet> dimWallets = new List<DbDataWarehouse.DimWallet>();
foreach (var item in wallets)
{
    dimWallets.Add(new DbDataWarehouse.DimWallet
    {
        WalletOriginalKey = item.WalletId,
        CustomerName = item.Customer.CustomerName,
        WalletTypeName = item.WalletType.WalletName,
        CustomerEmail = item.Customer.Email,
        Saldo = item.Saldo
    });
}

try
{
    dbDataWarehouse.Database.ExecuteSqlRaw("delete from Dim_Wallet");
    dbDataWarehouse.AddRange(dimWallets);
    dbDataWarehouse.SaveChanges();
    Console.WriteLine("Migrasi ke DimWallet selesai");
}
catch (Exception ex)
{
    Console.WriteLine(ex.InnerException.Message);
}

//mengisi fact sales
var orderDetails = dbTransaction.OrderDetails.Include(od => od.OrderHeader);
List<DbDataWarehouse.FactSale> factSales = new List<DbDataWarehouse.FactSale>();
foreach (var item in orderDetails)
{
    var productId = dbDataWarehouse.DimProducts
        .FirstOrDefault(p => p.ProductOriginalKey == item.ProductId).ProductId;

    var walletId = dbDataWarehouse.DimWallets
        .FirstOrDefault(p => p.WalletOriginalKey == item.OrderHeader.WalletId).WalletId;

    factSales.Add(new DbDataWarehouse.FactSale
    {
        ProductId = productId,
        WalletId = walletId,
        OrderHeaderId = item.OrderHeaderId,
        OrderDetailOriginalId = item.OrderDetailId,
        Qty = item.Qty,
        Price = item.Price,
        TotalSales = item.Qty * item.Price
    });
}

try
{
    dbDataWarehouse.AddRange(factSales);
    dbDataWarehouse.SaveChanges();
    Console.WriteLine("Migrasi ke FactSales selesai");
}
catch (Exception ex)
{
    Console.WriteLine(ex.InnerException.Message);
}