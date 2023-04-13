namespace BookStore.Controller
{
    using BookStore.DB;
    using BookStore.View;
    using BookStore.Model;

    public class ISPatron
    {
        public static void CheckExists(int id)
        {
            Patron patron = Queries.SelectPatron(id);
            if (patron != null)
            {
                throw new System.ArgumentException($"Patron {id} exist");
            }
        }

        public static void CheckStatus(int id, bool check)
        {
            Patron patron = Queries.SelectPatron(id);
            // check status of patron
            if (patron == null)
            {
                throw new System.ArgumentException($"Patron does not exist");
            }

            DateTime expired = patron.Register.AddMonths(patron.MemberStatus);
            // check if patron has unpaid fee
            decimal totalFee = Utility.CalculateTotalFee(id);

            if (expired < DateTime.Now && check == true)
            {
                throw new System.ArgumentException($"Patron, subscription expired");
            }
            else if (totalFee > 0 && check == true)
            {
                Commandline.ShowInfo($"Patron has unpaid fee {totalFee}");
                throw new System.ArgumentException("Patron, has unpaid fee");
            }

        }
    }
}