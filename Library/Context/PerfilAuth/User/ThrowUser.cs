using Library.Models.PerfilAuth;
using System;
using System.Collections.Generic;

namespace Library.Context.PerfilAuth.User
{
    public class ThrowUser : INterfaceUser
    {
        public IEnumerable<UserAuthLibrary> List()
        {
            throw new NotImplementedException();
        }
        public UserAuthLibrary Get(int? Id)
        {
            throw new NotImplementedException();
        }
        public void Post(UserAuthLibrary userAuthLibrary)
        {
            throw new NotImplementedException();
        }
        public void Put(UserAuthLibrary userAuthLibrary, int? Id)
        {
            throw new NotImplementedException();
        }
        public void Delete(int? Id)
        {
            throw new NotImplementedException();
        }
    }
}
