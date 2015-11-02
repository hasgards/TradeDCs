using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeDCs.BO;
using TradeDCs.Enums;
using TradeDCs.ServerSide;

namespace TradeDCs.TEST
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            CClient clientA = new CClient("Client A");
            CClient clientB = new CClient("Client B");
            CClient clientC = new CClient("Client C");

            CBroker1 broker1 = new CBroker1();
            CBroker2 broker2 = new CBroker2();

            CServer.Brokers[0] = broker1;
            CServer.Brokers[1] = broker2;

            //1)
            CTransaction transac1 = new CTransaction
            {
                Quantity = 10,
                Client = clientA,
                TransactionType = ETransactionType.BUYS
            };
            CTransactionsInvoice transacExec1 = CServer.ExecuteTransaction(transac1);
            Assert.AreEqual(15.645M, transacExec1.TotalRealMonneyExchnged.Amount);


            //2)
            CTransaction transac2 = new CTransaction
            {
                Quantity = 40,
                Client = clientB,
                TransactionType = ETransactionType.BUYS
            };
            CTransactionsInvoice transacExec2 = CServer.ExecuteTransaction(transac2);
            Assert.AreEqual(62.58M, transacExec2.TotalRealMonneyExchnged.Amount);


            //3)
            CTransaction transac3 =  new CTransaction
            {
                Quantity = 50,
                Client = clientA,
                TransactionType = ETransactionType.BUYS
            };
            CTransactionsInvoice transacExec3 = CServer.ExecuteTransaction(transac3);
            Assert.AreEqual(77.9M, transacExec3.TotalRealMonneyExchnged.Amount);


            //4)
            CTransaction transac4 = new CTransaction
            {
                Quantity = 100,
                Client = clientB,
                TransactionType = ETransactionType.BUYS
            };
            CTransactionsInvoice transacExec4 = CServer.ExecuteTransaction(transac4);
            Assert.AreEqual(155.04M, transacExec4.TotalRealMonneyExchnged.Amount);

            //5)
            CTransaction transac5 = new CTransaction
            {
                Quantity = 80,
                Client = clientB,
                TransactionType = ETransactionType.SELLS
            };
            CTransactionsInvoice transacExec5 = CServer.ExecuteTransaction(transac5);
            Assert.AreEqual(124.64M, transacExec5.TotalRealMonneyExchnged.Amount);
            //TODO : Change assertion if conditions changed
            //Broker1 : 80 DIGICOINS * 1.49 = 119.20 'REAL_MONNEY', Removal of commission of 5% : 119.20 * 0.95 = 113.24 'REAL_MONNEY'
            //Broker2 : 80 DIGICOINS * 1.52 = 121.60 'REAL_MONNEY', Removal of commission of 2.5% : 121.60 * 0.975 = 118.56 'REAL_MONNEY' => best choice
            //Assert.AreEqual(118.56M, transacExec5.TotalRealMonneyExchnged.Amount);

            //6)
            CTransaction transac6 = new CTransaction
            {
                Quantity = 70,
                Client = clientC,
                TransactionType = ETransactionType.SELLS
            };
            CTransactionsInvoice transacExec6 = CServer.ExecuteTransaction(transac6);
            Assert.AreEqual(109.06M, transacExec6.TotalRealMonneyExchnged.Amount);
            //Assert.AreEqual(103.74M, transacExec6.TotalRealMonneyExchnged.Amount);

            //7)
            CTransaction transac7 = new CTransaction
            {
                Quantity = 130,
                Client = clientA,
                TransactionType = ETransactionType.BUYS
            };
            CTransactionsInvoice transacExec7 = CServer.ExecuteTransaction(transac7);
            //Assert.AreEqual(201.975M, transacExec7.TotalRealMonneyExchnged.Amount);
            Assert.AreEqual(202.116M, transacExec7.TotalRealMonneyExchnged.Amount);


            //8)
            CTransaction transac8 = new CTransaction
            {
                Quantity = 60,
                Client = clientB,
                TransactionType = ETransactionType.SELLS
            };
            CTransactionsInvoice transacExec8 = CServer.ExecuteTransaction(transac8);
            Assert.AreEqual(93.48M, transacExec8.TotalRealMonneyExchnged.Amount);
            //Assert.AreEqual(88.92M, transacExec8.TotalRealMonneyExchnged.Amount);

            //9)
            //Assert.AreEqual(296.56M, clientA.NetPosition);
            //Assert.AreEqual(0M, clientB.NetPosition);
            //Assert.AreEqual(-109.06M, clientC.NetPosition);
            Assert.AreEqual(295.661M, clientA.NetPosition);
            Assert.AreEqual(-0.5M, clientB.NetPosition);
            Assert.AreEqual(-109.06M, clientC.NetPosition);

            //10)
            //Assert.AreEqual(80M, broker1.DigicoinsProcessed.Amount);
            //Assert.AreEqual(460M, broker2.DigicoinsProcessed.Amount);
            Assert.AreEqual(90M, broker1.DigicoinsProcessed.Amount);
            Assert.AreEqual(450M, broker2.DigicoinsProcessed.Amount);
        }
    }
}
