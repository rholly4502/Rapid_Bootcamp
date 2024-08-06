// See https://aka.ms/new-console-template for more information
using CsvHelper;
using System.Globalization;
using System.Text;
using System.Text.Json;
//membuat alias
using Database1 = RapidBootcamp.ReverseEF.Models;
using Database2 = RapidBootcamp.ReverseEF.DatabaseCF;

//Console.WriteLine("Migrasi Data dari Db1 ke Db2");

var db1 = new Database1.RapidDbContext();
var db2 = new Database2.RapidDbCFContext();

//var db1Categories = db1.Categories.ToList();

//List<RapidBootcamp.ReverseEF.DatabaseCF.Category> listCategory = new List<RapidBootcamp.ReverseEF.DatabaseCF.Category>();
//foreach (var item in db1Categories)
//{
//    listCategory.Add(new RapidBootcamp.ReverseEF.DatabaseCF.Category
//    {
//        CategoryName = item.CategoryName
//    });
//}

//db2.Categories.AddRange(listCategory);
//db2.SaveChanges();
//Console.WriteLine("Migrasi dari Db1 ke Db2 selesai");

//var db1Products = db1.Products.ToList();
//List<RapidBootcamp.ReverseEF.DatabaseCF.Product> listProduct = new List<RapidBootcamp.ReverseEF.DatabaseCF.Product>();
//foreach (var item in db1Products)
//{
//    listProduct.Add(new RapidBootcamp.ReverseEF.DatabaseCF.Product
//    {
//        ProductId = item.ProductId,
//        ProductName = item.ProductName,
//        CategoryId = item.CategoryId,
//        Price = item.Price,
//        Stock = item.Stock
//    });
//}

//try
//{
//    db2.AddRange(listProduct);
//    db2.SaveChanges();
//    Console.WriteLine("Migrasi data Product dari Db1 ke Db2 selesai");
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

//baca dari csv
//using (var reader = new StreamReader("C:\\Workshop\\2024\\RapidBootcamp\\Categories.csv"))
//{
//    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
//    {
//        var records = csv.GetRecords<RapidBootcamp.ReverseEF.DatabaseCF.Category>();
//        foreach (var item in records)
//        {
//            db2.Categories.Add(item);
//        }
//        db2.SaveChanges();
//    }
//}

var httClient = new HttpClient();

//akses API Categories untuk di migrasi ke db2
//try
//{
//    var response = await httClient.GetAsync("http://localhost:5168/api/Categories");
//    if (response.IsSuccessStatusCode)
//    {
//        var data = await response.Content.ReadAsStringAsync();
//        var categories = JsonSerializer.Deserialize<List<RapidBootcamp.ReverseEF.DatabaseCF.Category>>(data);
//        foreach (var item in categories)
//        {
//            Console.WriteLine($"{item.CategoryId} - {item.CategoryName}");
//        }
//        db2.Categories.AddRange(categories);
//        db2.SaveChanges();
//        Console.WriteLine("Migrasi data Category dari API ke Db2 selesai");
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

//akses product by id
//try
//{
//    Console.Write("Masukan ID Product :");
//    var id = Convert.ToInt32(Console.ReadLine());
//    var response = await httClient.GetAsync($"http://localhost:5168/api/Products/{id}");
//    if (response.IsSuccessStatusCode)
//    {
//        var data = await response.Content.ReadAsStringAsync();
//        Database1.Product product = JsonSerializer.Deserialize<Database1.Product>(data);
//        Console.WriteLine($"{product.ProductId} - {product.ProductName} - {product.Price} - {product.Category.CategoryName}");
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.InnerException.Message);
//}


//migrasi data dari csv ke api
//using (var reader = new StreamReader("C:\\Workshop\\2024\\RapidBootcamp\\Categories.csv"))
//{
//    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
//    {
//        var records = csv.GetRecords<Database1.Category>();
//        foreach (var item in records)
//        {
//            var newCategory = JsonSerializer.Serialize<Database1.Category>(item);
//            var content = new StringContent(newCategory, Encoding.UTF8, "application/json");
//            var response = await httClient.PostAsync("http://localhost:5168/api/Categories", content);
//            if (response.IsSuccessStatusCode)
//            {
//                Console.WriteLine($"Data Category {item.CategoryName} berhasil di insert");
//            }
//            else
//            {
//                Console.WriteLine($"Data Category {item.CategoryName} gagal di insert");
//            }
//        }
//    }
//}

//var db2Products = db2.Products.ToList();
//foreach (var item in db2Products)
//{
//    var db1Product = new Database1.Product
//    {
//        ProductId = item.ProductId,
//        CategoryId = item.CategoryId,
//        ProductName = item.ProductName,
//        Price = item.Price,
//        Stock = item.Stock,
//    };

//    try
//    {
//        var serializeProduct = JsonSerializer.Serialize<Database1.Product>(db1Product);
//        var content = new StringContent(serializeProduct, Encoding.UTF8, "application/json");
//        var response = await httClient.PutAsync($"http://localhost:5168/api/Products/{item.ProductId}", content);
//        if (response.IsSuccessStatusCode)
//        {
//            Console.WriteLine($"Data Product {item.ProductName} berhasil di update");
//        }
//        else
//        {
//            Console.WriteLine($"Data Product {item.ProductName} gagal di update");
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.InnerException.Message);
//    }

//}


using (var reader = new StreamReader("C:\\Workshop\\2024\\RapidBootcamp\\Categories.csv"))
{
    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
    {
        var records = csv.GetRecords<Database1.Category>();
        foreach (var item in records)
        {
            var response = await httClient.DeleteAsync($"http://localhost:5168/api/Categories/{item.CategoryId}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Data Category {item.CategoryName} berhasil di delete");
            }
            else
            {
                Console.WriteLine($"Data Category {item.CategoryName} gagal di delete");
            }
        }
    }
}

