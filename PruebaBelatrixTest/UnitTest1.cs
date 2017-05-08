using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PruebaBelatrix;

namespace PruebaBelatrixTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LogToConsoleTest()
        {
            Assert.AreEqual(JobLoggerJuanBazan.LogMessage("MensajeDB", TypesEnum.LogTo.Console, TypesEnum.TypeMessage.Error), TypesEnum.LogTo.Console);
        }

        [TestMethod]
        public void LogToDatabaseTest()
        {
            Assert.AreEqual(JobLoggerJuanBazan.LogMessage("MensajeDB", TypesEnum.LogTo.Database, TypesEnum.TypeMessage.Error), TypesEnum.LogTo.Database);
        }

        [TestMethod]
        public void LogToFileTest()
        {
            Assert.AreEqual(JobLoggerJuanBazan.LogMessage("MensajeDB", TypesEnum.LogTo.File, TypesEnum.TypeMessage.Error), TypesEnum.LogTo.File);
        }
    }
}
