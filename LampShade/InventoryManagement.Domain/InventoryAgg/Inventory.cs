using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool InStock { get; private set; }
        public List<InventoryOperation> Operations { get; private set; }
        protected Inventory()
        {
        }
        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
            Operations = new List<InventoryOperation>();
        }

        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }

        public long CalculateCurrentCount()
        {
            var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
            var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - minus;
        }

        public void Increase(long count , long operatorId , string description)
        {
            var currentCount = CalculateCurrentCount() + count;
            var inventoryOperation = new InventoryOperation(true, count, operatorId, currentCount, description, 0, Id);
            Operations.Add(inventoryOperation);
            InStock = currentCount > 0;
        }
        public void Reduce(long count, long operatorId, string description, long orderId)
        {
            var currentCount = CalculateCurrentCount() - count;
            var inventoryOperation = new InventoryOperation(false, count, operatorId, currentCount, description, orderId, Id);
            Operations.Add(inventoryOperation);
            InStock = currentCount > 0;
        }
    }
}
