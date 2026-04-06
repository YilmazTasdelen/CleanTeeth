using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTeeth.Tests.Domain.ValueObjects
{

    [TestClass]
    public class EmailTests
    {

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_NullEmail_Throws()
        {
            new Email(null!);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_EmailWithoutAt_Throws()
        {
            new Email("yilmaz.com");
        }


        [TestMethod]
        public void Constructor_ValidEmail_Throws()
        {
            new Email("yilmaz@test.com");
        }
    }
}
