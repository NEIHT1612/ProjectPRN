using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderTblDAO
    {
        private static OrderTblDAO instance = null;
        private static readonly object instanceLock = new object();

        public static OrderTblDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderTblDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<OrderTbl> GetOrdersList()
        {
            var orders = new List<OrderTbl>();
            try
            {
                using var context = new BookStorePRNContext();
                orders = context.OrderTbls.Include(x => x.Member).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }

        public OrderTbl GetOrderByID(int orderId)
        {
            OrderTbl order = null;
            try
            {
                using var context = new BookStorePRNContext();
                order = context.OrderTbls.SingleOrDefault(m => m.OrderId == orderId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public void AddNew(OrderTbl order)
        {
            try
            {
                OrderTbl _order = GetOrderByID(order.OrderId);
                if (_order == null)
                {
                    using var context = new BookStorePRNContext();
                    context.OrderTbls.Add(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The order is already exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(OrderTbl order)
        {
            try
            {
                OrderTbl _order = GetOrderByID(order.OrderId);
                if (_order != null)
                {
                    using var context = new BookStorePRNContext();
                    context.OrderTbls.Update(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The order does not already exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(int orderId)
        {
            try
            {
                OrderTbl order = GetOrderByID(orderId);
                if (order != null)
                {
                    using var context = new BookStorePRNContext();
                    context.OrderTbls.Remove(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The order does not already exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<OrderTbl> GetOrdersListByMemberId(int memberId)
        {
            var orders = new List<OrderTbl>();
            try
            {
                using var context = new BookStorePRNContext();
                orders = context.OrderTbls.Include(x => x.Member).Where(x => x.MemberId == memberId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }
    }
}
