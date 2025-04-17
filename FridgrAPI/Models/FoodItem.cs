namespace FridgrAPI.Models
{
    public class FoodItem
    {
        public int FoodItemId { get; set; }
        public string? FoodItemName { get; set; }
        public string? FoodItemNote { get; set; }
        public string? FoodItemQuantity { get; set; }
        public string? FoodItemUnit { get; set; }
        public string? FoodItemImageUrl { get; set; }
        public string? ExpiryDate { get; set; }
        public string? AddedDate { get; set; }
        public int? SpaceId { get; set; }
    }
}
