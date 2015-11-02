using System;
using TradeDCs.Enums;

namespace TradeDCs.BO
{
    public class CTransaction
    {
        public const int LOT_SIZE = 10;

        #region members
        private int m_Quantity;
        #endregion

        #region properties
        /// <summary>
        /// Quantity of Digicoins
        /// </summary>
        public int Quantity
        {
            get { return m_Quantity; }
            set
            {
                if (m_Quantity != value)
                {
                    if (value % LOT_SIZE == 0 && value > 0)
                    {
                        /*if(value > MAX_AMOUNT)
                        {
                            value = MAX_AMOUNT;
                        }*/

                        m_Quantity = value;
                    }
                    else
                    {
                        throw new Exception("Quantity must be positive value and multiple of 10");
                    }
                }
            }
        }

        /// <summary>
        /// Client of transaction
        /// </summary>
        public CClient Client { get; set; }

        /// <summary>
        /// Type of transaction
        /// </summary>
        public ETransactionType TransactionType { get; set; }
        #endregion

        #region CTOR
        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="pTransaction"></param>
        public CTransaction(CTransaction pTransaction)
        {
            Quantity = pTransaction.Quantity;
            Client = pTransaction.Client;
            TransactionType = pTransaction.TransactionType;
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="pTransaction"></param>
        public CTransaction() { }
        #endregion
    }
}
