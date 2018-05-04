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
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    ///     Oracle specific extension methods for metadata.
    /// </summary>
    public static class OracleMetadataExtensions
    {
        /// <summary>
        ///     Gets the Oracle specific metadata for a property.
        /// </summary>
        /// <param name="property"> The property to get metadata for. </param>
        /// <returns> The Oracle specific metadata for the property. </returns>
        public static OraclePropertyAnnotations Oracle([NotNull] this IMutableProperty property)
            => (OraclePropertyAnnotations)Oracle((IProperty)property);

        /// <summary>
        ///     Gets the Oracle specific metadata for a property.
        /// </summary>
        /// <param name="property"> The property to get metadata for. </param>
        /// <returns> The Oracle specific metadata for the property. </returns>
        public static IOraclePropertyAnnotations Oracle([NotNull] this IProperty property)
            => new OraclePropertyAnnotations(Check.NotNull(property, nameof(property)));

        /// <summary>
        ///     Gets the Oracle specific metadata for a model.
        /// </summary>
        /// <param name="model"> The model to get metadata for. </param>
        /// <returns> The Oracle specific metadata for the model. </returns>
        public static OracleModelAnnotations Oracle([NotNull] this IMutableModel model)
            => (OracleModelAnnotations)Oracle((IModel)model);

        /// <summary>
        ///     Gets the Oracle specific metadata for a model.
        /// </summary>
        /// <param name="model"> The model to get metadata for. </param>
        /// <returns> The Oracle specific metadata for the model. </returns>
        public static IOracleModelAnnotations Oracle([NotNull] this IModel model)
            => new OracleModelAnnotations(Check.NotNull(model, nameof(model)));
    }
}
