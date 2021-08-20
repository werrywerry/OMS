using OMS.Domain;
using OMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Controllers
{
    public class StockItemController
    {

        private readonly StockRepository _stockRepository = new StockRepository(); 

        private static StockItemController _instance;

        private StockItemController(){}

        public static StockItemController Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new StockItemController();                     
                }
                return _instance; 
            }
        }

        public IEnumerable<StockItem> GetStockItems()
        {
            return _stockRepository.GetStockItems(); 
        }

        public StockItem GetStockItem(int id)
        {
            return _stockRepository.GetStockItem(id); 
        }


    }
}
