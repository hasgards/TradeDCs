
namespace TradeDCs.BO
{
    public class CTransactionExecution
    {
        #region public properties
        /// <summary>
        /// trasaction executed
        /// </summary>
        public CTransaction Transaction { get; private set; }

        /// <summary>
        /// broker choosen
        /// </summary>
        public CBroker Broker { get; private set; }

        /// <summary>
        /// total digicoins involved
        /// </summary>
        public CAmount DigiCoinsExchanged { get; private set; }

        /// <summary>
        /// total real monney involved
        /// </summary>
        public CAmount RealMonneyExchanged { get; private set; }
        #endregion

        #region CTOR
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="pTransaction"></param>
        /// <param name="pBroker"></param>
        /// <param name="pRealMonneyExchanged"></param>
        /// <param name="pDigiCoinsExchanged"></param>
        internal CTransactionExecution(CTransaction pTransaction, CBroker pBroker, CAmount pRealMonneyExchanged, CAmount pDigiCoinsExchanged)
        {
            Transaction = pTransaction;
            Broker = pBroker;
            RealMonneyExchanged = pRealMonneyExchanged;
            DigiCoinsExchanged = pDigiCoinsExchanged;
        }
        #endregion
    }
}
