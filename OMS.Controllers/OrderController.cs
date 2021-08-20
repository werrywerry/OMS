using OMS.Domain;
using OMS.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Controllers
{
    public class OrderController
    {

        private readonly OrderRepository _orderRepository = new OrderRepository();
        private readonly StockRepository _stockRepository = new StockRepository();
        private static OrderController _instance;

        private OrderController(){}

        public static OrderController Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new OrderController(); 
                }
                return _instance; 
            }
        }

        public IEnumerable<OrderHeader> GetOrderHeaders()
        {
            return _orderRepository.GetOrderHeaders(); 
        }
        public OrderHeader CreateNewOrderHeader()
        {
            var order = _orderRepository.InsertOrderHeader();
            return order; 
        }
        public OrderHeader UpsertOrderItem(int orderHeaderId,int stockItemId,int quantity)
        {
            var stockItem = _stockRepository.GetStockItem(stockItemId);
            var order = _orderRepository.GetOrderHeader(orderHeaderId);
            var item = order.AddOrUpdateOrderItem(stockItem.Id, stockItem.Price, stockItem.Name, quantity);
            _orderRepository.UpsertOrderItem(item);
            return order; 
        }
        public OrderHeader SubmitOrder(int orderHeaderId)
        {
            var order = _orderRepository.GetOrderHeader(orderHeaderId);
            order.Submit();
            _orderRepository.UpdateOrderState(order);
            return order; 
        }


        public OrderHeader ProcessOrder(int orderHeaderId)
        {    
            try
            {
                var order = _orderRepository.GetOrderHeader(orderHeaderId);
                try
                {
                    _stockRepository.DecrementOrderStockItemAmount(order);
                    order.Complete(); 
                }
                catch(SqlException ex)
                {
                    if(ex.Number == 547)
                    {
                        order.Reject(); 
                    }
                }
                _orderRepository.UpdateOrderState(order);
                return order; 
            }          
            catch(InvalidOrderStateException ex)
            {
                throw ex; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOrderHeaderAndOrderItems(int orderHeaderId)
        {
            _orderRepository.DeleteOrderHeaderAndOrderItems(orderHeaderId);
        }

        public OrderHeader DeleteOrderItem(int orderHeaderId, int stockItemId)
        {
            _orderRepository.DeleteOrderItem(orderHeaderId, stockItemId);
            return _orderRepository.GetOrderHeader(orderHeaderId);
        }


        public void ResetDatabase()
        {
            _orderRepository.ResetDatabase(); 
        }

    }
}
