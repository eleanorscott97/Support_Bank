namespace Support_Bank
{
    public class Account
    {
        public string Name;
        public decimal Balance;

        public void AddDebt(decimal debt)
        {
            Balance = Balance - debt;
        }
        public void GainMonies(decimal money)
        {
            Balance = Balance + money;
        }
            
    }
}
