using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JSTest;
using JSTest.ScriptElements;
using JSTest.ScriptLibraries;

namespace CMPUnitTests
{
    /// <summary>
    /// Summary description for objQueryToolsTests
    /// </summary>
    [TestClass]
    public class objQueryToolsTests
    {
        private readonly TestScript _objTestScript = new TestScript();

        public objQueryToolsTests()
        {
            _objTestScript.AppendFile(@"JavaScript\objectQueryTools.js");
            _objTestScript.AppendBlock(new JsAssertLibrary());
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void NoObjectOrConditions_ReturnFalse()
        {
            _objTestScript.RunTest(@"assert.equal(false, [{id: 1, name: 'J Smith', department: 001}].AnyMatch());", true);
        }

        [TestMethod]
        public void ExactObjectExistsInArrayNoConditions_ReturnTrue()
        {
            _objTestScript.AppendBlock(@" var emp = {id: 1, name: 'J Smith', department: 001}; var emps = [emp]; ");
            _objTestScript.RunTest(@"assert.equal(true, emps.AnyMatch(emp));", true);
        }

        [TestMethod]
        public void ObjectWithConditionExistInArray_NoObjectSupplied_ReturnTrue()
        {
            _objTestScript.AppendBlock(@" var emp0 = {id: 0, name: '', department: 001}; var emp = {id: 1, name: 'J Smith', department: 001}; var emps = [emp]; ");
            _objTestScript.RunTest(@"assert.equal(true, emps.AnyMatch(null,function(l,o) {return l.department == 001}));", true);
        }

        [TestMethod]
        public void NobjectWithConditionExistInArray_ReturnFalse()
        {
            _objTestScript.AppendBlock(@" var emp0 = {id: 0, name: '', department: 002}; var emp = {id: 1, name: 'J Smith', department: 001}; var emps = [emp]; ");
            _objTestScript.RunTest(@"assert.equal(false, emps.AnyMatch(emp0,function(l,o) {return l.department == o.department}));", true);
        }

        [TestMethod]
        public void NoConditionStingExistsInArray_ReturnTrue()
        {
            _objTestScript.AppendBlock(@" var department = 'IT'; var departments = ['HR','IT','Accounting']; ");
            _objTestScript.RunTest(@"assert.equal(true, departments.AnyMatch(department));", true);
        }

        [TestMethod]
        public void ConditionStingExistsInArray_ReturnTrue()
        {
            _objTestScript.AppendBlock(@" var department = 'IT'; var departments = ['HR','IT','Accounting']; ");
            _objTestScript.RunTest(@"assert.equal(true, departments.AnyMatch(department, function(l,o) {return l === o}));", true);
        }

        [TestMethod]
        public void ConditionStingDoesNotExistsInArray_ReturnTrue()
        {
            _objTestScript.AppendBlock(@" var department = 'ITT'; var departments = ['HR','IT','Accounting']; ");
            _objTestScript.RunTest(@"assert.equal(false, departments.AnyMatch(department, function(l,o) {return l === o}));", true);
        }
    }
}
