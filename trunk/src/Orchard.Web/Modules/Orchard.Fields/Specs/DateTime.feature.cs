﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.8.1.0
//      SpecFlow Generator Version:1.8.0.0
//      Runtime Version:4.0.30319.269
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Orchard.Fields.Specs
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.8.1.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("DateTime Field")]
    public partial class DateTimeFieldFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "DateTime.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "DateTime Field", "  In order to add Date content to my types\r\nAs an administrator\r\n  I want to crea" +
                    "te, edit and publish DateTime fields", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Creating and using Date fields")]
        public virtual void CreatingAndUsingDateFields()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Creating and using Date fields", ((string[])(null)));
#line 6
this.ScenarioSetup(scenarioInfo);
#line 9
    testRunner.Given("I have installed Orchard");
#line 10
  testRunner.And("I have installed \"Orchard.Fields\"");
#line 11
    testRunner.When("I go to \"Admin/ContentTypes\"");
#line 12
    testRunner.Then("I should see \"<a[^>]*>.*?Create new type</a>\"");
#line 13
    testRunner.When("I go to \"Admin/ContentTypes/Create\"");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table1.AddRow(new string[] {
                        "DisplayName",
                        "Event"});
            table1.AddRow(new string[] {
                        "Name",
                        "Event"});
#line 14
        testRunner.And("I fill in", ((string)(null)), table1);
#line 18
        testRunner.And("I hit \"Create\"");
#line 19
        testRunner.And("I go to \"Admin/ContentTypes/\"");
#line 20
    testRunner.Then("I should see \"Event\"");
#line 23
 testRunner.When("I go to \"Admin/ContentTypes/Edit/Event\"");
#line 24
  testRunner.And("I follow \"Add Field\"");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table2.AddRow(new string[] {
                        "DisplayName",
                        "Date of the event"});
            table2.AddRow(new string[] {
                        "Name",
                        "EventDate"});
            table2.AddRow(new string[] {
                        "FieldTypeName",
                        "DateTimeField"});
#line 25
  testRunner.And("I fill in", ((string)(null)), table2);
#line 30
  testRunner.And("I hit \"Save\"");
#line 31
  testRunner.And("I am redirected");
#line 32
 testRunner.Then("I should see \"The \\\"Date of the event\\\" field has been added.\"");
#line 35
 testRunner.When("I go to \"Admin/Contents/Create/Event\"");
#line 36
 testRunner.Then("I should see \"Date of the event\"");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table3.AddRow(new string[] {
                        "Event.EventDate.Date",
                        "31/01/2012"});
            table3.AddRow(new string[] {
                        "Event.EventDate.Time",
                        "12:00 AM"});
#line 37
 testRunner.When("I fill in", ((string)(null)), table3);
#line 41
  testRunner.And("I hit \"Save\"");
#line 42
 testRunner.Then("I should see \"Date of the event is an invalid date and time\"");
#line 45
 testRunner.When("I go to \"Admin/Contents/Create/Event\"");
#line 46
 testRunner.Then("I should see \"Date of the event\"");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table4.AddRow(new string[] {
                        "Event.EventDate.Date",
                        "01/31/2012"});
#line 47
 testRunner.When("I fill in", ((string)(null)), table4);
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table5.AddRow(new string[] {
                        "Event.EventDate.Time",
                        "12:00 AM"});
#line 50
  testRunner.And("I fill in", ((string)(null)), table5);
#line 53
  testRunner.And("I hit \"Save\"");
#line 54
  testRunner.And("I am redirected");
#line 55
 testRunner.Then("I should see \"Your Event has been created.\"");
#line 56
 testRunner.When("I go to \"Admin/Contents/List\"");
#line 57
 testRunner.Then("I should see \"Date of the event:\"");
#line 58
  testRunner.And("I should see \"1/31/2012  12:00\"");
#line 61
 testRunner.When("I go to \"Admin/ContentTypes/Edit/Event\"");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table6.AddRow(new string[] {
                        "Fields[0].DateTimeFieldSettings.Hint",
                        "Enter the date of the event"});
#line 62
  testRunner.And("I fill in", ((string)(null)), table6);
#line 65
  testRunner.And("I hit \"Save\"");
#line 66
  testRunner.And("I go to \"Admin/Contents/Create/Event\"");
#line 67
 testRunner.Then("I should see \"Enter the date of the event\"");
#line 70
 testRunner.When("I go to \"Admin/ContentTypes/Edit/Event\"");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table7.AddRow(new string[] {
                        "Fields[0].DateTimeFieldSettings.Display",
                        "DateOnly"});
#line 71
  testRunner.And("I fill in", ((string)(null)), table7);
#line 74
  testRunner.And("I hit \"Save\"");
#line 75
  testRunner.And("I go to \"Admin/Contents/Create/Event\"");
#line 76
 testRunner.Then("I should see \"Event.EventDate.Date\"");
#line 77
  testRunner.And("I should not see \"Event.EventDate.Time\"");
#line 80
 testRunner.When("I go to \"Admin/ContentTypes/Edit/Event\"");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table8.AddRow(new string[] {
                        "Fields[0].DateTimeFieldSettings.Display",
                        "TimeOnly"});
#line 81
  testRunner.And("I fill in", ((string)(null)), table8);
#line 84
  testRunner.And("I hit \"Save\"");
#line 85
  testRunner.And("I go to \"Admin/Contents/Create/Event\"");
#line 86
 testRunner.Then("I should see \"Event.EventDate.Time\"");
#line 87
  testRunner.And("I should not see \"Event.EventDate.Date\"");
#line 90
 testRunner.When("I go to \"Admin/ContentTypes/Edit/Event\"");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table9.AddRow(new string[] {
                        "Fields[0].DateTimeFieldSettings.Display",
                        "DateAndTime"});
            table9.AddRow(new string[] {
                        "Fields[0].DateTimeFieldSettings.Required",
                        "true"});
#line 91
  testRunner.And("I fill in", ((string)(null)), table9);
#line 95
  testRunner.And("I hit \"Save\"");
#line 96
  testRunner.And("I go to \"Admin/Contents/Create/Event\"");
#line 97
 testRunner.Then("I should see \"Event.EventDate.Date\"");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table10.AddRow(new string[] {
                        "Event.EventDate.Date",
                        "01/31/2012"});
            table10.AddRow(new string[] {
                        "Event.EventDate.Time",
                        "12:00 AM"});
#line 98
 testRunner.When("I fill in", ((string)(null)), table10);
#line 102
  testRunner.And("I hit \"Save\"");
#line 103
  testRunner.And("I am redirected");
#line 104
 testRunner.Then("I should see \"Your Event has been created.\"");
#line 105
 testRunner.When("I go to \"Admin/Contents/Create/Event\"");
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table11.AddRow(new string[] {
                        "Event.EventDate.Date",
                        "01/31/2012"});
#line 106
  testRunner.And("I fill in", ((string)(null)), table11);
#line 109
  testRunner.And("I hit \"Save\"");
#line 110
 testRunner.Then("I should see \"Date of the event is required.\"");
#line 111
 testRunner.When("I go to \"Admin/Contents/Create/Event\"");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table12.AddRow(new string[] {
                        "Event.EventDate.Time",
                        "12:00 AM"});
#line 112
  testRunner.And("I fill in", ((string)(null)), table12);
#line 115
  testRunner.And("I hit \"Save\"");
#line 116
 testRunner.Then("I should see \"Date of the event is required.\"");
#line 119
 testRunner.When("I go to \"Admin/ContentTypes/Edit/Event\"");
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table13.AddRow(new string[] {
                        "Fields[0].DateTimeFieldSettings.Display",
                        "DateOnly"});
            table13.AddRow(new string[] {
                        "Fields[0].DateTimeFieldSettings.Required",
                        "true"});
#line 120
  testRunner.And("I fill in", ((string)(null)), table13);
#line 124
  testRunner.And("I hit \"Save\"");
#line 125
  testRunner.And("I go to \"Admin/Contents/Create/Event\"");
#line 126
 testRunner.Then("I should see \"Event.EventDate.Date\"");
#line 127
 testRunner.When("I hit \"Save\"");
#line 128
 testRunner.Then("I should see \"Date of the event is required.\"");
#line 131
 testRunner.When("I go to \"Admin/ContentTypes/Edit/Event\"");
#line hidden
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table14.AddRow(new string[] {
                        "Fields[0].DateTimeFieldSettings.Display",
                        "TimeOnly"});
            table14.AddRow(new string[] {
                        "Fields[0].DateTimeFieldSettings.Required",
                        "true"});
#line 132
  testRunner.And("I fill in", ((string)(null)), table14);
#line 136
  testRunner.And("I hit \"Save\"");
#line 137
  testRunner.And("I go to \"Admin/Contents/Create/Event\"");
#line 138
 testRunner.Then("I should see \"Event.EventDate.Date\"");
#line 139
 testRunner.When("I hit \"Save\"");
#line 140
 testRunner.Then("I should see \"Date of the event is required.\"");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Creating and using date time fields in another culture")]
        public virtual void CreatingAndUsingDateTimeFieldsInAnotherCulture()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Creating and using date time fields in another culture", ((string[])(null)));
#line 142
this.ScenarioSetup(scenarioInfo);
#line 145
    testRunner.Given("I have installed Orchard");
#line 146
  testRunner.And("I have installed \"Orchard.Fields\"");
#line 147
  testRunner.And("I have the file \"Content\\orchard.core.po\" in \"Core\\App_Data\\Localization\\fr-FR\\or" +
                    "chard.core.po\"");
#line 148
    testRunner.When("I go to \"Admin/ContentTypes\"");
#line 149
    testRunner.Then("I should see \"<a[^>]*>.*?Create new type</a>\"");
#line 150
    testRunner.When("I go to \"Admin/ContentTypes/Create\"");
#line hidden
            TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table15.AddRow(new string[] {
                        "DisplayName",
                        "Event"});
            table15.AddRow(new string[] {
                        "Name",
                        "Event"});
#line 151
        testRunner.And("I fill in", ((string)(null)), table15);
#line 155
        testRunner.And("I hit \"Create\"");
#line 156
        testRunner.And("I go to \"Admin/ContentTypes/\"");
#line 157
    testRunner.Then("I should see \"Event\"");
#line 160
 testRunner.When("I go to \"Admin/ContentTypes/Edit/Event\"");
#line 161
  testRunner.And("I follow \"Add Field\"");
#line hidden
            TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table16.AddRow(new string[] {
                        "DisplayName",
                        "Date of the event"});
            table16.AddRow(new string[] {
                        "Name",
                        "EventDate"});
            table16.AddRow(new string[] {
                        "FieldTypeName",
                        "DateTimeField"});
#line 162
  testRunner.And("I fill in", ((string)(null)), table16);
#line 167
  testRunner.And("I hit \"Save\"");
#line 168
  testRunner.And("I am redirected");
#line 169
 testRunner.Then("I should see \"The \\\"Date of the event\\\" field has been added.\"");
#line 172
 testRunner.When("I have \"fr-FR\" as the default culture");
#line 173
  testRunner.And("I go to \"Admin/ContentTypes/Edit/Event\"");
#line hidden
            TechTalk.SpecFlow.Table table17 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table17.AddRow(new string[] {
                        "Fields[0].DateTimeFieldSettings.Display",
                        "DateAndTime"});
            table17.AddRow(new string[] {
                        "Fields[0].DateTimeFieldSettings.Required",
                        "true"});
#line 174
  testRunner.And("I fill in", ((string)(null)), table17);
#line 178
  testRunner.And("I hit \"Save\"");
#line 179
 testRunner.When("I go to \"Admin/Contents/Create/Event\"");
#line hidden
            TechTalk.SpecFlow.Table table18 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table18.AddRow(new string[] {
                        "Event.EventDate.Date",
                        "01/31/2012"});
            table18.AddRow(new string[] {
                        "Event.EventDate.Time",
                        "12:00 AM"});
#line 180
  testRunner.And("I fill in", ((string)(null)), table18);
#line 184
  testRunner.And("I hit \"Save\"");
#line 185
 testRunner.Then("I should see \"Date of the event is an invalid date and time\"");
#line 186
 testRunner.When("I go to \"Admin/Contents/Create/Event\"");
#line hidden
            TechTalk.SpecFlow.Table table19 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table19.AddRow(new string[] {
                        "Event.EventDate.Date",
                        "31/01/2012"});
            table19.AddRow(new string[] {
                        "Event.EventDate.Time",
                        "18:00"});
#line 187
  testRunner.And("I fill in", ((string)(null)), table19);
#line 191
  testRunner.And("I hit \"Save\"");
#line 192
  testRunner.And("I am redirected");
#line 193
 testRunner.Then("I should see \"Your Event has been created.\"");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
