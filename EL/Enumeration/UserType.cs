namespace EL.Enumeration
{
    public class UserType
    {

        public static UserType Company = new UserType();
        public static UserType Person  = new UserType();

        private int _type;
        public int Type => _type;
        
    }
}