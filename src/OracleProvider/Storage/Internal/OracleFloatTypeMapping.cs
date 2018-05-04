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

using System.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ralms.EntityFrameworkCore.Oracle.Storage.Internal
{
    public class OracleFloatTypeMapping : FloatTypeMapping
    {
        public OracleFloatTypeMapping(
            [NotNull] string storeType,
            [CanBeNull] DbType? dbType = null)
            : base(storeType, dbType)
        {
        }

        protected OracleFloatTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters)
        {
        }

        public override RelationalTypeMapping Clone(string storeType, int? size)
            => new OracleFloatTypeMapping(Parameters.WithStoreTypeAndSize(storeType, size));

        public override CoreTypeMapping Clone(ValueConverter converter)
            => new OracleFloatTypeMapping(Parameters.WithComposedConverter(converter));

        protected override string GenerateNonNullSqlLiteral(object value)
        {
            return $"CAST({base.GenerateNonNullSqlLiteral(value)} AS {StoreType})";
        }
    }
}
