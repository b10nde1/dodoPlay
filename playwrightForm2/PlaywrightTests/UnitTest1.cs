using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Newtonsoft.Json;
using NUnit.Framework;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ExampleTest : PageTest
{
    #region private variables
    private readonly string _textTitle = "div.py-2 h5";
    private readonly string _textDescription = "#loremText";
    private readonly string _email = "#email";
    private readonly string _password = "#password";
    private readonly string _selectRice = "#exampleFormControlSelect1"; //added
    private readonly string _selectQA = "#exampleFormControlSelect2"; //added
    private readonly string _textarea = "#exampleFormControlTextarea1"; //added
    private readonly string _checkBox = "#exampleCheck1";
    private readonly string _cancelBtn = "button[type=\"Submit\"]"; //added
    private readonly string _submitBtn = "button[type=\"Submit\"]";
    
    private readonly MyUtitilies _myUtitilies = new();
    #endregion

    #region privateMethod
    private string GetGenericData (string dataPattern)
    {
        string genericID = _myUtitilies.GenerateID();
        Console.WriteLine($"Generic ID for ${dataPattern} :: {genericID}");

        return dataPattern.Replace("%generic-id%", genericID);
    }
    #endregion

    #region Test
    [Test]
    public async Task Form2()
    {
       try {
            Environment.SetEnvironmentVariable("HEADED","1");
            // get testdata
            dynamic testData = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(@"testData.json"))
                ?? "testDataNotFound";

            // prepare test data
            string emailAddress = testData.dataPattern.emailAddress;
            string password = testData.dataPattern.password;
            string url = testData.form2.url;
            string pageTitle = testData.form2.pageTitle;
            string textTitle = testData.form2.textTitle;
            string textDescription = testData.form2.textDescription;

            // open form 2
            await Page.GotoAsync(url);

            // check the title
            await Expect(Page).ToHaveTitleAsync(pageTitle);

            // check text title and description
            await Expect(Page.Locator(_textTitle)).ToHaveTextAsync(textTitle);
            await Expect(Page.Locator(_textDescription)).ToHaveTextAsync(textDescription);

            // type email and password
            await Page.Locator(_email).FillAsync(GetGenericData(emailAddress));
            await Page.Locator(_password).FillAsync(GetGenericData(password));

            // select rice
            await Page.Locator(_selectRice).SelectOptionAsync("Brown rice"); //added

            // select QA
            await Page.Locator(_selectQA).SelectOptionAsync("Front"); //added

            // type in textarea
            await Page.Locator(_textarea).FillAsync("dummy text"); //added 

            // click on checkbox
            await Page.Locator(_checkBox).ClickAsync();

            //  let's check if all good
            await Page.PauseAsync();

            //cancel

            // submit
            await Page.Locator(_submitBtn).ClickAsync();
       }
       catch (Exception ex)
       {
            Console.WriteLine($"Unable to run the test, please ref to {ex.Message} \n {ex.StackTrace}");
       }
    }
    #endregion
}