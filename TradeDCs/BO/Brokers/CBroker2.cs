
namespace TradeDCs.BO
{
    public class CBroker2 : CBroker
    {
        /// <summary>
        /// Returns the quote of the broker
        /// </summary>
        /// <returns></returns>
        public override double GetCommissionRate(int pQuantity)
        {
            if(pQuantity <= 40)
            {
                return 0.03;
            }
            else if(pQuantity <= 80)
            {
                return 0.025;
            }
            else
            {
                return 0.02;
            }
        }

        /// <summary>
        /// returns the commision rate of the broker
        /// </summary>
        /// <param name="pQuantity"></param>
        /// <returns></returns>
        public override double GetQuote()
        {
            return 1.52;
        }
    }
}
