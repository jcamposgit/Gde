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
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using ClubStarterKit.Infrastructure;
using ClubStarterKit.Infrastructure.Application;
using ClubStarterKit.Infrastructure.Cache;
using StructureMap;
using IBootstrapper=ClubStarterKit.Core.IBootstrapper;

namespace ClubStarterKit.Web.Infrastructure.Application
{
    public class SiteConfiguration : HttpCacheBase<Configuration>, ISiteConfigProvider
    {
        public const string ApplicationNameKey = "ApplicationName";
        public const string NotificationEmailKey = "Notification_Email";
        public const string NotificationHostKey = "Notification_Host";
        public const string NotificationHostPortKey = "Notification_HostPort";
        public const string NotificationPasswordKey = "Notification_Password";
        public const string NotificationUsernameKey = "Nofification_Username";

        public SiteConfiguration(IApplicationIdProvider appidProvider)
            : base(new HttpContextWrapper(System.Web.HttpContext.Current), appidProvider)
        {
        }

        private KeyValueConfigurationCollection AppSettings
        {
            get
            {
                try
                {
                    return CachedValue.AppSettings.Settings;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public override string ContentType
        {
            get { return "SiteConfiguration"; }
        }

        #region ISiteConfigProvider Members

        public string ApplicationName
        {
            get { return this[ApplicationNameKey]; }
            set { this[ApplicationNameKey] = value; }
        }

        public string NotificationEmailAddress
        {
            get { return this[NotificationEmailKey]; }
            set { this[NotificationEmailKey] = value; }
        }

        public string NotificationHost
        {
            get { return this[NotificationHostKey]; }
            set { this[NotificationHostKey] = value; }
        }

        public string NotificationUsername
        {
            get { return this[NotificationUsernameKey]; }
            set { this[NotificationUsernameKey] = value; }
        }

        public string NotificationPassword
        {
            get { return this[NotificationPasswordKey]; }
            set { this[NotificationPasswordKey] = value; }
        }

        public bool MinifyJavascript
        {
            get
            {
                return true;
            }
        }

        public bool MinifyCss
        {
            get
            {
                return true;
            }
        }


        public int NotificationHostPort
        {
            get
            {
                int outval = 0;
                bool parsed = int.TryParse(this[NotificationHostPortKey], out outval);

                return parsed ? outval : 0;
            }
            set
            {
                this[NotificationHostPortKey] = value.ToString();
            }
        }

        public string this[string key]
        {
            get
            {
                if (AppSettings == null)
                    return string.Empty;

                if (AppSettings[key] == null)
                    return string.Empty;

                return AppSettings[key].Value;
            }
            set
            {
                AppSettings[key].Value = value;
                CachedValue.Save();
                Expire();
            }
        }



        #endregion

        protected override Configuration Grab()
        {
            try
            {
                return WebConfigurationManager.OpenWebConfiguration("~");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public class SiteConfigurationBootstrapper : IBootstrapper
    {
        #region IBootstrapper Members

        public void Bootstrap()
        {
            ObjectFactory.Configure(
                config => config.ForRequestedType<ISiteConfigProvider>().TheDefaultIsConcreteType<SiteConfiguration>());
        }

        #endregion
    }
}