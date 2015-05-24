using MagnoMedia.Data.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MagnoMedia.Web.Api.Utilities
{
    public static class DbHelper
    {

        internal static int SaveInDB<T>(IDbConnectionFactory dbFactory, T data, Func<T, bool> predicate) where T : DBEntity
        {
            using (IDbConnection db = dbFactory.Open())
            {
                var existingData = db.Single<T>(predicate);
                if (existingData != null)
                    return existingData.Id;
                return (int)db.Insert<T>(data, selectIdentity: true);
            }
        }
    }
}