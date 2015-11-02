using System;
using TradeDCs.Enums;

namespace TradeDCs.BO
{
    public class CAmount
    {
        #region static private
        /// <summary>
        /// Check if two amounts are comparable
        /// </summary>
        /// <param name="pAmount1"></param>
        /// <param name="pAmount2"></param>
        private static void CheckCompare(CAmount pAmount1, CAmount pAmount2)
        {
            if (pAmount1.Currency != pAmount2.Currency)
            {
                throw new ArithmeticException("Cannot compare two amounts associated with different currencies");
            }
        }

        #endregion

        #region operators overload

        public static bool operator <(CAmount pAmount1, CAmount pAmount2)
        {
            CheckCompare(pAmount1, pAmount2);
            return pAmount1.Amount < pAmount2.Amount;
        }

        public static bool operator >(CAmount pAmount1, CAmount pAmount2)
        {
            CheckCompare(pAmount1, pAmount2);
            return pAmount1.Amount > pAmount2.Amount;
        }

        public static CAmount operator +(CAmount pAmount1, CAmount pAmount2)
        {
            CheckCompare(pAmount1, pAmount2);
            return new CAmount(pAmount1.Amount + pAmount2.Amount, pAmount1.Currency);
        }

        public override string ToString()
        {
            return Amount.ToString() + " " + Currency.ToString();
        }

        #endregion

        #region properties
        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Currency of the amount
        /// </summary>
        public ECurrencies Currency { get; private set; }
        #endregion

        #region CTOR
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pAmount"></param>
        /// <param name="pCurrency"></param>
        public CAmount(decimal pAmount, ECurrencies pCurrency)
        {
            Amount = pAmount;
            Currency = pCurrency;
        }
        #endregion
    }
}
