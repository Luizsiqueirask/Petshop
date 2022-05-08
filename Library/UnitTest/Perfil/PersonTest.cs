namespace Library.UnitTest.Perfil
{
    public static class PersonTest
    {
        public static bool AgeCheck(int age)
        {
            if (age >= 18)
            {
                return true;
            }
            return false;
        }
        public static bool EmailCheck(string email)
        {
            if (email.Contains("@"))
            {
                return true;
            }
            return false;
        }
        public static bool FirstNameCheck(string firstname)
        {
            if (firstname.Length < 3)
            {
                return false;
            }
            return true;
        }
    }
}
