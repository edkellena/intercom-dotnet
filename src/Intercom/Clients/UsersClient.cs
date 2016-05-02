﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Library.Clients;
using Library.Core;
using Library.Core;
using Library.Data;
using Library.Exceptions;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;

namespace Library.Clients
{
    public class UsersClient : Client
    {
        public static class UserSortBy
        {
            public const String created_at = "created_at";
            public const String updated_at = "updated_at";
            public const String signed_up_at = "signed_up_at";
        }

        private const String USERS_RESOURCE = "users";

        public UsersClient(Authentication authentication)
            : base(INTERCOM_API_BASE_URL, USERS_RESOURCE, authentication)
        {
        }

        public UsersClient(String intercomApiUrl, Authentication authentication)
            : base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, USERS_RESOURCE, authentication)
        {
        }

        public User Create(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("'user' argument is null.");
            }

            if (String.IsNullOrEmpty(user.user_id) && string.IsNullOrEmpty(user.email))
            {
                throw new ArgumentException("you need to provide either 'user.user_id', 'user.email' to create a user.");
            }

            ClientResponse<User> result = null;
            result = Post<User>(user);
            return result.Result;
        }

        public User Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("'user' argument is null.");
            }

            if (String.IsNullOrEmpty(user.id) && String.IsNullOrEmpty(user.user_id) && string.IsNullOrEmpty(user.email))
            {
                throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', 'user.email' to view a user.");
            }

            ClientResponse<User> result = null;
            result = Post<User>(user);

            return result.Result;
        }

        private User CreateOrUpdate(User user)
        {
            ClientResponse<User> result = null;
            result = Post<User>(user);
            return result.Result;
        }

        public User View(Dictionary<String, String> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("'parameters' argument is null.");
            }

            if (!parameters.Any())
            {
                throw new ArgumentException("'parameters' argument should include user_id parameter.");
            }

            ClientResponse<User> result = null;

            result = Get<User>(parameters: parameters);
            return result.Result;
        }

        public User View(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("'parameters' argument is null.");
            }

            ClientResponse<User> result = null;
            result = Get<User>(resource: USERS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;		
        }

        public User View(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("'user' argument is null.");
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            ClientResponse<User> result = null;

            if (!String.IsNullOrEmpty(user.id))
            {
                result = Get<User>(resource: USERS_RESOURCE + Path.DirectorySeparatorChar + user.id);
            }
            else if (!String.IsNullOrEmpty(user.user_id))
            {
                parameters.Add(Constants.USER_ID, user.id);
                result = Get<User>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(user.email))
            {
                parameters.Add(Constants.EMAIL, user.email);
                result = Get<User>(parameters: parameters);			
            }
            else
            {
                throw new ArgumentNullException("you need to provide either 'user.id', 'user.user_id', 'user.email' to view a user.");
            }

            return result.Result;	
        }

        public Users List()
        {
            ClientResponse<Users> result = null;
            result = Get<Users>();
            return result.Result;
        }

        public Users List(Dictionary<String, String> parameters)
        {
            ClientResponse<Users> result = null;
            result = Get<Users>(parameters: parameters);
            return result.Result;
        }

        // TODO: Implement paging (by Pages argument)
        private Users Next(Pages pages)
        {
            return null;
        }

        // TODO: Implement paging
        private Users Next(int page = 1, int perPage = 50, OrderBy orderBy = OrderBy.Dsc, String sortBy = UserSortBy.created_at)
        {
            return null;
        }

        public User Delete(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("'user' argument is null.");
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            ClientResponse<User> result = null;

            if (!String.IsNullOrEmpty(user.id))
            {
                result = Delete<User>(resource: USERS_RESOURCE + Path.DirectorySeparatorChar + user.id);
            }
            else if (!String.IsNullOrEmpty(user.user_id))
            {
                parameters.Add(Constants.USER_ID, user.id);
                result = Delete<User>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(user.email))
            {
                parameters.Add(Constants.EMAIL, user.email);
                result = Delete<User>(parameters: parameters);			
            }
            else
            {
                throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', 'user.email' to view a user.");
            }

            return result.Result;		
        }

        public User Delete(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("'id' argument is null.");
            }

            ClientResponse<User> result = null;
            result = Delete<User>(resource: USERS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;			
        }

        public User UpdateLastSeenAt(String id, long timestamp)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("'id' argument is null.");
            }

            if (timestamp <= 0)
            {
                throw new ArgumentException("'timestamp' argument should be bigger than zero.");
            }

            ClientResponse<User> result = null;
            String body = JsonConvert.SerializeObject(new { user_id = id, last_request_at = timestamp });
            result = Post<User>(body);
            return result.Result;   
        }

        public User UpdateLastSeenAt(User user, long timestamp)
        {
            if (user == null)
            {
                throw new ArgumentNullException("'user' argument is null.");
            }
            
            if (String.IsNullOrEmpty(user.id))
            {
                throw new ArgumentNullException("'id' argument is null.");
            }

            if (timestamp <= 0)
            {
                throw new ArgumentException("'timestamp' argument should be bigger than zero.");
            }

            ClientResponse<User> result = null;
            String body = JsonConvert.SerializeObject(new { user_id = user.id, last_request_at = timestamp });
            result = Post<User>(body);
            return result.Result;   
        }

        public User UpdateLastSeenAt(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("'id' argument is null.");
            }

            ClientResponse<User> result = null;
            String body = JsonConvert.SerializeObject(new { user_id = id, update_last_request_at = true });
            result = Post<User>(body);
            return result.Result;   
        }

        public User UpdateLastSeenAt(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("'user' argument is null.");
            }

            if (String.IsNullOrEmpty(user.id))
            {
                throw new ArgumentNullException("'id' argument is null.");
            }

            ClientResponse<User> result = null;
            String body = JsonConvert.SerializeObject(new { user_id = user.id, update_last_request_at = true });
            result = Post<User>(body);
            return result.Result;   
        }

        public User IncrementUserSession(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("'id' argument is null.");
            }

            ClientResponse<User> result = null;
            String body = JsonConvert.SerializeObject(new { id = id, new_session = true });
            result = Post<User>(body);
            return result.Result;   
        }

        public User IncrementUserSession(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("'user' argument is null.");
            }

            if (String.IsNullOrEmpty(user.id))
            {
                throw new ArgumentNullException("'id' argument is null.");
            }

            ClientResponse<User> result = null;
            String body = JsonConvert.SerializeObject(new { id = user.id, new_session = true });
            result = Post<User>(body);
            return result.Result;   
        }

        public User RemoveCompanyFromUser(String userId, List<String> companyIds)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("'userId' argument is null.");
            }

            if (companyIds == null)
            {
                throw new ArgumentNullException("'companyIds' argument is null.");
            }

            if (!companyIds.Any() == null)
            {
                throw new ArgumentNullException("'companyIds' shouldnt be empty.");
            }

            ClientResponse<User> result = null;
            String body = JsonConvert.SerializeObject(new { id = userId, companies = companyIds.Select(c => new { id = c, remove = true })});
            result = Post<User>(body);
            return result.Result;   

            return null;
        }

        public User RemoveCompanyFromUser(User user, List<String> companyIds)
        {
            if (user == null)
            {
                throw new ArgumentNullException("'user' argument is null.");
            }

            if (String.IsNullOrEmpty(user.id))
            {
                throw new ArgumentNullException("'user.id' is null.");
            }

            if (companyIds == null)
            {
                throw new ArgumentNullException("'companyIds' argument is null.");
            }

            if (!companyIds.Any() == null)
            {
                throw new ArgumentException("'companyIds' shouldnt be empty.");
            }

            ClientResponse<User> result = null;
            String body = JsonConvert.SerializeObject(new { id = user.id, companies = companyIds.Select(c => new { id = c, remove = true })});
            result = Post<User>(body);
            return result.Result;   

            return null;        
        }

    }
}