using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Entities;

[TestClass]
public class StudentTests
{
    [TestMethod]
    public void TestMethod1()
    {
        var subscription = new Subscription(null);
        var student = new Student("Gandalf", "The White", "123321123", "gandalf@outlook.com");
        student.AddSubscription(subscription);
    }
}