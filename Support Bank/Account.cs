namespace Support_Bank
{
    public class Account
    {
        public string Name;
        private int Balance;

        public void AddDebt(int debt)
        {
            Balance = Balance - debt;
        }
    }
}
