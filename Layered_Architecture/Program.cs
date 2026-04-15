using Layered_Architecture.Business;
using Layered_Architecture.DataAccess;
using Layered_Architecture.Models;

CustomerRepository customerRepository = new CustomerRepository();
ProductRepository productRepository = new ProductRepository();
OrderService orderService = new OrderService();

SeedData();

while (true)
{
    Console.Clear();
    Console.WriteLine("===== DEMO LAYERED ARCHITECTURE =====");
    Console.WriteLine("1. Xem danh sach khach hang");
    Console.WriteLine("2. Xem danh sach san pham");
    Console.WriteLine("3. Tao don hang");
    Console.WriteLine("4. Xem danh sach don hang");
    Console.WriteLine("0. Thoat");
    Console.Write("Chon: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            ShowCustomers();
            break;
        case "2":
            ShowProducts();
            break;
        case "3":
            CreateOrder();
            break;
        case "4":
            ShowOrders();
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Lua chon khong hop le.");
            Pause();
            break;
    }
}

void SeedData()
{
    if (customerRepository.GetAll().Count == 0)
    {
        customerRepository.Add(new Customer { FullName = "Nguyen Van A", Phone = "0901111111" });
        customerRepository.Add(new Customer { FullName = "Tran Thi B", Phone = "0902222222" });
    }

    if (productRepository.GetAll().Count == 0)
    {
        productRepository.Add(new Product { ProductName = "Ao so mi", Price = 250000 });
        productRepository.Add(new Product { ProductName = "Quan tay", Price = 350000 });
        productRepository.Add(new Product { ProductName = "Vest nam", Price = 1200000 });
    }
}

void ShowCustomers()
{
    Console.Clear();
    Console.WriteLine("===== DANH SACH KHACH HANG =====");

    var customers = customerRepository.GetAll();
    foreach (var c in customers)
    {
        Console.WriteLine($"ID: {c.CustomerId} | Ten: {c.FullName} | SDT: {c.Phone}");
    }

    Pause();
}

void ShowProducts()
{
    Console.Clear();
    Console.WriteLine("===== DANH SACH SAN PHAM =====");

    var products = productRepository.GetAll();
    foreach (var p in products)
    {
        Console.WriteLine($"ID: {p.ProductId} | Ten: {p.ProductName} | Gia: {p.Price:N0} VND");
    }

    Pause();
}

void CreateOrder()
{
    Console.Clear();
    Console.WriteLine("===== TAO DON HANG =====");

    ShowCustomersSimple();
    Console.Write("Nhap Customer ID: ");
    bool checkCustomer = int.TryParse(Console.ReadLine(), out int customerId);

    ShowProductsSimple();
    Console.Write("Nhap Product ID: ");
    bool checkProduct = int.TryParse(Console.ReadLine(), out int productId);

    Console.Write("Nhap so luong: ");
    bool checkQuantity = int.TryParse(Console.ReadLine(), out int quantity);

    if (!checkCustomer || !checkProduct || !checkQuantity)
    {
        Console.WriteLine("Du lieu nhap khong hop le.");
        Pause();
        return;
    }

    string result = orderService.CreateOrder(customerId, productId, quantity);
    Console.WriteLine(result);
    Pause();
}

void ShowOrders()
{
    Console.Clear();
    Console.WriteLine("===== DANH SACH DON HANG =====");

    var orders = orderService.GetAllOrders();

    if (orders.Count == 0)
    {
        Console.WriteLine("Chua co don hang nao.");
    }
    else
    {
        foreach (var o in orders)
        {
            Console.WriteLine($"Order ID: {o.OrderId}");
            Console.WriteLine($"Khach hang: {o.CustomerName}");
            Console.WriteLine($"San pham: {o.ProductName}");
            Console.WriteLine($"Gia: {o.Price:N0} VND");
            Console.WriteLine($"So luong: {o.Quantity}");
            Console.WriteLine($"Ngay dat: {o.OrderDate}");
            Console.WriteLine($"Tong tien: {o.TotalAmount:N0} VND");
            Console.WriteLine(new string('-', 40));
        }
    }

    Pause();
}

void ShowCustomersSimple()
{
    Console.WriteLine("---- Khach hang ----");
    var customers = customerRepository.GetAll();
    foreach (var c in customers)
    {
        Console.WriteLine($"{c.CustomerId}. {c.FullName}");
    }
}

void ShowProductsSimple()
{
    Console.WriteLine("---- San pham ----");
    var products = productRepository.GetAll();
    foreach (var p in products)
    {
        Console.WriteLine($"{p.ProductId}. {p.ProductName} - {p.Price:N0} VND");
    }
}

void Pause()
{
    Console.WriteLine();
    Console.WriteLine("Nhan Enter de tiep tuc...");
    Console.ReadLine();
}