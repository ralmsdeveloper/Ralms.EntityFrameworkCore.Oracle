/* This project came from a fork of: https://github.com/aspnet/EntityFrameworkCore
 * Copyright (c) .NET Foundation. All rights reserved.
 * Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
 * 
 *          Copyright (c)  2018 Rafael Almeida (ralms@ralms.net)
 *
 *                    Ralms.EntityFrameworkCore.Oracle
 *
 * THIS MATERIAL IS PROVIDED AS IS, WITH ABSOLUTELY NO WARRANTY EXPRESSED
 * OR IMPLIED.  ANY USE IS AT YOUR OWN RISK.
 *
 * Permission is hereby granted to use or copy this program
 * for any purpose,  provided the above notices are retained on all copies.
 * Permission to modify the code and to distribute modified code is granted,
 * provided the above notices are retained, and a notice that the code was
 * modified is included with the above copyright notice.
 *
 */

using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;

namespace Ralms.EntityFrameworkCore.Oracle.Query.Internal
{
    public class OracleCompiledQueryCacheKeyGenerator : RelationalCompiledQueryCacheKeyGenerator
    {
        public OracleCompiledQueryCacheKeyGenerator(
            [NotNull] CompiledQueryCacheKeyGeneratorDependencies dependencies,
            [NotNull] RelationalCompiledQueryCacheKeyGeneratorDependencies relationalDependencies)
            : base(dependencies, relationalDependencies)
        {
        }

        public override object GenerateCacheKey(Expression query, bool async)
            => new OracleCompiledQueryCacheKey(GenerateCacheKeyCore(query, async));

        private struct OracleCompiledQueryCacheKey
        {
            private readonly RelationalCompiledQueryCacheKey _relationalCompiledQueryCacheKey;

            public OracleCompiledQueryCacheKey(
                RelationalCompiledQueryCacheKey relationalCompiledQueryCacheKey)
            {
                _relationalCompiledQueryCacheKey = relationalCompiledQueryCacheKey;
            }

            public override bool Equals(object obj)
                => !(obj is null)
                   && obj is OracleCompiledQueryCacheKey
                   && Equals((OracleCompiledQueryCacheKey)obj);

            private bool Equals(OracleCompiledQueryCacheKey other)
                => _relationalCompiledQueryCacheKey.Equals(other._relationalCompiledQueryCacheKey);

            public override int GetHashCode()
            {
                return _relationalCompiledQueryCacheKey.GetHashCode();
            }
        }
    }
}
