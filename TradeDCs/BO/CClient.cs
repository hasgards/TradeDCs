using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeDCs.BO
{
    public class CClient
    {
        #region overrides

        /// <summary>
        /// Returns the name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region public properties
        /// <summary>
        /// Name of client
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of executed transactions by client
        /// </summary>
        public List<CTransactionExecution> TransactionsExecuted { get; private set; }

        /// <summary>
        /// Net position of client
        /// </summary>
        public decimal NetPosition
        {
            get
            {
                decimal sumOfPriceOfTransactions = 0;
                foreach (CTransactionExecution transaction in TransactionsExecuted.Where(t => t.Transaction.TransactionType == Enums.ETransactionType.BUYS))
                {
                    sumOfPriceOfTransactions += transaction.RealMonneyExchanged.Amount;
                }
                foreach (CTransactionExecution transaction in TransactionsExecuted.Where(t => t.Transaction.TransactionType == Enums.ETransactionType.SELLS))
                {
                    sumOfPriceOfTransactions -= transaction.RealMonneyExchanged.Amount;
                }
                return sumOfPriceOfTransactions;
            }
        }

        #endregion

        #region CTOR
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pName"></param>
        public CClient(string pName)
        {
            Name = pName;
            TransactionsExecuted = new List<CTransactionExecution>();
        }
        #endregion
    }
}
