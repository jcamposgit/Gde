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
using System.Collections.Generic;
using ClubStarterKit.Data.NHibernate;
using ClubStarterKit.Domain;
using ClubStarterKit.Tests.Data.NHibernate;
using ClubStarterKit.Web.Infrastructure.Membership;
using NHibernate;
using Xunit;

namespace ClubStarterKit.Tests
{
    public class UpdateDb
    {
        //[Fact]
        public void UpgradeDb()
        {
            var db = new SampleDb();
            var factory = db.GetSessionFactory();
            new NHDataProvider(db).Hydrate();

            using (var session = factory.OpenSession())
            {
                var usrs = Users();
                usrs.Foreach(u => session.Save(u));
                // add role
                var role = new Role{ RoleName = "Admin"};
                session.Save(role);
            }

            using (var session = factory.OpenSession())
            {
                var role = session.CreateCriteria<Role>().SetMaxResults(1).UniqueResult<Role>();
                var user = session.CreateCriteria<User>().SetMaxResults(1).UniqueResult<User>();
                var uir = new UserInRole { Role = role, User = user };
                session.Save(uir);

                Announcements(user).Foreach(a => session.Save(a));
                BlogPosts(user).Foreach(a => session.Save(a));
                Topics(session, user);
            }
        }

        private void Topics(ISession session, User user)
        {
            Topic tp = null;
            for (int i = 0; i < 3; i++)
            {
                tp = new Topic
                {
                    Description = "topic" + i,
                    TopicGroup = "group1",
                    Locked = false,
                    Position = i,
                    Title = "Topic " + i,
                    VisibleToAnonymous = true
                };

                session.Save(tp);
            }

            for (int i = 0; i < 20; i++)
            {
                Thread th = new Thread
                {
                    DateCreated = DateTimeOffset.Now,
                    Locked = false,
                    Title = "Thread " + i,
                    Topic = tp,
                    Views = 123,
                    Hidden = false
                };
                session.Save(th);
                for (int j = 0; j < 5; j++)
                    session.Save(new Message
                    {
                        Body = "This is message " + j,
                        Hidden = false,
                        SubmissionDate = DateTimeOffset.Now,
                        Thread = th,
                        User = user

                    });
            }
        }

        public IEnumerable<BlogPost> BlogPosts(User user)
        {
            for (int i = 0; i < 25; i++)
                yield return new BlogPost
                {
                    Author = user,
                    Title = "Blog Post Number " + i,
                    PostDate = DateTimeOffset.Now.AddDays(i),
                    Content = "this is the " + i + " blog post"
                };
        }

        public IEnumerable<Announcement> Announcements(User user)
        {
            for (int i = 0; i < 25; i++)
                yield return new Announcement
                {
                    Description = string.Format("this is the number {0} news item..", i),
                    Title = "News Item " + i,
                    ItemDate = DateTime.Now.AddDays(-i),
                    Owner = user
                };
        }

        public IEnumerable<User> Users()
        {
            var provider = new PasswordCryptographyProvider();
            for (int i = 0; i < 25; i++)
            {
                var user = new User
                {
                    Username = "zowens" + i,
                    Address = i.ToString(),
                    Bio = i.ToString(),
                    Signature = i.ToString(),
                    Email = "zowens" + i + "@eagleenvision.net",
                    FirstName = "Zack" + i,
                    LastName = "Owens" + i,
                    ShowGravatar = true,
                    ShowEmail = true,
                    SendNewsletter = true,
                    MessagesPerPage = 10,
                    Phone = i.ToString()
                };

                user.PasswordSalt = provider.GenerateSalt();
                user.SaltedPassword = provider.GeneratePassword("clubstarterkit", user.PasswordSalt);

                yield return user;
            }
        }
    }
}
