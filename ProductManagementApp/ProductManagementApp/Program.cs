using Spectre.Console;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace ProductManagementApp
{
    class Product
    {
        SqlConnection con = new SqlConnection("Server=IN-4W3K9S3; database=ProductManagement; User Id=sa; password=Nani falls down@22!@nc");
        DataSet ds = new DataSet();
        public void AddNewProduct()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Products", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            da.Fill(ds);
            var row = ds.Tables[0].NewRow();
            Console.Write(" Enter Product Name: ");
            string pname = Console.ReadLine();
            Console.Write("Enter Product Brand: ");
            string pbrand = Console.ReadLine();
            Console.Write("Enter Product Quantity: ");
            int quan = Convert.ToInt16(Console.ReadLine());
            Console.Write("Enter Price: ");
            int price = Convert.ToInt16(Console.ReadLine());
            row["ProductName"] = $"{pname}";
            row["ProductBrand"] = $"{pbrand}";
            row["Quantity"] = $"{quan}";
            row["Price"] = $"{price}";
            ds.Tables[0].Rows.Add(row);
            da.Update(ds);
            Console.WriteLine("Product added successfully");
        }
        public void GetProduct()
        {
            SqlDataAdapter da = new SqlDataAdapter($"select * from Products", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            da.Fill(ds);
            Console.Write("Enter Product id to view the product details: ");
            int id = Convert.ToInt16(Console.ReadLine());
            var row = ds.Tables[0].Select($"ProductId={id}");
            if (row.Length > 0)
            {
                var rows = row[0];
                Console.WriteLine($"{rows["ProductId"]} | {rows["ProductName"]} | {rows["ProductBrand"]} | {rows["Quantity"]} | {rows["Price"]}");
            }
            else
            {
                Console.WriteLine("Product does not exists");
            }
            da.Update(ds);
        }
        public void GetAllProducts()
        {
            SqlDataAdapter da = new SqlDataAdapter($"select * from Products", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables[0].Rows[i][j]} | ");
                }
                Console.WriteLine();
            }
        }
        public void UpdateProduct()
        {
            SqlDataAdapter da = new SqlDataAdapter($"select * from Products", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            da.Fill(ds);
            Console.Write("Enter Product id to update: ");
            int id = Convert.ToInt16(Console.ReadLine());
            var row = ds.Tables[0].Select($"ProductId={id}");
            if (row.Length > 0)
            {
                var rows = row[0];
                Console.Write(" Enter updated Product Name: ");
                string pname = Console.ReadLine();
                Console.Write("Enter updated Product Brand: ");
                string pbrand = Console.ReadLine();
                Console.Write("Enter updated Product Quantity: ");
                int quan = Convert.ToInt16(Console.ReadLine());
                Console.Write("Enter updated Price: ");
                int price = Convert.ToInt16(Console.ReadLine());
                rows["ProductName"] = $"{pname}";
                rows["ProductBrand"] = $"{pbrand}";
                rows["Quantity"] = $"{quan}";
                rows["Price"] = $"{price}";
                da.Update(ds);
                Console.WriteLine("Product Updated Successfully");
            }
            else
            {
                Console.WriteLine("Enter Id which exist in the Products");
            }
        }
        public void DeleteProduct()
        {
            SqlDataAdapter da = new SqlDataAdapter($"select * from Products", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            da.Fill(ds);
            Console.WriteLine("Enter Id to delete:");
            int id = Convert.ToInt16(Console.ReadLine());
            var row = ds.Tables[0].Select($"ProductId={id}");
            if (row.Length > 0)
            {
                var rows = row[0];
                rows.Delete();
                da.Update(ds);
                Console.WriteLine("Product Deleted Successfully");
            }
            else
            {
                Console.WriteLine( "Enter Id which exist in the products");
            }
        } 
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string result = null;
            do
            {
                Product p = new Product();
                AnsiConsole.MarkupLine($"[green] Welcome to Product Management App [/]");
                try
                {
                    AnsiConsole.MarkupLine($"[DarkRed] 1. Create Notes [/] ");
                    AnsiConsole.MarkupLine($"[DarkRed] 2. View Note by Id [/] ");
                    AnsiConsole.MarkupLine($"[DarkRed] 3. View All Notes [/] ");
                    AnsiConsole.MarkupLine($"[DarkRed] 4. Update Notes By Id [/] ");
                    AnsiConsole.MarkupLine($"[DarkRed] 5. Delete Notes By Id [/]");
                    int choice = Convert.ToInt16(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            {
                                p.AddNewProduct();
                                break;
                            }
                        case 2:
                            {
                                p.GetProduct();
                                break;
                            }
                        case 3:
                            {
                                p.GetAllProducts();
                                break;
                            }
                        case 4:
                            {
                                p.UpdateProduct();
                                break;
                            }
                        case 5:
                            {
                                p.DeleteProduct();
                                break;
                            }

                    }
                }

                catch (FormatException)
                {
                    Console.WriteLine("Only 1 to 5 numbers are allowed");
                }
                Console.WriteLine(" Do you wish to continue? [y/n] ");
                result = Console.ReadLine();
            } while (result.ToLower() == "y");
            
        }
    }
}