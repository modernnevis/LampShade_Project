using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application.Inventory
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;

        }

        public OperationResult Create(CreateInventory command)
        {
            var operationResult = new OperationResult();
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId))
                return operationResult.Failed(ApplicationMessages.Duplicate);

            var inventory = new Domain.InventoryAgg.Inventory(command.ProductId, command.UnitPrice);
            _inventoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operationResult = new OperationResult();
            var inventory = _inventoryRepository.Get(command.Id);

            if (inventory == null)
                return operationResult.Failed(ApplicationMessages.NotFound);
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id!=command.Id))
                return operationResult.Failed(ApplicationMessages.Duplicate);
            
            inventory.Edit(command.ProductId,command.UnitPrice);
            _inventoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operationResult = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);

            if (inventory == null)
                return operationResult.Failed(ApplicationMessages.NotFound);

            const long  operatorId = 1;
            inventory.Increase(command.Count, operatorId, command.Description);
            _inventoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operationResult = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);

            if (inventory == null)
                return operationResult.Failed(ApplicationMessages.NotFound);

            const long operatorId = 1;
            inventory.Reduce(command.Count, operatorId, command.Description,command.OrderId);
            _inventoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operationResult = new OperationResult();

            const long operatorId = 1;
            foreach (var item in command)
            {
                var inventory = _inventoryRepository.Get(item.InventoryId);
                inventory.Reduce(item.Count, operatorId, item.Description, item.OrderId);
            }

            _inventoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }
    }
}
