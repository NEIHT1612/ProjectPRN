using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderTblRepository : IOrderTblRepository
    {
        public void DeleteOrder(int orderID) => OrderTblDAO.Instance.Delete(orderID);

        public OrderTbl GetOrderByID(int orderID) => OrderTblDAO.Instance.GetOrderByID(orderID);

        public IEnumerable<OrderTbl> GetOrders() => OrderTblDAO.Instance.GetOrdersList();

        public List<OrderTbl> GetOrdersByMemberId(int memberId) => OrderTblDAO.Instance.GetOrdersListByMemberId(memberId);

        public void InsertOrder(OrderTbl order) => OrderTblDAO.Instance.AddNew(order);

        public void UpdateOrder(OrderTbl order) => OrderTblDAO.Instance.Update(order);
    }
}
