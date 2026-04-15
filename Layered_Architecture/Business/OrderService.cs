using Layered_Architecture.Models;
using Layered_Architecture.DataAccess;

namespace Layered_Architecture.Business
{
    public class OrderService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly ProductRepository _productRepository;
        private readonly OrderRepository _orderRepository;

        public OrderService()
        {
            _customerRepository = new CustomerRepository();
            _productRepository = new ProductRepository();
            _orderRepository = new OrderRepository();
        }

        public string CreateOrder(int customerId, int productId, int quantity)
        {
            if (!_customerRepository.Exists(customerId))
            {
                return "Khách hàng không tồn tại.";
            }

            Product? product = _productRepository.GetById(productId);
            if (product == null)
            {
                return "Sản phẩm không tồn tại.";
            }

            if (quantity <= 0)
            {
                return "Số lượng phải lớn hơn 0.";
            }

            Order order = new Order
            {
                CustomerId = customerId,
                ProductId = productId,
                Quantity = quantity,
                OrderDate = DateTime.Now
            };

            _orderRepository.Add(order);
            return "Tạo đơn hàng thành công.";
        }

        public List<OrderDetail> GetAllOrders()
        {
            return _orderRepository.GetAllDetails();
        }
    }
}