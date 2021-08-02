﻿namespace InventoryManagement.Application.Contract.Inventory
{
    public class ReduceInventory
    {
        public long Count { get; set; }
        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public string Description { get; set; }
    }
}