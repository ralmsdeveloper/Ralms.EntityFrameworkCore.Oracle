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

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ralms.EntityFrameworkCore.Oracle.Metadata.Internal
{
    public class OraclePropertyBuilderAnnotations : OraclePropertyAnnotations
    {
        public OraclePropertyBuilderAnnotations(
            [NotNull] InternalPropertyBuilder internalBuilder,
            ConfigurationSource configurationSource)
            : base(new RelationalAnnotationsBuilder(internalBuilder, configurationSource))
        {
        }

        protected new virtual RelationalAnnotationsBuilder Annotations => (RelationalAnnotationsBuilder)base.Annotations;

        protected override bool ShouldThrowOnConflict => false;

        protected override bool ShouldThrowOnInvalidConfiguration => Annotations.ConfigurationSource == ConfigurationSource.Explicit;

#pragma warning disable 109

        public new virtual bool ColumnName([CanBeNull] string value) => SetColumnName(value);

        public new virtual bool ColumnType([CanBeNull] string value) => SetColumnType(value);

        public new virtual bool DefaultValueSql([CanBeNull] string value) => SetDefaultValueSql(value);

        public new virtual bool ComputedColumnSql([CanBeNull] string value) => SetComputedColumnSql(value);

        public new virtual bool DefaultValue([CanBeNull] object value) => SetDefaultValue(value);

        public new virtual bool HiLoSequenceName([CanBeNull] string value) => SetHiLoSequenceName(value);

        public new virtual bool ValueGenerationStrategy(OracleValueGenerationStrategy? value)
        {
            if (!SetValueGenerationStrategy(value))
            {
                return false;
            }

            if (value == null)
            {
                HiLoSequenceName(null);
            }
            else if (value.Value == OracleValueGenerationStrategy.IdentityColumn)
            {
                HiLoSequenceName(null);
            }

            return true;
        }
#pragma warning restore 109
    }
}
