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

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using ClubStarterKit.Core.DataAccess;
using StructureMap;

namespace ClubStarterKit.Infrastructure
{
    public abstract class NotifiableDataAction<TContext, TOutput> : IDataAction<TContext, TOutput>
        where TContext : class
    {
        protected readonly ISiteConfigProvider SiteConfig = ObjectFactory.GetInstance<ISiteConfigProvider>();

        /// <summary>
        /// Users that recieve a notification via email
        /// </summary>
        protected abstract IEnumerable<string> Users { get; }

        /// <summary>
        /// Subject of the notification
        /// </summary>
        protected abstract string Subject { get; }

        /// <summary>
        /// Body of the notification
        /// </summary>
        protected abstract string Body { get; }

        #region IDataAction<TContext,TOutput> Members

        public TOutput Execute()
        {
            TOutput output = ExecuteAction();

            // dont send message if send message disabled
            if (!SendMessage)
                return output;

            MailMessage message = ConstructMessage(output);

            if (message == null)
                return output;

            // send the message using an SMTP client
            var client = new SmtpClient(SiteConfig.NotificationHost)
                             {
                                 UseDefaultCredentials = false,
                                 Credentials =
                                     new NetworkCredential(SiteConfig.NotificationUsername,
                                                           SiteConfig.NotificationPassword)
                             };

            // set port if valid
            if (SiteConfig.NotificationHostPort > 0)
                client.Port = SiteConfig.NotificationHostPort;

            client.Send(message);

            return output;
        }

        public TContext Context { get; protected set; }

        #endregion

        protected virtual MailMessage ConstructMessage(TOutput output)
        {
            // construct message
            var sender = new MailAddress(SiteConfig.NotificationEmailAddress, SiteConfig.ApplicationName);
            var message = new MailMessage(sender, sender)
                              {
                                  Subject = Subject,
                                  Body = Body,
                                  IsBodyHtml = true
                              };

            // select the user's email, add to list
            message.Bcc.AddRange(Users.Select(u => new MailAddress(u)));

            return message;
        }

        /// <summary>
        /// Commits any data to the database
        /// </summary>
        protected abstract TOutput ExecuteAction();

        private bool sendmessage = true;
        protected bool SendMessage
        {
            get 
            {
                return sendmessage;
            }
            set
            {
                sendmessage = value;
            }
        }
    }
}