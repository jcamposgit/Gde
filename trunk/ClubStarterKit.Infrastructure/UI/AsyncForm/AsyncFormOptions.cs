#region license

//Copyright 2009 Zack Owens

//Licensed under the Microsoft Public License (Ms-PL) (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://clubstarterkit.codeplex.com/license

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 

#endregion

using System;
using System.Web.Mvc;

namespace ClubStarterKit.Infrastructure.UI.AsyncForm
{
    public class AsyncFormOptions
    {
        public AsyncFormOptions(AsyncFormType formType,
                                FormMethod formMethod = FormMethod.Post,
                                string formid = "", 
                                AjaxReturnType returnType = AjaxReturnType.none, 
                                string targetUpdate = "",
                                string preRequestFunction = "function(){ return true; }", 
                                string postRequestFunction = "function(){return true;}", 
                                string elementBlockId = "")
        {
            FormId = string.IsNullOrEmpty(formid) ? Guid.NewGuid().ToString() : formid;
            ReturnType = returnType;
            FormMethod = formMethod;
            FormType = formType;

            if (formType == AsyncFormType.Redirection)
                ReturnType = AjaxReturnType.Json;

            if (formType == AsyncFormType.TargetUpdate)
                ReturnType = AjaxReturnType.none;

            if (!string.IsNullOrEmpty(targetUpdate) && formType != AsyncFormType.TargetUpdate)
                throw new InvalidOperationException("The target update id is only valid for a TargetUpdate form.");

            TargetUpdate = targetUpdate;
            PreRequestFunction = preRequestFunction;
            PostRequestFunction = postRequestFunction;
            ElementBlockId = string.IsNullOrEmpty(elementBlockId) ? FormId : elementBlockId;
        }

        public string FormId { get; set; }
        public AjaxReturnType ReturnType { get; set; }
        public FormMethod FormMethod { get; set; }
        public AsyncFormType FormType { get; set; }
        public string TargetUpdate { get; set; }
        public string PreRequestFunction { get; set; }
        public string PostRequestFunction { get; set; }
        public string ElementBlockId { get; set; }

        public override string ToString()
        {
            string metadata = @"{action: '" + FormType.ToString().ToLower() + @"'";

            Action<string, string> addValue = (key, value) => metadata += ", " + key + ": " + value;

            if (ReturnType == AjaxReturnType.none)
                addValue("datatype", "null");
            else
                addValue("datatype", "'" + ReturnType.ToString().ToLower() + "'");

            addValue("before", PreRequestFunction);
            addValue("after", PostRequestFunction);
            addValue("elem", "'#" + ElementBlockId + "'");

            if (!string.IsNullOrEmpty(TargetUpdate))
                addValue("target", "'#" + TargetUpdate + "'");
            else
                addValue("target", "null");

            return metadata + "}";
        }
    }
}
