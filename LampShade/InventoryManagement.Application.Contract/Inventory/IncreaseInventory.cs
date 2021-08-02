namespace InventoryManagement.Application.Contract.Inventory
{
    public class IncreaseInventory
    {
        public long Count { get;  set; }
        public long OperatorId { get;  set; }
        public string Description { get; set; }

    }
}
