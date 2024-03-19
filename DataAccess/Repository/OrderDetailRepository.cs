using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void DeleteOrderDetail(int orderID) => OrderDetailDAO.Instance.Delete(orderID);

        public OrderDetail GetOrderDetailByID(int orderID) => OrderDetailDAO.Instance.GetOrderDetailByID(orderID);  

        public IEnumerable<OrderDetail> GetOrderDetails() => OrderDetailDAO.Instance.GetOrderDetailsList();

        public void InsertOrderDetail(OrderDetail orderdetail) => OrderDetailDAO.Instance.AddNew(orderdetail);

        public void UpdateOrderDetail(OrderDetail orderdetail) => OrderDetailDAO.Instance.Update(orderdetail);
    }
}
