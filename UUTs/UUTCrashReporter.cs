using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UUT.Common.CrashReporter
{
    [TestClass]
    public class CrashReporter
    {
        private QoSCalc.Common.CrashReporter cr;
        private Exception exept;
        //ExpectedException
        #region Test Init
        [AssemblyInitialize( )]
        public static void AssemblyInit (TestContext context)
        {
            //MessageBox.Show("AssemblyInit " + context.TestName);
        }

        [ClassInitialize( )]
        public static void ClassInit (TestContext context)
        {
            //MessageBox.Show("ClassInit " + context.TestName);
        }
        [TestInitialize]
        public void TestPreperations ( )
        {
            // Preperation, make sure no files exist at test start
            if (QoSCalc.Common.CrashReporter.CrashReportExist)
                QoSCalc.Common.CrashReporter.RemoveCrashReport( );
            // check if no report exist in the beginning
            Assert.IsFalse(QoSCalc.Common.CrashReporter.CrashReportExist, "CrashReport exist already, cant test creation.");
            System.IO.File.Delete(System.IO.Path.Combine(Environment.CurrentDirectory, "crash2.txt"));
            Assert.IsFalse(System.IO.File.Exists(System.IO.Path.Combine(Environment.CurrentDirectory, "crash2.txt")), "CrashReport copy exist already.");
            cr = new QoSCalc.Common.CrashReporter( );

            exept = new Exception("Level 1 Exception");
            exept.Source = "UUT";
            for (int i = 2; i < 25; i++)
            {
                exept = new Exception(String.Format("Level {0} Exception", i), exept);
                exept.Source = "UUT";
            }
        }
        #endregion
        #region Test Cleanup
        [TestCleanup( )]
        public void Cleanup ( )
        {
            //MessageBox.Show("TestMethodCleanup");
        }

        [ClassCleanup( )]
        public static void ClassCleanup ( )
        {
            //MessageBox.Show("ClassCleanup");
        }

        [AssemblyCleanup( )]
        public static void AssemblyCleanup ( )
        {
            if (QoSCalc.Common.CrashReporter.CrashReportExist)
                QoSCalc.Common.CrashReporter.RemoveCrashReport( );
            Assert.IsFalse(QoSCalc.Common.CrashReporter.CrashReportExist, "CrashReport could not be deleted.");
            System.IO.File.Delete(System.IO.Path.Combine(Environment.CurrentDirectory, "crash2.txt"));
            Assert.IsFalse(System.IO.File.Exists(System.IO.Path.Combine(Environment.CurrentDirectory, "crash2.txt")), "CrashReport copy could not be deleted.");
        }
        #endregion
        #region TestMethods
        [TestMethod]
        [TestProperty("Group", "Common")]
        public void CreateCrashReport ( )
        {
            // create a simple report and remove it after wards
            cr.CreateCrashReport( );
            Assert.IsTrue(QoSCalc.Common.CrashReporter.CrashReportExist, "CrashReport could not be created.");
            QoSCalc.Common.CrashReporter.RemoveCrashReport( );
            Assert.IsFalse(QoSCalc.Common.CrashReporter.CrashReportExist, "CrashReport could not be deleted.");

            // create a report with exceptions 
            cr.CreateCrashReport(exept);
            Assert.IsTrue(QoSCalc.Common.CrashReporter.CrashReportExist, "CrashReport+Exception could not be created.");
            QoSCalc.Common.CrashReporter.RemoveCrashReport( );
            Assert.IsFalse(QoSCalc.Common.CrashReporter.CrashReportExist, "CrashReport could not be deleted.");

            // create a simple report and remove it after wards
            cr.CreateCrashReport("UnitUnderTest", exept);
            Assert.IsTrue(QoSCalc.Common.CrashReporter.CrashReportExist, "CrashReport+Excption+Source could not be created.");
            QoSCalc.Common.CrashReporter.RemoveCrashReport( );
            Assert.IsFalse(QoSCalc.Common.CrashReporter.CrashReportExist, "CrashReport could not be deleted.");

        }
        [TestMethod]
        [TestProperty("Group", "Common")]
        public void SaveCrashReport ( )
        {
            // create a simple report and remove it after wards
            cr.CreateCrashReport(exept);
            Assert.IsTrue(QoSCalc.Common.CrashReporter.CrashReportExist, "CrashReport could not be created.");

            QoSCalc.Common.CrashReporter.SaveReport(System.IO.Path.Combine(Environment.CurrentDirectory, "crash2.txt"));
            Assert.IsTrue(System.IO.File.Exists(System.IO.Path.Combine(Environment.CurrentDirectory, "crash2.txt")), "CrashReport not saved in target location.");
        }
        [TestMethod]
        [TestProperty("Group", "Common")]
        public void MoveCrashReport ( )
        {
            // create a simple report and remove it after wards
            cr.CreateCrashReport(exept);
            Assert.IsTrue(QoSCalc.Common.CrashReporter.CrashReportExist, "CrashReport could not be created.");

            QoSCalc.Common.CrashReporter.MoveReport(System.IO.Path.Combine(Environment.CurrentDirectory, "crash2.txt"));
            Assert.IsTrue(System.IO.File.Exists(System.IO.Path.Combine(Environment.CurrentDirectory, "crash2.txt")), "CrashReport not saved in target location.");

        }
        [TestMethod]
        [TestProperty("Group", "Common")]
        public void ReadCrashReport ( )
        {
            // create a simple report and remove it after wards
            cr.CreateCrashReport(exept);
            Assert.IsTrue(QoSCalc.Common.CrashReporter.CrashReportExist, "CrashReport could not be created.");

            QoSCalc.Common.CrashReport report = QoSCalc.Common.CrashReporter.Report;

        }
        #endregion

    }
}
