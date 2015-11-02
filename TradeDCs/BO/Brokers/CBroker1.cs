namespace TradeDCs.BO
{
    public class CBroker1 : CBroker
    {
        /// <summary>
        /// Returns the quote of the broker
        /// </summary>
        /// <returns></returns>
        public override double GetCommissionRate(int pQuantity)
        {
            return 0.05;
        }

        /// <summary>
        /// returns the commision rate of the broker
        /// </summary>
        /// <param name="pQuantity"></param>
        /// <returns></returns>
        public override double GetQuote()
        {
            return 1.49;
        }
    }
}
