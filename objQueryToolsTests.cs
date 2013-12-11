using JSTest;
using JSTest.ScriptLibraries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace objectQueryLib
{
    /// <summary>
    /// Summary description for objQueryToolsTests
    /// </summary>
    [TestClass]
    public class ObjQueryToolsTests
    {
        private readonly TestScript _objTestScript = new TestScript();

        public ObjQueryToolsTests()
        {
            _objTestScript.AppendFile(@"C:\objectQueryLib\objectQueryLib\JavaScript\objectQueryTools.js");
            _objTestScript.AppendBlock(new JsAssertLibrary());
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

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

        #region AnyMatch

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

        #endregion

        #region Sum

        [TestMethod]
        public void ArrayIsEmpty_ReturnZero()
        {
            _objTestScript.AppendBlock(@" var amounts = []; ");
            _objTestScript.RunTest(@"assert.equal(0, amounts.Sum());", true);
        }

        [TestMethod]
        public void ArrayIsAllNumbers_ReturnTotal()
        {
            _objTestScript.AppendBlock(@" var amounts = [100,150,200]; ");
            _objTestScript.RunTest(@"assert.equal(450, amounts.Sum());", true);
        }

        [TestMethod]
        public void ArrayNonNumericIgnored_ReturnTotal()
        {
            _objTestScript.AppendBlock(@" var amounts = [100,'test',200]; ");
            _objTestScript.RunTest(@"assert.equal(300, amounts.Sum());", true);
        }

        [TestMethod]
        public void ArrayNumericStringNotIgnored_ReturnTotal()
        {
            _objTestScript.AppendBlock(@" var amounts = [100,'50',200]; ");
            _objTestScript.RunTest(@"assert.equal(350, amounts.Sum());", true);
        }

        #endregion
    }
}
