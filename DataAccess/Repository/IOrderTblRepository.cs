using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderTblRepository
    {
        IEnumerable<OrderTbl> GetOrders();
        OrderTbl GetOrderByID(int orderID);
        void InsertOrder(OrderTbl order);
        void UpdateOrder(OrderTbl order);
        void DeleteOrder(int orderID);
        List<OrderTbl> GetOrdersByMemberId(int memberId);
    }
}
