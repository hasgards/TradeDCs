using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeDCs.BO;
using TradeDCs.Enums;

namespace TradeDCs.ServerSide
{
    public class CServer
    {
        #region private static members
        /// <summary>
        /// Max Qantity of digicoin per transaction
        /// </summary>
        private const int MAX_AMOUNT_PER_TRANSACTION = 100;

        /// <summary>
        /// Array of brokers to compare
        /// </summary>
        private static CBroker[] m_Brokers = new CBroker[2];
        #endregion

        #region public static properties
        /// <summary>
        /// Array of brokers to compare
        /// </summary>
        public static CBroker[] Brokers
        {
            get { return m_Brokers; }
        }
        #endregion

        #region private static methods
        /// <summary>
        /// Chack price of a single transactions in a broker
        /// </summary>
        /// <param name="pTransaction">Transaction</param>
        /// <param name="pBroker">Broker</param>
        /// <returns></returns>
        private static CTransactionExecution CheckSingleTransaction(CTransaction pTransaction, CBroker pBroker)
        {
            CAmount transactionAmount = pBroker.GetTransationAmount(pTransaction);

            CAmount digicoinsExchanged = new CAmount(pTransaction.Quantity, ECurrencies.DIGICOINS);
            CTransactionExecution transactionExecuted = new CTransactionExecution(pTransaction, pBroker, transactionAmount, digicoinsExchanged);

            return transactionExecuted;
        }
        #endregion

        #region public static methods
        /// <summary>
        /// Executes client order
        /// </summary>
        /// <param name="pTransaction">Transaction</param>
        /// <returns></returns>
        public static CTransactionsInvoice ExecuteTransaction(CTransaction pTransaction)
        {
            if (Brokers == null || !Brokers.Any())
            {
                throw new Exception("No broker specified");
            }

            List<CTransactionsInvoice> combinaisons = new List<CTransactionsInvoice>();

            //Cases of one simple transaction
            if (pTransaction.Quantity <= MAX_AMOUNT_PER_TRANSACTION)
            {
                combinaisons.Add(new CTransactionsInvoice(new List<CTransactionExecution> { CheckSingleTransaction(pTransaction, Brokers[0]) }));
                combinaisons.Add(new CTransactionsInvoice(new List<CTransactionExecution> { CheckSingleTransaction(pTransaction, Brokers[1]) }));
            }

            //Cases of combination of transactions (transaction splitted between two brookers)
            for (int i = CTransaction.LOT_SIZE; i <= MAX_AMOUNT_PER_TRANSACTION; i += CTransaction.LOT_SIZE)
            {
                //If max quanttity is not reached
                if(pTransaction.Quantity - i <= MAX_AMOUNT_PER_TRANSACTION && (pTransaction.Quantity - i) > 0)
                {
                    CTransaction transaction1 = new CTransaction(pTransaction);
                    CTransaction transaction2 = new CTransaction(pTransaction);
                    
                    //Split into 2 transactions
                    transaction1.Quantity = i;
                    transaction2.Quantity = pTransaction.Quantity - i;

                    List<CTransactionExecution> transactions = new List<CTransactionExecution>
                    {
                        CheckSingleTransaction(transaction1, Brokers[0]),
                        CheckSingleTransaction(transaction2, Brokers[1])
                    };

                    combinaisons.Add(new CTransactionsInvoice(transactions));
                }
            }

            //init best combinaison
            CTransactionsInvoice bestCombinaison = combinaisons[0];
            combinaisons.RemoveAt(0);

            foreach (CTransactionsInvoice combinaison in combinaisons)
            {
                //TODO : Why does lower price is the most interesting when we sell Digicoint ? here is code that get best transaction for clien
                if ((combinaison.TotalRealMonneyExchanged < bestCombinaison.TotalRealMonneyExchanged && pTransaction.TransactionType == ETransactionType.BUYS) //smaller amount is better when buying
                    || (combinaison.TotalRealMonneyExchanged > bestCombinaison.TotalRealMonneyExchanged && pTransaction.TransactionType == ETransactionType.SELLS)) //bigger amount is better when selling
                {
                    //better combinaison than previous
                    bestCombinaison = combinaison;
                }
            }

            //Executes order buy adding it in customer and broker histories
            foreach(CTransactionExecution transactionExecuted in bestCombinaison.TransactionsExecuted)
            {
                transactionExecuted.Broker.TransactionsExecuted.Add(transactionExecuted);
                pTransaction.Client.TransactionsExecuted.Add(transactionExecuted);
            }

            //returns the invoice
            return bestCombinaison;
        }
        #endregion

    }
}
