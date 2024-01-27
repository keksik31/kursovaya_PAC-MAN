using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MySql.Data.MySqlClient;
using Pac_Man;
using System.Reflection;
using Pac_Man;

namespace Pac_Man.Tests
{
    [TestClass]
    public class AuthenticationTests
    {
        [TestMethod()]
        [DataRow("новый", "юзер", "новенький", "юзерок", true)]
        [DataRow("новый", "юзер", "новенький", "юзерок", false)]
        public void TestSuccessfulRegistration(string LogTest, string PassTest, string NameTest, string SurnameTest, bool expected)
        {

            //Act
            DB db = new DB();
            db.InitializeDatabase();
            bool actual = db.RegisterUser(LogTest, PassTest, NameTest, SurnameTest);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow("agent", "007", true)]
        [DataRow("agent", "123", false)]
        [DataRow("nevozmoshno", "nevozmoshno", false)]
        public void TestSuccessfulLogin(string LogTest, string PassTest, bool expected)
        {

            //Act
            DB db = new DB();
            bool actual = db.ValidateLogin(LogTest, PassTest);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow("Иван", true)]
        [DataRow("привет", false)]
        public void TestSuccessfulDelete(string LogTest, bool expected)
        {

            //Act
            DB db = new DB();
            bool actual = db.DeleteUser(LogTest);

            //Assert
            Assert.AreEqual(expected, actual);
        }




    }
}