using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();

        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<OrderDetail> GetOrderDetailsList()
        {
            var orderdetails = new List<OrderDetail>();
            try
            {
                using var context = new BookStorePRNContext();
                orderdetails = context.OrderDetails.Include(x => x.Book).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderdetails;
        }

        public OrderDetail GetOrderDetailByID(int orderId)
        {
            OrderDetail orderdetail = null;
            try
            {
                using var context = new BookStorePRNContext();
                orderdetail = context.OrderDetails.Include(x => x.Book).SingleOrDefault(m => m.OrderId == orderId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderdetail;
        }

        public void AddNew(OrderDetail orderdetail)
        {
            try
            {
                OrderDetail _orderdetail = GetOrderDetailByID(orderdetail.OrderId);
                if (_orderdetail == null)
                {
                    using var context = new BookStorePRNContext();
                    context.OrderDetails.Add(orderdetail);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The order detail is already exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(OrderDetail orderdetail)
        {
            try
            {
                OrderDetail _orderdetail = GetOrderDetailByID(orderdetail.OrderId);
                if (_orderdetail != null)
                {
                    using var context = new BookStorePRNContext();
                    context.OrderDetails.Update(orderdetail);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The order detail does not already exist");
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
                OrderDetail orderdetail = GetOrderDetailByID(orderId);
                if (orderdetail != null)
                {
                    using var context = new BookStorePRNContext();
                    context.OrderDetails.Remove(orderdetail);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The order detail does not already exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
