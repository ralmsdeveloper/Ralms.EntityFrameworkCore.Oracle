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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace Ralms.EntityFrameworkCore.Oracle.Query.ExpressionTranslators.Internal
{
    public class OracleDateTimeMemberTranslator : IMemberTranslator
    {
        private static readonly Dictionary<string, string> _datePartMapping
            = new Dictionary<string, string>
            {
                { nameof(DateTime.Year), "YEAR" },
                { nameof(DateTime.Month), "MONTH" },
                { nameof(DateTime.Day), "DAY" },
                { nameof(DateTime.Hour), "HOUR" },
                { nameof(DateTime.Minute), "MINUTE" },
                { nameof(DateTime.Second), "SECOND" },
            };

        public virtual Expression Translate(MemberExpression memberExpression)
        {
            var declaringType = memberExpression.Member.DeclaringType;
            if (declaringType == typeof(DateTime)
                || declaringType == typeof(DateTimeOffset))
            {
                var memberName = memberExpression.Member.Name;

                if (_datePartMapping.TryGetValue(memberName, out var datePart))
                {
                    if (declaringType == typeof(DateTimeOffset)
                        && (string.Equals(datePart, "HOUR")
                            || string.Equals(datePart, "MINUTE")))
                    {
                        // TODO: See issue#10515
                        return null;
                        //datePart = "TIMEZONE_" + datePart;
                    }

                    return new SqlFunctionExpression(
                        "EXTRACT",
                        memberExpression.Type,
                        arguments: new[] { new SqlFragmentExpression(datePart), memberExpression.Expression });
                }

                switch (memberName)
                {
                    case nameof(DateTime.Now):
                        var sysDate = new SqlFragmentExpression("SYSDATE");
                        return declaringType == typeof(DateTimeOffset)
                            ? (Expression)new ExplicitCastExpression(sysDate, typeof(DateTimeOffset))
                            : sysDate;

                    case nameof(DateTime.UtcNow):
                        var sysTimeStamp = new SqlFragmentExpression("SYSTIMESTAMP");
                        return declaringType == typeof(DateTimeOffset)
                            ? (Expression)new ExplicitCastExpression(sysTimeStamp, typeof(DateTimeOffset))
                            : sysTimeStamp;

                    case nameof(DateTime.Date):
                        return new SqlFunctionExpression(
                            "TRUNC",
                            memberExpression.Type,
                            new[] { memberExpression.Expression });

                    case nameof(DateTime.Today):
                        return new SqlFunctionExpression(
                            "TRUNC",
                            memberExpression.Type,
                            new[] { new SqlFragmentExpression("SYSDATE") });
                }
            }

            return null;
        }
    }
}
