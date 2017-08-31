namespace KitchenService.Schema
{
    public class ShoppingListItem
    {
        public long Id { get; set; }

        public float Amount { get; set; }

        public float TotalAmount { get; set; }

        public bool IsDone { get; set; }

        public bool IsOptional { get; set; }

        public Item Item { get; set; }
    }
}
