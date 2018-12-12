using BzStruc.Facade.Implement;
using BzStruc.Repository.DAL;
using BzStruc.Repository.Models;
using Moq;
using System;
using Xunit;

namespace XUnitTestProject1
{ 
    public class UnitTest1
    {
        Mock<MsSql1DbContext> msSql1DbContext;
        [Fact]
        public void TestInterlocutorFacade()
        {
            msSql1DbContext = new Mock<MsSql1DbContext>();
            //msSql1DbContext.Object.Set<Logs>().Add();
            var InterlocutorFacadeTest = new InterlocutorFacade(msSql1DbContext.Object);
             
            Assert.Equal(1, 1);
        }
    }
}
