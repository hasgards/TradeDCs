using System.Collections.Generic;
using TradeDCs.BO;
using TradeDCs.Enums;

namespace TradeDCs.ServerSide
{
    public class CTransactionsInvoice
    {
        /// <summary>
        /// Total Real Monney involved
        /// </summary>
        public CAmount TotalRealMonneyExchanged { get; private set; }

        /// <summary>
        /// Total Digicoins involved
        /// </summary>
        public CAmount TotalDigicoinsExchanged { get; private set; }

        /// <summary>
        /// List of transactions executed to fully complete client order
        /// </summary>
        public List<CTransactionExecution> TransactionsExecuted { get; private set; }

        /// <summary>
        /// constructor
        /// </summary>
        private CTransactionsInvoice() { }

        /// <summary>
        /// contructor with 
        /// </summary>
        /// <param name="pTransactionsExecuted">Transactions executed</param>
        public CTransactionsInvoice(List<CTransactionExecution> pTransactionsExecuted)
        {
            TransactionsExecuted = pTransactionsExecuted;
            TotalRealMonneyExchanged = new CAmount(0, ECurrencies.REAL_MONNEY);
            TotalDigicoinsExchanged = new CAmount(0, ECurrencies.DIGICOINS);
            foreach (CTransactionExecution transaction in pTransactionsExecuted)
            {
                TotalRealMonneyExchanged += transaction.RealMonneyExchanged;
                TotalDigicoinsExchanged += transaction.DigiCoinsExchanged;
            }
        }

    }
}
