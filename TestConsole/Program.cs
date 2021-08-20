using OMS.Controllers;
using OMS.Domain;
using OMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {

        static void Main(string[] args)
        {

            //var stock = StockItemController.Instance.GetStockItems();

            //var order = OrderController.Instance.CreateNewOrderHeader(); 

            //foreach(var s in stock)
            //{
            //    order = OrderController.Instance.UpsertOrderItem(order.Id, s.Id, 1);
            //}

            //order = OrderController.Instance.SubmitOrder(order.Id);
            //order = OrderController.Instance.ProcessOrder(order.Id); 

            OrderHeader oh = new OrderHeader(1, new DateTime(), 1);
            oh.AddOrUpdateOrderItem(100, 100, "Desc1", 10);
            oh.AddOrUpdateOrderItem(100, 100, "Desc1", 20);
            Console.WriteLine(oh.OrderItems[0].Description + " " + oh.OrderItems[0].Quantity);

    
            Console.ReadKey(); 
        }
    }
}
