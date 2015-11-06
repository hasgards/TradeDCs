using System.Collections.Generic;
using TradeDCs.Enums;

namespace TradeDCs.BO
{
    public abstract class CBroker
    {
        #region public properties
        /// <summary>
        /// Total of digicoins processed
        /// </summary>
        public CAmount DigicoinsProcessed
        {
            get
            {
                CAmount result = new CAmount(0, ECurrencies.DIGICOINS);

                foreach(CTransactionExecution transaction in TransactionsExecuted)
                {
                    result += transaction.DigiCoinsExchanged;
                }

                return result;
            }
        }

        public CAmount RealMonneyProcessed
        {
            get
            {
                CAmount result = new CAmount(0, ECurrencies.REAL_MONNEY);

                foreach (CTransactionExecution transaction in TransactionsExecuted)
                {
                    result += transaction.RealMonneyExchanged;
                }

                return result;
            }
        }

        public List<CTransactionExecution> TransactionsExecuted { get; private set; }
        #endregion

        #region abstract methods
        /// <summary>
        /// Returns the quote of the broker
        /// </summary>
        /// <returns></returns>
        public abstract double GetQuote();

        /// <summary>
        /// returns the commision rate of the broker
        /// </summary>
        /// <param name="pQuantity"></param>
        /// <returns></returns>
        public abstract double GetCommissionRate(int pQuantity);
        #endregion

        #region public methods
        /// <summary>
        /// Get the real monney amount that will be exchanged for a transaction
        /// </summary>
        /// <param name="pTransaction">Transaction</param>
        /// <returns>Amount of real monney</returns>
        public CAmount GetTransationAmount(CTransaction pTransaction)
        {
            switch (pTransaction.TransactionType)
            {
                case ETransactionType.BUYS:
                    return new CAmount((decimal)((pTransaction.Quantity * GetQuote()) * (1 + GetCommissionRate(pTransaction.Quantity))), ECurrencies.REAL_MONNEY);
                default:
                    //TODO : Why does the broker gives his commission here instead of taking it ? Here is corrected version of the formula
                    //return new CAmount((decimal)((pTransaction.Quantity * GetQuote()) * (1 + GetCommissionRate(pTransaction.Quantity))), ECurrencies.REAL_MONNEY);
                    return new CAmount((decimal)((pTransaction.Quantity * GetQuote()) * (1 - GetCommissionRate(pTransaction.Quantity))), ECurrencies.REAL_MONNEY);
            }
        }
        #endregion

        #region CTOR
        /// <summary>
        /// build new object
        /// </summary>
        public CBroker()
        {
            TransactionsExecuted = new List<CTransactionExecution>();
        }
        #endregion
    }
}
