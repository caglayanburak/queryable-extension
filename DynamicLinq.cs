using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DynamicLinqSelectSample
{
    public class DynamicLinq<T>
    {
        public Func<T, K> CreateNewStatement<K>() where K : class
        {
            var sourceMembers = typeof(T).GetProperties();
            var destinationMembers = typeof(K).GetProperties();
            // input parameter "o"
            var xParameter = Expression.Parameter(typeof(T), "o");
            // new statement "new Data()"
            var xNew = Expression.New(typeof(K));
            // create initializers
            var bindings = destinationMembers
                .Select(dest =>
                {
                    var mi = sourceMembers.First(pi => pi.Name == dest.Name);
                    // original value "o.Field1"
                    var xOriginal = Expression.Property(xParameter, mi);

                    // set value "Field1 = o.Field1"
                    return Expression.Bind(dest, xOriginal);
                }
            );

            // initialization "new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var xInit = Expression.MemberInit(xNew, bindings);

            // expression "o => new Data { Field1 = o.Field1, Field2 = o.Field2 }"

            var lambda = Expression.Lambda<Func<T, K>>(xInit, new[] { xParameter });

            // compile to Func<Data, Data>
            return lambda.Compile();
        }

    }
}