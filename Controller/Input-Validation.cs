namespace BookStore.Controller
{
        using BookStore.View;
        public class InputValidation
        {
                public static void IsIDValid(int id)
                {
                        // length lower than 4 is not valid
                        if (id.ToString().Length >= 4)
                        {
                                throw new System.ArgumentException("ID must be 4 digits");

                        }
                        if (id < 0)
                        {
                                throw new System.ArgumentException("ID cannot be negative");
                        }
                }

                public static void IsNameValid(string name)
                {
                        if (name == null)
                        {
                                throw new System.ArgumentException("Name cannot be null");
                        }else if (name.Any(char.IsDigit))
                        {
                                throw new System.ArgumentException("Name cannot contain numbers");
                        }else if (name.Length < 2)
                        {
                                throw new System.ArgumentException("Name must be at least 2 characters");
                        }else if (name.Length > 20)
                        {
                                throw new System.ArgumentException("Name cannot be longer than 20 characters");
                        }else if (name.Any(char.IsPunctuation))
                        {
                                throw new System.ArgumentException("Name cannot contain punctuation");
                        }else if (name.Any(char.IsSymbol))
                        {
                                throw new System.ArgumentException("Name cannot contain symbols");
                        }else if (name.Any(char.IsWhiteSpace))
                        {
                                throw new System.ArgumentException("Name cannot contain whitespace");
                        } 
                }

                public static void IsAvailabilityValid(int number)
                {
                        if (number != 1 && number != -1)
                                throw new System.ArgumentException("Availability must be 1 or -1");
                        
                }

                public static void IsMembershipValid(int number)
                {
                        if (number != 1 && number != 3 && number != 6 && number != 12)
                                throw new System.ArgumentException("Membership must be 1, 3, 6, or 12");
                        
                }

        }       
}