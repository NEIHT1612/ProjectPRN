using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetOrderDetails();
        OrderDetail GetOrderDetailByID(int orderID);
        void InsertOrderDetail(OrderDetail orderdetail);
        void UpdateOrderDetail(OrderDetail orderdetail);
        void DeleteOrderDetail(int orderID);
    }
}
