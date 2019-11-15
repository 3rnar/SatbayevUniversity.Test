using S.U.TEST.Models;

namespace S.U.TEST.Repositories
{
    public interface IItemsRepository : IRepositoryBase<Item>
    {
        void DeleteItem(int id);
        Item GetByItemName(string itemName);
    }
}