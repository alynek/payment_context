using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Entities;

[TestClass]
public class StudentTests
{
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;
        private readonly Student _student;

        public StudentTests()
        {
            _name = new Name("Bilbo", "Baggins");
            _document = new Document("12332112111",  EDocumentType.CPF);
            _email = new Email("bilbo.baggins@gmail.com");
            _address = new Address("Baggin's street", "12", "Center", "Shire", "Eriador", "Arnor", "1298988");
            _student = new Student(_name, _document, _email);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment
            (
                "124343", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, 
                _address, _student.Document, "Bilbo Baggins", _student.Email
            );

            subscription.AddPayment(payment);
            _student.AddSubscription(subscription);
            _student.AddSubscription(subscription);
            
            Assert.IsTrue(!_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            
            var subscription = new Subscription(null);
            _student.AddSubscription(subscription);
            
            Assert.IsTrue(!_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment
            (
                "124343", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, 
                _address, _student.Document, "Bilbo Baggins", _student.Email
            );

            subscription.AddPayment(payment);
            _student.AddSubscription(subscription);
            
            Assert.IsTrue(_student.IsValid);
        }  
    }