using Microsoft.VisualStudio.TestTools.UnitTesting;
using QATest.Models;
using QATest.Services;

namespace E470_Code_Exercise
{
    [TestClass]
    public class UnitTests
    {
        //The program should be able to create a new Account with valid paramaters and the Constructor works as intended
        [TestMethod]
        [DataRow(1, "test", "one", 1.0)]
        [DataRow(7, "next", "sevennnnnnn", 7.2)]
        [DataRow(10000, "test", "one", 4.5)]
        [DataRow(-1, "test", "one", 0)]
        public void AccountCreation(int accountId, string firstName, string lastName, double value)
        {
            decimal tempValue = Convert.ToDecimal(value);
            var tempAccount = new Account(accountId, firstName, lastName, tempValue, new List<Payment>());
            Assert.IsTrue(tempAccount.Id == accountId);
            Assert.IsTrue(tempAccount.FirstName == firstName);
            Assert.IsTrue(tempAccount.LastName == lastName);
            Assert.IsTrue(tempAccount.Balance == tempValue);
            Assert.IsNotNull(tempAccount.Payments);
        }

        //The program should be able to create a new PaymentDto with valid paramaters and the Constructor works as intended
        [TestMethod]
        [DataRow(1, 1.0)]
        [DataRow(7, 7.2)]
        [DataRow(10000, 4.5)]
        [DataRow(-1, 0)]
        public void CreateAPaymentDto(int accountId, double value)
        {
            decimal tempValue = Convert.ToDecimal(value);
            var tempPaymentDto = new PaymentDto(accountId, tempValue);
            Assert.IsTrue(tempPaymentDto.AccountId == accountId);
            Assert.IsTrue(tempPaymentDto.Amount == tempValue);
        }

        //The program should be able to create a new Payment with valid paramaters and the Constructor works as intended
        [TestMethod]
        [DataRow(1, 1, 1.0)]
        [DataRow(7, 2, 7.2)]
        [DataRow(10000, 100000, 4.5)]
        [DataRow(-1, -1, -1.0)]
        public void CreateAPayment(int id, int accountId, double value)
        {
            decimal amount = Convert.ToDecimal(value);
            var tempPayment = new Payment(id, accountId, amount, DateTime.MinValue);
            Assert.IsTrue(tempPayment.Id == id);
            Assert.IsTrue(tempPayment.AccountId == accountId);
            Assert.IsTrue(tempPayment.Amount == amount);
            Assert.IsTrue(tempPayment.PaymentDate == DateTime.MinValue);
        }

        //The program should be able to create a new MakePaymentResult with valid paramaters and the Constructor works as intended
        [TestMethod]
        [DataRow(1, 1, 1.0)]
        [DataRow(5, 8, 13.7)]
        [DataRow(20000, 900000, 2.56)]
        [DataRow(-1, -1, -1.0)]
        public void CreateAMakePaymentResult(int id, int accountId, double value)
        {
            decimal amount = Convert.ToDecimal(value);
            var tempResult = new MakePaymentResult(new Payment(id, accountId, amount, DateTime.MinValue), PaymentStatus.Succeeded);
            Assert.IsNotNull(tempResult.Payment);
            Assert.IsTrue(tempResult.Payment.PaymentDate == System.DateTime.MinValue);
            Assert.IsTrue(tempResult.Payment.AccountId == accountId);
            Assert.IsTrue(tempResult.PaymentStatus == PaymentStatus.Succeeded);
        }

        //The program should be able to call the MakePayment Method with valid paramaters and return a successful payment
        [TestMethod]
        [DataRow(1, 1.0)]
        [DataRow(4, 12.21)]
        [DataRow(555555, 64.5)]
        [DataRow(-1, -1.0)]
        public void MakeAPayment(int accountId, double value)
        {
            decimal tempValue = Convert.ToDecimal(value);
            var tempPaymentDto = new PaymentDto(accountId, tempValue);
            AccountService accountService = new AccountService();
            var result = accountService.MakePayment(123, tempPaymentDto).Result;
            var tempTime = DateTime.Now;
            Assert.IsNotNull(result);
            if (result.PaymentStatus == PaymentStatus.PaymentExceedsBalance)
            {
                Assert.IsNull(result.Payment);
            }
            else
            {
                Assert.IsNotNull(result.Payment);
                Assert.IsTrue(result.PaymentStatus == PaymentStatus.Succeeded);
                Assert.IsTrue(result.Payment.PaymentDate.Date == tempTime.Date);
            }          
        }
    }
}
