namespace BuyHouse.DAL.Entities.ApplicationUserEntities
{
    public class UserAvatar
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Path { get; set; }

        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
