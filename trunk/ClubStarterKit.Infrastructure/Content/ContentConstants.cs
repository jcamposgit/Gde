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

namespace ClubStarterKit.Infrastructure.Content
{
    public static class ContentConstants
    {
        public static string ApplicationIdParameterName = "applicationId";
        public static string ContentControllerVanityName = "SiteContent";
        public static string CssReplacement_Sprite = "url({0}); background-position: 0px -{1}px;width:{2};height:{3};";
        public static string CssRouteName = "content_css_route";
        public static string FileParameterName = "file";
        public static string JavascriptRouteName = "content_js_route";
        public static string PageJavascriptRouteName = "content_pagejs_route";
        public static string SiteImageRouteName = "content_siteimage_route";
        public static string ContentResetRouteName = "content_reset";
        public static string SpriteImageRouteName = "content_sprite_route";
        public static string JavascriptContentType = "text/javascript";
    }
}