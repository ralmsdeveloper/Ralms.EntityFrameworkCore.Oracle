// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.TestModels.GearsOfWarModel;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class AsyncGearsOfWarQueryOracleTest : AsyncGearsOfWarQueryTestBase<GearsOfWarQueryOracleFixture>
    {
        public AsyncGearsOfWarQueryOracleTest(GearsOfWarQueryOracleFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
            //TestSqlLoggerFactory.CaptureOutput(testOutputHelper);
        }

        public override async Task Correlated_collection_order_by_constant()
        {
            await AssertQuery<Gear>(
                           gs => gs.OrderByDescending(s => 1).Select(g => new { g.Nickname, Weapons = g.Weapons.Select(w => w.Name).ToList() }),
                           elementSorter: e => e.Nickname,
                           elementAsserter: (e, a) =>
                           {
                               Assert.Equal(e.Nickname, a.Nickname);
                               CollectionAsserter<string>(ee => ee)(e.Weapons, a.Weapons);
                           });
        }

        private void AssertSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);
    }
}
