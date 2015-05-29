using MagnoMedia.Data.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Linq;

namespace MagnoMedia.Web.Api.Utilities
{
    public static class DbHelper
    {
        internal static int SaveInDB<T>(IDbConnectionFactory dbFactory, T data, Func<T, bool> predicate) where T : DBEntity
        {
            using (IDbConnection db = dbFactory.Open())
            {
                var existingData = db.Select<T>().SingleOrDefault(predicate);
                if (existingData != null)
                {
                    return existingData.Id;
                }

                return (int)db.Insert<T>(data, selectIdentity: true);
            }
        }

        internal static long InsertInDB<T>(IDbConnectionFactory dbFactory, T data)
        {
            using (IDbConnection db = dbFactory.Open())
            {
                return db.Insert<T>(data, selectIdentity: true);
            }
        }
    }
}