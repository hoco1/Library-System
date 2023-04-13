namespace BookStore.Model
{
    public class Patron
    {
        public int PatronId { get; set; }
        public string Name { get; set; }
        public DateTime Register { get; set; }
        public int MemberStatus { get; set; }

    }
}